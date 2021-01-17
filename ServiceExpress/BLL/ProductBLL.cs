using ServiceExpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceExpress.BLL
{
    public class ProductBLL
    {
        public ProductBLL getPDetail(string prodID)
        {
            ProductBLL pDetail = new ProductBLL();

            return pDetail.getPDetail(prodID);
        }

        public List<ProductDAL> getAllProduct()
        {
            List<ProductDAL> allProduct = new List<ProductDAL>();
            ProductDAL product = new ProductDAL();

            allProduct = product.getProductAll();

            return allProduct;
        }

        public List<ProductDAL> getAllProductSearch(string tbSearch)
        {
            List<ProductDAL> allProduct = new List<ProductDAL>();
            ProductDAL product = new ProductDAL();

            allProduct = product.getAllProductSearch(tbSearch);

            return allProduct;
        }

        public string ProductInsert(string _prodID, string _prodName, string _image, double _price, string _prodDescription, string _prodCategory)
        {
            string msg = null;
            int result = 0;


            ProductDAL p1 = new ProductDAL(_prodID, _prodName, _image, _price, _prodDescription, _prodCategory);

            result = p1.ProductInsert();
            if (result == 1)
            {
                msg = "Product has been insert successfully";
            }
            else
            {
                msg = "Error! Please try again.";
            }

            return msg;
        }
        public string ProductUpdate(string _prodID, string _prodName, string _image, double _price, string _prodDescription, string _prodCategory)
        {
            string msg = null;
            int result = 0;
            ProductDAL update1 = new ProductDAL();

            result = update1.ProductUpdate(_prodID, _prodName, _image, _price, _prodDescription, _prodCategory);
            if (result == 1)
            {
                msg = "Product has been updated successfully";
            }
            else
            {
                msg = "Error! Please try again.";
            }
            return msg;
        }

        public int ProductDelete(string prodID)
        {
            string msg = null;
            int result = 0;
            ProductDAL PDAL = new ProductDAL();
            result = PDAL.ProductDelete(prodID);
            if (result == 1)
            {
                msg = "Product has been deleted successfully";
            }
            else
            {
                msg = "Error! Please try again.";
            }
            return result;
        }
    }
}