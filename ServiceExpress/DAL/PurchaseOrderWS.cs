using ServiceExpress.FlyAsiaService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiceExpress.DAL
{
    public class PurchaseOrderWS
    {
        private String errMsg;
        DalDbConn dbConn = new DalDbConn();
        public int InserTPOWS(string purOrderNo, string orderPersonnel, string orderCompany, double totalPrice, string statusPO, string orderPersonnelId)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();


                DateTime orderDate = DateTime.Now;



                string strCommandText = "INSERT PurOrder(purOrderNo, totalPrice,orderDate, orderPersonnel, orderCompany, statusPO, orderPersonnelId)";
                strCommandText += " Values (@purOrderNo, @totalPrice, @orderDate, @orderPersonnel, @orderCompany,@statusPO, @orderPersonnelId)";

               

                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
                cmd.Parameters.AddWithValue("@purOrderNo", purOrderNo);
                cmd.Parameters.AddWithValue("@totalPrice", totalPrice);

                cmd.Parameters.AddWithValue("@orderDate", orderDate);
                cmd.Parameters.AddWithValue("@orderPersonnel", orderPersonnel);
                cmd.Parameters.AddWithValue("@orderCompany", orderCompany);
                cmd.Parameters.AddWithValue("@statusPO", statusPO);


           
                cmd.Parameters.AddWithValue("@orderPersonnelId", orderPersonnelId);
        
                myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            return result;
        }
        public int InserTPOCustomerWS( string orderPersonnel, string orderCompany,string orderPersonnelId, string addresss, string contact)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();


                DateTime orderDate = DateTime.Now;



                string strCommandText = "INSERT CustomerCompany(orderPersonnelId, orderPersonnel,addresss, contact, companyName)";
                strCommandText += " Values (@orderPersonnelId, @orderPersonnel, @addresss, @contact, @companyName)";
           

                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
             
                cmd.Parameters.AddWithValue("@orderPersonnel", orderPersonnel);
        


                cmd.Parameters.AddWithValue("@companyName", orderCompany);
                cmd.Parameters.AddWithValue("@orderPersonnelId", orderPersonnelId);
                cmd.Parameters.AddWithValue("@addresss", addresss);
                cmd.Parameters.AddWithValue("@contact", contact);
                myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            return result;
        }
        public int InsertPOItemsWS(string ProdID, string Quantity, string Price, string purOrderNo)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();


                String purOrderItemID = "POD" + DAL.GeneratePW.Generate(5, 6);


                string strCommandText = "INSERT PurOrderItem(purOrderItemID, purOrderNo, prodID, quantity, price)";
                strCommandText += " Values (@purOrderItemID, @purOrderNo, @prodID, @quatity, @price)";

                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);



                cmd.Parameters.AddWithValue("@purOrderItemID", purOrderItemID);
                cmd.Parameters.AddWithValue("@purOrderNo", purOrderNo);
                cmd.Parameters.AddWithValue("@prodID", ProdID);
                cmd.Parameters.AddWithValue("@quatity", Quantity);
                cmd.Parameters.AddWithValue("@price", Price);

                myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            return result;
        }

        public DataTable poall()
        {
            //get connection string from web.config
             SqlConnection myConnect = dbConn.GetConnection();

             string strCommandText = "SELECT purOrderNo, totalPrice, CONVERT(varchar, orderDate, 103) 'orderDate', orderPersonnel, orderCompany, statusPO  from PurOrder";


            SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
            myConnect.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt;
        }
        public DataTable searchPOall(string statusPO, string startDate, string endDate, string Users)
        {
            SqlConnection myConnect = dbConn.GetConnection();
            string strCommandText = strCoomandText(statusPO, startDate, endDate, Users);
            SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
            cmd.Parameters.AddWithValue("@statusPO", statusPO);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@Users", Users);
            myConnect.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt;
        }
        public string strCoomandText(string statusPO, string startDate, string endDate, string Users)
        {
            string strCommandText = "SELECT purOrderNo, totalPrice, CONVERT(varchar, orderDate, 103) 'orderDate', orderPersonnel, orderCompany, statusPO  from PurOrder";
            if (!statusPO.Equals("Please Select Status") && Users.Equals("Please Select Users") && startDate.Equals(""))
            {
                strCommandText = "SELECT purOrderNo, totalPrice, CONVERT(varchar, orderDate, 103) 'orderDate', orderPersonnel, orderCompany, statusPO  from PurOrder where statusPO=@statusPO";

            }

            if (statusPO.Equals("Please Select Status") && (!Users.Equals("Please Select Users")) && (startDate.Equals("")))
            {
                strCommandText = "SELECT purOrderNo, totalPrice, CONVERT(varchar, orderDate, 103) 'orderDate', orderPersonnel, orderCompany, statusPO  from PurOrder where orderPersonnel=@Users";

            }
            if (statusPO.Equals("Please Select Status") && (Users.Equals("Please Select Users")) && (!startDate.Equals("")))
            {
                strCommandText = "SELECT purOrderNo, totalPrice, CONVERT(varchar, orderDate, 103) 'orderDate', orderPersonnel, orderCompany, statusPO from PurOrder where orderDate between @startDate and @endDate";

            }
            if (!statusPO.Equals("Please Select Status") && (!Users.Equals("Please Select Users")) && (startDate.Equals("")))
            {
                strCommandText = "SELECT purOrderNo, totalPrice, CONVERT(varchar, orderDate, 103) 'orderDate', orderPersonnel, orderCompany, statusPO  from PurOrder where statusPO=@statusPO and orderPersonnel=@Users";

            }
            if (!statusPO.Equals("Please Select Status") && (Users.Equals("Please Select Users")) && (!startDate.Equals("")))
            {
                strCommandText = "SELECT purOrderNo, totalPrice, CONVERT(varchar, orderDate, 103) 'orderDate', orderPersonnel, orderCompany, statusPO  from PurOrder where statusPO=@statusPO and orderDate between @startDate and @endDate";

            }
            if (statusPO.Equals("Please Select Status") && (!Users.Equals("Please Select Users")) && (!startDate.Equals("")))
            {
                strCommandText = "SELECT purOrderNo, totalPrice, CONVERT(varchar, orderDate, 103) 'orderDate', orderPersonnel, orderCompany, statusPO  from PurOrder where orderPersonnel=@Users and orderDate between @startDate and @endDate";

            }

            if (!statusPO.Equals("Please Select Status") && (!Users.Equals("Please Select Users")) && (!startDate.Equals("")))
            {
                strCommandText = "SELECT purOrderNo, totalPrice, CONVERT(varchar, orderDate, 103) 'orderDate', orderPersonnel, orderCompany, statusPO  from PurOrder where statusPO=@statusPO and orderPersonnel=@Users and orderDate between @startDate and @endDate";

            }
            return strCommandText;
        }
        public int updateStatusWS(string statusPO, string purOrderNo)
        {
            FlyAsiaWebServiceClient flyAsiaservice = new FlyAsiaWebServiceClient();
            return flyAsiaservice.updateStatusWS(statusPO, purOrderNo);
        }
        public int updateStatus(string statusPO, string purOrderNo)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();
                string strCommandText = "Update PurOrder set statusPO = @statusPO where purOrderNo=@purOrderNo";
                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
                cmd.Parameters.AddWithValue("@statusPO", statusPO);
                cmd.Parameters.AddWithValue("@purOrderNo", purOrderNo);
                 myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            return result;
        }
        public int updatePOquantityWS(int quantity, string purOrderNo, string prodID)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();
                string strCommandText = "Update PurOrderItem set quantity=@quantity where purOrderNo=@purOrderNo and prodID=@prodID";
                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@purOrderNo", purOrderNo);
                cmd.Parameters.AddWithValue("@prodID", prodID);
                myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            return result;
        }
        public DataSet dailySales(string date)
        {
            DataSet dt = new DataSet();
            SqlDataAdapter da;
            try
            {
            //get connection string from web.config
            SqlConnection myConnect = dbConn.GetConnection();
           
           
                string strCommandText = "SELECT PurOrderItem.prodID, Product.prodName," +
                    " PurOrderItem.quantity, PurOrderItem.price, CONVERT(varchar, PurOrder.orderDate, 103) 'orderDate', PurOrder.purOrderNo" +
                    " FROM PurOrder INNER JOIN" +
                         " PurOrderItem ON PurOrder.purOrderNo = PurOrderItem.purOrderNo INNER JOIN" +
                         " Product ON PurOrderItem.prodID = Product.prodID" +
                        " WHERE (PurOrder.orderDate = @orderDate)";
      
               da = new SqlDataAdapter(strCommandText, myConnect);
                da.SelectCommand.Parameters.AddWithValue("@orderDate", date);
                da.Fill(dt);
          

            }
            catch
            {

            }
            return dt;
        }
        public int updateReason(string reason, string purOrderNo)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();


                DateTime orderDate = DateTime.Now;



                string strCommandText = "Update PurOrder set reasonssss=@reason where purOrderNo=@purOrderNo";



                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
                cmd.Parameters.AddWithValue("@reason", reason);
                cmd.Parameters.AddWithValue("@purOrderNo", purOrderNo);




                myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            return result;
        }
    }
}