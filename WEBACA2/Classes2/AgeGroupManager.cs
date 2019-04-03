using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes2
{
    public class AgeGroupManager
    {
        public List<AgeGroup> getAllAgeGroup()
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            //Create an empty List, courseList
            List<AgeGroup> AgeGroupList = new List<AgeGroup>();
            //Obtain connection string information from web.config
            cn.ConnectionString =
                ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
            cmd.Connection = cn;//tell the cmd to use the cn
            //Suppy the cmd with the necessary SQL
            cmd.CommandText = "SELECT AgeGroupID, AgeGroupName, MinimumAge, MaximumAge, CONVERT(char (10) ,CreatedAt,103)AS CreatedAt, "+
            "CONVERT(char (10),DeletedAt,103) AS DeletedAt,CONVERT(char (10),UpdatedAt,103) AS UpdatedAt, CreatedBy, UpdatedBy "+
             " FROM AgeGroup where DeletedAt='01/01/1970'";
            //Tell the da (DataAdapter) to use the cmd
            da.SelectCommand = cmd;
            //Open an active connection 
            cn.Open();
            //Tell the da to tell the cmd to send the SQL
            //to the database, obtain the results and write
            //the results into the Dataset(ds), and name the
            //results as CourseData (the named results is also
            //called a DataTable)W
            da.Fill(ds, "AgeGroupData");
            cn.Close();//close the connection
            //loop through the datarows in the DataTable, ds.Tables["CourseData"]
            //to fetch all the course records and pump them into the List, courseList.
            foreach (DataRow dr in ds.Tables["AgeGroupData"].Rows)
            {
                AgeGroup ageGroup = new AgeGroup();
                ageGroup.AgeGroupID = int.Parse(dr["AgeGroupID"].ToString());
                ageGroup.AgeGroupName = dr["AgeGroupName"].ToString();
                ageGroup.MaximumAge = int.Parse(dr["MaximumAge"].ToString());
                ageGroup.MinimumAge = int.Parse(dr["MinimumAge"].ToString());
                ageGroup.CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString());
                ageGroup.UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString());
                ageGroup.DeletedAt = DateTime.Parse(dr["DeletedAt"].ToString());
                ageGroup.CreatedBy = int.Parse(dr["CreatedBy"].ToString());
                ageGroup.UpdatedBy = int.Parse(dr["UpdatedBy"].ToString());
                AgeGroupList.Add(ageGroup);
            }
            return AgeGroupList;//return the List to the calling program.
        }//end of getAllAgeGroup() method
        public bool addOneAgeGroup(dynamic inWebFormData)
        {
            int numOfRecordsAffected = 0;
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["WebaConnectionString"].ToString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;//tell the cmd to use the cn
                    cmd.CommandText = "Insert AgeGroup (AgeGroupName,MinimumAge,MaximumAge) values " +
                  " (@inAgeGroupName,@inMinimumAge,@inMaximumAge) ";
                    //20/Jan/2015
                    //I kept having error on "Failed to convert parameter value from a JValue to a String" from
                    //the command cmd.Parameters.Add(...).Value = inStudent.FullName; (I fixed by adding .Value after the FullName)
                    //By analyzing the content of the inStudent.FullName, I noticed that it returns somekind of object
                    //instead of a string. To take the string value out , I need to use inStudent.FullName.Value;
                    cmd.Parameters.Add("@inAgeGroupName", SqlDbType.VarChar, 40).Value = inWebFormData.ageGroupName.Value;
                    cmd.Parameters.Add("@inMinimumAge", SqlDbType.Int).Value = inWebFormData.minimumAge.Value;
                    cmd.Parameters.Add("@inMaximumAge", SqlDbType.Int).Value = inWebFormData.maximumAge.Value;
                    cn.Open();
                    try
                    {
                        numOfRecordsAffected=Int32.Parse(cmd.ExecuteScalar().ToString());
                    }
                    catch (SqlException sqlEx)
                    {
                        /*
                         ALTER TABLE Student 
                       ADD CONSTRAINT Student_AdminIdUniqueConstraint UNIQUE (AdmissionId); 
                       GO  */

                        if (sqlEx.Message.Contains("AgeGroup_AdminIdUniqueConstraint") == true)
                        {
                            string messageTemplate = "Unable to save due to {0} admission id found in other records.";
                            string message = string.Format(messageTemplate, inWebFormData.AgeGroupName.Value);
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
          
            if (numOfRecordsAffected == 0)
            { return false; }
            else
            { return true; }
        }
    }
}