using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using ServiceExpress.BLL;
using ServiceExpress.DAL;

namespace ServiceExpress.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceExpressWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceExpressWebService.svc or ServiceExpressWebService.svc.cs at the Solution Explorer and start debugging.
    public class ServiceExpressWebService : IServiceExpressWebService
    {
        public DataSet getProduct(string productID)
        {
            //retrieve from database get coonection 
            ProductWSBLL prodwsBll = new ProductWSBLL();
            return prodwsBll.getProduct(productID);

        }
        public DataSet getAllProduct()
        {
            //retrieve from database get coonection 
            ProductWSBLL prodwsBll = new ProductWSBLL();
            return prodwsBll.getAllProduct();

        }
       
        public DataSet getInvoiceWS(string InvoiceNo)
        {
            //retrieve from database get coonection 
            InvoiceWSBLL invoicewsBll = new InvoiceWSBLL();
            return invoicewsBll.getInvoiceWS(InvoiceNo);

        }
        public DataSet getAllInvoiceByCNWS(string fDate, string toDate, string status, string sortBy, string companyName)
        {
            InvoiceWSBLL invoicewsBll = new InvoiceWSBLL();
            return invoicewsBll.getAllInvoiceByCNWS(fDate, toDate, status, sortBy, companyName);
        }
        public DataSet getAllInvoiceWS()
        {
            //retrieve from database get coonection 
            InvoiceWSBLL invoicewsBll = new InvoiceWSBLL();
            return invoicewsBll.getAllInvoiceWS();

        }
        public DataSet getPoIdFromIvById(string ivNo)
        {
            InvoiceWSBLL invoicewsBll = new InvoiceWSBLL();
            return invoicewsBll.getPoIdFromIvById(ivNo);
        }

        public DataSet getInvoiceDetailsFromPO(string poNo)
        {
            InvoiceWSBLL invoicewsBll = new InvoiceWSBLL();
            return invoicewsBll.getInvoiceDetailsFromPO(poNo);
        }
        public DataSet getCreditDiscInfo(string invoiceNo)
        {
            InvoiceWSDAL invoicewsBll = new InvoiceWSDAL();
            return invoicewsBll.getCreditDiscInfo(invoiceNo);
        }
        public int UpdateInvoice(string invoiceNo, string totalPrice, string depositAmt, string outstandingAmt, string invoiceStatus,
            string paymentDate, string company, string contactPersonnel, string creditPeriod, string deliveryNo, string purOrderNo, string nric, string dateCreated)
        {
            //retrieve from database get coonection 
            InvoiceWSBLL invoicewsBll = new InvoiceWSBLL();
            return invoicewsBll.UpdateInvoice(invoiceNo, totalPrice, depositAmt, outstandingAmt, invoiceStatus, paymentDate, company, contactPersonnel, creditPeriod, deliveryNo, purOrderNo, nric, dateCreated);

        }

        public int InsertInvoicePayment(string paymentNo, string paidAmt, string paymentDate, string transactionNo, string paymentPersonnel, string paymentType, string invoiceNo)
        {
            InvoiceWSBLL invoicewsBll = new InvoiceWSBLL();
            return invoicewsBll.InsertInvoicePayment(paymentNo, paidAmt, paymentDate, transactionNo, paymentPersonnel, paymentType, invoiceNo);

        }

        public int InvoiceStatusUpdate(string invoiceStatus, string invoiceNo)
        {
            //retrieve from database get coonection 
            InvoiceWSBLL invoicewsBll = new InvoiceWSBLL();
            return invoicewsBll.InvoiceStatusUpdate(invoiceStatus, invoiceNo);

        }

        public int InvoiceCreditDiscRecordUpdate(string creditStatus, string invoiceNo)
        {
            InvoiceWSDAL invoiceDal = new InvoiceWSDAL();
            return invoiceDal.InvoiceCreditDiscRecordUpdate(creditStatus, invoiceNo);
        }

        public int UpdateDeliveryOrder(string deliveryNo, string deliveryStatus)
        {
            //retrieve from database get coonection 
            DeliveryOrderWSBLL deliveryOrderwsBll = new DeliveryOrderWSBLL();
            return deliveryOrderwsBll.UpdateDeliveryOrder(deliveryNo, deliveryStatus);

        }
        public DataSet getAllDelivery()
        {
            //retrieve from database get coonection 
            DeliveryOrderWSBLL deliveryOrderwsBll = new DeliveryOrderWSBLL();
            return deliveryOrderwsBll.getAllDelivery();


        }
        public int InsertPOItemsWS(string ProdID, string Quantity, string Price, string purOrderNo)
        {
            //retrieve from database get coonection 
            PurchaseOrderWSBLL prodwsBll = new PurchaseOrderWSBLL();
            return prodwsBll.InsertPOItemsWS(ProdID, Quantity, Price, purOrderNo);

        }
        public int updateStatus(string statusPO, string purOrderNo)
        {
            //retrieve from database get coonection 
            PurchaseOrderWSBLL prodwsBll = new PurchaseOrderWSBLL();
            return prodwsBll.updateStatus(statusPO, purOrderNo);

        }
        public int updateReason(string reason, string purOrderNo)
        {
            PurchaseOrderWS deliveryOrderwsBll = new PurchaseOrderWS();
            return deliveryOrderwsBll.updateReason(reason, purOrderNo);
        }

        public int InserTPOWS(string purOrderNo, string orderPersonnel, string orderCompany, double totalPrice, string statusPO, string orderPersonnelId)
        {
            //retrieve from database get coonection 
            PurchaseOrderWSBLL prodwsBll = new PurchaseOrderWSBLL();
            return prodwsBll.InserTPOWS(purOrderNo, orderPersonnel, orderCompany, totalPrice, statusPO, orderPersonnelId);

        }
        public int InserTPOCustomerWS(string orderPersonnel, string orderCompany, string orderPersonnelId, string addresss, string contact)
        {
            PurchaseOrderWSBLL purOrderWS = new PurchaseOrderWSBLL();
            return purOrderWS.InserTPOCustomerWS(orderPersonnel, orderCompany, orderPersonnelId, addresss, contact);

        }
        public DataSet getPurchaseOrderForDo(string purOrderNo)
        {
            DeliveryOrderWSBLL deliveryOrderwsBll = new DeliveryOrderWSBLL();
            return deliveryOrderwsBll.getPurchaseOrderForDo(purOrderNo);
        }
        public int updatePOquantityWS(int quantity, string purOrderNo, string prodID)
        {
            PurchaseOrderWSBLL flyAsiaservice = new PurchaseOrderWSBLL();
            return flyAsiaservice.updatePOquantityWS(quantity, purOrderNo, prodID);
        }
        public DataSet getDeliveryByStatus(string deliveryStatus)
        {
            DeliveryOrderWSDAL deliveryOrderwsBll = new DeliveryOrderWSDAL();
            return deliveryOrderwsBll.getDeliveryByStatus( deliveryStatus);
        }
        public DataSet getAllProductSearch(string Button3)
        {
            ProductWSDAL deliveryOrderwsBll = new ProductWSDAL();
            return deliveryOrderwsBll.getAllProductSearch(Button3);
        }
        public int InsertPayment(string paymentID, string paymentType, string cardHolderName, string expriredDate, string cardNumber, string ccvCode, string nric)
        {
            Flight flig = new Flight();
            return flig.InsertPayment(paymentID, paymentType, cardHolderName, expriredDate, cardNumber, ccvCode, nric);
        }
        public int insertMember(string name, string email, int contact, string address, DateTime dob, string gender, string nric, string passportNo, string PassportExpiry)
        {
            Flight flig = new Flight();
            return flig.insertMember(name, email, contact, address, dob, gender, nric, passportNo, PassportExpiry);
        }
        public int insertFlightDetail(string orderDetailfightID, string arrivaldate, string nric, string departuredate, string arrival, string departure, string mealType)
        {
            Flight flig = new Flight();
            return flig.insertFlightDetail(orderDetailfightID, arrivaldate, nric, departuredate, arrival, departure, mealType);
        }
        public int InsertCustOrder(string orderID, string nric)
        {
            Flight flig = new Flight();
            return flig.InsertCustOrder(orderID, nric);
        }
        public int InsertCustOrderItem(string orderDetailID, string prodID, double price, int quantity, string orderDetailFlightID, string orderID)
        {
            Flight flig = new Flight();
            return flig.InsertCustOrderItem(orderDetailID, prodID, price, quantity, orderDetailFlightID, orderID);

        }
    }
}
