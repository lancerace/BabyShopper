using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WEBACA2.Classes
{
    public class SubCategoryManager
    {


        public List<SubCategory> GetAllSubCategory()
        {
            DbConnection dbConn = new DbConnection();
            List<SubCategory> subCategoryList = new List<SubCategory>();
            dbConn.Cmd.CommandText = " SELECT SubCategoryID, SubCategoryName, CreatedAt, UpdatedAt , CategoryID " +
                                     " FROM SubCategory where DeletedAt is null  ";

           
            dbConn.Fill();
            try
            {
                foreach (DataRow dr in dbConn.Dt.Rows)
                {

                    SubCategory subCategory = new SubCategory
                    {

                        SubCategoryID = Int32.Parse(dr["SubCategoryId"].ToString()),
                        SubCategoryName = dr["SubCategoryName"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                        CategoryID = Int32.Parse(dr["CategoryID"].ToString())
                    };

                    subCategoryList.Add(subCategory);
                }
            }//end of try block
            catch (SqlException ex)
            {
            }

            return subCategoryList;

        }//end of GetAllSubCategory

        public List<SubCategory> GetAllSubCategoryByCategoryID(string inCategoryID) {

            DbConnection dbConn = new DbConnection();

            List<SubCategory> subCategoryListbyCategoryID = new List<SubCategory>();
            dbConn.Cmd.CommandText = " SELECT SubCategoryID, SubCategoryName, CreatedAt, UpdatedAt, CategoryID " +
                                     " FROM SubCategory WHERE DeletedAt is NULL AND CategoryID=@inCategoryID ";
            dbConn.Cmd.Parameters.Add("@inCategoryID", SqlDbType.Int).Value = inCategoryID;
            dbConn.Fill();
            foreach (DataRow dr in dbConn.Dt.Rows) {
                SubCategory subCategory = new SubCategory
                {
                    SubCategoryID = Int32.Parse(dr["SubCategoryID"].ToString()),
                    SubCategoryName = dr["SubCategoryName"].ToString(),
                    CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                    UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                    CategoryID = Int32.Parse(dr["CategoryID"].ToString())                
                };
                subCategoryListbyCategoryID.Add(subCategory);
            }
            return subCategoryListbyCategoryID;     
        }//end of GetAllSubCategoryByCategoryID()






        public List<SubCategory> GetAllSubCategoryBySearchTxt(string inCategoryId ,string inSearchTxt)
        {
            DbConnection dbConn = new DbConnection();
            List<SubCategory> subCategoryList = new List<SubCategory>();
            dbConn.Cmd.CommandText = " SELECT SubCategoryID, SubCategoryName, CreatedAt, UpdatedAt, CategoryID " +
                                     " FROM SubCategory where CategoryID = @inCategoryID AND DeletedAt IS NULL " +
                                     " AND SubCategoryName like @inSearchTxt ";
                           

            dbConn.Cmd.Parameters.Add("@inSearchTxt", SqlDbType.VarChar, 100).Value = "%" + inSearchTxt + "%";
            dbConn.Cmd.Parameters.Add("@inCategoryID", SqlDbType.Int).Value = inCategoryId;
            dbConn.Fill();
            try
            {
                foreach (DataRow dr in dbConn.Dt.Rows)
                {

                    SubCategory subCategory = new SubCategory
                    {

                        SubCategoryID = Int32.Parse(dr["SubCategoryID"].ToString()),
                        SubCategoryName = dr["SubCategoryName"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                        CategoryID = Int32.Parse(dr["CategoryID"].ToString())
                    };

                    subCategoryList.Add(subCategory);
                }
            }//end of try block
            catch (SqlException ex)
            {
            }

            return subCategoryList;

        }//end of GetAllSubCategory

        public bool DeleteOneSubCategory(string inSubCategoryId)
        {

            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;

            dbConn.Cmd.CommandText = "UPDATE SubCategory SET DeletedAt = getdate() WHERE SubCategoryID= @inSubCategoryID;";
            dbConn.Cmd.Parameters.Add("@inSubCategoryId", SqlDbType.Int).Value = inSubCategoryId;

            dbConn.Conn.Open();
            rowAffected = dbConn.Cmd.ExecuteNonQuery();
            dbConn.Conn.Close();


            if (rowAffected == 0)
                return false;
            else
                return true;


        }//end of DeleteOneSubCategory

        public bool AddOneSubCategory(string inSubCategoryName, string inCategoryID) 
        {
            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = " INSERT INTO SubCategory (SubCategoryName,CategoryID) VALUES (@inSubCategoryName,@inCategoryID) ";
            dbConn.Cmd.Parameters.Add("@inSubCategoryName", SqlDbType.VarChar, 50).Value = inSubCategoryName;
            dbConn.Cmd.Parameters.Add("@inCategoryID", SqlDbType.Int).Value = inCategoryID;
            try
            {
                dbConn.Conn.Open();
                rowAffected = dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
            }
            catch (SqlException ex)
            {
            }

            if (rowAffected == 0)
                return false;
            else
                return true;
        }//end of AddOneSubCategory
        public SubCategory GetOneSubCategory(string inSubCategoryID)
        {
            DbConnection dbConn = new DbConnection();
            SubCategory subCategory = new SubCategory();
            dbConn.Cmd.CommandText = " SELECT SubCategoryID, SubCategoryName, CreatedAt, UpdatedAt, CategoryID " +
                                     " FROM SubCategory where SubCategoryID = @inSubCategoryID ";

            dbConn.Cmd.Parameters.Add("@inSubCategoryID", SqlDbType.Int).Value = inSubCategoryID;
            try
            {
                dbConn.Fill();
                DataRow dr = dbConn.Dt.Rows[0];
                subCategory.SubCategoryID = Int32.Parse(dr["SubCategoryID"].ToString());
                subCategory.SubCategoryName = dr["SubCategoryName"].ToString();
                subCategory.CategoryID = Int32.Parse(dr["CategoryID"].ToString());
            }
            catch (SqlException ex)
            {
            }

            return subCategory;

        }//end of GetOneSubCategory

        public bool UpdateOneSubCategory(string inSubCategoryID, string inSubCategoryName)
        {
            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = "Update SubCategory SET SubCategoryName = @inSubCategoryName where SubCategoryID = @inSubCategoryID ";
            dbConn.Cmd.Parameters.Add("@inSubCategoryID", SqlDbType.Int).Value = inSubCategoryID;
            dbConn.Cmd.Parameters.Add("@inSubCategoryName", SqlDbType.VarChar, 50).Value = inSubCategoryName;


            try
            {
                dbConn.Conn.Open();
                rowAffected = dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();

            }
            catch (SqlException ex)
            {
                throw new System.ArgumentException(ex.Message); 
            }

            if (rowAffected == 0)
                return false;
            else
                return true;
        }//end of UpdateOneSubCategory

    }
}