using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceExpress.BLL;

namespace ServiceExpress
{
    public partial class SE_BE : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InvoiceWSBLL invoiceBLL = new InvoiceWSBLL();

                invoiceBLL.UpdateInvoiceStatusToBadDebt();
                invoiceBLL.UpdateInvoiceStatusToDue();
            } 
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("LogIn.aspx");
        }
    }
}