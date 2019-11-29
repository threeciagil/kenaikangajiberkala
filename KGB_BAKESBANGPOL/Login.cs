using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace KGB_BAKESBANGPOL
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection();

        public void MyConn()
        {
            String connString;
            connString = "server=127.0.0.1;uid=root;pwd=;database=bang;SslMode=none";
            conn.ConnectionString = connString;
            conn.Open();
            // Open DB connection 
            //MessageBox.Show("Database connection successful");
        }

        private void Login_Click(object sender, EventArgs e)
        {
            MyConn();
            String query = "select Username, Password from admin where Username=@usern and Password=@pass";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@usern", username.Text);
            cmd.Parameters.AddWithValue("@pass", password.Text);
            MySqlDataReader rdr = cmd.ExecuteReader();
            String usernameAd = "";
            String passwordAd = "";
            while (rdr.Read())
            {
                usernameAd = rdr[0].ToString();
                passwordAd = rdr[1].ToString();
            }
            if (username.Text.Equals(usernameAd) && password.Text.Equals(passwordAd))
            {
                Home utama = new Home();
                utama.passValue(usernameAd);
                this.Hide();
                utama.Show();
            }
            else
            {
                MessageBox.Show("Maaf, username/password tidak sesuai");
            }
            rdr.Close();
            conn.Close();
        }

        private void password_Enter(object sender, EventArgs e)
        {
            password.Text = "";
            password.ForeColor = Color.Black;
            password.PasswordChar = '*';
        }

        private void username_Enter(object sender, EventArgs e)
        {
            username.Text = "";
            username.ForeColor = Color.Black;
        }

    }
}

