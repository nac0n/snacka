using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatclient
{
    public partial class LoginForm : Form
    {

        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
        }
     

        private void Login_Click(object sender, EventArgs e)
        {
            string password = textBox2.Text.Trim();
            string hashed = Hash(password);
            var path = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
            Console.WriteLine("****"+path);
            path = path + @"\db\snackaDb.mdf";

            //C:\Users\Jessica\snacka\db\snackaDb.mdf
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+path+";Integrated Security=True;Connect Timeout=30");
            string query = "SELECT * from login Where username = '" + textBox1.Text.Trim() + "' and password = '"+ hashed + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if(dtbl.Rows.Count == 1)
            {
                Form1 chat = new Form1();
                this.Hide();
                chat.Show();
            }
            else
            {
                MessageBox.Show("Wrong Password, try again");
            }

        }

        private void GetHashCode(string password)
        {
            throw new NotImplementedException();
        }
    }
}
