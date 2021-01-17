using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceExpress.BLL;
using ServiceExpress.DAL;

namespace ServiceExpress
{
    public partial class InvoiceCreate : System.Web.UI.Page
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
            InvoiceWSBLL doBLL = new InvoiceWSBLL();
            DataSet ds;

            //string invoiceNo = (string)Session["test"];

            ds = doBLL.deliveryOrderForInvoice();
            gvDO.DataSource = ds;
            gvDO.DataBind();
        }
 

        protected void gvDO_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Session["deliveryOrderID"] = gvDO.SelectedRow.Cells[0].Text;
           // Session["ivCompany"] = gvDO.SelectedRow.Cells[1].Text;
          //  Session["ivConPersonnel"] = gvDO.SelectedRow.Cells[2].Text;
          //  Session["ivDOdate"] = gvDO.SelectedRow.Cells[3].Text;
          //  Session["ivTotalAmt"] = gvDO.SelectedRow.Cells[4].Text;
            Response.Redirect("InvoiceCreateDetails.aspx");   
            
        }

        protected void gvDO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newPageIndex = e.NewPageIndex; //get new page
            gvDO.PageIndex = newPageIndex;
            BindInvoiceList();
        }
    }
}