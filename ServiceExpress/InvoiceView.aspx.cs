using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using ServiceExpress.BLL;
using ServiceExpress.DAL;

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace ServiceExpress
{
    public partial class InvoiceView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             BindPaymentGV();
            if (!IsPostBack)
            {
                

                string ivNo = (string)Session["invoiceNo"];

                //return to create invoice if delivery order is nt selected
                if (ivNo == null)
                {
                    Response.Redirect("Invoice.aspx");
                }

               

                InvoiceWSBLL ivBLL = new InvoiceWSBLL();
                DataSet dsIvDetails;

                dsIvDetails = ivBLL.getInvoice(ivNo);
                for (int i = 0; i < dsIvDetails.Tables[0].Rows.Count; i++)
                {
                    lblcustName.Text = dsIvDetails.Tables[0].Rows[i]["companyName"].ToString();
                    lblCreditPeriod.Text = dsIvDetails.Tables[0].Rows[i]["creditPeriod"].ToString();
                    lblGrandTotal.Text = dsIvDetails.Tables[0].Rows[i]["grandTotal"].ToString();
                    lblNoOfDays.Text = dsIvDetails.Tables[0].Rows[i]["noOfDayLeft"].ToString();
                    lblPaidAmt.Text = dsIvDetails.Tables[0].Rows[i]["amtPaid"].ToString();
                    lblOutstandAmt.Text = dsIvDetails.Tables[0].Rows[i]["outstandingAmt"].ToString();
                    lblIVdate.Text = dsIvDetails.Tables[0].Rows[i]["invoiceDate"].ToString();
                    lblStatus.Text = dsIvDetails.Tables[0].Rows[i]["invoiceStatus"].ToString();
                    lblNoOfDaysDisc.Text = dsIvDetails.Tables[0].Rows[i]["noOfDayLeftDisc"].ToString();
                    if (lblNoOfDaysDisc.Text.Equals(""))
                    {
                        lblNoOfDaysDisc.Visible = false;
                        lblNoOfDayDisc.Visible = false;
                    }
                    if (dsIvDetails.Tables[0].Rows[i]["amtPaid"].ToString().Equals(""))
                    {
                        lblPaidAmt.Text = "0";
                        lblOutstandAmt.Text = dsIvDetails.Tables[0].Rows[i]["grandTotal"].ToString();
                    }
                    
                

                }

                lblInvoiceNo.Text = ivNo;

                // bind gv
                BindGVList();

            }
        }

        private void BindGVList()
        {
            InvoiceWSBLL ivivBLL = new InvoiceWSBLL();

            //get pur ID by DoID
            DataSet dsForid;

            string ivNo = (string)Session["invoiceNo"];
            dsForid = ivivBLL.getPoIdFromIvById(ivNo);
            string poID = "";
            for (int i = 0; i < dsForid.Tables[0].Rows.Count; i++)
            {
                poID = dsForid.Tables[0].Rows[i]["purOrderNo"].ToString();

            }

            // use poID to retreive po details for invoice
            DataSet ds;

            ds = ivivBLL.getInvoiceDetailsFromPO(poID);
            //string invoiceNo = (string)Session["test"];
            //bind data to table

            gvInvoiceDetails.DataSource = ds;
            gvInvoiceDetails.DataBind();
        }

        protected void btnExportEx_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                //MessageBox.Text = ("Excel is not properly installed!!");
                return;
            }


            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[7, 4] = "Service Express 18, Changi Business Park Central 1";
            xlWorkSheet.Cells[8, 4] = "Singapore 486097, Tel: 6782-6690";

            //xlWorkSheet.Cells[20, 20].Font.Bold = true;
            

            //get image (logo) path
            string parentPath = Server.MapPath("~Images\\LogoSvcExp.gif");
            string imagePath = parentPath.Replace("~", "");
            xlWorkSheet.Shapes.AddPicture(imagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 180, 20, 55, 45);
            //xlWorkSheet.Shapes.AddPicture("E:\\ESPJ files\\ESPJ\\ServiceExpress\\ServiceExpress\\ServiceExpress\\Images\\LogoSvcExp.gif", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 50, 50, 300, 45); 
            //xlWorkSheet.Shapes.AddPicture("E:\\ESPJ files\\ESPJ\\ServiceExpress\\ServiceExpress\\ServiceExpress\\Images\\delivery.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 50, 50, 300, 45); 

            InvoiceWSBLL myInvoice = new InvoiceWSBLL();
            //getpoid
            DataSet dsForid;

            string ivNo = (string)Session["invoiceNo"];
            dsForid = myInvoice.getPoIdFromIvById(ivNo);
            string poID = "";
            for (int i = 0; i < dsForid.Tables[0].Rows.Count; i++)
            { poID = dsForid.Tables[0].Rows[i]["purOrderNo"].ToString(); }
            //get po details
            DataSet ds;
            ds = myInvoice.getInvoiceDetailsFromPO(poID);
            //DataSet ds = (DataSet)gvInvoice.DataSource;
            System.Data.DataTable table = ds.Tables[0];
            int columnIndex = 0;

            foreach (DataColumn col in table.Columns)
            {
                columnIndex++;
                xlApp.Cells[16, columnIndex + 1] = col.ColumnName; // control header row
                xlApp.Cells[16, columnIndex + 1].Font.Bold = true;
                xlApp.Cells.Font.Name = "Tahoma";
                xlApp.Cells.Font.Size = 8;
                xlApp.Cells.WrapText = false;

            }

            int rowIndex = 15; //control row
            foreach (DataRow row in table.Rows)
            {
                rowIndex++;
                columnIndex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    columnIndex++;
                    xlApp.Cells[rowIndex + 1, columnIndex + 1] = row.ItemArray[columnIndex - 1].ToString();


                }
            }
            //set width
            xlWorkSheet.Columns[4].ColumnWidth = 35.14;
            //warp text
            xlWorkSheet.Cells[8, 4].WrapText = true;
            xlWorkSheet.Cells[7, 4].WrapText = true;

            xlWorkSheet.Cells[9, columnIndex + 1] = lblInvoiceNo.Text;
            xlWorkSheet.Cells[10, columnIndex + 1] = lblIVdate.Text;
            xlWorkSheet.Cells[11, columnIndex + 1] = "";
            xlWorkSheet.Cells[12, columnIndex + 1] = lblCreditPeriod.Text;
            xlWorkSheet.Cells[13, columnIndex + 1] = "";
            //xlWorkSheet.Cells[14, columnIndex] = "Sheet 20 content";
            xlWorkSheet.Cells[9, columnIndex] = "Invoice No.: ";
            xlWorkSheet.Cells[10, columnIndex] = "Invoice Date: ";
            xlWorkSheet.Cells[11, columnIndex] = "Ref Deliver No.:";
            xlWorkSheet.Cells[12, columnIndex] = "Credit Period: ";
            xlWorkSheet.Cells[13, columnIndex] = "Credit Terms: ";
            //xlWorkSheet.Cells[14, columnIndex - 1] = "Sheet 20 content";

            xlWorkSheet.Cells[rowIndex + 2, columnIndex] = "Sub Total: ";
            xlWorkSheet.Cells[rowIndex + 3, columnIndex] = "GST: ";
            xlWorkSheet.Cells[rowIndex + 4, columnIndex] = "Total Amount";

            xlWorkSheet.Cells[rowIndex + 2, columnIndex + 1] = "";
            xlWorkSheet.Cells[rowIndex + 3, columnIndex + 1] = "";
            xlWorkSheet.Cells[rowIndex + 4, columnIndex + 1] = "$" + lblGrandTotal.Text;

            xlWorkSheet.Cells[9, 2] = lblcustName.Text;
            xlWorkSheet.Cells[10, 2] = "The Purchase Manager";
            xlWorkSheet.Cells[11, 2] = "34 Changi Business Park";
            xlWorkSheet.Cells[12, 2] = "Singapore 533451";
           // xlWorkSheet.Cells[9, 3] = "Signature";
           // xlWorkSheet.Cells[10, 3] = "Signature";
           // xlWorkSheet.Cells[11, 3] = "Signature";

            xlWorkSheet.Cells[rowIndex + 8, 2] = "Signature";
            xlWorkSheet.Cells[rowIndex + 8, 2].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlDouble;
            //xlWorkSheet.Cells[rowIndex + 6, 2] = MessageBox.Text;
            xlWorkSheet.Cells[rowIndex + 8, columnIndex] = "Date";
            xlWorkSheet.Cells[rowIndex + 8, columnIndex].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlDouble;

            //xlApp.Cells[rowIndex + 5, 2].Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 1d;
            //xlApp.Cells[rowIndex + 5, 2].Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 1d;


            Microsoft.Office.Interop.Excel.Dialog dialog = xlApp.Dialogs[Microsoft.Office.Interop.Excel.XlBuiltInDialog.xlDialogSaveAs];
            dialog.Show(Type.Missing, // document_text
            Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, // type_num
            Type.Missing, // prot_pwd
            Type.Missing, // backup
            Type.Missing, // write_res_pwd
            Type.Missing, // read_only_rec
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //xlWorkBook.SaveAs("C:\\Users\\Lim\\Desktop\\ESP Project\\csharp-Excel.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            //MessageBox.Text = ("Excel file created , you can find the file d:\\csharp-Excel.xls");
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                //MessageBox.Text = ("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Invoice.aspx");//btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;");
        }

        protected void btnActualIV_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoiceViewDetails.aspx");
        }
        private void BindPaymentGV()
        {
            string ivNo = (string)Session["invoiceNo"];

            //return to create invoice if delivery order is nt selected
            if (ivNo == null)
            {
                Response.Redirect("Invoice.aspx");
            }

            lblivnb.Text = ivNo;

            InvoiceWSBLL myPayment = new InvoiceWSBLL();
            DataSet ds;
            //ivNo = (string)Session["invoiceNo"];
            ds = myPayment.getPaymentRecord(ivNo);
            gvPayment.DataSource = ds;
            gvPayment.DataBind();
        }
        
    }
}