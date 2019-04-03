using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes2
{
    public class SubCategoryManager
    {
          public List<object> getAllSubCategoryExcludeDeletedDataByInCategoryID(string inCategoryID)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            string sqlcommand;
            List<object> subCategoryList = new List<object>();
            cmd.Connection = cn;
            da.SelectCommand = cmd;
            sqlcommand = "SELECT SubCategory.SubCategoryID, Category.CategoryName, SubCategory.SubCategoryName, SubCategory.CreatedAt, SubCategory.UpdatedAt, SubCategory.DeletedAt ";
            sqlcommand += "FROM UserInfomation AS UserInfomation_1  RIGHT OUTER JOIN UserInfomation RIGHT OUTER JOIN Category RIGHT OUTER JOIN ";
            sqlcommand += "SubCategory ON Category.CategoryID = SubCategory.CategoryID ON UserInfomation.UserID = SubCategory.CreatedBy ON UserInfomation_1.UserID = SubCategory.UpdatedBy ";
            sqlcommand += "where subCategory.DeletedAt = '1970-01-01 00:00:00.000' and SubCategory.CategoryID = @inCategoryID";
            cmd.CommandText = sqlcommand;
            cn.ConnectionString =
              ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();

            cmd.Parameters.Add("@inCategoryID", SqlDbType.Int).Value = inCategoryID; 
            cn.Open();
            try
            {
                da.Fill(ds, "SubCategoryData");

                foreach (DataRow dr in ds.Tables["SubCategoryData"].Rows)
                {
                    var genericObject = new
                    {
                        SubCategoryID = Int32.Parse(dr["SubCategoryID"].ToString()),
                        CategoryName = dr["CategoryName"].ToString(),
                        SubCategoryName = dr["SubCategoryName"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                        DeletedAt = DateTime.Parse(dr["DeletedAt"].ToString()),
                        //CreatedBy = dr["CreatedBy"].ToString(),
                        //UpdatedBy = dr["UpdatedBy"].ToString()
                    };
                    subCategoryList.Add(genericObject);
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
            return subCategoryList;
        }


        public List<object> getAllSubCategoryExcludeDeletedData()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            string sqlcommand;
            List<object> subCategoryList = new List<object>();
            cmd.Connection = cn;
            da.SelectCommand = cmd;
            sqlcommand = "SELECT SubCategory.SubCategoryID, Category.CategoryName, SubCategory.SubCategoryName, SubCategory.CreatedAt, SubCategory.UpdatedAt, SubCategory.DeletedAt ";
            sqlcommand += "FROM UserInfomation AS UserInfomation_1  RIGHT OUTER JOIN UserInfomation RIGHT OUTER JOIN Category RIGHT OUTER JOIN ";
            sqlcommand += "SubCategory ON Category.CategoryID = SubCategory.CategoryID ON UserInfomation.UserID = SubCategory.CreatedBy ON UserInfomation_1.UserID = SubCategory.UpdatedBy ";
            sqlcommand += "where subCategory.DeletedAt = '1970-01-01 00:00:00.000'";
            cmd.CommandText = sqlcommand;
            cn.ConnectionString =
              ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            cn.Open();
            try
            {
                da.Fill(ds, "SubCategoryData");

                foreach (DataRow dr in ds.Tables["SubCategoryData"].Rows)
                {
                    var genericObject = new
                    {
                        SubCategoryID = Int32.Parse(dr["SubCategoryID"].ToString()),
                        CategoryName = dr["CategoryName"].ToString(),
                        SubCategoryName = dr["SubCategoryName"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                        DeletedAt = DateTime.Parse(dr["DeletedAt"].ToString()),
                        //CreatedBy = dr["CreatedBy"].ToString(),
                        //UpdatedBy = dr["UpdatedBy"].ToString()
                    };
                    subCategoryList.Add(genericObject);
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
            return subCategoryList;
        }
        public bool addOneSubCategory(string categoryId, string subCategoryName)
        {

            SqlCommand cmd = new SqlCommand(); SqlConnection cn = new SqlConnection();
            int numOfRecordsAffected = 0;
            cmd.Connection = cn;//tell the cmd to use the cn
            cmd.CommandText = "Insert SubCategory (CategoryID,SubCategoryName) values " +
                  " (@inCategoryID,@inSubCategoryName) ";
            cmd.Parameters.Add("@inSubCategoryName", SqlDbType.VarChar, 50).Value =subCategoryName;
            cmd.Parameters.Add("@inCategoryID", SqlDbType.Int).Value = categoryId;
            //cmd.Parameters.Add("@inCreatedBy", SqlDbType.Int).Value = inCreatedBy;
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            try
            {
                cn.Open();
                numOfRecordsAffected = cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch(SqlException ex)
            {

            }            
            if (numOfRecordsAffected == 0)
            { return false; }
            else
            { return true; }
        }
        public bool deleteOneSubCategory(string subCategoryId)
        {
            SqlCommand cmd = new SqlCommand(); SqlConnection cn = new SqlConnection();
            int numOfRecordsAffected = 0;
            cmd.Connection = cn;//tell the cmd to use the cn
            cmd.CommandText = "Update SubCategory set DeletedAt=GETDATE() " +
                  " where subCategoryId= @inSubCategoryId ";
            cmd.Parameters.Add("@inSubCategoryId", SqlDbType.Int).Value = subCategoryId;
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            cn.Open();
            numOfRecordsAffected = cmd.ExecuteNonQuery();
            cn.Close();
            if (numOfRecordsAffected == 0)
            { return false; }
            else
            { return true; }
        }
        public object getOneSubCateogry(string subCategoryId)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            string sqlcommand;
            List<object> subCategoryList = new List<object>();
            cmd.Connection = cn;
            da.SelectCommand = cmd;
            sqlcommand = "SELECT SubCategory.SubCategoryID, Category.CategoryID, SubCategory.SubCategoryName ";
            sqlcommand += "FROM Category RIGHT OUTER JOIN SubCategory ON Category.CategoryID = SubCategory.CategoryID  ";
            sqlcommand += "where SubCategory.SubCategoryID=@inSubCategoryId";
            cmd.Parameters.Add("@inSubCategoryId", SqlDbType.Int).Value = subCategoryId;
            cmd.CommandText = sqlcommand;
            cn.ConnectionString =
              ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            cn.Open();
            try
            {
                da.Fill(ds, "SubCategoryData");

                foreach (DataRow dr in ds.Tables["SubCategoryData"].Rows)
                {
                    var genericObject = new
                    {
                        SubCategoryID = Int32.Parse(dr["SubCategoryID"].ToString()),
                        CategoryID = dr["CategoryID"].ToString(),
                        SubCategoryName = dr["SubCategoryName"].ToString()
                    };
                    subCategoryList.Add(genericObject);
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
            return subCategoryList;
        }
        public bool updateOneSubCategory(string subCategoryId,string categoryId,string subCategoryName)
        {
            SqlCommand cmd = new SqlCommand(); SqlConnection cn = new SqlConnection();
            int numOfRecordsAffected = 0;
            cmd.Connection = cn;//tell the cmd to use the cn
            cmd.CommandText = "Update SubCategory set categoryId=@inCategoryId,subCategoryName=@inSubCategoryName " +
                  " where subCategoryId= @inSubCategoryId ";
            cmd.Parameters.Add("@inSubCategoryId", SqlDbType.Int).Value = subCategoryId;
            cmd.Parameters.Add("@inCategoryId", SqlDbType.Int).Value = categoryId;
            cmd.Parameters.Add("@inSubCategoryName", SqlDbType.VarChar, 100).Value = subCategoryName;
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            cn.Open();
            numOfRecordsAffected = cmd.ExecuteNonQuery();
            cn.Close();
            if (numOfRecordsAffected == 0)
            { return false; }
            else
            { return true; }
        }
    }
}