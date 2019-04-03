using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WEBACA2.Classes
{
    public class BrandManager
    {
        //for ddl
        public List<Brand> GetAllBrand()
        {
            DbConnection dbConn = new DbConnection();
            List<Brand> brandList = new List<Brand>();

            try
            {
                dbConn.Cmd.CommandText = " SELECT BrandID, BrandName, Description, CreatedAt, UpdatedAt FROM Brand Where DeletedAt is null";
                dbConn.Fill();
                foreach (DataRow dr in dbConn.Dt.Rows)
                {
                    Brand brand = new Brand
                    {
                        BrandID = Int32.Parse(dr["BrandID"].ToString()),
                        BrandName = dr["BrandName"].ToString(),
                        Description = dr["Description"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
                    };

                    brandList.Add(brand);
                }
            }//end of try block
            catch (SqlException ex)
            {
                throw new System.Exception(ex.Message);
            }

            return brandList;
        }//end of GetAllBrand

        public bool AddImageToOneBrand(string inBrandImageName, Byte[] inBrandImagedata)
        {
            DbConnection dbConn = new DbConnection();
            int rowaffected = 0;
            //get last BrandID Identity for the newly brand inserted
            dbConn.Cmd.CommandText = "SELECT MAX(BrandID) FROM Brand";
            dbConn.Conn.Open();
            string BrandID = dbConn.Cmd.ExecuteScalar().ToString();
            dbConn.Conn.Close();

            //BrandImage Table
            dbConn.Cmd.CommandText = " INSERT into BrandImage (BrandImageName,BrandImageData,BrandID) Values " +
                                     " (@inBrandImageName,@inBrandImageData,@inBrandID) ";
            dbConn.Cmd.Parameters.Add("@inBrandImageName", SqlDbType.VarChar, 50).Value = inBrandImageName;
            dbConn.Cmd.Parameters.Add("@inBrandImageData", SqlDbType.Binary).Value = inBrandImagedata;
            dbConn.Cmd.Parameters.Add("@inBrandID", SqlDbType.Int).Value = BrandID;
            dbConn.Conn.Open();
            rowaffected = dbConn.Cmd.ExecuteNonQuery();
            dbConn.Conn.Close();

            if (rowaffected == 0)
                return false;
            else
                return true;
        }
        public void AddOneBrandOfImageByBrandIDWImageNameValidation(string inBrandImageName, Byte[] inBrandImagedata, string inBrandID)
        {

            bool check = true;
            DbConnection dbConn = new DbConnection();
            dbConn.Cmd.CommandText = " Insert into BrandImage(BrandImageName,BrandImageData,BrandID) VALUES " +
                                     " (@inBrandImageName,@inBrandImageData,@inBrandID) ";

            dbConn.Cmd.Parameters.Add("@inBrandImageName", SqlDbType.VarChar, 50).Value = inBrandImageName;
            dbConn.Cmd.Parameters.Add("@inBrandImageData", SqlDbType.Binary).Value = inBrandImagedata;
            dbConn.Cmd.Parameters.Add("@inBrandID", SqlDbType.Int).Value = inBrandID;

            try
            {
                dbConn.Conn.Open();
                dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
            }

            catch (SqlException ex)
            {
                //prevent showing error message to user, if uniqueconstraint exist,do nth

                if (ex.Message.Contains("BrandImageNameUniqueConstraint") == true)
                {
                }
            }


           
        }
        public bool AddOneBrand(string inBrandName, string inDescription, string inBrandVideoLink)
        {
            DbConnection dbConn = new DbConnection();
            bool checkConstraint = true;
            try
            {
                //brand table
                dbConn.Cmd.CommandText = " INSERT INTO Brand (BrandName,Description) values (@inBrandName,@inDescription) ";
                dbConn.Cmd.Parameters.Add("@inBrandName", SqlDbType.VarChar, 50).Value = inBrandName;
                dbConn.Cmd.Parameters.Add("@inDescription", SqlDbType.VarChar, 3000).Value = inDescription;
                dbConn.Conn.Open();
                dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
            }
            catch (SqlException ex)
            {


                if (ex.Message.Contains("BrandNameUniqueConstraint") == true)
                {
                    checkConstraint = false;
                }
            }
            //if same brandname does not exist
            if (checkConstraint == true)
            {
                //get last BrandID Identity for the newly brand inserted
                dbConn.Cmd.CommandText = "SELECT MAX(BrandID) FROM Brand";
                dbConn.Conn.Open();
                //executescalar = get first column,first row
                string BrandID = dbConn.Cmd.ExecuteScalar().ToString();
                dbConn.Conn.Close();

                //BrandVideo Table
                dbConn.Cmd.CommandText = " INSERT into BrandVideo (BrandVideoLink,BrandID) Values " +
                                         " (@inBrandVideoLink,@inBrandID); ";
                dbConn.Cmd.Parameters.Add("inBrandVideoLink", SqlDbType.VarChar, 50).Value = inBrandVideoLink;
                dbConn.Cmd.Parameters.Add("@inBrandID", SqlDbType.Int).Value = BrandID;
                dbConn.Conn.Open();
                dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
            }
            return checkConstraint;
        }


        //for brand updateform
        public List<object> getAllImagesByBrandID(string inBrandID)
        {

            DbConnection dbConn = new DbConnection();
            List<object> ImageList = new List<object>();
            dbConn.Cmd.CommandText = " SELECT BrandImageID, BrandImageName, BrandImageData, CreatedAt, UpdatedAt, BrandID " +
                                   " FROM BrandImage WHERE BrandID = @inBrandID AND DeletedAt is null ";
            dbConn.Cmd.Parameters.Add("@inBrandID", SqlDbType.Int).Value = inBrandID;
            dbConn.Fill();

            foreach (DataRow dr in dbConn.Dt.Rows)
            {
                var image = new
                {

                    BrandImageID = Int32.Parse(dr["BrandImageID"].ToString()),
                    BrandImageName = dr["BrandImageName"].ToString(),
                    BrandImageData = (Byte[])dr["BrandImageData"],
                    BrandImageCreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                    BrandImageUpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                    BrandID = Int32.Parse(dr["BrandID"].ToString())
                };
                ImageList.Add(image);

            };

            return ImageList;
        }
        public bool deleteOneImageByBrandID(string inBrandImageName, string inBrandID)
        {
            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = " UPDATE brandimage SET deletedat = getdate() WHERE brandID = @inBrandID " +
                                     " AND BrandImageName = @inBrandImageName ";
            dbConn.Cmd.Parameters.Add("@inBrandID", SqlDbType.Int).Value = inBrandID;
            dbConn.Cmd.Parameters.Add("@inBrandImageName", SqlDbType.VarChar, 50).Value = inBrandImageName;
            dbConn.Conn.Open();
            rowAffected = dbConn.Cmd.ExecuteNonQuery();
            dbConn.Conn.Close();


            if (rowAffected == 0)
                return false;
            else
                return true;

        }
        public object GetOneBrandWVideoUrlByRecordId(string InRecordId)
        {

            DbConnection dbConn = new DbConnection();

            dbConn.Cmd.CommandText = " SELECT Brand.BrandID, Brand.BrandName, Brand.Description, BrandVideo.BrandVideoLink, Brand.CreatedAt, Brand.UpdatedAt " +
                             " FROM Brand LEFT JOIN " +
                             " BrandVideo ON Brand.BrandID = BrandVideo.BrandID where Brand.BrandId=@inBrand";

            dbConn.Cmd.Parameters.Add("@inBrand", SqlDbType.Int).Value = InRecordId;
            dbConn.Fill();


            DataRow dr = dbConn.Dt.Rows[0];

            var brandwVideoUrl = new
            {

                BrandId = Int32.Parse(dr["BrandID"].ToString()),
                BrandName = dr["BrandName"].ToString(),
                Description = dr["Description"].ToString(),
                BrandVideoLink = dr["BrandVideoLink"].ToString(),
                CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())


            };//end var instances



            return brandwVideoUrl;
        }
        public List<object> GetAllBrandWithVideoLink()
        {
            DbConnection dbConn = new DbConnection();
            List<object> brandList = new List<object>();
            try
            {
                dbConn.Cmd.CommandText = " SELECT Brand.BrandID, Brand.BrandName, Brand.Description, BrandVideo.BrandVideoLink, Brand.CreatedAt, Brand.UpdatedAt " +
                                   " FROM Brand LEFT JOIN " +
                                   " BrandVideo ON Brand.BrandID = BrandVideo.BrandID " +
                                   " WHERE Brand.DeletedAt is null ";
                dbConn.Fill();
                foreach (DataRow dr in dbConn.Dt.Rows)
                {


                    var brandListwVideoLink = new
                    {

                        BrandId = Int32.Parse(dr["BrandID"].ToString()),
                        BrandName = dr["BrandName"].ToString(),
                        Description = dr["Description"].ToString(),
                        BrandVideoLink = dr["BrandVideoLink"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())


                    };//end var instances


                    brandList.Add(brandListwVideoLink);
                }
            }//end of try block
            catch (SqlException ex)
            {

            }

            return brandList;
        }//end of GetAllBrandWithVideoLink
        public bool UpdateOneBrandByRecordId(string inBrandId, string inBrandName, string inDescription, string inBrandVideoLink)
        {
            DbConnection dbConn = new DbConnection();
            bool checkConstraint = true;
            dbConn.Cmd.CommandText =
           " UPDATE Brand set BrandName = @inBrandName, Description = @inDescription where BrandId = @inBrandId; " +
           " UPDATE BrandVideo set BrandVideoLink =@inBrandVideoLink where Brandid=@inBrandId;";

            dbConn.Cmd.Parameters.Add("@inBrandId", SqlDbType.Int).Value = inBrandId;
            dbConn.Cmd.Parameters.Add("@inBrandName", SqlDbType.VarChar, 50).Value = inBrandName;
            dbConn.Cmd.Parameters.Add("@inDescription", SqlDbType.VarChar, 3000).Value = inDescription;
            dbConn.Cmd.Parameters.Add("inBrandVideoLink", SqlDbType.VarChar, 50).Value = inBrandVideoLink;
            try
            {
                dbConn.Conn.Open();
                dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("BrandNameUniqueConstraint") == true)
                {
                    checkConstraint = false;
                }
            }
            return checkConstraint;
        }//end of UpdateOneBrandByRecordId
        public List<object> GetAllBrandWithVideoLinkBySearchTxt(string inSearchTxt)
        {
            DbConnection dbConn = new DbConnection();
            List<object> brandList = new List<object>();
            try
            {
                dbConn.Cmd.CommandText = " SELECT Brand.BrandID, Brand.BrandName, Brand.Description, BrandVideo.BrandVideoLink, Brand.CreatedAt, Brand.UpdatedAt " +
                                         " FROM Brand LEFT JOIN " +
                                         " BrandVideo ON Brand.BrandID = BrandVideo.BrandID where " +
                                         " BrandName like @inSearchTxt or " +
                                         " Description like @inSearchTxt or " +
                                         " BrandVideoLink like @inSearchTxt " +
                                         " AND Brand.DeletedAt is null ";

                dbConn.Cmd.Parameters.Add("@inSearchTxt", SqlDbType.VarChar, 100).Value = "%" + inSearchTxt + "%";
                dbConn.Fill();

                foreach (DataRow dr in dbConn.Dt.Rows)
                {

                    var brandListwVideoLink = new
                    {

                        BrandId = Int32.Parse(dr["BrandID"].ToString()),
                        BrandName = dr["BrandName"].ToString(),
                        Description = dr["Description"].ToString(),
                        BrandVideoLink = dr["BrandVideoLink"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())


                    };//end var instances


                    brandList.Add(brandListwVideoLink);
                }
            }//end of try block
            catch (SqlException ex)
            {
                throw new System.ArgumentException(ex.Message);
            }

            return brandList;
        }//end of GetAllBrandWithVideoLinkBySearchTxt
        public bool DeleteOneBrand(string inBrandId)
        {

            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = " UPDATE Brand SET DeletedAt = getdate() WHERE BrandID= @inBrandID;" +
                                     " UPDATE Brandimage SET DeletedAt = getdate() WHERE BrandID = @inBrandID; " +
                                     " UPDATE BrandVideo SET DeletedAt = getdate() WHERE BrandID = @inBrandID; ";

            dbConn.Cmd.Parameters.Add("@inBrandID", SqlDbType.Int).Value = inBrandId;

            dbConn.Conn.Open();
            rowAffected = dbConn.Cmd.ExecuteNonQuery();
            dbConn.Conn.Close();


            if (rowAffected == 0)
                return false;
            else
                return true;

        }//end of DeleteOneBrand

    }
}