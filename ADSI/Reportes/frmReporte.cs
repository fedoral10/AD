using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADSI.Reportes
{
    public partial class frmReporte : Form
    {

        ReportDataSource rds;
        public frmReporte(DataTable ds)
        {
            InitializeComponent();
            rptViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            LocalReport r = rptViewer.LocalReport;
            r.ReportPath = "Reportes\\rptEntidades.rdlc";
            rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = ds;
            r.DataSources.Add(rds);
            rptViewer.RefreshReport();
            //rptViewer.Show();
        }

        private void frmReporte_Load(object sender, EventArgs e)
        {

            this.rptViewer.RefreshReport();
        }

        public void exportXLS()
        {
            // Setup DataSet
            //MyDataSetTableAdapters.YourTableAdapterHere ds = new MyDataSetTableAdapters.YourTableAdapterHere();


            // Create Report DataSource
            //ReportDataSource rds = this.rds;


            // Variables
           


            // Setup the report viewer object and get the array of bytes
            /*ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "YourReportHere.rdlc";
            viewer.LocalReport.DataSources.Add(rds); // Add datasource here
          */

           

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivo de Excel (*.xls)|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                
                byte[] bytes = rptViewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (FileStream stream = System.IO.File.Create(sfd.FileName))
                {
                    //  byte[] byteArray = Convert.FromBase64String(base64BinaryStr);
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            /*Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download*/

        }

    }
}
