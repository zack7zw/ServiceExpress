using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ServiceExpress.DAL
{
    public class ProductWSDAL
    {
        private String errMsg;
        DalDbConn dbConn = new DalDbConn();

        public DataSet getProduct(string prodID)
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet booksData;

            SqlConnection conn = dbConn.GetConnection();
            booksData = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT prodID, prodName, image, price, prodDescription, totalNumRoom, totalFlightSeat, prodCategory");
            sql.AppendLine("FROM Product");
            sql.AppendLine("where prodID=@prodID");

            try
            {
                da = new SqlDataAdapter(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("@prodID", prodID);
                da.Fill(booksData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return booksData;
        }
        public DataSet getAllProduct()
        {
            StringBuilder sql;
            SqlDataAdapter da;
            DataSet booksData;

            SqlConnection conn = dbConn.GetConnection();
            booksData = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT prodID, prodName, image, price, prodDescription, totalNumRoom, totalFlightSeat, prodCategory");
            sql.AppendLine("FROM Product");


            try
            {
                da = new SqlDataAdapter(sql.ToString(), conn);

                da.Fill(booksData);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return booksData;
        }
        public DataSet getAllProductSearch(string Button3)
        {


            string queryStr = "SELECT * FROM Product WHERE prodName like " + "'" + Button3 + "%'" + " Order By prodCategory";
            //edit comment 2 desc
            SqlConnection conn = dbConn.GetConnection();
            //      SqlCommand cmd = new SqlCommand(queryStr, conn);
            //   cmd.Parameters.AddWithValue("@prodCategory", Button3 + "%");

            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(queryStr, conn);
            DataSet booksData = new DataSet();
            da.Fill(booksData);


            conn.Close();


            return booksData;
        }
    }
}