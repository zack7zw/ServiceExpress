using System;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using ServiceExpress.BLL;

namespace ServiceExpress
{
    public partial class exportExcelTest2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
                BindInvoiceList();
        }

        private void BindInvoiceList()
        {
            InvoiceWSBLL myInvoice = new InvoiceWSBLL();
            DataSet ds;
            ds = myInvoice.getAllInvoice("", "", "All", "asc", "");
            gvInvoice.DataSource = ds;
            gvInvoice.DataBind();


            GridView1.DataSource = ds;
            GridView1.DataBind();
        }



        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = ConfigurationManager
                .ConnectionStrings["conString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }



        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition",
             "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            PrepareForExport(GridView1);
            PrepareForExport(gvInvoice);

            Table tb = new Table();
            TableRow tr1 = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Controls.Add(GridView1);
            tr1.Cells.Add(cell1);
            TableCell cell3 = new TableCell();
            cell3.Controls.Add(gvInvoice);
            TableCell cell2 = new TableCell();
            cell2.Text = "&nbsp;";
            if (rbPreference.SelectedValue == "2")
            {
                tr1.Cells.Add(cell2);
                tr1.Cells.Add(cell3);
                tb.Rows.Add(tr1);
            }
            else
            {
                TableRow tr2 = new TableRow();
                tr2.Cells.Add(cell2);
                TableRow tr3 = new TableRow();
                tr3.Cells.Add(cell3);
                tb.Rows.Add(tr1);
                tb.Rows.Add(tr2);
                tb.Rows.Add(tr3);
            }
            tb.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        protected void PrepareForExport(GridView Gridview)
        {
            Gridview.AllowPaging = Convert.ToBoolean(rbPaging.SelectedItem.Value);
            Gridview.DataBind();

            //Change the Header Row back to white color
            Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF");

            //Apply style to Individual Cells
            for (int k = 0; k < Gridview.HeaderRow.Cells.Count; k++)
            {
                Gridview.HeaderRow.Cells[k].Style.Add("background-color", "green");
            }

            for (int i = 0; i < Gridview.Rows.Count; i++)
            {
                GridViewRow row = Gridview.Rows[i];

                //Change Color back to white
                row.BackColor = System.Drawing.Color.White;

                //Apply text style to each Row
                row.Attributes.Add("class", "textmode");

                //Apply style to Individual Cells of Alternating Row
                if (i % 2 != 0)
                {
                    for (int j = 0; j < Gridview.Rows[i].Cells.Count; j++)
                    {
                        row.Cells[j].Style.Add("background-color", "#C2D69B");
                    }
                }
            }
        }

        protected void GVinvoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}