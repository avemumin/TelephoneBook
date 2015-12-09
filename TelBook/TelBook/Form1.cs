using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace TelBook
{
    public partial class Form1 : Form
    {

        SQLiteConnection myConnection = new SQLiteConnection(@"Data Source=E:\ZAPISANE\DataBases\PB\DaneOsob.db");
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpgradeDataView();
        }
        /// <summary>
        /// Uaktalnianie widoku 
        /// </summary>
        private void UpgradeDataView()
        {
            var dataView = new DataTable();
            string displayCommand = "Select * From persons";
            SQLiteDataAdapter myAdapter = new SQLiteDataAdapter(displayCommand, myConnection);
            myAdapter.Fill(dataView);
            dataGridView1.DataSource = dataView;
        }
    }
}
