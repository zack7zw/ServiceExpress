using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
namespace ServiceExpress.DAL
{
    public class InvoiceWSDAL
    {
        private String errMsg;
        DalDbConn dbConn = new DalDbConn();

        public DataSet getAllInvoiceWS()
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet invoiceData;

            SqlConnection conn = dbConn.GetConnection();
            invoiceData = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT invoiceNo, 'Service Express' supCompany, invoicePersonnel, creditPeriod, creditTerm, invoiceStatus, grandTotal, CONVERT(varchar, invoiceDate, 103) invoiceDate, invoiceStatus ");
            sql.AppendLine("FROM Invoice iv");
            sql.AppendLine("INNER JOIN PurOrder po on po.purOrderNo = iv.purOrderNo");
            sql.AppendLine("INNER JOIN CustomerCompany cc on cc.orderPersonnelId = po.orderPersonnelId");


            try
            {
                da = new SqlDataAdapter(sql.ToString(), conn);

                da.Fill(invoiceData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return invoiceData;
        }


        public DataSet getAllInvoiceByCNWS(string fDate, string toDate, string statusSort, string sortBy, string companyName)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet invoiceData;

            SqlConnection conn = dbConn.GetConnection();
            invoiceData = new DataSet();


            sql = new StringBuilder();
            sql.AppendLine("SELECT invoiceNo, 'Service Express' supCompany, invoicePersonnel, creditPeriod, creditTerm, invoiceStatus, grandTotal, CONVERT(varchar, invoiceDate, 103) invoiceDate, invoiceStatus ");
            sql.AppendLine("FROM Invoice iv ");
            sql.AppendLine("INNER JOIN PurOrder po on po.purOrderNo = iv.purOrderNo ");
            sql.AppendLine("INNER JOIN CustomerCompany cc on cc.orderPersonnelId = po.orderPersonnelId ");
            // where search
            //get all unpaid invoice for payment
            if (fDate.Equals("") && toDate.Equals("") && statusSort.Equals(""))
            {
                sql.AppendLine("WHERE cc.companyName = @companyName and invoiceStatus <> 'paid'  ORDER BY iv.invoiceDate " + sortBy);
            }
            //get all default
            else if (fDate.Equals("") && toDate.Equals("") && statusSort.Equals("All"))
            {
                sql.AppendLine("WHERE cc.companyName = @companyName  ORDER BY iv.invoiceDate " + sortBy);
            }
            //get all sort
            else if (fDate.Equals("") && toDate.Equals("") && !(statusSort.Equals("All")))
            {
                sql.AppendLine("WHERE cc.companyName = @companyName  and invoiceStatus = @statusSort  ORDER BY iv.invoiceDate " + sortBy);
            }
            //get by date
            else if (!(fDate.Equals("")) && !(toDate.Equals("")) && statusSort.Equals("All"))
            {
                // sql.AppendLine("WHERE cc.companyName = @companyName  ORDER BY statusSort=@statusSort " + sortBy);
                sql.AppendLine("WHERE cc.companyName = @companyName and invoiceDate between  @fDate and  @toDate order by iv.invoiceDate " + sortBy);

            }
            else if (!(fDate.Equals("")) && !(toDate.Equals("")) && !(statusSort.Equals("All")))
            {
                // sql.AppendLine("WHERE cc.companyName = @companyName  ORDER BY statusSort=@statusSort " + sortBy);
                sql.AppendLine("WHERE cc.companyName = @companyName and invoiceStatus = @statusSort  and invoiceDate between  @fDate and  @toDate ORDER BY iv.invoiceDate " + sortBy);

            }




            try
            {
                da = new SqlDataAdapter(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("@companyName", companyName);
                da.SelectCommand.Parameters.AddWithValue("@statusSort", statusSort);
                da.SelectCommand.Parameters.AddWithValue("@fDate", fDate);
                da.SelectCommand.Parameters.AddWithValue("@toDate", toDate);
                da.Fill(invoiceData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return invoiceData;

        }

        public DataSet getInvoiceWS(string invoiceNo)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet invoiceData;

            SqlConnection conn = dbConn.GetConnection();
            invoiceData = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT iv.invoiceNo, invoiceStatus, gstAmt, grandTotal, creditPeriod, creditTerm, deliveryNo, CONVERT(varchar, invoiceDate, 103) invoiceDate, companyName, po.orderPersonnel, addresss, 'Service Express' supCompany, invoicePersonnel, '&lt;center&gt;Service Express &lt;br/&gt;18, Changi Business Park Central 1 &lt;br/&gt; Singapore 486097 &lt;br/&gt;6782-6690 &lt;center/&gt;' addresss, po.TotalPrice , ");
            sql.AppendLine("(SELECT  sum(paidAmt) FROM InvoicePayment WHERE invoiceNo = @invoiceNo group by invoiceNo ) amtPaid , grandTotal- (SELECT  sum(paidAmt) FROM InvoicePayment WHERE invoiceNo = @invoiceNo group by invoiceNo ) outstandingAmt, creditPeriod - DATEDIFF(day, invoiceDate, GETDATE()) noOfDayLeft, ");
            sql.AppendLine("(SELECT creditDiscPeriod FROM InvoiceCreditDiscRecord WHERE invoiceNo = @invoiceNo) - DATEDIFF(day, invoiceDate, GETDATE()) noOfDayLeftDisc, iv.purOrderNo  ");
            sql.AppendLine("FROM Invoice iv ");
            sql.AppendLine("INNER JOIN PurOrder po on po.purOrderNo = iv.purOrderNo");
            sql.AppendLine("INNER JOIN CustomerCompany cc on cc.orderPersonnelId = po.orderPersonnelId");
            sql.AppendLine("WHERE iv.invoiceNo = @invoiceNo");
            


            try
            {
                da = new SqlDataAdapter(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("@invoiceNo", invoiceNo);
                da.Fill(invoiceData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return invoiceData;
        }

        public DataSet getCreditDiscInfo(string invoiceNo)
        {

            StringBuilder sql;
            SqlDataAdapter da;
            DataSet invoiceData;

            SqlConnection conn = dbConn.GetConnection();
            invoiceData = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT creditRecordID, creditDiscount, creditDiscPeriod, creditDiscAmt, creditStatus ");
            sql.AppendLine("FROM InvoiceCreditDiscRecord icr ");
            sql.AppendLine("INNER JOIN Invoice iv on iv.invoiceNo = icr.invoiceNo ");
            sql.AppendLine("WHERE  icr.invoiceNo = @invoiceNo and iv.invoiceStatus <> 'Paid' ");


            try
            {
                da = new SqlDataAdapter(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("@invoiceNo", invoiceNo);
                da.Fill(invoiceData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return invoiceData;

        }
        public int UpdateInvoice(string invoiceNo, double totalPrice, double depositAmt, double outstandingAmt, string invoiceStatus, string paymentDate, string company,
            string contactPersonnel, int creditPeriod, int deliveryNo, string purOrderNo, string nric, string dateCreated)
        {

            StringBuilder sql;
            SqlCommand sqlCmd;
            int result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Invoice");
            sql.AppendLine(" ");
            sql.AppendLine("SET totalPrice=@totalPrice, depositAmt=@depositAmt, outstandingAmt=@outstandingAmt, invoiceStatus=@invoiceStatus, ");
            sql.AppendLine("paymentDate= CONVERT(DATETIME, @paymentDate, 103), company=@company, contactPersonnel=@contactPersonnel, creditPeriod=@creditPeriod, deliveryNo=@deliveryNo, purOrderNo=@purOrderNo, nric=@nric,  dateCreated= CONVERT(DATETIME, @dateCreated, 103)");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE invoiceNo=@invoiceNo");
           
            SqlConnection conn = dbConn.GetConnection();
           try
            {
                sqlCmd = new SqlCommand(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@invoiceNo", invoiceNo);
                sqlCmd.Parameters.AddWithValue("@totalPrice", totalPrice);
                sqlCmd.Parameters.AddWithValue("@depositAmt", depositAmt);
                sqlCmd.Parameters.AddWithValue("@outstandingAmt", outstandingAmt);
                sqlCmd.Parameters.AddWithValue("@invoiceStatus", invoiceStatus);
                sqlCmd.Parameters.AddWithValue("@paymentDate", DateTime.Today.ToString());//paymentDate);
                sqlCmd.Parameters.AddWithValue("@company", company);
                sqlCmd.Parameters.AddWithValue("@contactPersonnel", contactPersonnel);
                sqlCmd.Parameters.AddWithValue("@creditPeriod", creditPeriod);
                sqlCmd.Parameters.AddWithValue("@deliveryNo", deliveryNo);
                sqlCmd.Parameters.AddWithValue("@purOrderNo", purOrderNo);
                sqlCmd.Parameters.AddWithValue("@nric", nric);
                sqlCmd.Parameters.AddWithValue("@dateCreated", dateCreated);
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

        public int UpdateInvoiceStatusToDue()
        {

            StringBuilder sql;
            SqlCommand sqlCmd;
            int result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE INVOICE SET invoiceStatus = 'Due' ");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE ((creditPeriod - DATEDIFF(day, invoiceDate, GETDATE())) between -30 and 0) and invoiceStatus <> 'Due' and invoiceStatus <> 'WarningSent-Due' and invoiceStatus <> 'WarningSent-BadDept' and invoiceStatus <> 'Paid'");

            SqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = new SqlCommand(sql.ToString(), conn);
                
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

        public int UpdateInvoiceStatusToBadDebt()
        {

            StringBuilder sql;
            SqlCommand sqlCmd;
            int result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE INVOICE SET invoiceStatus = 'BadDebt'");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE ((creditPeriod - DATEDIFF(day, invoiceDate, GETDATE())) < -30) and invoiceStatus <> 'BadDebt'  and invoiceStatus <> 'WarningSent-BadDept' and invoiceStatus <> 'Paid'");

            SqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = new SqlCommand(sql.ToString(), conn);

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


        
        public int InvoiceStatusUpdate(string invoiceStatus, string invoiceNo)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();
                string strCommandText = "Update Invoice set invoiceStatus = @invoiceStatus where invoiceNo=@invoiceNo";
                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
                cmd.Parameters.AddWithValue("@invoiceStatus", invoiceStatus);
                cmd.Parameters.AddWithValue("@invoiceNo", invoiceNo);
                 myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            return result;
        }

        public int InvoiceCreditDiscRecordUpdate(string creditStatus, string invoiceNo)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();
                string strCommandText = "Update InvoiceCreditDiscRecord set creditStatus = @creditStatus where invoiceNo=@invoiceNo";
                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
                cmd.Parameters.AddWithValue("@creditStatus", creditStatus);
                cmd.Parameters.AddWithValue("@invoiceNo", invoiceNo);
                 myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            return result;
        }
        
        public int InsertInvoicePayment(string paymentNo, double paidAmt, string paymentDate, string transactionNo, string paymentPersonnel, string paymentType, string invoiceNo)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();


                DateTime orderDate = DateTime.Now;

   
                string strCommandText = "INSERT InvoicePayment(paymentNo, paidAmt, paymentDate, transactionNo, paymentPersonnel, paymentType, invoiceNo)";
                strCommandText += " Values (@paymentNo, @paidAmt, @paymentDate, @transactionNo, @paymentPersonnel, @paymentType, @invoiceNo)";


                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);

                cmd.Parameters.AddWithValue("@paymentNo", paymentNo);
                cmd.Parameters.AddWithValue("@paidAmt", paidAmt);
                cmd.Parameters.AddWithValue("@paymentDate", paymentDate);
                cmd.Parameters.AddWithValue("@transactionNo", transactionNo);
                cmd.Parameters.AddWithValue("@paymentPersonnel", paymentPersonnel);
                cmd.Parameters.AddWithValue("@paymentType", paymentType);
                cmd.Parameters.AddWithValue("@invoiceNo", invoiceNo);

                myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            return result;
        }



        //---------- local


        public DataSet deliveryOrderForInvoice()
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet deliveryDetails;
            
            SqlConnection conn = dbConn.GetConnection();
            deliveryDetails = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT do.deliveryNo, com.companyName, po.orderPersonnel, CONVERT(varchar, do.deliveryDate, 103) AS doDate, po.totalPrice ");
            sql.AppendLine("From deliveryOrder do ");
            sql.AppendLine("INNER JOIN purOrder po ON po.purOrderNo = do.purOrderNo ");
            sql.AppendLine("INNER JOIN CustomerCompany com ON com.orderPersonnelId = po.orderPersonnelId ");
            sql.AppendLine("WHERE do.deliveryStatus='Received'");


            try
            {
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                deliveryDetails.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return deliveryDetails;

        }


        public DataSet getPoIdFromDoById(string doNo)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet deliveryDetails;

            SqlConnection conn = dbConn.GetConnection();
            deliveryDetails = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine(" SELECT do.purOrderNo ");
            sql.AppendLine("FROM deliveryOrder do ");
            sql.AppendLine("INNER JOIN PurOrder po ON do.purOrderNo = po.purOrderNo ");
            sql.AppendLine("WHERE do.deliveryNo =@deliveryNo ");


            try
            {
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@deliveryNo", doNo);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                deliveryDetails.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return deliveryDetails;

        }

        public DataSet getInvoiceDetailFromDo(string doNo)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet deliveryDetails;

            SqlConnection conn = dbConn.GetConnection();
            deliveryDetails = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT cc.companyName, cc.orderPersonnel, cc.addresss, CONVERT(varchar, do.deliveryDate, 103) AS DOdate, po.totalPrice ");
            sql.AppendLine("FROM deliveryOrder do ");
            sql.AppendLine("INNER JOIN PurOrder po ON do.purOrderNo = po.purOrderNo  ");
            sql.AppendLine("INNER JOIN CustomerCompany cc  ON po.orderPersonnelId = cc.orderPersonnelId  ");
            sql.AppendLine("WHERE do.deliveryNo =@deliveryNo  ");


            try
            {
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@deliveryNo", doNo);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                deliveryDetails.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return deliveryDetails;

        }

        public DataSet getInvoiceDetailsFromPO(string poNo)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet deliveryDetails;

            SqlConnection conn = dbConn.GetConnection();
            deliveryDetails = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine(" SELECT p.prodID, p.prodName, p.prodDescription, quantity , p.price , (p.price * quantity) totalprice ");
            sql.AppendLine("FROM PurOrderItem poi ");
            sql.AppendLine("INNER JOIN Product p ON p.prodID = poi.prodID  ");
            sql.AppendLine("INNER JOIN PurOrder po ON po.purOrderNo = poi.purOrderNo  ");
            sql.AppendLine("INNER JOIN deliveryOrder do ON do.purOrderNo = po.purOrderNo  ");
            sql.AppendLine("WHERE po.purOrderNo=@purOrderNo  ");
            //'POiL!8-5Q'

            try
            {
               /* da = new SqlDataAdapter(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("@purOrderNo", poNo);
                da.Fill(deliveryDetails); */

                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@purOrderNo", poNo);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                deliveryDetails.Tables.Add(dt); 

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return deliveryDetails;

        }
        

            public DataSet getPaymentRecord(string ivNo)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet deliveryDetails;

            SqlConnection conn = dbConn.GetConnection();
            deliveryDetails = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine(" SELECT paymentNo, paidAmt, paymentDate, transactionNo, paymentPersonnel, paymentType, invoiceNo ");
            sql.AppendLine("FROM InvoicePayment ");
            sql.AppendLine("WHERE invoiceNo = @invoiceNo ");

            try
            {
               /* da = new SqlDataAdapter(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("@purOrderNo", poNo);
                da.Fill(deliveryDetails); */

                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@invoiceNo", ivNo);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                deliveryDetails.Tables.Add(dt); 

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return deliveryDetails;

        }

        public int InsertInvoice(string invoiceNo, string invoiceStatus, double gstAmt, double grandTotal, int creditPeriod, string creditTerm, string deliveryNo, string purOrderNo, string invoiceDate, string invoicePersonnel)
        {

            StringBuilder sql;
            SqlCommand sqlCmd;
            int result;

            result = 0;
            
            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO Invoice (invoiceNo, invoiceStatus, gstAmt, grandTotal, creditPeriod, creditTerm, deliveryNo, purOrderNo, invoiceDate, invoicePersonnel)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@invoiceNo, @invoiceStatus, @gstAmt, @grandTotal, @creditPeriod, @creditTerm, @deliveryNo, @purOrderNo, CONVERT(DATETIME, @invoiceDate, 103), @invoicePersonnel )");
            SqlConnection conn = dbConn.GetConnection();
           // try
            //{
          
                sqlCmd = new SqlCommand(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@invoiceNo", invoiceNo);
                sqlCmd.Parameters.AddWithValue("@invoiceStatus", invoiceStatus);
                sqlCmd.Parameters.AddWithValue("@gstAmt", gstAmt);
                sqlCmd.Parameters.AddWithValue("@grandTotal", grandTotal);
                sqlCmd.Parameters.AddWithValue("@creditPeriod", creditPeriod);
                sqlCmd.Parameters.AddWithValue("@creditTerm", creditTerm);
                sqlCmd.Parameters.AddWithValue("@deliveryNo", deliveryNo);
                sqlCmd.Parameters.AddWithValue("@purOrderNo", purOrderNo);
                sqlCmd.Parameters.AddWithValue("@invoiceDate", invoiceDate); 
                sqlCmd.Parameters.AddWithValue("@invoicePersonnel", invoicePersonnel);

                conn.Open();
                result = sqlCmd.ExecuteNonQuery();
           // }
           // catch (Exception ex)
           // {
             //   errMsg = ex.Message;
           // }
           // finally
            //{
                conn.Close();
          //  }

            return result;
        }

        public int InsertInvoiceCreditDiscRecord(string creditRecordID, double creditDiscount, int creditDiscPeriod, double creditDiscAmt, string creditStatus, string invoiceNo)
        {

            StringBuilder sql;
            SqlCommand sqlCmd;
            int result;

            result = 0;
            
            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO InvoiceCreditDiscRecord (creditRecordID, creditDiscount, creditDiscPeriod, creditDiscAmt, creditStatus, invoiceNo)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@creditRecordID, @creditDiscount, @creditDiscPeriod, @creditDiscAmt, @creditStatus, @invoiceNo )");
            SqlConnection conn = dbConn.GetConnection();
           // try
            //{
          
                sqlCmd = new SqlCommand(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@creditRecordID", creditRecordID);
                sqlCmd.Parameters.AddWithValue("@creditDiscount", creditDiscount);
                sqlCmd.Parameters.AddWithValue("@creditDiscPeriod", creditDiscPeriod);
                sqlCmd.Parameters.AddWithValue("@creditDiscAmt", creditDiscAmt);
                sqlCmd.Parameters.AddWithValue("@creditStatus", creditStatus);
                sqlCmd.Parameters.AddWithValue("@invoiceNo", invoiceNo); 

                conn.Open();
                result = sqlCmd.ExecuteNonQuery();
           // }
           // catch (Exception ex)
           // {
             //   errMsg = ex.Message;
           // }
           // finally
            //{
                conn.Close();
          //  }

            return result;
        }

        public SqlDataReader getCreditTerm()
        {
            SqlDataReader reader;
            SqlConnection conn = dbConn.GetConnection();

            string strCommandText = "SELECT creditTermID, creditDiscount, creditDiscPeriod, creditPeriod FROM CreditTerms";

            SqlCommand cmd = new SqlCommand(strCommandText, conn);
            conn.Open();
            // CommandBehavior.CloseConnection will automatically close connection
           

            //try
           // {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                 
           // }
           // catch (Exception ex)
           // {
            //    errMsg = ex.Message;
            //}
           // finally
           // {
               // conn.Close();
           // }

            return reader;
        }


        public DataSet getCreditTermByDays(string selectedDays)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet creditTerms;

            SqlConnection conn = dbConn.GetConnection();
            creditTerms = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine(" SELECT  creditDiscount, creditDiscPeriod ");
            sql.AppendLine("FROM CreditTerms ");
            sql.AppendLine("WHERE creditPeriod =@creditPeriod ");


            try
            {
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@creditPeriod", selectedDays);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                creditTerms.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return creditTerms;

        }

        public DataSet getAllInvoiceSearch(string fDate, string toDate, string status, string sortBy, string companyName)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet invoiceData;

            SqlConnection conn = dbConn.GetConnection();
            invoiceData = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT iv.invoiceNo, companyName, po.orderPersonnel,  creditPeriod, invoiceStatus, grandTotal, CONVERT(varchar, invoiceDate, 103) invoiceDate ");
            sql.AppendLine("FROM Invoice iv ");
            sql.AppendLine("INNER JOIN PurOrder po on po.purOrderNo = iv.purOrderNo");
            sql.AppendLine("INNER JOIN CustomerCompany cc on cc.orderPersonnelId = po.orderPersonnelId");
            //sql.AppendLine("Group by iv.invoiceNo, companyName, po.orderPersonnel,  creditPeriod, invoiceStatus, grandTotal, invoiceDate");

            //get all default
            if (fDate.Equals("") && toDate.Equals("") && status.Equals("All") && companyName.Equals(""))
            {
                sql.AppendLine("ORDER BY invoiceDate " + sortBy);
            }


            else if (fDate.Equals("") && toDate.Equals("") && !(status.Equals("All")) && companyName.Equals(""))
            {
                sql.AppendLine("WHERE invoiceStatus = @statusSort  ORDER BY invoiceDate " + sortBy);
            }
            //get all sort
            else if (fDate.Equals("") && toDate.Equals("") && !(status.Equals("All")) && !(companyName.Equals("")))
            {
                sql.AppendLine("WHERE invoiceStatus = @statusSort  ORDER BY invoiceDate " + sortBy);
            }
            //get by date
            else if (!(fDate.Equals("")) && !(toDate.Equals("")) && status.Equals("All") && companyName.Equals(""))
            {
                // sql.AppendLine("WHERE cc.companyName = @companyName  ORDER BY statusSort=invoiceDate " + sortBy);
                sql.AppendLine("WHERE  invoiceDate between @fDate and @toDate order by invoiceDate " + sortBy);

            }

            else if (!(fDate.Equals("")) && !(toDate.Equals("")) && status.Equals("All") && !(companyName.Equals("")))
            {
                // sql.AppendLine("WHERE cc.companyName = @companyName  ORDER BY statusSort=invoiceDate " + sortBy);
                sql.AppendLine("WHERE cc.companyName = @companyName and invoiceDate between @fDate and  @toDate order by invoiceDate " + sortBy);

            }

            else if (!(fDate.Equals("")) && !(toDate.Equals("")) && !status.Equals("All") && (companyName.Equals("")))
            {
                // sql.AppendLine("WHERE cc.companyName = @companyName  ORDER BY statusSort=invoiceDate " + sortBy);
                sql.AppendLine("WHERE invoiceStatus = @statusSort and invoiceDate between @fDate and  @toDate order by invoiceDate " + sortBy);

            }

            else if (!(fDate.Equals("")) && !(toDate.Equals("")) && !(status.Equals("All")) && !(companyName.Equals("")))
            {
                // sql.AppendLine("WHERE cc.companyName = @companyName  ORDER BY statusSort=invoiceDate " + sortBy);
                sql.AppendLine("WHERE cc.companyName = @companyName and invoiceStatus = @statusSort  and invoiceDate between @fDate and  @toDate order by invoiceDate " + sortBy);

            }

            try
            {
                da = new SqlDataAdapter(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("@companyName", companyName);
                da.SelectCommand.Parameters.AddWithValue("@statusSort", status);
                da.SelectCommand.Parameters.AddWithValue("@fDate", fDate);
                da.SelectCommand.Parameters.AddWithValue("@toDate", toDate);
                da.Fill(invoiceData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return invoiceData;
        }


        public DataSet getAllInvoice()
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet invoiceData;

            SqlConnection conn = dbConn.GetConnection();
            invoiceData = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT iv.invoiceNo, companyName, po.orderPersonnel,  creditPeriod, invoiceStatus, grandTotal, CONVERT(varchar, invoiceDate, 103) invoiceDate, po.orderPersonnel, addresss, deliveryNo, creditTerm, po.totalPrice, gstAmt ");
            sql.AppendLine("FROM Invoice iv ");
            sql.AppendLine("INNER JOIN PurOrder po on po.purOrderNo = iv.purOrderNo");
            sql.AppendLine("INNER JOIN CustomerCompany cc on cc.orderPersonnelId = po.orderPersonnelId");
            
            try
            {
                da = new SqlDataAdapter(sql.ToString(), conn);
                da.Fill(invoiceData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return invoiceData;
        }

        public DataSet getInvoice(string invoiceNo)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet invoiceData;

            SqlConnection conn = dbConn.GetConnection();
            invoiceData = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT iv.invoiceNo, invoiceStatus, gstAmt, grandTotal, creditPeriod, creditTerm, deliveryNo, invoiceDate, companyName, po.orderPersonnel, addresss, ");
            sql.AppendLine("(SELECT  sum(paidAmt) FROM InvoicePayment WHERE invoiceNo = @invoiceNo group by invoiceNo ) amtPaid , grandTotal- (SELECT  sum(paidAmt) FROM InvoicePayment WHERE invoiceNo = @invoiceNo group by invoiceNo ) outstandingAmt, creditPeriod - DATEDIFF(day, invoiceDate, GETDATE()) noOfDayLeft, ");
            sql.AppendLine("(SELECT creditDiscPeriod FROM InvoiceCreditDiscRecord WHERE invoiceNo = @invoiceNo) - DATEDIFF(day, invoiceDate, GETDATE()) noOfDayLeftDisc "); 
            sql.AppendLine("FROM Invoice iv ");
            sql.AppendLine("INNER JOIN PurOrder po on po.purOrderNo = iv.purOrderNo");
            sql.AppendLine("INNER JOIN CustomerCompany cc on cc.orderPersonnelId = po.orderPersonnelId");
            sql.AppendLine("WHERE iv.invoiceNo = @invoiceNo");
            //sql.AppendLine("GROUP BY iv.invoiceNo, companyName, po.orderPersonnel, creditDiscount, creditDiscPeriod, creditPeriod, creditDiscAmt, invoiceStatus, grandTotal, deliveryNo, iv.purOrderNo, invoiceDate");


            try
            {
                da = new SqlDataAdapter(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("@invoiceNo", invoiceNo);
                da.Fill(invoiceData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return invoiceData;
        }

        public DataSet getPoIdFromIvById(string ivNo)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet deliveryDetails;

            SqlConnection conn = dbConn.GetConnection();
            deliveryDetails = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine(" SELECT iv.purOrderNo  ");
            sql.AppendLine("FROM Invoice iv  ");
            sql.AppendLine("INNER JOIN PurOrder po ON iv.purOrderNo = po.purOrderNo ");
            sql.AppendLine("WHERE iv.invoiceNo =@invoiceNo ");

            try
            {
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@invoiceNo", ivNo);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                deliveryDetails.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return deliveryDetails;

        }
    }
}