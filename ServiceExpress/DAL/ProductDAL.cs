using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiceExpress.DAL
{
    public class ProductDAL
    {
        private string _connStr = ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;
        private string _prodID = "";
        private string _prodName = "";
        private string _image = "";
        private double _price = 0;
        private string _prodDescription = "";
        private string _prodCategory = "";


        // Default constructor
        public ProductDAL()
        {
        }

        // Constructor that take in all data required to build a Product object
        public ProductDAL(string prodID, string prodName, string image, double price, string prodDescription, string prodCategory)
        {

            _prodID = prodID;
            _prodName = prodName;
            _image = image;
            _price = price;
            _prodDescription = prodDescription;
            _prodCategory = prodCategory;
        }

        // Constructor that take in all except ID
        public ProductDAL(string prodName, string image, double price, string prodDescription, string prodCategory)

            : this("", prodName, image, price, prodDescription, prodCategory)
        {
        }

        // Constructor that take in only Product ID. The other attributes will be set to 0 or empty.
        public ProductDAL(string prodID)
            : this(prodID, "", "", 0, "", "")
        {
        }

        // Get/Set the attributes of the Product object.
        // Note the attribute name (e.g. Product_ID) is same as the actual database field name.
        // This is for ease of referencing.
        public string prodID
        {
            get { return _prodID; }
            set { _prodID = value; }
        }

        public string prodName
        {
            get { return _prodName; }
            set { _prodName = value; }
        }
        public string image
        {
            get { return _image; }
            set { _image = value; }
        }
        public double price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string prodDescription
        {
            get { return _prodDescription; }
            set { _prodDescription = value; }
        }

        public string prodCategory
        {
            get { return _prodCategory; }
            set { _prodCategory = value; }
        }



        /// <summary>
        /// This method retrieve details of gallery from database table Gallery.
        /// It return a Product object.
        /// </summary>
        /// <param name="galleryID">Product ID</param>
        /// <returns></returns>
        public ProductDAL getPDetail(string prodID)
        {
            ProductDAL pDetail = null;


            double price;
            string prodName, image, prodDescription, prodCategory;

            string queryStr = "SELECT * FROM Product WHERE prodID = @prodID";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@prodID", prodID);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {

                prodName = dr["prodName"].ToString();
                image = dr["image"].ToString();
                price = double.Parse(dr["price"].ToString());
                prodDescription = dr["prodDescription"].ToString();

                prodCategory = dr["prodCategory"].ToString();


                pDetail = new ProductDAL(prodID, prodName, image, price, prodDescription, prodCategory);
            }
            else
            {
                pDetail = null;
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return pDetail;
        }

        /// <summary>
        /// This method retrieves all gallery from Product database table.
        /// The Product object is stored in a list and return to calling program.
        /// </summary>
        /// <returns></returns>
        public List<ProductDAL> getProductAll()
        {
            List<ProductDAL> pAll = new List<ProductDAL>();


            double price;
            string prodID, prodName, image, prodDescription, prodCategory;

            string queryStr = "SELECT * FROM Product Order By prodName";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                prodID = dr["prodID"].ToString();
                prodName = dr["prodName"].ToString();
                image = dr["image"].ToString();
                price = double.Parse(dr["price"].ToString());
                prodDescription = dr["prodDescription"].ToString();

                prodCategory = dr["prodCategory"].ToString();


                pAll.Add(new ProductDAL(prodID, prodName, image, price, prodDescription, prodCategory));

            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return pAll;
        }

        public List<ProductDAL> getAllProductSearch(string tbSearch)
        {
            List<ProductDAL> pAll = new List<ProductDAL>();


            double price;
            string prodID, prodName, image, prodDescription, prodCategory;

            string queryStr = "SELECT * FROM Product WHERE prodName like " + "'" + tbSearch + "%'" + " Order By prodName";
            //edit comment 2 desc
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@prodCategory", tbSearch + "%");

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                prodID = dr["prodID"].ToString();
                prodName = dr["prodName"].ToString();
                image = dr["image"].ToString();
                price = double.Parse(dr["price"].ToString());
                prodDescription = dr["prodDescription"].ToString();

                prodCategory = dr["prodCategory"].ToString();

                pAll.Add(new ProductDAL(prodID, prodName, image, price, prodDescription, prodCategory));
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return pAll;
        }



        public int ProductInsert()
        {

            int result = 0;
            string queryStr = "INSERT INTO Product(prodID, prodName, image, price, prodDescription, prodCategory)" + "values ( @prodID, @prodName, @image, @price, @prodDescription, @prodCategory)";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@prodID", prodID);
            cmd.Parameters.AddWithValue("@prodName", this.prodName);
            cmd.Parameters.AddWithValue("@image", this.image);
            cmd.Parameters.AddWithValue("@price", this.price);
            cmd.Parameters.AddWithValue("@prodDescription", this.prodDescription);

            cmd.Parameters.AddWithValue("@prodCategory", this.prodCategory);


            conn.Open();

            result += cmd.ExecuteNonQuery();

            conn.Close();

            return result;

        }//end Insert 

        public int ProductUpdate(string _prodID, string _prodName, string _image, double _price, string _prodDescription, string _prodCategory)
        {

            int result = 0;
            string queryStr = "UPDATE Product SET prodName=@prodName, image=@image, price=@price, prodDescription=@prodDescription,prodCategory=@prodCategory WHERE prodID=@prodID";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@prodID", _prodID);
            cmd.Parameters.AddWithValue("@prodName", _prodName);
            cmd.Parameters.AddWithValue("@image", _image);
            cmd.Parameters.AddWithValue("@price", _price);
            cmd.Parameters.AddWithValue("@prodDescription", _prodDescription);
            cmd.Parameters.AddWithValue("@prodCategory", _prodCategory);


            conn.Open();

            result += cmd.ExecuteNonQuery();

            conn.Close();

            return result;

        }




        public int ProductDelete(string ID)
        {
            string queryStr = "DELETE FROM Product WHERE prodID=@ID";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            conn.Open();
            int nofRow = 0;
            nofRow = cmd.ExecuteNonQuery();

            conn.Close();
            return nofRow;
        }//end Delete  
    }
}