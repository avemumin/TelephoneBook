using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
namespace TelBook
{
    public partial class Menu : Form
    {
        private string name;

        public string Names
        {
            get { return name; }
            set { name = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        SQLiteConnection passConnection = new SQLiteConnection(@"Data Source=E:\ZAPISANE\DataBases\PersonalPassword\loginID.db");
        Form1 forma;
        public Menu()
        {
            InitializeComponent();
            passConnection.Open();
            forma = new Form1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = new DataTable();
            // SQLiteCommand myCommand = new SQLiteCommand(passConnection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("Select * from logins", passConnection);
            adapter.Fill(data);
             Names = data.Rows[0]["LoginName"].ToString();
             Password = data.Rows[0]["UsersPassword"].ToString();

            if ((Names == textBox1.Text) && (Password == textBox2.Text))
            {
                forma.Show();
                this.Hide();
            }
            else
            {
                textBox1.BackColor = Color.Red;
                textBox2.BackColor = Color.Red;
                MessageBox.Show("Zle haslo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
