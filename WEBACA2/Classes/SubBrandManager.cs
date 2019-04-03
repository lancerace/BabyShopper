using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WEBACA2.Classes
{
    public class SubBrandManager
    {


        public List<SubBrand> GetAllSubBrand()
        {
            DbConnection dbConn = new DbConnection();
            List<SubBrand> subBrandList = new List<SubBrand>();
            dbConn.Cmd.CommandText = " SELECT SubBrandID, SubBrandName, Description, SubBrandVideoID, CreatedAt, UpdatedAt, BrandID FROM SubBrand where DeletedAt IS NULL";

            dbConn.Fill();

            try
            {
                foreach (DataRow dr in dbConn.Dt.Rows)
                {
                    SubBrand subbrand = new SubBrand
                    {
                        SubBrandID = Int32.Parse(dr["SubBrandID"].ToString()),
                        SubBrandName = dr["SubBrandName"].ToString(),
                        Description = dr["Description"].ToString(),
                        SubBrandVideoID = Int32.Parse(dr["SubBrandVideoID"].ToString()),                            //Int32.TryParse(dr["SubBrandVideoID"].ToString()
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                        BrandID = Int32.Parse(dr["BrandID"].ToString())
                    };
                    subBrandList.Add(subbrand);
                }
            }
            catch (SqlException ex)
            {

            }
            return subBrandList;
        
        }


        public List<SubBrand> GetAllSubBrandbyBrandId(string BrandId)
        {
            DbConnection dbConn = new DbConnection();
            List<SubBrand> subBrandList = new List<SubBrand>();
            dbConn.Cmd.CommandText = " SELECT SubBrandID, SubBrandName, Description, SubBrandVideoID, CreatedAt, UpdatedAt, BrandID FROM SubBrand where BrandID = @inBrandId AND DeletedAt IS NULL";
            dbConn.Cmd.Parameters.Add("@inBrandId", SqlDbType.Int).Value = BrandId;
            dbConn.Fill();

            try
            {
                foreach (DataRow dr in dbConn.Dt.Rows)
                {
                    SubBrand subbrand = new SubBrand
                    {
                        SubBrandID = Int32.Parse(dr["SubBrandID"].ToString()),
                        SubBrandName = dr["SubBrandName"].ToString(),
                        Description = dr["Description"].ToString(),
                        SubBrandVideoID = Int32.Parse(dr["SubBrandVideoID"].ToString()),                            //Int32.TryParse(dr["SubBrandVideoID"].ToString()
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                        BrandID = Int32.Parse(dr["BrandID"].ToString())
                    };
                    subBrandList.Add(subbrand);
                }
            }
            catch (SqlException ex)
            {

            }
            return subBrandList;
        }//end of GetAllSubBrandbyBrandId

        public SubBrand getOneSubBrandByRecordId(string inRecordId)
        {

            DbConnection dbConn = new DbConnection();
            SubBrand subBrand = new SubBrand();
            dbConn.Cmd.CommandText = "SELECT SubBrandID, SubBrandName, Description, CreatedAt, UpdatedAt, BrandID, SubBrandVideoID " +
                                     " FROM SubBrand where SubBrandID = @inSubBrandID and DeletedAt is null";

            dbConn.Cmd.Parameters.Add("@inSubBrandId", SqlDbType.Int).Value = inRecordId;

            try
            {
                dbConn.Fill();
                DataRow dr = dbConn.Dt.Rows[0];
                subBrand.SubBrandID = Int32.Parse(dr["SubBrandID"].ToString());
                subBrand.SubBrandName = dr["SubBrandName"].ToString();
                subBrand.Description = dr["Description"].ToString();
                subBrand.CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString());
                subBrand.UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString());
                subBrand.BrandID = Int32.Parse(dr["BrandID"].ToString());
                subBrand.SubBrandVideoID = Int32.Parse(dr["SubBrandVideoID"].ToString());

            }//end of try block
            catch (SqlException ex)
            {

            }
            return subBrand;

        }//end of getOneSubBrandByRecordId

        public object addOneSubBrand(string inSubBrandName, string inDescription, string inVideoLink, string inBrandID)
        {

            DbConnection dbConn = new DbConnection();
            SubBrand subBrand = new SubBrand();
            int subBrandID = 0;

            object subBrandIDAndValidationMsg = new object();

            dbConn.Cmd.CommandText = " INSERT into subbrand" +
                " (subbrandname,Description,BrandID) " +
                " OUTPUT inserted.SubBrandID " +
                " values " +
                " (@inSubBrandName,@inDescription,@inBrandID) ";

            dbConn.Cmd.Parameters.Add("@inSubBrandName", SqlDbType.VarChar, 50).Value = inSubBrandName;
            dbConn.Cmd.Parameters.Add("@inDescription", SqlDbType.VarChar, 3000).Value = inDescription;
            dbConn.Cmd.Parameters.Add("@inBrandID", SqlDbType.Int).Value = inBrandID;
            try
            {
                dbConn.Conn.Open();
                subBrandID = Int32.Parse(dbConn.Cmd.ExecuteScalar().ToString());

                dbConn.Cmd.CommandText = "INSERT into subbrandvideo (SubBrandVideoLink,subBrandID) " +
                            " values " +
                            " (@inSubBrandVideoLink,@insubBrandID) ";
                dbConn.Cmd.Parameters.Add("@inSubBrandVideoLink", SqlDbType.VarChar, 50).Value = inVideoLink;
                dbConn.Cmd.Parameters.Add("@insubBrandID", SqlDbType.VarChar, 3000).Value = subBrandID;
                dbConn.Conn.Close();

                subBrandIDAndValidationMsg = new
                {
                    collectedSubBrandID = subBrandID,
                    uniqueConstraint = false
                };
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("SubBrandNameUniqueConstraint") == true)
                {
                    subBrandIDAndValidationMsg = new
                   {
                       collectedSubBrandID = subBrandID,
                       uniqueConstraint = true
                   };

                }
            }
            return subBrandIDAndValidationMsg;
        }


        public SubBrandImage getOneSubBrandImage(string subBrandImageID)
        {

            DbConnection dbConn = new DbConnection();
            SubBrandImage subBrandImage = new SubBrandImage();


            dbConn.Cmd.CommandText = " SELECT SubBrandImageID, SubBrandImageName, SubBrandImageData, UpdatedAt, CreatedAt, " +
                                     " DeletedAt, SubBrandID " +
                                     " FROM SubBrandImage WHERE SubBrandImageID = @inSubBrandImageID and DeletedAt is null ";

            dbConn.Cmd.Parameters.Add("@inSubBrandImageID", SqlDbType.VarChar, 50).Value = subBrandImageID;
           
             dbConn.Fill();
             DataRow dr = dbConn.Dt.Rows[0];
             subBrandImage.SubBrandImageID = Int32.Parse(dr["SubBrandImageID"].ToString());
             subBrandImage.SubBrandImageName = dr["SubBrandImageName"].ToString();
             subBrandImage.SubBrandImageData= (byte[])dr["SubBrandImageData"];
             subBrandImage.CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString());
             subBrandImage.UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString());
             subBrandImage.SubBrandID = Int32.Parse(dr["SubBrandID"].ToString());

            return subBrandImage;
        }


        public bool AddOneSubBrandOfImageBySubBrandIDWImageNameValidation(string inSubBrandImageName, Byte[] inSubBrandImagedata, string inSubBrandID)
        {
            DbConnection dbConn = new DbConnection();
            dbConn.Cmd.CommandText = " Insert into SubBrandImage(SubBrandImageName,SubBrandImageData,SubBrandID) VALUES " +
                                     " (@inSubBrandImageName,@inSubBrandImageData,@inSubBrandID) ";
            //track if there is constraint
            bool uniqueConstraint = false;
            dbConn.Cmd.Parameters.Add("@inSubBrandImageName", SqlDbType.VarChar, 50).Value = inSubBrandImageName;
            dbConn.Cmd.Parameters.Add("@inSubBrandImageData", SqlDbType.Binary).Value = inSubBrandImagedata;
            dbConn.Cmd.Parameters.Add("@inSubBrandID", SqlDbType.Int).Value = inSubBrandID;

            try
            {
                dbConn.Conn.Open();
                dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
            }

            catch (SqlException ex)
            {
                //prevent showing error message to user, if uniqueconstraint exist,do nth

                if (ex.Message.Contains("SubBrandImageNameUniqueConstraint") == true)
                {
                    uniqueConstraint = true;

                }
            }//end try catch block


            return uniqueConstraint;
        }


        public bool DeleteOneImage(string inSubBrandImageID) {

            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = " UPDATE Subbrandimage SET deletedat = getdate() WHERE SubbrandImageID = @inSubImageBrandID ";

            dbConn.Cmd.Parameters.Add("@inSubImageBrandID", SqlDbType.Int).Value = inSubBrandImageID;
            dbConn.Conn.Open();
            rowAffected = dbConn.Cmd.ExecuteNonQuery();
            dbConn.Conn.Close();

            if (rowAffected == 0)
                return false;
            else
                return true;
        
        }

        public List<object> getAllImagesBySubBrandID(string inSubBrandID)
        {

            DbConnection dbConn = new DbConnection();
            List<object> ImageList = new List<object>();
            dbConn.Cmd.CommandText = " SELECT SubBrandImageID, SubBrandImageName, SubBrandImageData, CreatedAt, UpdatedAt, SubBrandID " +
                                   " FROM SubBrandImage WHERE SubBrandID = @inSubBrandID AND DeletedAt is null ";

            dbConn.Cmd.Parameters.Add("@inSubBrandID", SqlDbType.Int).Value = inSubBrandID;
            dbConn.Fill();
            foreach (DataRow dr in dbConn.Dt.Rows)
            {
                var image = new
                {

                    SubBrandImageID = Int32.Parse(dr["SubBrandImageID"].ToString()),
                    SubBrandImageName = dr["SubBrandImageName"].ToString(),
                    //SubBrandImageData = (Byte[])dr["SubBrandImageData"],
                    SubBrandImageCreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                    SubBrandImageUpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                    BrandID = Int32.Parse(dr["SubBrandID"].ToString())
                };
                ImageList.Add(image);

            };

  
            return ImageList;

        }
        public bool deleteOneImageBySubBrandID(string inSubBrandImageName, string inSubBrandID)
        {
            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = " UPDATE Subbrandimage SET deletedat = getdate() WHERE SubbrandID = @inSubBrandID " +
                                     " AND SubBrandImageName = @inSubBrandImageName ";
            dbConn.Cmd.Parameters.Add("@inSubBrandID", SqlDbType.Int).Value = inSubBrandID;
            dbConn.Cmd.Parameters.Add("@inSubBrandImageName", SqlDbType.VarChar, 50).Value = inSubBrandImageName;
            dbConn.Conn.Open();
            rowAffected = dbConn.Cmd.ExecuteNonQuery();
            dbConn.Conn.Close();


            if (rowAffected == 0)
                return false;
            else
                return true;

        }
        public object GetOneSubBrandWVideoLinkByRecordId(string SubBrandId)
        {

            DbConnection dbConn = new DbConnection();
            var subbrandListwVideoLink = new Object();
            try
            {
                dbConn.Cmd.CommandText = " SELECT SubBrand.SubBrandID, SubBrand.SubBrandName, SubBrand.Description,  SubBrand.SubBrandVideoID, SubBrandVideo.SubBrandVideoLink, SubBrand.CreatedAt, SubBrand.UpdatedAt, SubBrand.BrandID " +
                                         " FROM SubBrand LEFT JOIN " +
                                         " SubBrandVideo ON SubBrand.SubBrandVideoID = SubBrandVideo.SubBrandVideoID " +
                                         " where SubBrand.SubBrandID = @inSubBrandId ";
                dbConn.Cmd.Parameters.Add("@inSubBrandId", SqlDbType.Int).Value = SubBrandId;
                dbConn.Fill();



                DataRow dr = dbConn.Dt.Rows[0];
                var genericObj = new
                 {
                     SubBrandId = Int32.Parse(dr["SubBrandID"].ToString()),
                     SubBrandName = dr["SubBrandName"].ToString(),
                     Description = dr["Description"].ToString(),
                     SubBrandVideoId = Int32.Parse(dr["SubBrandVideoID"].ToString()),
                     SubBrandVideoLink = dr["SubBrandVideoLink"].ToString(),
                     CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                     UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
                 };//end var instances

                subbrandListwVideoLink = genericObj;


            }//end of try block
            catch (SqlException ex)
            {

            }

            return subbrandListwVideoLink;

        }//end of GetOneSubBrandWVideoLinkByRecordId
        public List<SubBrand> GetAllSubBrandByBrandRecordIDAndSearchTxt(string inSearchTxt, string brandId)
        {
            DbConnection dbConn = new DbConnection();
            List<SubBrand> subBrandList = new List<SubBrand>();

            dbConn.Cmd.CommandText = " SELECT SubBrandID, SubBrandName, Description, CreatedAt, UpdatedAt, BrandID, SubBrandVideoID " +
                                     " FROM SubBrand where brandID = @inBrandId AND DeletedAt IS NULL " +
                                     " AND (subbrandname like @inSearchTxt or Description like @inSearchTxt) ";

            dbConn.Cmd.Parameters.Add("@inSearchTxt", SqlDbType.VarChar, 100).Value = "%" + inSearchTxt + "%";
            dbConn.Cmd.Parameters.Add("@inBrandId", SqlDbType.Int).Value = brandId;
            dbConn.Fill();

            try
            {
                foreach (DataRow dr in dbConn.Dt.Rows)
                {

                    SubBrand subbrand = new SubBrand
                    {

                        SubBrandID = Int32.Parse(dr["SubBrandID"].ToString()),
                        SubBrandName = dr["SubBrandName"].ToString(),
                        Description = dr["Description"].ToString(),
                        SubBrandVideoID = Int32.Parse(dr["SubBrandVideoID"].ToString()),                            //Int32.TryParse(dr["SubBrandVideoID"].ToString()
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                        BrandID = Int32.Parse(dr["BrandID"].ToString())
                    };

                    subBrandList.Add(subbrand);
                }
            }
            catch (SqlException ex)
            {

            }


            return subBrandList;

        }//end of GetAllSubBrandWithVideoLinkBySearchTxt
        public bool UpdateOneSubBrandByRecordId(string inSubBrandId, string inSubBrandName, string inDescription, string inSubBrandVideoLink)
        {

            DbConnection dbConn = new DbConnection();
            bool checkConstraint = true;

            dbConn.Cmd.CommandText =
           " UPDATE SubBrand set SubBrandName = @inSubBrandName, Description = @inDescription where SubBrandId = @inSubBrandId; " +
           " UPDATE SubBrandVideo set SubBrandVideoLink =@inSubBrandVideoLink where SubBrandid=@inSubBrandId;";

            dbConn.Cmd.Parameters.Add("@inSubBrandId", SqlDbType.Int).Value = inSubBrandId;
            dbConn.Cmd.Parameters.Add("@inSubBrandName", SqlDbType.VarChar, 50).Value = inSubBrandName;
            dbConn.Cmd.Parameters.Add("@inDescription", SqlDbType.VarChar, 3000).Value = inDescription;
            dbConn.Cmd.Parameters.Add("inSubBrandVideoLink", SqlDbType.VarChar, 50).Value = inSubBrandVideoLink;

            dbConn.Conn.Open();
            try
            {
                dbConn.Cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("SubBrandNameUniqueConstraint") == true)
                {
                    checkConstraint = false;
                }
            }
            dbConn.Conn.Close();

            return checkConstraint;
        }//end of UpdateOneSubBrandByRecordId
        public bool DeleteOneSubBrand(string inSubBrandId)
        {

            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = " UPDATE SubBrand SET DeletedAt = getdate() WHERE SubBrandID = @inSubBrandID" +
                                     " UPDATE subBrandimage SET DeletedAt = getdate() WHERE SubBrandID = @inSubBrandID; " +
                                     " UPDATE subBrandVideo SET DeletedAt = getdate() WHERE SubBrandID = @inSubBrandID; ";

            dbConn.Cmd.Parameters.Add("@inSubBrandID", SqlDbType.Int).Value = inSubBrandId;
            dbConn.Conn.Open();
            rowAffected = dbConn.Cmd.ExecuteNonQuery();
            dbConn.Conn.Close();


            if (rowAffected == 0)
                return false;
            else
                return true;

        }//end of DeleteOneSubBrand
    }
}