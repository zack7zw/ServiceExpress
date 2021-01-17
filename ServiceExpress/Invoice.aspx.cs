using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using ServiceExpress.BLL;
using ServiceExpress.DAL;
using System.Net.Mail;

namespace ServiceExpress
{
    public partial class Invoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInvoiceList();
            }
                
        }

        private void BindInvoiceList()
        {
            InvoiceWSBLL myInvoice = new InvoiceWSBLL();
            DataSet ds;
            ds = myInvoice.getAllInvoice(tbFromDate.Text, tbToDate.Text, ddlViewOnly.SelectedValue.ToString(), rblSort.SelectedValue.ToString(), tbComName.Text);

            gvInvoice.DataSource = ds;
            gvInvoice.DataBind();

            gvInvoiceDue.DataSource = ds;
            gvInvoiceDue.DataBind();

        }

      

        protected void btnClear_Click(object sender, EventArgs e)
        {
            InvoiceWSBLL myInvoice = new InvoiceWSBLL();
            DataSet ds;

            tbFromDate.Text = "";
            tbToDate.Text = "";
            ddlViewOnly.SelectedValue = "All";
            rblSort.SelectedValue = "asc";
            tbComName.Text = "";
            ds = myInvoice.getAllInvoice("", "", "All", "asc", "");
            gvInvoice.DataSource = ds;
            gvInvoice.DataBind();
        }
        

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            BindInvoiceList();
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {

            if ((ddlViewOnly.SelectedValue.ToString().Equals("Due")) || (ddlViewOnly.SelectedValue.ToString().Equals("BadDebt")))
            {
                BindInvoiceList();
                gvInvoice.Visible = false;
                gvInvoiceDue.Visible = true;

            }
            else
            {
                BindInvoiceList();
                gvInvoice.Visible = true;
                gvInvoiceDue.Visible = false;
            }
           
            
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoiceView.aspx");

        }

        protected void GVinvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["invoiceNo"] = gvInvoice.SelectedRow.Cells[0].Text;
            Response.Redirect("InvoiceView.aspx");
        }

        protected void gvInvoiceDue_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvoiceWSBLL invoiceBLL = new InvoiceWSBLL();

            string ivNo = gvInvoiceDue.SelectedRow.Cells[0].Text;
            string company = gvInvoiceDue.SelectedRow.Cells[1].Text;
            string conpersonnel = gvInvoiceDue.SelectedRow.Cells[2].Text;
            string ivStatus = gvInvoiceDue.SelectedRow.Cells[6].Text;

            string invoiceStatus = "WarningSent";
            int result = invoiceBLL.InvoiceStatusUpdate(invoiceStatus, gvInvoiceDue.SelectedRow.Cells[0].Text);
            if (result > 0)
            {

                string display = "Notice have been sent to customer company";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                SendMail(ivNo, company, conpersonnel, ivStatus);
            }
            else
            {
                string display = "Notice have not been sent";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);


            }

            BindInvoiceList();

        }

        public string SendMail(string ivNo, string company, string conpersonnel, string ivStatus)
        {

            MailMessage m = new MailMessage();

            SmtpClient sc = new SmtpClient();
            string msg = string.Empty;

            InvoiceWSBLL ivBLL = new InvoiceWSBLL();
            DataSet dsIvDetails;

            string creditPeriod = ""; string total = ""; string outstandingAmt = ""; string amtpaid = ""; string invoiceDate = "";

            dsIvDetails = ivBLL.getInvoiceWS(ivNo);
            for (int i = 0; i < dsIvDetails.Tables[0].Rows.Count; i++)
            {

                creditPeriod = dsIvDetails.Tables[0].Rows[i]["creditPeriod"].ToString();
                total = dsIvDetails.Tables[0].Rows[i]["grandTotal"].ToString();
                outstandingAmt = dsIvDetails.Tables[0].Rows[i]["outstandingAmt"].ToString();
                amtpaid = dsIvDetails.Tables[0].Rows[i]["amtPaid"].ToString();
                invoiceDate = dsIvDetails.Tables[0].Rows[i]["invoiceDate"].ToString();
                if (outstandingAmt.Equals("") || amtpaid.Equals(""))
                {
                    outstandingAmt = total;
                    amtpaid = "0";
                }
            }

            try
            {

                m.From = new MailAddress("zhiwei93zack@gmail.com");
                m.To.Add("lim_hui_zhong@hotmail.com");

                m.Subject = "REMINDER: To " + company + " for invoice dated on " + invoiceDate;

                m.IsBodyHtml = true;


                m.Body = "Dear " + company + "," + "<br/><br/>This email is send as a reminder as your invoice is" + ivStatus
                    + ". <br/>Invoice Details:<br/><br/>Invoice No.: " + ivNo + "<br/>Order Personnel: " + conpersonnel + "<br/>Total Price: $" + total + "<br/>Amount Paid: $" + amtpaid
                    + "<br/>Outstanding Amount: $" + outstandingAmt + "<br/><br/>Please pay the outstanding amount as soon as possible. We thank you for your cooperation" +
                    "<br/><br/>Best regards," + "<br/>Service Express";



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

        protected void gvInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
            int newPageIndex = e.NewPageIndex; //get new page
            gvInvoice.PageIndex = newPageIndex; //index to new page
            BindInvoiceList();
        }

        protected void btnGen_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoiceMonthlyReport.aspx");
        }

        protected void gvInvoiceDue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newPageIndex = e.NewPageIndex; //get new page
            gvInvoiceDue.PageIndex = newPageIndex; //index to new page
            BindInvoiceList();
        }
        protected void LinkButton1_Click(object sender, ImageClickEventArgs e)
        {
            var buttonlink = (Control)sender;
            GridViewRow row = (GridViewRow)buttonlink.NamingContainer;
            Session["invoiceNo"] = row.Cells[0].Text;
            Response.Redirect("InvoiceView.aspx");
        }
    }
}