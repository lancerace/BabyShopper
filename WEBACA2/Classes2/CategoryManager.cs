using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes2
{
    public class CategoryManager
    {
        public List<Category> getAllCategory()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            string sqlcommand;
            List<Category> categoryList = new List<Category>();
            cmd.Connection = cn;
            da.SelectCommand = cmd;
            sqlcommand = "SELECT CategoryID,CategoryName, CreatedAt, DeletedAt, UpdatedAt, CreatedBy, UpdatedBy ";
            sqlcommand+="FROM Category ";
            sqlcommand+="where DeletedAt='1970-01-01 00:00:00.000' ";
            cmd.CommandText = sqlcommand;             
            cn.ConnectionString =
            ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            cn.Open();
             da.Fill(ds,"categoryData");
            cn.Close();

           foreach (DataRow dr in ds.Tables["categoryData"].Rows)
            {
                Category category = new Category();
                category.CategoryID = int.Parse(dr["CategoryID"].ToString());
                category.CategoryName= dr["CategoryName"].ToString();
                categoryList.Add(category);
            }
            return categoryList;
        }
    }
}