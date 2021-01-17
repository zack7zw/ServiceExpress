using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ServiceExpress.DAL;
using System.Data.SqlClient;
namespace ServiceExpress.BLL
{
    public class InvoiceWSBLL
    {

        public DataSet getInvoiceWS(string InvoiceID)
        {
            InvoiceWSDAL InvoiceDal = new InvoiceWSDAL();
            return InvoiceDal.getInvoiceWS(InvoiceID);

        }
        public DataSet getAllInvoiceByCNWS(string fDate, string toDate, string status, string sortBy, string companyName)
        {
            InvoiceWSDAL InvoiceDal = new InvoiceWSDAL();
            return InvoiceDal.getAllInvoiceByCNWS(fDate, toDate, status, sortBy, companyName);
        }
        public DataSet getAllInvoiceWS()
        {
            InvoiceWSDAL InvoiceDal = new InvoiceWSDAL();
            return InvoiceDal.getAllInvoiceWS();

        }
        public int UpdateInvoice(string invoiceNo, string totalPrice, string depositAmt, string outstandingAmt, string invoiceStatus,
            string paymentDate, string company, string contactPersonnel, string creditPeriod, string deliveryNo, string purOrderNo, string nric, string dateCreated)    
       {
            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.UpdateInvoice(invoiceNo, Double.Parse(totalPrice), Double.Parse(depositAmt), Double.Parse(outstandingAmt), invoiceStatus, paymentDate, company, contactPersonnel, Int32.Parse(creditPeriod), Int32.Parse(deliveryNo), purOrderNo, nric, dateCreated);

        }

        public int InvoiceStatusUpdate(string invoiceStatus, string invoiceNo)    
       {
            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.InvoiceStatusUpdate(invoiceStatus, invoiceNo);

        }

        public int InsertInvoicePayment(string paymentNo, string paidAmt, string paymentDate, string transactionNo, string paymentPersonnel, string paymentType, string invoiceNo)
        {

            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();



            return invoiceDal.InsertInvoicePayment(paymentNo, double.Parse(paidAmt), paymentDate, transactionNo, paymentPersonnel, paymentType, invoiceNo);
        }

        public DataSet getCreditDiscInfo(string ivNo)
        {

            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.getCreditDiscInfo(ivNo);

        }

        public int InvoiceCreditDiscRecordUpdate(string creditStatus, string invoiceNo)
        {
            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.InvoiceCreditDiscRecordUpdate(creditStatus, invoiceNo);
        }


        //------- local

        public DataSet deliveryOrderForInvoice()//String deliveryStatus)
        {

            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.deliveryOrderForInvoice();

        }

        public DataSet getPoIdFromDoById(string doNo)//String deliveryStatus)
        {

            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.getPoIdFromDoById(doNo);

        }


        public DataSet getInvoiceDetailFromDo(string doNo)//String deliveryStatus)
        {

            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.getInvoiceDetailFromDo(doNo);

        }

        public DataSet getInvoiceDetailsFromPO(string poID)//String deliveryStatus)
        {

            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.getInvoiceDetailsFromPO(poID);

        }

        public DataSet getPaymentRecord(string ivNo)//String deliveryStatus)
        {

            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.getPaymentRecord(ivNo);

        }
        

        public int InsertInvoice(string invoiceNo, string gstAmt, string grandTotal, string creditPeriod, string creditTerm, string deliveryNo, string purOrderNo, string invoicePersonnel)
        {
            string invoiceStatus = "Sent";

          

            string invoiceDate = DateTime.Now.ToString("dd/MM/yyyy");
            // invoice status can be sent, outstanding or deposit, paid

            // creditdiscount(terms)
            // current discount is 0%
           
            

            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.InsertInvoice(invoiceNo, invoiceStatus, double.Parse(gstAmt), double.Parse(grandTotal), int.Parse(creditPeriod), creditTerm, deliveryNo, purOrderNo, invoiceDate, invoicePersonnel);

        }

        public int InsertInvoiceCreditDiscRecord(string creditRecordID, string creditDiscount, string creditDiscPeriod, string creditDiscAmt, string creditStatus, string invoiceNo)
        {
            

            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.InsertInvoiceCreditDiscRecord(creditRecordID, double.Parse(creditDiscount) / 100, int.Parse(creditDiscPeriod), double.Parse(creditDiscAmt), creditStatus, invoiceNo);

        }

        public SqlDataReader getCreditTerm()
        {
            InvoiceWSDAL InvoiceDal = new InvoiceWSDAL();
            return InvoiceDal.getCreditTerm();

        }

        public DataSet getCreditTermByDays(string selectedDays)//String deliveryStatus)
        {

            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.getCreditTermByDays(selectedDays);

        }

        public DataSet getInvoice(string InvoiceID)
        {
            InvoiceWSDAL InvoiceDal = new InvoiceWSDAL();
            return InvoiceDal.getInvoice(InvoiceID);

        }
        public DataSet getAllInvoice(string fDate, string toDate, string status, string sortBy, string companyName)
        {
            InvoiceWSDAL InvoiceDal = new InvoiceWSDAL();
            return InvoiceDal.getAllInvoiceSearch(fDate, toDate, status, sortBy, companyName);

        }

        public DataSet getPoIdFromIvById(string ivNo)//String deliveryStatus)
        {

            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.getPoIdFromIvById(ivNo);

        }

        public int UpdateInvoiceStatusToBadDebt()
        {
            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.UpdateInvoiceStatusToBadDebt();
        }

        public int UpdateInvoiceStatusToDue()
        {
            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.UpdateInvoiceStatusToDue();
        }

    }
}