using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using ServiceExpress.BLL;
using ServiceExpress.DAL;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace ServiceExpress
{
    public partial class InvoiceCreateDetails : System.Web.UI.Page
    {
        string invoiceNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                invoiceNo = "I15" + CreateRandomID();
                lblInvoiceNo.Text = invoiceNo;
                

                string doNo = (string)Session["deliveryOrderID"];

                //return to create invoice if delivery order is nt selected
                if (doNo == null)
                {
                    Response.Redirect("InvoiceCreate.aspx");
                }

                InvoiceWSBLL doBLL = new InvoiceWSBLL();
                DataSet dsIvDetails;

                dsIvDetails = doBLL.getInvoiceDetailFromDo(doNo);
                for (int i = 0; i < dsIvDetails.Tables[0].Rows.Count; i++)
                {
                    //poID = dsIvDetails.Tables[0].Rows[i]["purOrderNo"].ToString();
                    lblCustComName.Text = dsIvDetails.Tables[0].Rows[i]["companyName"].ToString();
                    lblPersonnel.Text = dsIvDetails.Tables[0].Rows[i]["orderPersonnel"].ToString();
                    lblAddress.Text = dsIvDetails.Tables[0].Rows[i]["addresss"].ToString();
                    //lblDeliverDate.Text = dsIvDetails.Tables[0].Rows[i]["DOdate"].ToString();
                    lblTotalAmt.Text = dsIvDetails.Tables[0].Rows[i]["totalPrice"].ToString();
                    

                }
                lblInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblDONo.Text = doNo;
                double totalAmount = double.Parse(lblTotalAmt.Text);
                double gstAmt = totalAmount * 0.07;
                double grandTotal = totalAmount + gstAmt;
                lblGST.Text = gstAmt.ToString();
                lblGrandTotal.Text = grandTotal.ToString();


                // bind gv
                BindGVList();
                bindListBox();

                lblNetCP.Text = "None";
                lblCDdicsDays.Visible = false;
                lblCDpercent.Visible = false;
                lblDash.Visible = false;
            }
        }
        private void bindListBox()
        {
            InvoiceWSBLL ivBLL = new InvoiceWSBLL();


            ddlCategory.DataSource = ivBLL.getCreditTerm();
            // Name will be displayed in the dropdownlist control
            ddlCategory.DataTextField = "creditPeriod";
            // when an item is selected in dropdownlist
            // CategoryID will be returned in ddlCategory.SelectedValue
            ddlCategory.DataValueField = "creditPeriod";
            ddlCategory.DataBind();

            DataSet dsCT;

            string selectedDay = ddlCategory.SelectedValue.ToString();

            dsCT = ivBLL.getCreditTermByDays(selectedDay);
            for (int i = 0; i < dsCT.Tables[0].Rows.Count; i++)
            {
                lblCDpercent.Text = dsCT.Tables[0].Rows[i]["creditDiscount"].ToString();
                lblCDdicsDays.Text = dsCT.Tables[0].Rows[i]["creditDiscPeriod"].ToString();

            }

        }

        private void BindGVList()
        {
            InvoiceWSBLL ivdoBLL = new InvoiceWSBLL();

            //get pur ID by DoID
            DataSet dsForid;

            string doID = (string)Session["deliveryOrderID"];
            dsForid = ivdoBLL.getPoIdFromDoById(doID);
            string poID = "";
            for (int i = 0; i < dsForid.Tables[0].Rows.Count; i++)
            {
                poID = dsForid.Tables[0].Rows[i]["purOrderNo"].ToString();

            }
          
            // use poID to retreive po details for invoice
            DataSet ds;

            ds = ivdoBLL.getInvoiceDetailsFromPO(poID);
            //string invoiceNo = (string)Session["test"];
             //bind data to table

            gvInvoiceDetails.DataSource = ds;
            gvInvoiceDetails.DataBind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {

            InvoiceWSBLL doBLL = new InvoiceWSBLL();

            //get pur ID by DoID
            DataSet dsForid;

            string doID = (string)Session["deliveryOrderID"];
            dsForid = doBLL.getPoIdFromDoById(doID);
            string poID = "";
            for (int i = 0; i < dsForid.Tables[0].Rows.Count; i++)
            {
                poID = dsForid.Tables[0].Rows[i]["purOrderNo"].ToString();

            }

            
            InvoiceWSBLL invoiceBLL = new InvoiceWSBLL();

            string creditPeriod = ddlCategory.SelectedValue.ToString();
            string creditDiscount = lblCDpercent.Text;
            string creditDiscPeriod = lblCDdicsDays.Text;
            string invoicePersonnel = "Lim Zhi Wei";
            string creditTerm = "None";

            DeliveryOrderWSDAL deliveryUpdate = new DeliveryOrderWSDAL();
            int resultDelivery = deliveryUpdate.UpdateDeliveryOrder(doID, "Invoice Created");
            if (cbCreditTerm.Checked == true)
            {
                 creditTerm = lblCDpercent.Text + lblDash.Text + lblCDdicsDays.Text + lblNetCP.Text;
            }



            int result = invoiceBLL.InsertInvoice(lblInvoiceNo.Text, lblGST.Text, lblGrandTotal.Text, creditPeriod, creditTerm, doID, poID, invoicePersonnel);
            if (result > 0)
            {

                if (cbCreditTerm.Checked == true)
                {
                    //generate id
                    string crID = "CR15" + CreateRandomID();
                    double creditDiscAmt = double.Parse(lblTotalAmt.Text) * (Double.Parse(creditDiscount) / 100.0);
                    string creditDiscAmount = creditDiscAmt.ToString();

                    invoiceBLL.InsertInvoiceCreditDiscRecord(crID, creditDiscount, creditDiscPeriod, creditDiscAmount, "Available", lblInvoiceNo.Text);
                }

                SendMail();

                string display = "Record has been inserted successfully!";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);


            }
            else
            {
                string display = "Record has been inserted fail!";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);

            }

            Response.Redirect("Invoice.aspx");
        }

        public static string CreateRandomID()  //If you are always going to want 8 characters then there is no need to pass a length argument
        {
            string _allowedChars = "0123456789";
            Random randNum = new Random((int)DateTime.Now.Ticks);  //Don't forget to seed your random, or else it won't really be random
            char[] chars = new char[4];
            //again, no need to pass this a variable if you always want 8

            for (int i = 0; i < 4; i++)
            {
                chars[i] = _allowedChars[randNum.Next(_allowedChars.Length)];
                //No need to over complicate this, passing an integer value to Random.Next will "Return a nonnegative random number less than the specified maximum."
            }
            return new string(chars);
        }

        public string SendMail()
        {

            MailMessage m = new MailMessage();

            SmtpClient sc = new SmtpClient();
            string msg = string.Empty;
            try
            {

                m.From = new MailAddress("zhiwei93zack@gmail.com");
                m.To.Add("zhiwei93zack@gmail.com"); // change to customer email add

                m.Subject = "Service Express Invoice for " + DateTime.Now.ToString("dd/MM/yyyy");

                m.IsBodyHtml = true;

                string creditPeriod = ddlCategory.SelectedValue.ToString();

                m.Body = "Dear FlyAsia, " + "<br/><br/>Thank you for doing business with us. The invoice details is listed below: <br/><br/>"
                    + "Invoice No: " + lblInvoiceNo.Text + "<br/>Delivery No: " + lblDONo.Text + "<br/>Credit Period: " + creditPeriod + "<br/>Total Amount: " + lblTotalAmt.Text
                    + " <br/><br/>Further Discount will be given if payment is being made with in the credit period.<br/>For more detail, please acknowledge our invoice." +
                    "<br/><br/>Best regards," + "<br/>Service Express Finance Department";



                sc.Host = "smtp.gmail.com";

                sc.Port = 587;

                sc.Credentials = new System.Net.NetworkCredential("zhiwei93zack@gmail.com", "27071993");


                sc.EnableSsl = true;

                sc.Send(m);

                msg = "Successful<BR>";

            }

            catch (Exception ex)
            {

            }
            return msg;
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvoiceWSBLL ivBLL = new InvoiceWSBLL();
            DataSet dsCT;

            string selectedDay = ddlCategory.SelectedValue.ToString();

            dsCT = ivBLL.getCreditTermByDays(selectedDay);
            for (int i = 0; i < dsCT.Tables[0].Rows.Count; i++)
            {
                lblCDpercent.Text = dsCT.Tables[0].Rows[i]["creditDiscount"].ToString();
                lblCDdicsDays.Text = dsCT.Tables[0].Rows[i]["creditDiscPeriod"].ToString();
                lblNetCP.Text = ",Net " + ddlCategory.SelectedValue.ToString() + " days";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoiceCreate.aspx");
        }

        protected void cbCreditTerm_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCreditTerm.Checked == true)
            {
                lblNetCP.Text = ",Net " + ddlCategory.SelectedValue.ToString() + " days";
                lblCDdicsDays.Visible = true;
                lblCDpercent.Visible = true;
                lblDash.Visible = true;
            }
            else
            {
                lblNetCP.Text = "None";
                lblCDdicsDays.Visible = false;
                lblCDpercent.Visible = false;
                lblDash.Visible = false;
            }
        }

        
    }
}