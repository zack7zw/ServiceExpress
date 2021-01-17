using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
namespace ServiceExpress.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceExpressWebService" in both code and config file together.
    [ServiceContract]
    public interface IServiceExpressWebService
    {
        [OperationContract]
        DataSet getProduct(string productID);
        [OperationContract]
        DataSet getAllProduct();
        [OperationContract]
        DataSet getInvoiceWS(string InvoiceNo);
        [OperationContract]
        DataSet getAllInvoiceByCNWS(string fDate, string toDate, string status, string sortBy, string companyName);
        [OperationContract]
        int InsertInvoicePayment(string paymentNo, string paidAmt, string paymentDate, string transactionNo, string paymentPersonnel, string paymentType, string invoiceNo);
        [OperationContract]
        DataSet getPoIdFromIvById(string ivNo);
        [OperationContract]
        DataSet getInvoiceDetailsFromPO(string poNo);

        [OperationContract]
        DataSet getCreditDiscInfo(string invoiceNo);

        [OperationContract]
        int UpdateInvoice(string invoiceNo, string totalPrice, string depositAmt, string outstandingAmt, string invoiceStatus,
            string paymentDate, string company, string contactPersonnel, string creditPeriod, string deliveryNo, string purOrderNo, string nric, string dateCreated);

        [OperationContract]
        int InvoiceStatusUpdate(string invoiceStatus, string invoiceNo);
        [OperationContract]
        int InvoiceCreditDiscRecordUpdate(string creditStatus, string invoiceNo);

        [OperationContract]
        int UpdateDeliveryOrder(string deliveryNo, string deliveryStatus);
    
        [OperationContract]
        DataSet getAllDelivery();

        [OperationContract]
        int InserTPOWS(string purOrderNo, string orderPersonnel, string orderCompany, double totalPrice, string statusPO, string orderPersonnelId);
        [OperationContract]
        int InsertPOItemsWS(string ProdID, string Quantity, string Price, string purOrderNo);
        [OperationContract]
        int InserTPOCustomerWS(string orderPersonnel, string orderCompany, string orderPersonnelId, string addresss, string contact);
        [OperationContract]
        int updateStatus(string statusPO, string purOrderNo);
        [OperationContract]
        DataSet getPurchaseOrderForDo(string purOrderNo);
        [OperationContract]
        int updatePOquantityWS(int quantity, string purOrderNo, string prodID);
        [OperationContract]
        int updateReason(string reason, string purOrderNo);

        [OperationContract]
        DataSet getDeliveryByStatus(string deliveryStatus);
        [OperationContract]
        DataSet getAllProductSearch(string Button3);
        [OperationContract]
        int InsertPayment(string paymentID, string paymentType, string cardHolderName, string expriredDate, string cardNumber, string ccvCode, string nric);
        [OperationContract]
        int insertMember(string name, string email, int contact, string address, DateTime dob, string gender, string nric, string passportNo, string PassportExpiry);
        [OperationContract]
        int insertFlightDetail(string orderDetailfightID, string arrivaldate, string nric, string departuredate, string arrival, string departure, string mealType);
        [OperationContract]
        int InsertCustOrder(string orderID, string nric);
        [OperationContract]
        int InsertCustOrderItem(string orderDetailID, string prodID, double price, int quantity, string orderDetailFlightID, string orderID);
     
    }
}
