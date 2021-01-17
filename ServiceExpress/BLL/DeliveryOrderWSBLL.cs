using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ServiceExpress.DAL;
namespace ServiceExpress.BLL
{
    public class DeliveryOrderWSBLL
    {
        public int UpdateDeliveryOrder(string deliveryNo, string deliveryStatus)
        {
            DeliveryOrderWSDAL deliveryOrderDal = new DeliveryOrderWSDAL();
            return deliveryOrderDal.UpdateDeliveryOrder(deliveryNo, deliveryStatus);

        }

        public DataSet getAllDelivery()
        {

            DeliveryOrderWSDAL deliveryOrderDal = new DeliveryOrderWSDAL();
            return deliveryOrderDal.getAllDelivery();

 
       }


        public DataSet getPurchaseOrderForDo(string purOrderNo)
        {
            DeliveryOrderWSDAL deliveryOrderDal = new DeliveryOrderWSDAL();
            return deliveryOrderDal.getPurchaseOrderForDo(purOrderNo);
        }

        public DataSet getAllPurchaseProducts(string purOrderNo)
        {

            DeliveryOrderWSDAL deliveryOrderDal = new DeliveryOrderWSDAL();
            return deliveryOrderDal.getAllPurchaseProducts(purOrderNo);

        }
        public int InsertDelivery(String deliveryNo, String deliveryStatus, DateTime deliveryDate, String purOrderNo, String deliverySuppName)
        {

            DeliveryOrderWSDAL deliveryOrderDal = new DeliveryOrderWSDAL();
            return deliveryOrderDal.InsertDelivery(deliveryNo, deliveryStatus, deliveryDate, purOrderNo, deliverySuppName);

        }




    }
}