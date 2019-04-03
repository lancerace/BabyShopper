using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes2
{
    public class BrandManager
    {

        public List<object> GetAllBrand()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            string sqlcommand;
            List<object> brandList = new List<object>();
            cmd.Connection = cn;
            da.SelectCommand = cmd;
            sqlcommand = "SELECT Brand.BrandID, Brand.BrandName, Brand.BrandDescription, Brand.CreatedAt, Brand.UpdatedAt, Brand.DeletedAt, ISNULL(UserInfomation_1.UserName,'') AS CreatedBy, ISNULL(UserInfomation.UserName,'') AS UpdatedBy ";
            sqlcommand += "FROM Brand LEFT OUTER JOIN ";
            sqlcommand += "UserInfomation AS UserInfomation_1 ON Brand.CreatedBy = UserInfomation_1.UserID LEFT OUTER JOIN ";
            sqlcommand += "UserInfomation ON Brand.UpdatedBy = UserInfomation.UserID ";
            cmd.CommandText = sqlcommand;
            cn.ConnectionString =
              ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            cn.Open();
            try
            {
                da.Fill(ds, "brandData");

                foreach (DataRow dr in ds.Tables["brandData"].Rows)
                {
                    var genericObject = new
                    {
                        BrandID = Int32.Parse(dr["BrandID"].ToString()),
                        BrandName = dr["BrandName"].ToString(),
                        BrandDescription = dr["BrandDescription"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                        DeletedAt = DateTime.Parse(dr["DeletedAt"].ToString()),
                        CreatedBy = dr["CreatedBy"].ToString(),
                        UpdatedBy = dr["UpdatedBy"].ToString()
                    };
                    brandList.Add(genericObject);
                }
            }//end of try block
            catch (SqlException sqlEx)
            {       //If there is any error just throw(raise) the system error
                //message to the calling program.
                throw new System.ArgumentException(sqlEx.Message);
            }
            finally
            {
                cn.Close();//Close the connection
            }
            return brandList;
        }
        public int AddOneBrand(dynamic inWebFormData)
        {
            int newBrandRecordId = 0;
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;//tell the cmd to use the cn
                    cmd.CommandText = "INSERT Brand (BrandName,BrandDescription ) " +
                          " OUTPUT Inserted.BrandId VALUES " +
                          "(@inBrandName ,@inBrandDescription);";
                    //20/Jan/2015
                    //I kept having error on "Failed to convert parameter value from a JValue to a String" from
                    //the command cmd.Parameters.Add(...).Value = inStudent.FullName; (I fixed by adding .Value after the FullName)
                    //By analyzing the content of the inStudent.FullName, I noticed that it returns somekind of object
                    //instead of a string. To take the string value out , I need to use inStudent.FullName.Value;
                    cmd.Parameters.Add("@inBrandName", SqlDbType.VarChar, 100).Value = inWebFormData.BrandName.Value;
                    cmd.Parameters.Add("@inBrandDescription", SqlDbType.VarChar, 2500).Value = inWebFormData.BrandDescription.Value;
                    cn.Open();
                    try
                    {
                        newBrandRecordId = Int32.Parse(cmd.ExecuteScalar().ToString());
                    }
                    catch (SqlException sqlEx)
                    {
                        /*
                         ALTER TABLE Student 
                       ADD CONSTRAINT Student_AdminIdUniqueConstraint UNIQUE (AdmissionId); 
                       GO  */

                        if (sqlEx.Message.Contains("Brand_AdminIdUniqueConstraint") == true)
                        {
                            string messageTemplate = "Unable to save due to {0} admission id found in other records.";
                            string message = string.Format(messageTemplate, inWebFormData.AdmissionId.Value);
                            //Throw an exception message to the calling program. 
                            throw new System.ArgumentException(message);
                        }
                        else
                        {
                            throw new System.ArgumentException(sqlEx.Message);
                        }

                    }

                    cn.Close();
                }//end of using SqlCommand, cmd
            }//end of using SqlConnection, cn
            return newBrandRecordId; //return the new record id back to the calling program.
        }
        public bool AddBrandPhoto(BrandImage inBrandPhoto)
        {
            int numOfRecordsAffected = 0;
            SqlCommand command = new SqlCommand();
            SqlConnection connection = new SqlConnection();
            string connString = ConfigurationManager.ConnectionStrings["WEBAConnectionString"].ConnectionString;
            connection.ConnectionString = connString;
            command.Connection = connection;
            //The SQL Insert statement involves one table, Brand. The record to be created requires
            //4 fields to be filled, BrandName, BrandImage, BrandContentLength, BrandContentType
            string sqlCommand = " INSERT INTO BrandImage ";
            sqlCommand += "       (ImageContentLength, ImageContentType, ImageFileName, Photo, BrandId) ";
            sqlCommand += " VALUES (@inImageContentLength,@inImageContentType,@inImageFileName, @inPhoto, @inBrandId) ";

            command.CommandText = sqlCommand;
            //The SQL statement has 4 parameters, these 4 parameters are provided by the properties which belongs to
            //the inBrand object which is passed in from the calling program (processAddBrand method of ADMAddBrand.aspx)
            command.Parameters.Add("@inPhoto", SqlDbType.Image, inBrandPhoto.Photo.Length).Value = inBrandPhoto.Photo;
            command.Parameters.Add("@inImageContentLength", SqlDbType.Int, 100).Value = inBrandPhoto.BrandImageContentLength;
            command.Parameters.Add("@inImageFileName", SqlDbType.VarChar, 100).Value = inBrandPhoto.BrandImageFileName;
            command.Parameters.Add("@inImageContentType", SqlDbType.VarChar, 50).Value = inBrandPhoto.BrandImageContentType;
            command.Parameters.Add("@inBrandId", SqlDbType.Int).Value = inBrandPhoto.BrandID;
            //Open an active connection 
            connection.Open();
            //Send the SQL statement to the database
            //Send the SQL statement to the database
            numOfRecordsAffected = command.ExecuteNonQuery();
            //Close the database connection.
            connection.Close();
            if (numOfRecordsAffected != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Object> getAllBrandBySearchText(string inSearchText)
        {
            SqlCommand cmd = new SqlCommand();//To hold the SQL
            SqlConnection cn = new SqlConnection();//To make the db connection
            //To retrieve the data and store in dataset
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();//The dataset which holds the returned records
            List<Object> brandList = new List<Object>();
            string sqlcommand;
            cmd.Connection = cn;//tell the cmd to use the cn
            da.SelectCommand = cmd;//tell the da to use the cmd
            sqlcommand = "SELECT Brand.BrandID, Brand.BrandName, Brand.BrandDescription, Brand.CreatedAt, Brand.UpdatedAt, Brand.DeletedAt, UserInfomation_1.UserName AS CreatedBy, UserInfomation.UserName AS UpdatedBy ";
            sqlcommand += "FROM Brand INNER JOIN ";
            sqlcommand += "UserInfomation AS UserInfomation_1 ON Brand.CreatedBy = UserInfomation_1.UserID INNER JOIN ";
            sqlcommand += "UserInfomation ON Brand.UpdatedBy = UserInfomation.UserID ";
            sqlcommand += "Where Brand.BrandID LIKE @inSearchText or Brand.BrandName LIKE @inSearchText or Brand.BrandDescription LIKE @inSearchText ";
            sqlcommand += "or Brand.CreatedAt LIKE @inSearchText or Brand.UpdatedAt LIKE @inSearchText or UserInfomation_1.UserName LIKE @inSearchText or UserInfomation.UserName LIKE @inSearchText ";

            cmd.Parameters.Add("@inSearchText", SqlDbType.VarChar, 30).Value =
                                 "%" + inSearchText + "%";
            //Setup the connection to database information for the SQLConnection cn.             
            cmd.CommandText = sqlcommand;
            cn.ConnectionString =
              ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            cn.Open();
            try
            {
                da.Fill(ds, "brandData");

                foreach (DataRow dr in ds.Tables["brandData"].Rows)
                {
                    var genericObject = new
                    {
                        BrandID = Int32.Parse(dr["BrandID"].ToString()),
                        BrandName = dr["BrandName"].ToString(),
                        BrandDescription = dr["BrandDescription"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                        DeletedAt = DateTime.Parse(dr["DeletedAt"].ToString()),
                        CreatedBy = dr["CreatedBy"].ToString(),
                        UpdatedBy = dr["UpdatedBy"].ToString()
                    };
                    brandList.Add(genericObject);
                }
            }//end of try block
            catch (SqlException sqlEx)
            {       //If there is any error just throw(raise) the system error
                //message to the calling program.
                throw new System.ArgumentException(sqlEx.Message);
            }
            finally
            {
                cn.Close();//Close the connection
            }
            return brandList;
        }
        public List<object> getAllBrandExcludingDeletedData()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            string sqlcommand;
            List<object> brandList = new List<object>();
            cmd.Connection = cn;
            da.SelectCommand = cmd;
            sqlcommand = "SELECT Brand.BrandID, Brand.BrandName, Brand.BrandDescription, Brand.CreatedAt, Brand.UpdatedAt, Brand.DeletedAt, ISNULL(UserInfomation_1.UserName,'') AS CreatedBy, ISNULL(UserInfomation.UserName,'') AS UpdatedBy ";
            sqlcommand += "FROM Brand LEFT OUTER JOIN ";
            sqlcommand += "UserInfomation AS UserInfomation_1 ON Brand.CreatedBy = UserInfomation_1.UserID LEFT OUTER JOIN ";
            sqlcommand += "UserInfomation ON Brand.UpdatedBy = UserInfomation.UserID ";
            sqlcommand += "where Brand.DeletedAt='1970-01-01 00:00:00.000'";
            cmd.CommandText = sqlcommand;
            cn.ConnectionString =
              ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            cn.Open();
            try
            {
                da.Fill(ds, "brandData");

                foreach (DataRow dr in ds.Tables["brandData"].Rows)
                {
                    var genericObject = new
                    {
                        BrandID = Int32.Parse(dr["BrandID"].ToString()),
                        BrandName = dr["BrandName"].ToString(),
                        BrandDescription = dr["BrandDescription"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                        DeletedAt = DateTime.Parse(dr["DeletedAt"].ToString()),
                        CreatedBy = dr["CreatedBy"].ToString(),
                        UpdatedBy = dr["UpdatedBy"].ToString()
                    };
                    brandList.Add(genericObject);
                }
            }//end of try block
            catch (SqlException sqlEx)
            {       //If there is any error just throw(raise) the system error
                //message to the calling program.
                throw new System.ArgumentException(sqlEx.Message);
            }
            finally
            {
                cn.Close();//Close the connection
            }
            return brandList;
        }
        public bool deleteOneBrand(string inBrandId)
        {
            SqlCommand cmd = new SqlCommand(); SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter(); DataSet ds = new DataSet();
            int numOfRecordsAffected = 0;
            cmd.Connection = cn;//tell the cmd to use the cn
            cmd.CommandText = "update Brand set DeletedAt=GETDATE(),UpdatedAt=GETDATE() " +
                  "WHERE BrandID=@inBrandId";
            cmd.Parameters.Add("@inBrandId", SqlDbType.Int).Value = inBrandId;
            cn.ConnectionString =
                ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();

            cn.Open();
            numOfRecordsAffected = cmd.ExecuteNonQuery();
            cn.Close();
            if (numOfRecordsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }//deleteOneBrand()
        public string getOneBrandDescByRecordId(string inBrandId)
        {
            string result = "";
            string sqlText = "";
            SqlCommand cmd = new SqlCommand();//To hold the SQL
            SqlConnection cn = new SqlConnection();//To make the db connection
            //To retrieve the data and store in dataset
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();//The dataset which holds the returned records
            Brand brand;
            List<Brand> brandList = new List<Brand>();
            cmd.Connection = cn;//tell the cmd to use the cn
            da.SelectCommand = cmd;//tell the da to use the cmd
            sqlText = "select BrandDescription ";
            sqlText += "from Brand ";
            sqlText += "where BrandID=@inBrandId";
            //setup the SQL statement for the cmd
            cmd.CommandText = sqlText;
            cmd.Parameters.Add("@inBrandId", SqlDbType.Int).Value = inBrandId;
            //Setup the connection to database information for the SQLConnection cn.             
            cn.ConnectionString =
              ConfigurationManager.ConnectionStrings["WEBAConnectionString"].ToString();
            cn.Open();
            try
            {
                result = cmd.ExecuteScalar().ToString();
                da.Fill(ds, "BrandDescData");
                DataRow dr = ds.Tables["BrandDescData"].Rows[0];
                {
                    brand = new Brand();
                    brand.BrandDescription = dr["BrandDescription"].ToString();
                    brandList.Add(brand);

                };
            }//end of try block
            catch (SqlException sqlEx)
            {   //If there is any error just throw(raise) the system error
                //message to the calling program.
                throw new System.ArgumentException(sqlEx.Message);
            }
            finally
            {
                cn.Close();//Close the connection
            }
            result.ToString();
            return result;
        }
        public Brand getOneBrand(string inBrandID)
        {
            SqlCommand cmd = new SqlCommand(); SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter(); DataTable dt = new DataTable();
            Brand brand = new Brand();
            cmd.Connection = cn;//tell the cmd to use the cn
            da.SelectCommand = cmd;
            cmd.CommandText = "SELECT BrandId, BrandName,BrandDescription From Brand where BrandID= @inBrandID ";
            cmd.Parameters.Add("@inBrandID", SqlDbType.Int).Value = inBrandID;
            cn.ConnectionString =
              ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            try
            {
                cn.Open();
                da.Fill(dt);
                cn.Close();
                DataRow dr = dt.Rows[0];
                brand.BrandID = Int32.Parse(dr["brandId"].ToString());
                brand.BrandName = dr["brandName"].ToString();
                brand.BrandDescription = dr["brandDescription"].ToString();
            }
            catch (SqlException sqlEx)
            { }
            return brand;
        }
        public bool UpdateOneBrand(string inBrandId, string inBrandName, string inBrandDescription, string inUpdatedBy)
        {

            SqlCommand cmd = new SqlCommand(); SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter(); DataSet ds = new DataSet();
            int numOfRecordsAffected = 0;
            cmd.Connection = cn;//tell the cmd to use the cn
            cmd.CommandText = "UPDATE Brand SET BrandName = @inBrandName," +
                  " BrandDescription=@inBrandDescription,UpdatedBy=@inUpdatedBy, UpdatedAt=getdate() " +
                  " WHERE BrandId=@inBrandId";
            cmd.Parameters.Add("@inBrandId", SqlDbType.Int).Value = inBrandId;
            cmd.Parameters.Add("@inBrandName", SqlDbType.VarChar, 50).Value = inBrandName;
            cmd.Parameters.Add("@inBrandDescription", SqlDbType.VarChar, 2500).Value = inBrandDescription;
            cmd.Parameters.Add("@inUpdatedBy", SqlDbType.Int).Value = inUpdatedBy;
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            cn.Open();
            try
            {
                numOfRecordsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Message.Contains("Brand_BrandNameUniqueConstraint") == true)
                {
                    string messageTemplate = "Unable to save due to {0} Brand name found in other records.";
                    string message = string.Format(messageTemplate, inBrandName);
                    //Throw an exception message to the calling program. 
                    throw new System.ArgumentException(message);
                }
            }
            cn.Close();
            if (numOfRecordsAffected == 0)
            { return false; }
            else
            { return true; }
        }
        public bool addOneBrand(string inBrandName, string inBrandDescription, string inCreatedBy)
        {
            SqlCommand cmd = new SqlCommand(); SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter(); DataSet ds = new DataSet();
            int numOfRecordsAffected = 0;
            cmd.Connection = cn;//tell the cmd to use the cn
            cmd.CommandText = "Insert Brand (BrandName,BrandDescription,CreatedBy,UpdatedBy) values " +
                  " (@inBrandName,@inBrandDescription,@inCreatedBy,@inCreatedBy) ";
            cmd.Parameters.Add("@inBrandName", SqlDbType.VarChar, 50).Value = inBrandName;
            cmd.Parameters.Add("@inBrandDescription", SqlDbType.VarChar, 2500).Value = inBrandDescription;
            cmd.Parameters.Add("@inCreatedBy", SqlDbType.Int).Value = inCreatedBy;
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            cn.Open();
            try
            {
                numOfRecordsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Message.Contains("Brand_BrandNameUniqueConstraint") == true)
                {
                    string messageTemplate = "Unable to save due to {0} Brand name found in other records.";
                    string message = string.Format(messageTemplate, inBrandName);
                    //Throw an exception message to the calling program. 
                    throw new System.ArgumentException(message);
                }
            }
            cn.Close();
            if (numOfRecordsAffected == 0)
            { return false; }
            else
            { return true; }
        }

        public int CountSubBrand (string inBrandId)
        {
            SqlCommand cmd = new SqlCommand(); SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter(); DataTable dt = new DataTable();
            int numberOfSubBrand = 0;
            cmd.Connection = cn;//tell the cmd to use the cn
            da.SelectCommand = cmd;
            cmd.CommandText = "select count(SubBrandID) from SubBrand where BrandID= @inBrandID and DeletedAt is null ";
            cmd.Parameters.Add("@inBrandID", SqlDbType.Int).Value = inBrandId;
            cn.ConnectionString =
              ConfigurationManager.ConnectionStrings["MyConnection"].ToString();
            try
            {
                cn.Open();
                numberOfSubBrand = Int32.Parse(cmd.ExecuteScalar().ToString());
                cn.Close();
            }
            catch (SqlException sqlEx)
            { }
            return numberOfSubBrand;
        }
        public BrandImage GetOneBrandImage(string inBrandPhotoId)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection connection = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            BrandImage bi = new BrandImage();
            string connString = ConfigurationManager.ConnectionStrings["WEBAConnectionString"].ConnectionString;
            connection.ConnectionString = connString;
            cmd.Connection = connection;
            da.SelectCommand = cmd;
            string sqlCommand = " SELECT BrandImageId,Photo,ImageFileName,ImageContentType,ImageContentLength,BrandId FROM BrandImage ";
            sqlCommand += " WHERE BrandImageId =  ";
            sqlCommand += " @inBrandPhotoId ";

            cmd.CommandText = sqlCommand;
            cmd.Parameters.Add("@inBrandPhotoId", SqlDbType.Int).Value = inBrandPhotoId;
            DataTable productPhotoDataTable = new DataTable();
            da.Fill(productPhotoDataTable);
            DataRow dr = productPhotoDataTable.Rows[0];
            bi.Photo = (byte[])dr["Photo"];
            bi.BrandImageContentLength = Int32.Parse(dr["ImageContentLength"].ToString());
            bi.BrandImageContentType = dr["ImageContentType"].ToString();
            bi.BrandImageFileName = dr["ImageFileName"].ToString();
            bi.Brand.BrandID = Int32.Parse(dr["BrandId"].ToString());
            return bi;
        }
        public object GetBrandImages(string inBrandId)
        {
            DataSet ds = new DataSet();
            object brandObject = new object();
            object brandPhotoObject = new Object();
            List<object> brandPhotoList = new List<object>();
            string sqlText = "";
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["WEBAConnectionString"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //Setup the SQL statement which is to be sent to the database
                    //inside the SqlCommand cmd object
                    cmd.Connection = cn; //setup the 
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        sqlText = " SELECT BrandImageId,Photo,ImageFileName,ImageContentType,ImageContentLength,BrandId,IsPrimaryPhoto FROM BrandImage ";
                        sqlText += " WHERE BrandId =@inBrandId ";
                        cmd.CommandText = sqlText;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@inBrandId", SqlDbType.Int).Value = inBrandId;
                        da.Fill(ds, "BrandPhotoData");


                    }//using SqlDataAdapter da
                    cn.Close();

                }//using SQLCommand cmd
            }//using SQLConnection cn
            foreach (DataRow brandPhotoDataRow in ds.Tables["BrandPhotoData"].Rows)
            {
                brandPhotoObject = new
                {
                    BrandImageId = brandPhotoDataRow["BrandImageId"].ToString(),
                    ImageContentLength = Int32.Parse(brandPhotoDataRow["ImageContentLength"].ToString()),
                    ImageContentType = brandPhotoDataRow["ImageContentType"].ToString(),
                    ImageFileName = brandPhotoDataRow["ImageFileName"].ToString(),
                    BrandId = Int32.Parse(brandPhotoDataRow["BrandId"].ToString()),
                    IsPrimaryPhoto = brandPhotoDataRow["IsPrimaryPhoto"].ToString()
                };
                brandPhotoList.Add(brandPhotoObject);
            }//foreach
            brandObject = new
            {
                PhotoList = brandPhotoList
            };

            return brandObject;
        }

        public bool DeleteBrandPhoto(string inbrandPhotoId)
        {
            int numOfRecordsAffected = 0;
            string sqlCommand = "";
            BrandImage bm = new BrandImage();
            string connString = ConfigurationManager.ConnectionStrings["WEBAConnectionString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = connString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    sqlCommand = " DELETE BrandImage ";
                    sqlCommand += " WHERE BrandImageId =  ";
                    sqlCommand += " @inBrandImageId ";
                    cmd.CommandText = sqlCommand;
                    cmd.Parameters.Add("@inBrandImageId", SqlDbType.Int).Value = inbrandPhotoId;
                    cn.Open();
                    numOfRecordsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                }//end of using SqlCommand cmd
            }//end of using SqlConnection cn
            return (numOfRecordsAffected > 0) ? true : false;
        }

    }
}