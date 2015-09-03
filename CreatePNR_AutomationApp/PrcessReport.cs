using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CreatePNR_AutomationApp
{
    public partial class frmProcessReport : Form
    {
        public List<PNRsProcessed> DSPNRsProcessed { get; set; }
        public int Failed { get; set; }
        public int Processed { get; set; }
        public int Successful { get; set; }
        public frmProcessReport()
        {
            InitializeComponent();
            this.Load += frmPrcessReport_Load;
        }

        void frmPrcessReport_Load(object sender, EventArgs e)
        {
            lblFailed.Text = Convert.ToString(Failed);
            lblProcessed.Text = Convert.ToString(Processed);
            lblSuccessful.Text = Convert.ToString(Successful);
            //DSPNRsProcessed = new List<PNRsProcessed>();
            //DSPNRsProcessed.Add(new PNRsProcessed() { DepartureDate=DateTime.Now,Destination="asd",LastName="sdf",Origin="sdf",PNR="sdf",ReturnDate=null});

            PNRsProcessedBindingSource.DataSource = DSPNRsProcessed;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new XmlSerializer instance with the type of the test class
            //XmlSerializer SerializerObj = new XmlSerializer(typeof(List<PNRsProcessed>));
            StreamWriter WriteFileStream;
            // Create a new file stream to write the serialized object to a file
            string path = Environment.CurrentDirectory + @"\Report\";
            StringBuilder logfilename = new StringBuilder();
            logfilename.Append(path);

            logfilename.Append(String.Format("{0:0000}", DateTime.Now.Year));
            logfilename.Append("_");
            logfilename.Append(String.Format("{0:00}", DateTime.Now.Month));
            logfilename.Append("_");
            logfilename.Append(String.Format("{0:00}", DateTime.Now.Day));
            logfilename.Append("_");
            logfilename.Append(String.Format("{0:00}", DateTime.Now.Hour));
            logfilename.Append("_");
            logfilename.Append(String.Format("{0:00}", DateTime.Now.Minute));
            logfilename.Append("_");
            logfilename.Append(String.Format("{0:00}", DateTime.Now.Second));
            logfilename.Append(".csv");

            if (!File.Exists(logfilename.ToString()))
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                WriteFileStream = new StreamWriter(logfilename.ToString());
            }
            else
            {
                WriteFileStream = File.AppendText(logfilename.ToString());
            }


            //SerializerObj.Serialize(WriteFileStream, DSPNRsProcessed);

            DataTable dt = ClassToDatatable.ToDataTable(DSPNRsProcessed);
            ExportDatatableToCsv(WriteFileStream, dt);
            // Cleanup
            // WriteFileStream.Close();
            System.Diagnostics.Process.Start(logfilename.ToString());

        }
        private void ExportDatatableToCsv(StreamWriter swFile, DataTable dt)
        {
            // Open output stream
            //StreamWriter swFile = new StreamWriter(iFilename);

            // Header
            string[] colLbls = new string[dt.Columns.Count];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colLbls[i] = dt.Columns[i].ColumnName;
                colLbls[i] = GetWriteableValueForCsv(colLbls[i]);
            }

            // Write labels
            swFile.WriteLine(string.Join(",", colLbls));

            // Rows of Data
            foreach (DataRow rowData in dt.Rows)
            {
                string[] colData = new string[dt.Columns.Count];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    object obj = rowData[i];
                    if (obj is DateTime || obj is DateTime?)
                    {
                        if (Convert.ToDateTime(obj) == DateTime.MinValue)
                        {
                            obj = null;
                        }
                    }
                    colData[i] = GetWriteableValueForCsv(obj);
                }

                // Write data in row
                swFile.WriteLine(string.Join(",", colData));
            }

            // Close output stream
            swFile.Close();
        }

        public static string GetWriteableValueForCsv(object obj)
        {
            // Nullable types to blank
            if (obj == null || obj == Convert.DBNull)
                return "";

            // if string has no ','
            if (obj.ToString().IndexOf(",") == -1)
                return obj.ToString();

            // remove backslahes
            return "\"" + obj.ToString() + "\"";
        }

    }
}
