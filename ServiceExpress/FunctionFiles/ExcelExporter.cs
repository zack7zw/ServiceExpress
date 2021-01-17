using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
 
using iTextSharp.text.html;
 
using iTextSharp.text.html.simpleparser;
 
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ServiceExpress.FunctionFiles
{
   /// <summary>
    /// Summary description for ExcelExport
    /// </summary>
    public partial class ExcelExport
    {

        public static void Export(string fileName, GridView gv)
        {
          /*  HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                  "content-disposition", string.Format("attachment;filename={0}", fileName + ".xls"));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //   Create a table to contain the grid
                    Table table = new Table();

                    //   include the gridline settings
                    table.GridLines = gv.GridLines;

                    //   add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        ExcelExport.PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //   add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        ExcelExport.PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //   add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        ExcelExport.PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //   render the table into the htmlwriter
                    table.RenderControl(htw);

                    //   render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }*/
        }

        /// <summary>
        /// Replace any of the contained controls with literals
        /// </summary>
        /// <param name="control"></param>
        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current
as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current
as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current
as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current
as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current
as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    ExcelExport.PrepareControlForExport(current);
                }
            }
        }

    }
}
      