using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiceExpress.DAL
{
    public class Flight
    {
        DalDbConn dbConn = new DalDbConn();
        public int InsertPayment(string paymentID, string paymentType, string cardHolderName, string expriredDate, string cardNumber, string ccvCode, string nric)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();


                DateTime orderDate = DateTime.Now;



                string strCommandText = "INSERT CustPayment(paymentID, paymentType, cardHolderName,expriredDate, cardNumber, ccvCode,nric)";
                strCommandText += " Values (@paymentID, @paymentType, @cardHolderName, @expriredDate, @cardNumber, @ccvCode, @nric )";

                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
                cmd.Parameters.AddWithValue("@paymentID", paymentID);
                cmd.Parameters.AddWithValue("@paymentType", paymentType);

                cmd.Parameters.AddWithValue("@cardHolderName", cardHolderName);
                cmd.Parameters.AddWithValue("@expriredDate", expriredDate);
                cmd.Parameters.AddWithValue("@cardNumber", cardNumber);

                cmd.Parameters.AddWithValue("@ccvCode", ccvCode);
                cmd.Parameters.AddWithValue("@nric", nric);

                myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            return result;

        }
        public int insertMember(string name, string email, int contact, string address, DateTime dob, string gender, string nric, string passportNo, string PassportExpiry)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();


                DateTime orderDate = DateTime.Now;



                string strCommandText = "INSERT Customer(name, email, contact,address, dob, gender,nric, passportNo, PassportExpireDate)";
                strCommandText += " Values (@name, @email, @contact, @address, @dob, @gender, @nric,@passportNo,@PassportExpiry )";

                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);

                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@dob", dob);

                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@nric", nric);
                cmd.Parameters.AddWithValue("@passportNo", passportNo);
                cmd.Parameters.AddWithValue("@PassportExpiry", PassportExpiry);
                myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            return result;
        }
        public int insertFlightDetail(string orderDetailfightID, string arrivaldate, string nric, string departuredate, string arrival, string departure, string mealType)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();


                DateTime orderDate = DateTime.Now;



                string strCommandText = "INSERT OrderDetailFlight(orderDetailFlightID, arrivalDate, nric,departureDate, arrival, departure,mealType)";
                strCommandText += " Values (@orderDetailfightID, @arrivaldate, @nric, @departuredate, @arrival, @departure, @mealType)";

                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
                cmd.Parameters.AddWithValue("@orderDetailfightID", orderDetailfightID);
                cmd.Parameters.AddWithValue("@arrivaldate", arrivaldate);

                cmd.Parameters.AddWithValue("@nric", nric);
                cmd.Parameters.AddWithValue("@departuredate", departuredate);
                cmd.Parameters.AddWithValue("@arrival", arrival);

                cmd.Parameters.AddWithValue("@departure", departure);
                cmd.Parameters.AddWithValue("@mealType", mealType);

                myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            return result;
        }
        public int InsertCustOrder(string orderID, string nric)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();


                DateTime orderDate = DateTime.Now;



                string strCommandText = "INSERT Orders(orderID, nric, orderDate)";
                strCommandText += " Values (@orderID, @nric, @orderDate)";

                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
                cmd.Parameters.AddWithValue("@orderID", orderID);
                cmd.Parameters.AddWithValue("@nric", nric);

                cmd.Parameters.AddWithValue("@orderDate", orderDate);


                myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            return result;
        }
        public int InsertCustOrderItem(string orderDetailID, string prodID, double price, int quantity, string orderDetailFlightID, string orderID)
        {
            int result = 0;
            try
            {
                SqlConnection myConnect = dbConn.GetConnection();



                string strCommandText = "INSERT orderDetail(orderDetailID, prodID, price, quantity, orderDetailFlightID, orderID)";
                strCommandText += " Values (@orderDetailID, @prodID, @price, @quatity,@orderDetailFlightID, @orderID)";

                SqlCommand cmd = new SqlCommand(strCommandText, myConnect);



                cmd.Parameters.AddWithValue("@orderDetailID", orderDetailID);
                cmd.Parameters.AddWithValue("@prodID", prodID);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@quatity", quantity);
                cmd.Parameters.AddWithValue("@orderDetailFlightID", orderDetailFlightID);
                cmd.Parameters.AddWithValue("@orderID", orderID);

                myConnect.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            return result;
        }

    }
}