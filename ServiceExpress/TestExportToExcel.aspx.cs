using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Excel = Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;
using ServiceExpress.BLL;
using System.Data;

namespace ServiceExpress
{
    public partial class TestExportToExcel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
                BindPaymentGV();
        }

        private void BindPaymentGV()
        {
            InvoiceWSBLL myPayment = new InvoiceWSBLL();
            DataSet ds;
            string ivNo = (string)Session["invoiceNo"];
            ds = myPayment.getPaymentRecord(ivNo);
            gvPayment.DataSource = ds;
            gvPayment.DataBind();
        }

     /*   protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
                BindInvoiceList();
        }

        private void BindInvoiceList()
        {
            InvoiceWSBLL myInvoice = new InvoiceWSBLL();
            DataSet ds;
            ds = myInvoice.getAllInvoice();
            gvInvoice.DataSource = ds;
            gvInvoice.DataBind();
        }

        protected void Buttonexcel_Click(object sender, EventArgs e)
        {
            //ExportGridToExcel();  

           /* try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Charset = "";
                Response.AddHeader("content-disposition", "attachment;filename=dados.xls");
                StringWriter sWriter = new StringWriter();
                HtmlTextWriter hWriter = new HtmlTextWriter(sWriter);
                gvInvoice.RenderControl(hWriter);
                string style = @"<style> .textmode {mso-number-format:General} </style>";
                Response.Output.Write(sWriter.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                MessageBox.Text = ex.ToString();
            }
            */
        }

       /* private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Vithal" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvInvoice.GridLines = GridLines.Both;
            gvInvoice.HeaderStyle.Font.Bold = true;
            gvInvoice.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }  
        protected void btnExport_Click(object sender, EventArgs e)
        {

            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Text = ("Excel is not properly installed!!");
                return;
            }


            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //xlWorkSheet.Cells[20, 20] = "Sheet 20 content";
            //xlWorkSheet.Cells[20, 20].Font.Bold = true;

            //get image (logo) path
            string parentPath = Server.MapPath("~Images\\LogoSvcExp.gif");
            string imagePath = parentPath.Replace("~", "");
            xlWorkSheet.Shapes.AddPicture(imagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 200, 50, 55, 45);
            //xlWorkSheet.Shapes.AddPicture("E:\\ESPJ files\\ESPJ\\ServiceExpress\\ServiceExpress\\ServiceExpress\\Images\\LogoSvcExp.gif", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 50, 50, 300, 45); 
            //xlWorkSheet.Shapes.AddPicture("E:\\ESPJ files\\ESPJ\\ServiceExpress\\ServiceExpress\\ServiceExpress\\Images\\delivery.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 50, 50, 300, 45); 

            InvoiceWSBLL myInvoice = new InvoiceWSBLL();
            DataSet ds;
            ds = myInvoice.getAllInvoice();
            //DataSet ds = (DataSet)gvInvoice.DataSource;
            System.Data.DataTable table = ds.Tables[0];
            int columnIndex = 0;

            foreach (DataColumn col in table.Columns)
            {
                columnIndex++;
                xlApp.Cells[16, columnIndex +1] = col.ColumnName; // control header row
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
            xlWorkSheet.Cells[rowIndex + 5, 2] = "Signature";
            xlWorkSheet.Cells[rowIndex + 5, 2].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlDouble;
            xlWorkSheet.Cells[rowIndex + 6, 2] = MessageBox.Text;
            xlWorkSheet.Cells[rowIndex + 5, columnIndex] = "Date";
            xlWorkSheet.Cells[rowIndex + 5, columnIndex].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlDouble;
            
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

            MessageBox.Text = ("Excel file created , you can find the file d:\\csharp-Excel.xls");
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
                MessageBox.Text = ("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
       /* void ExportGridToExcelUsingApplication()
        {
            Excel.ApplicationClass excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excel.Application.Workbooks.Add(true);
            DataSet ds = (DataSet)GridView1.DataSource;
            DataTable table = ds.Tables[0];
            int columnIndex = 0;

            foreach (DataColumn col in table.Columns)
            {
                columnIndex++;
                excel.Cells[1, columnIndex] = col.ColumnName;
                excel.Cells.Font.Name = "Tahoma";
                excel.Cells.Font.Size = 8;
                excel.Cells.WrapText = true;

            }

            int rowIndex = 0;
            foreach (DataRow row in table.Rows)
            {
                rowIndex++;
                columnIndex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    columnIndex++;
                    excel.Cells[rowIndex + 1, columnIndex] = row.ItemArray[columnIndex - 1].ToString();


                }
            }

            excel.Visible = true;
        }

        

        protected void GVinvoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //string str_directory = Environment.CurrentDirectory.ToString();
            //string parentPath =System.IO.Directory.GetParent(str_directory).FullName;//Directory.GetCurrentDirectory();//System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string parentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //string parentPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
           // string xslLocation = Path.Combine(parentPath, "delivery.jpg");
            string parentPath = Server.MapPath("~Images\\LogoSvcExp.gif");
            MessageBox.Text = parentPath;
            string imagePath = parentPath.Replace("\\", "\\\\");
            MessageBox0.Text = "     " + imagePath;
            
        }

        

        /*protected void btnExport_Click(object sender, EventArgs e)
        {
            string sQry = "SELECT FileID, OriginalFileName [File Name], FriendlyName [Image Name] FROM [File] WHERE Extension IN ('jpg','bmp','png') AND DataTrackLevelID = 80";
            string simagepath = @"C:\Documents and Settings\All Users\Documents\My Pictures\Sample Pictures\";
            string excelpath = @"D:\Chandan\Shyni\ExoprtImage_Excel\ExcelFiles\Excel_ImageLoad.xlsx";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TEST_CONNECTION_STRING"].ConnectionString);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sQry, con);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            con.Close();

            dt.Columns.Add("ImagePath");
            dt.Columns.Add("Image");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["ImagePath"] = simagepath + dt.Rows[i]["File Name"];
                dt.Rows[i]["Image"] = simagepath + dt.Rows[i]["File Name"];
            }

            ExporttoExcel(dt, excelpath);
        }

        public void ExporttoExcel(System.Data.DataTable dtexcel, string filepath)
        {
            //Creae an Excel application instance
            Excel.Application excelApp = new Excel.Application();

            //Create an Excel workbook instance and open it from the predefined location
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(filepath);

            //Add a new worksheet to workbook with the Datatable name
            Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
            excelWorkSheet.Name = "Photos";



            //Adding Columns to the Excel Sheet.
            for (int i = 1; i < dtexcel.Columns.Count + 1; i++)
            {
                excelWorkSheet.Cells[1, i] = dtexcel.Columns[i - 1].ColumnName;
            }

            //Adding Rows to Excel Sheet.

            for (int j = 0; j < dtexcel.Rows.Count; j++)
            {
                for (int k = 0; k < dtexcel.Columns.Count; k++)
                {
                    if (k == dtexcel.Columns.Count - 1)
                    {
                        //Checking for the particular Column with Image file. If Image exists in that directory, then add image to excel.
                        string path = dtexcel.Rows[j].ItemArray[k].ToString();
                        if (File.Exists(path))
                        {
                            excelWorkSheet.Shapes.AddPicture(path, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 100, 100, ImageSize, ImageSize);

                        }
                        else
                        {
                            excelWorkSheet.Cells[j + 2, k + 1] = "No File found";
                        }

                    }

                    //insert the anchor link
                    else if (k == dtexcel.Columns.Count - 2)
                    {
                        string filename = dtexcel.Rows[j].ItemArray[k - 2].ToString();
                        string colname = "D" + (j + 2);
                        excelWorkSheet.Cells[j + 2, k + 1] = dtexcel.Rows[j].ItemArray[k].ToString();
                        excelWorkSheet.Hyperlinks.Add(excelWorkSheet.Range[colname], dtexcel.Rows[j].ItemArray[k].ToString(), "", "Click Image", filename);

                    }

                    else
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = dtexcel.Rows[j].ItemArray[k].ToString();
                    }

                }
            }

            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApp.Quit();
            Response.Write("Exported Successfully");
        } */

    }
