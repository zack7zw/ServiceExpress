using ServiceExpress.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServiceExpress.BLL
{
    public class PurchaseOrderWSBLL
    {
     public int InserTPOWS(string purOrderNo, string orderPersonnel, string orderCompany, double totalPrice, string statusPO, string orderPersonnelId)
        {
            PurchaseOrderWS purOrderWS = new PurchaseOrderWS();
            return purOrderWS.InserTPOWS( purOrderNo,  orderPersonnel,  orderCompany,  totalPrice,  statusPO,  orderPersonnelId);
        }
          public int InserTPOCustomerWS( string orderPersonnel, string orderCompany,string orderPersonnelId, string addresss, string contact)
     {
         PurchaseOrderWS purOrderWS = new PurchaseOrderWS();
         return purOrderWS.InserTPOCustomerWS(orderPersonnel, orderCompany, orderPersonnelId, addresss, contact);
   
     }
        public int InsertPOItemsWS(string ProdID, string Quantity, string Price, string purOrderNo)
        {
            PurchaseOrderWS purOrderWS = new PurchaseOrderWS();
            return purOrderWS.InsertPOItemsWS( ProdID,  Quantity,  Price,  purOrderNo);
        }
       public  DataTable poall()
        {
            PurchaseOrderWS purOrderWS = new PurchaseOrderWS();
            return purOrderWS.poall();
        }
        public DataTable searchPOall(string status, string startDate,string endDate, string Users)
       {
           PurchaseOrderWS purOrderWS = new PurchaseOrderWS();
           return purOrderWS.searchPOall(status, startDate, endDate, Users);
       }

        public int updateStatusWS(string statusPO, string purOrderNo)
        {
            PurchaseOrderWS flyAsiaservice = new PurchaseOrderWS();
            return flyAsiaservice.updateStatusWS(statusPO, purOrderNo);
        }
        public int updateStatus(string statusPO, string purOrderNo)
        {
            PurchaseOrderWS flyAsiaservice = new PurchaseOrderWS();
            return flyAsiaservice.updateStatus(statusPO, purOrderNo);
        }
          public int updatePOquantityWS(int quantity, string purOrderNo, string prodID)
        {
            PurchaseOrderWS flyAsiaservice = new PurchaseOrderWS();
            return flyAsiaservice.updatePOquantityWS( quantity,  purOrderNo,  prodID);
        }
    }
}