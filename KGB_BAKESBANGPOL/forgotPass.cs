using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KGB_BAKESBANGPOL
{
    public partial class forgotPass : Form
    {
        public forgotPass()
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

        private void simpan_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
            }
            else if (textBox2.Text == textBox3.Text)
            {
                MyConn();
                string sql = "SELECT nip FROM admin WHERE nip=@nippp";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nippp", textBox1.Text);
                string query = "UPDATE admin SET Password=@pass WHERE nip=@nippp";
                MySqlCommand cmd1 = new MySqlCommand(query, conn);
                cmd1.Parameters.AddWithValue("@nippp", textBox1.Text);
                cmd1.Parameters.AddWithValue("@pass", textBox2.Text);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    conn.Close();
                    MyConn();
                    cmd1.ExecuteNonQuery();
                    DialogResult result = MessageBox.Show("Berhasil", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                    if (result == DialogResult.OK)
                    {
                        Login h = new Login();
                        h.Show();
                        this.Hide();
                    }
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("akun tidak ditemukan, NIP yang dimasukkan tidak sesuai");
                    rdr.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Password tidak sesuai");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
