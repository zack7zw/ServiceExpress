using ServiceExpress.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServiceExpress.BLL
{
    public class ProductWSBLL
    {
        public DataSet getProduct(string prodID)
        {
            ProductWSDAL prodDal = new ProductWSDAL();
            return prodDal.getProduct(prodID);

        }
        public DataSet getAllProduct()
        {
            ProductWSDAL prodDal = new ProductWSDAL();
            return prodDal.getAllProduct();

        }
    }
}