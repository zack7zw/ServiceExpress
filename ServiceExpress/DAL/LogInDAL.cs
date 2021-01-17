using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ServiceExpress.DAL
{
    public class LogInDAL
    {
        DalDbConn dbConn = new DalDbConn();
        public Boolean login(string userName, string password)
        {
            string strConnectionString =
                ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;
            string pw = "";
            //create connection
            SqlConnection myConnect = new SqlConnection(strConnectionString);
            DataSet retailer_ds = new DataSet();
            //create command
            string strCommandText = "SELECT userName, Password from Login where userName=@userName and Password=@password";
            SqlCommand cmd = new SqlCommand(strCommandText, myConnect);

            //open connection and retrieve data by calling ExecuteReader
            myConnect.Open();
            cmd.Parameters.AddWithValue("@userName", userName);
              try
            {
            SqlDataAdapter reader =new SqlDataAdapter(strCommandText, myConnect);
            reader.SelectCommand.Parameters.AddWithValue("@userName", userName);
            reader.SelectCommand.Parameters.AddWithValue("@password", password);
            reader.Fill(retailer_ds);
            }
              catch (Exception ex)
              {
                  throw;
              }
              finally
              {
                  myConnect.Close();
              }
              if (retailer_ds.Tables[0].Rows.Count == 0)
                  return false;
              else
                  return true;
        }

        public Boolean isAppUser(String category)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet retailerds = new DataSet();
            SqlConnection conn = dbConn.GetConnection();

            // Complete ccode to formulate SQL command to retrieve record
            // where User role equal to 'appuser' for the particular user id 


            sql = new StringBuilder();
            sql.AppendLine("SELECT userName, Password ");
            sql.AppendLine("FROM Login");
            sql.AppendLine("WHERE category = @category");



            try
            {
                da = new SqlDataAdapter(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("@category", category);
                da.Fill(retailerds);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            if (retailerds.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}