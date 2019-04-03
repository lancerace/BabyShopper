using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace WEBACA2.Classes
{
    public class CategoryManager
    {

        public List<Category> GetAllCategory() 
        {
            DbConnection dbConn = new DbConnection();
            List<Category> categoryList = new List<Category>();
            dbConn.Cmd.CommandText = " SELECT CategoryID, CategoryName, CreatedAt, UpdatedAt " +
                                     " FROM Category WHERE DeletedAt is null ";
            dbConn.Fill();
            try
            {
                foreach (DataRow dr in dbConn.Dt.Rows)
                {

                    Category category = new Category
                    {

                        CategoryID = Int32.Parse(dr["CategoryID"].ToString()),
                        CategoryName = dr["CategoryName"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
                    };

                    categoryList.Add(category);
                }
            }//end of try block
            catch (SqlException ex) 
            { 
            }

        return categoryList;

        }//end of GetAllCategory
        public bool DeleteOneCategory(string inCategoryId)
        {
            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;

            dbConn.Cmd.CommandText = "UPDATE Category SET DeletedAt = getdate() WHERE CategoryID= @inCategoryID;";
            dbConn.Cmd.Parameters.Add("@inCategoryID", SqlDbType.Int).Value = inCategoryId;

            dbConn.Conn.Open();
            rowAffected = dbConn.Cmd.ExecuteNonQuery();
            dbConn.Conn.Close();


            if (rowAffected == 0)
                return false;
            else
                return true;


        
        }
        public List<Category> GetAllCategoryBySearchTxt(string inSearchTxt) 
        {

            DbConnection dbConn = new DbConnection();
            List<Category> categoryList = new List<Category>();
            dbConn.Cmd.CommandText = " SELECT CategoryID, CategoryName, CreatedAt, UpdatedAt " +
                                     " FROM Category where CategoryName like @inSearchTxt " +
                                     " AND DeletedAt IS NULL " ;
            dbConn.Cmd.Parameters.Add("@inSearchTxt", SqlDbType.VarChar, 100).Value = "%" + inSearchTxt + "%";

            dbConn.Fill();
            try
            {
                foreach (DataRow dr in dbConn.Dt.Rows)
                {

                    Category category = new Category
                    {

                        CategoryID = Int32.Parse(dr["CategoryId"].ToString()),
                        CategoryName = dr["CategoryName"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
                    };

                    categoryList.Add(category);
                }
            }//end of try block
            catch (SqlException ex)
            {
            }

            return categoryList;
        
        }
        public Category GetOneCategory(string inCategoryID) {

            DbConnection dbConn = new DbConnection();
            Category category = new Category();
            dbConn.Cmd.CommandText = " SELECT CategoryID, CategoryName, CreatedAt, UpdatedAt " +
                                     " FROM Category where CategoryID = @inCategoryID ";

            dbConn.Cmd.Parameters.Add("@inCategoryID", SqlDbType.Int).Value = inCategoryID;
            try
            {
                dbConn.Fill();
                DataRow dr = dbConn.Dt.Rows[0];
                    category.CategoryID = Int32.Parse(dr["CategoryID"].ToString());
                    category.CategoryName = dr["CategoryName"].ToString();
                
            }
            catch (SqlException ex) 
            { 
            }

            return category;
        }//end of GetOneCategory
        public bool UpdateOneCategory(string inCategoryID,string inCategoryName)
        {
            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = "Update Category SET CategoryName = @inCategoryName,UpdatedAt = getdate() where CategoryID = @inCategoryID ";
            dbConn.Cmd.Parameters.Add("@inCategoryName", SqlDbType.VarChar, 100).Value = inCategoryName;
            dbConn.Cmd.Parameters.Add("@inCategoryID", SqlDbType.Int).Value = inCategoryID;

            try
            {
                dbConn.Conn.Open();
                rowAffected = dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();

            }catch(SqlException ex)
            {

            }

            if (rowAffected == 0)
                return false;
            else
                return true;

        }//end of UpdateOneCategory
        public bool AddOneCategory(string inCategoryName) 
        {
            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = " INSERT INTO Category (CategoryName) VALUES (@inCategoryName) ";
            dbConn.Cmd.Parameters.Add("@inCategoryName", SqlDbType.VarChar, 50).Value = inCategoryName;

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
        }//end of AddOneCategory
    }
}