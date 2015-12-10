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
            string name = data.Rows[0]["LoginName"].ToString();
            string password = data.Rows[0]["UsersPassword"].ToString();

            if ((name == textBox1.Text) && (password == textBox2.Text))
            {
                forma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Zle haslo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.BackColor = Color.Red;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
