using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace ServiceExpress.DAL
{
    public class DeliveryOrderWSDAL
    {
        private String errMsg;
        DalDbConn dbConn = new DalDbConn();

 

        public int InsertDelivery(String deliveryNo, String deliveryStatus, DateTime deliveryDate, String purOrderNo, String deliverySuppName)
        {

            StringBuilder sql;
            SqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO deliveryOrder (deliveryNo, deliveryStatus, deliveryDate, purOrderNo, deliverySuppName)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@deliveryNo, @deliveryStatus, @deliveryDate, @purOrderNo, @deliverySuppName)");
            SqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = new SqlCommand(sql.ToString(), conn);
                conn.Open();
                sqlCmd.Parameters.AddWithValue("@deliveryNo", deliveryNo);
                sqlCmd.Parameters.AddWithValue("@deliveryStatus", deliveryStatus);
                sqlCmd.Parameters.AddWithValue("@deliveryDate", deliveryDate);
                sqlCmd.Parameters.AddWithValue("@purOrderNo", purOrderNo);
                sqlCmd.Parameters.AddWithValue("@deliverySuppName", deliverySuppName);
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int UpdateDeliveryOrder(string deliveryNo, string deliveryStatus)
        {

            StringBuilder sql;
            SqlCommand sqlCmd;
            int result;

            result = 0;
            string strCommandText = "UPDATE deliveryOrder SET deliveryStatus=@deliveryStatus WHERE deliveryNo=@deliveryNo";



            SqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = new SqlCommand(strCommandText, conn);
                sqlCmd.Parameters.AddWithValue("@deliveryNo", deliveryNo);
                sqlCmd.Parameters.AddWithValue("@deliveryStatus", deliveryStatus);
                conn.Open();
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public DataSet getAllDelivery()
        {
            StringBuilder sql;
           
            DataSet delivery;

            SqlConnection conn = dbConn.GetConnection();
            delivery = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT deliveryNo, po.purOrderNo, deliverySuppName, po.orderDate, deliveryDate, deliveryStatus");
            sql.AppendLine("FROM deliveryOrder do");
            sql.AppendLine("INNER JOIN purOrder po ON po.purOrderNo = do.purOrderNo");
            sql.AppendLine("ORDER BY deliveryStatus");


            try
            {
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                delivery.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return delivery;

        }


        public DataSet getDeliveryByStatus(string deliveryStatus)
        {
            StringBuilder sql;
           
            DataSet delivery;

            SqlConnection conn = dbConn.GetConnection();
            delivery = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT deliveryNo, deliveryStatus, deliveryDate, puro.purOrderNo, deliverySuppName, puro.orderDate");
            sql.AppendLine("FROM deliveryOrder do");
            sql.AppendLine("INNER JOIN PurOrder puro ON puro.purOrderNo = do.purOrderNo");
            sql.AppendLine("WHERE deliveryStatus = @deliveryStatus");



            try
            {
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@deliveryStatus", deliveryStatus);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                delivery.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return delivery;

        }


        public DataSet getAllPurchaseProducts(string purOrderNo)
        {
            StringBuilder sql;
           
            DataSet delivery;

            SqlConnection conn = dbConn.GetConnection();
            delivery = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT p.prodName, p.prodDescription, quantity");
            sql.AppendLine("FROM PurOrderItem pi");
            sql.AppendLine("INNER JOIN Product p ON p.prodID = pi.prodID");
            sql.AppendLine("WHERE purOrderNo = @purOrderNo");



            try
            {
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@purOrderNo", purOrderNo);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                delivery.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return delivery;

        }

        public DataSet getPurchaseOrderForDo(string purOrderNo)
        {
            //get connection string from web.config
            string strConnectionString = ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;
            SqlConnection myConnect = new SqlConnection(strConnectionString);

            string strCommandText = "SELECT p.prodID, p.prodName, p.prodDescription, quantity from PurOrderItem poi INNER JOIN Product p ON p.prodID = poi.prodID WHERE purOrderNo = @purOrderItemID";

            DataSet delivery;
            delivery = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
                cmd.Parameters.AddWithValue("@purOrderItemID", purOrderNo);
                myConnect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();

                dt.Load(reader);
                delivery.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                myConnect.Close();
            }
            return delivery;



        }


        public string[] getAllLabels(string purOrderNo)
        {
            StringBuilder sql;
           
            DataSet delivery;

            SqlConnection conn = dbConn.GetConnection();
            delivery = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT custcom.orderPersonnel, custcom.addresss, custcom.contact, CONVERT(varchar, puro.orderDate, 103) orderDate");
            sql.AppendLine("FROM CustomerCompany custcom");
            sql.AppendLine("INNER JOIN PurOrder puro ON puro.orderPersonnelId = custcom.orderPersonnelId");
            sql.AppendLine("WHERE purOrderNo = @purOrderNo");



            string[] storevalue = new string[4];
            try
            {
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@purOrderNo", purOrderNo);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    storevalue[0] = reader["orderPersonnel"].ToString();
                    storevalue[1] = reader["addresss"].ToString();
                    storevalue[2] = reader["contact"].ToString();
                    storevalue[3] = reader["orderDate"].ToString();
                }

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return storevalue;

        }
    }

}