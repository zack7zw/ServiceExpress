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
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            string username = txtForgetPWUuserName.Text;
            string email = txtForgetPWEmail.Text;
            string name = loginMember(username, email);
            if (!name.Equals(""))
            {
                string pw = DAL.GeneratePW.Generate(8, 10);

                try
                {
                    string newPW = GetHashString(pw);
                    //here on button click what will done 
                    SendMail(email, pw, name);
                    insertforgetPW(newPW, username);
                    updateLogin(newPW, username);
                    lblForgetPWResult.Text = "An email has been sent out, and please login with 24 hour as this is one time used password";
                    ModalPopupExtender1.Show();

                }
                catch (Exception) { }
            }
            else
            {
                lblForgetPWResult.Text = "No Record Found";
                ModalPopupExtender1.Show();
            }
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
        public string loginMember(string userName, string email)
        {
            string strConnectionString =
                ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;
            string name = "";
            //create connection
            SqlConnection myConnect = new SqlConnection(strConnectionString);

            //create command
            string strCommandText = "SELECT name from Login l" +
                " Inner Join Member m " +
                "on l.userName=m.userName" +
                " where l.userName=@userName and email=@email";
            SqlCommand cmd = new SqlCommand(strCommandText, myConnect);

            //open connection and retrieve data by calling ExecuteReader
            myConnect.Open();
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                name = reader["name"].ToString();
                myConnect.Close();
            }
            else
            {
                ModalPopupExtender1.Show();

                myConnect.Close();
            }
            return name;

        }
    }
}