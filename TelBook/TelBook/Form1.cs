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
            myConnection.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpgradeDataView();
        }
        
        private void UpgradeDataView() //Uaktalnienie wyswietlanych danych
        {
            var dataView = new DataTable();
            string displayCommand = "Select * From persons";
            SQLiteDataAdapter myAdapter = new SQLiteDataAdapter(displayCommand, myConnection);
            myAdapter.Fill(dataView);
            dataGridView1.DataSource = dataView;
        }
        
        private void saveButton_Click(object sender, EventArgs e)//Zapis danych do bazy
        {
            SQLiteCommand myCommand = new SQLiteCommand(myConnection);
            myCommand.CommandText = "Insert Into persons (FirstName,LastName,Address,Email,PhoneNumber) values('" + firstNameTextBox.Text + "','" + lastNameBextBox.Text + "','" + addressTextBox.Text + "','" + emailTextBox.Text + "','" + phoneNumberTextBox.Text + "')";
            myCommand.ExecuteNonQuery();
            UpgradeDataView();
            MessageBox.Show("Dane Zapisano", "Zapis Poprawny", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e) //Ustawia dane do edycji w textobxach
        {
            var dataTable = new DataTable();
            string actualPositionOnDataGrid = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            string displayData = "Select * From persons Where ID=" + actualPositionOnDataGrid;
            SQLiteDataAdapter myAdapter = new SQLiteDataAdapter(displayData, myConnection);
            myAdapter.Fill(dataTable);


            customTextbox.Text = dataTable.Rows[0]["ID"].ToString();
            firstNameTextBox.Text = dataTable.Rows[0]["FirstName"].ToString();
            lastNameBextBox.Text = dataTable.Rows[0]["LastName"].ToString();
            addressTextBox.Text = dataTable.Rows[0]["Address"].ToString();
            emailTextBox.Text = dataTable.Rows[0]["Email"].ToString();
            phoneNumberTextBox.Text = dataTable.Rows[0]["Email"].ToString();
            editAcceptButton.Visible = true;


        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)//kasowanie rekordu
        {
            int myActualPositionOnDataGridView = int.Parse(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            SQLiteCommand myCommand = new SQLiteCommand(myConnection);
            myCommand.CommandText = "Delete From persons where ID=" + myActualPositionOnDataGridView;
            myCommand.ExecuteNonQuery();
            UpgradeDataView();
            MessageBox.Show("Rekord został usunięty", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void editAcceptButton_Click(object sender, EventArgs e)
        {
            EditMethod();
        }

        private void EditMethod()// motoda edytujaca dane w bazie
        {
            SQLiteCommand myCommand = new SQLiteCommand(myConnection);
            myCommand.CommandText = "Update persons Set FirstName='" + firstNameTextBox.Text + "',LastName='" + lastNameBextBox.Text + "',Address='" + addressTextBox.Text + "',Email='" + emailTextBox.Text + "',PhoneNumber='" + phoneNumberTextBox.Text + "' Where ID='" + customTextbox.Text + "'";
            myCommand.ExecuteNonQuery();
            UpgradeDataView();
            MessageBox.Show("Rekord poprawiono", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            editAcceptButton.Visible = false;
        }
    }
}
