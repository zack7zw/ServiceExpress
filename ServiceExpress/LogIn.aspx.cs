using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceExpress
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public int countStore()
        {
            int revalue = 0;
            if (Session["countTime"] == null)
            {
                Session["countTime"] = revalue;
                revalue = Convert.ToInt32(Session["countTime"].ToString());
            }
            else
            {
                revalue = 1 + Convert.ToInt32(Session["countTime"].ToString());
                Session["countTime"] = revalue;
                revalue = Convert.ToInt32(Session["countTime"].ToString());

            }

            return revalue;
        }
        public int getCount()
        {
            int revalue = 0;
            if (Session["countTime"] == null)
            {
                Session["countTime"] = revalue;
                revalue = Convert.ToInt32(Session["countTime"].ToString());
            }
            else
            {
                revalue = Convert.ToInt32(Session["countTime"].ToString());

            }
            return revalue;
        }
        public int countNum()
        {
            int revalue;
            if (Session["getnum"] == null)
            {

                revalue = Convert.ToInt32(Session["getnum"].ToString());
            }
            else
            {
                revalue = Convert.ToInt32(Session["getnum"].ToString()) - 1;
                Session["getnum"] = revalue;
                revalue = Convert.ToInt32(Session["getnum"].ToString());

            }

            return revalue;
        }
        public int getNumber()
        {
            int revalue = 2;
            if (Session["getnum"] == null)
            {
                Session["getnum"] = revalue.ToString();

                revalue = Convert.ToInt32(Session["getnum"].ToString());
            }
            else
            {
                revalue = Convert.ToInt32(Session["getnum"].ToString());

            }
            return revalue;
        }
        protected void btnLoginclick(object sender, EventArgs e)
        {
            string username = txtLogInUserName.Text;
            string password = GetHashString(txtLogInPassword.Text);

            // retrieve connection info from web.config


            int check;
            // get the login table password and hash it
            string pw = (login(username));
            if (!pw.Equals(""))
            {

                check = getCount();
                // check user number of login
                if ((check < 3))
                {
                    // chech the user password
                    if ((password.Equals(pw)))
                    {   // check the user is it first time log in 
                        string[] checkFirstTimelogIn = FirstTimeLoginCheck(username, password);
                        string status = checkFirstTimelogIn[0];


                        if (!status.Equals(""))
                        {   // check the status of the forgetPW table is it first time login
                            if ((status.Equals("Not Yet LogIn")))
                            {   // whether the user took more than 24 hour to login when changed a password

                                DateTime dateforget = Convert.ToDateTime(checkFirstTimelogIn[1]);
                                DateTime today = DateTime.Now;

                                int daydifferent = today.Subtract(dateforget).Days;

                                if ((daydifferent <= 1))
                                {// the user took within 24 hour to login, and it required the user to change password. 
                                    int lastLoginCheck = updateLoginlastLogin(username);
                                    if (lastLoginCheck > 0)
                                    {   // update the user last login and the user has to change password due to forget password. 

                                        Response.Redirect("CustomerOrder.aspx");
                                    }
                                }
                                else
                                {   // the user took more than 24 hours to login after passwrod is change, therefore need to resend password to his email to relogin again
                                    string[] memberEmail = memberEmails(username);
                                    string email = memberEmail[0];
                                    string name = memberEmail[1];
                                    string newPW = DAL.GeneratePW.Generate(8, 10);
                                    SendMail(email, newPW, name);

                                    string newPWStore = GetHashString(newPW);
                                    //here on button click what will done 

                                    insertforgetPW(newPWStore, username);
                                    updateLogin(newPWStore, username);

                                    lblLogInResult.Text = "Your One Time Password has expired. We will send you another Password ";
                                    ModalPopupExtender2.Show();
                                    ModalPopupExtender2.Show();
                                }
                            }
                            else
                            {   // the user login successfully with forgotten password before
                                int lastLoginCheck = updateLoginlastLogin(username);
                                if (lastLoginCheck > 0)
                                {
                                
                                    Session["UserName"] = username;
                                    string category = logincategory(username);
                                    if (category.Equals("Admin"))
                                    {
                                        Response.Redirect("CustomerOrder.aspx");

                                    }
                                }
                            }

                        }
                        else
                        {   // the user login successfully and did not forget password at all
                            int lastLoginCheck = updateLoginlastLogin(username);
                            if (lastLoginCheck > 0)
                            {
                               
                                Session["UserName"] = username;
                                string category = logincategory(username);
                                if (category.Equals("Admin"))
                                {
                                    Response.Redirect("CustomerOrder.aspx");

                                }
                            }
                        }

                    }
                    else
                    { // if the user give the wrong password
                        int test = countStore();
                        int getNum = getNumber();
                        lblLogInResult.Text = "Wrong Password. You left " + getNum + "time to try to login";
                        countNum();
                        ModalPopupExtender2.Show();
                    }
                }
                else
                {
                    // frozen the user
                    int updateDeactiveLogin = updateLoginDeactive(username);
                    if (updateDeactiveLogin > 0)
                    {
                        Response.Redirect("CustomerOrder.aspx");
                    }
                }
            }
            else
            {
                lblLogInResult.Text = "User does not exist";
                ModalPopupExtender2.Show();
            }


            //close reader  & connection

        }

        public string[] FirstTimeLoginCheck(string userName, string password)
        {
            string[] return2value = new string[2];
            string strConnectionString =
                ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;
            string status = "";
            string dateforget = "";
            //create connection
            SqlConnection myConnect = new SqlConnection(strConnectionString);

            //create command
            string strCommandText = "SELECT * from ForgetPW where userName=@userName and password=@password";
            SqlCommand cmd = new SqlCommand(strCommandText, myConnect);

            //open connection and retrieve data by calling ExecuteReader
            myConnect.Open();
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                status = reader["status"].ToString();
                dateforget = reader["dateforget"].ToString();
                myConnect.Close();
            }
            else
            {
                ModalPopupExtender2.Show();

                myConnect.Close();
            }
            return2value[0] = status;
            return2value[1] = dateforget;
            return return2value;

        }
        public string[] memberEmails(string userName)
        {
            string[] return2value = new string[2];
            string strConnectionString =
                ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;
            string email = "";
            string name = "";
            //create connection
            SqlConnection myConnect = new SqlConnection(strConnectionString);

            //create command
            string strCommandText = "SELECT * from Member where userName=@userName";
            SqlCommand cmd = new SqlCommand(strCommandText, myConnect);

            //open connection and retrieve data by calling ExecuteReader
            myConnect.Open();
            cmd.Parameters.AddWithValue("@userName", userName);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                email = reader["email"].ToString();
                name = reader["name"].ToString();
                myConnect.Close();
            }
            else
            {
                ModalPopupExtender2.Show();

                myConnect.Close();
            }
            return2value[0] = email;
            return2value[1] = name;
            return return2value;

        }
        public int updateLoginDeactive(string userName)
        {

            DateTime lastlogInDate = DateTime.Now;
            string status = "Deactive";
            string reason = "Get wrong password for more than 3 time";
            int result = 0;

            string strConnectionString =
             ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;


            //create connection
            SqlConnection myConnect = new SqlConnection(strConnectionString);
            string queryStr = "UPDATE Login SET lastlogInDate=@lastlogInDate," +
           " status=@status, reason=@reason WHERE userName=@userName";


            SqlCommand cmd = new SqlCommand(queryStr, myConnect);
            //cmd.Parameters.AddWithValue("@Product_ID", this.Product_ID);
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@lastlogInDate", lastlogInDate);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@reason", reason);


            myConnect.Open();
            result += cmd.ExecuteNonQuery();
            myConnect.Close();
            return result;
        }
        public int updateLoginlastLogin(string userName)
        {

            DateTime lastlogInDate = DateTime.Now;

            int result = 0;

            string strConnectionString =
             ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;


            //create connection
            SqlConnection myConnect = new SqlConnection(strConnectionString);
            string queryStr = "UPDATE Login SET lastlogInDate=@lastlogInDate WHERE userName=@userName";


            SqlCommand cmd = new SqlCommand(queryStr, myConnect);
            //cmd.Parameters.AddWithValue("@Product_ID", this.Product_ID);
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@lastlogInDate", lastlogInDate);



            myConnect.Open();
            result += cmd.ExecuteNonQuery();
            myConnect.Close();
            return result;
        }
        public string login(string userName)
        {
            string strConnectionString =
                ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;
            string pw = "";
            //create connection
            SqlConnection myConnect = new SqlConnection(strConnectionString);

            //create command
            string strCommandText = "SELECT userName, Password from Login where userName=@userName";
            SqlCommand cmd = new SqlCommand(strCommandText, myConnect);

            //open connection and retrieve data by calling ExecuteReader
            myConnect.Open();
            cmd.Parameters.AddWithValue("@userName", userName);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                pw = reader["Password"].ToString();
                myConnect.Close();
            }
            else
            {
                ModalPopupExtender2.Show();

                myConnect.Close();
            }
            return pw;

        }
        public string logincategory(string userName)
        {
            string strConnectionString =
                ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;
            string category = "";
            //create connection
            SqlConnection myConnect = new SqlConnection(strConnectionString);

            //create command
            string strCommandText = "SELECT category from Login where userName=@userName";
            SqlCommand cmd = new SqlCommand(strCommandText, myConnect);

            //open connection and retrieve data by calling ExecuteReader
            myConnect.Open();
            cmd.Parameters.AddWithValue("@userName", userName);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                category = reader["category"].ToString();
                myConnect.Close();
            }
            else
            {
                ModalPopupExtender2.Show();

                myConnect.Close();
            }
            return category;

        }
        protected void SendMail(string email, string password, string name)
        {
            // Gmail Address from where you send the mail
            var fromAddress = "hui.zhong90@gmail.com";
            // any address where the email will be sending
            var toAddress = email;
            //Password of your gmail address
            const string fromPassword = "whatshouldido";
            // Passing the values and make a email formate to display
            string subject = "New Password from FoodParsdise";
            string body = "From: FoodParsdise " + "\n";
            body += "To: " + name + "\n";
            body += "Email: " + email + "\n";
            body += "Subject: Since you have forgotten your password, now we are providing you with a new password. \n";
            body += "Do note that this password is only for one time used.  \n";
            body += "Password: " + password + "\n\n";
            body += "Best ragards \n";
            body += "Human Relationship Manager \n";
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);
        }
        public int insertforgetPW(string Password, string userName)
        {
            int result = 0;
            DateTime dateForget = DateTime.Now;
            string status = "Not Yet LogIn";

            string strConnectionString =
             ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;


            //create connection
            SqlConnection myConnect = new SqlConnection(strConnectionString);

            string queryStr = "INSERT INTO ForgetPW(userName,  Password,  dateForget, status)" +
         "values (@userName, @Password,   @dateForget, @status)";

            SqlCommand cmd = new SqlCommand(queryStr, myConnect);
            //cmd.Parameters.AddWithValue("@Product_ID", this.Product_ID);
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@Password", Password);

            cmd.Parameters.AddWithValue("@dateForget", dateForget);
            cmd.Parameters.AddWithValue("@status", status);
            myConnect.Open();
            result += cmd.ExecuteNonQuery();
            myConnect.Close();
            return result;
        }


        public int updateLogin(string Password, string userName)
        {


            int result = 0;

            string strConnectionString =
             ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;


            //create connection
            SqlConnection myConnect = new SqlConnection(strConnectionString);
            string queryStr = "UPDATE Login SET Password=@Password WHERE userName=@userName";


            SqlCommand cmd = new SqlCommand(queryStr, myConnect);
            //cmd.Parameters.AddWithValue("@Product_ID", this.Product_ID);
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@Password", Password);


            myConnect.Open();
            result += cmd.ExecuteNonQuery();
            myConnect.Close();
            return result;
        }
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

    }
}