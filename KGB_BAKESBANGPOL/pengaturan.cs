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
    public partial class pengaturan : Form
    {
        public pengaturan()
        {
            InitializeComponent();
            view();
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

        String username;
        //String nip = "";
        public void passValue(String username)
        {
            this.username = username;
            //label8.Text = username;

            //textBox1.Text = user;
        }

        public void view()
        {
            MyConn();
            String sql = "select Nama, Username, Password from admin where Username=@userr";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userr", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                textBox5.Text = rdr[0].ToString();
                textBox4.Text = rdr[1].ToString();
                textBox2.Text = rdr[2].ToString();
                //textBox3.Text = rdr[1].ToString();
                //textBox1.Text = rdr[2].ToString();
                // textBox4.Text = rdr.GetValue(0).ToString();
            }
            rdr.Close();
            conn.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Home x = new Home();
            x.passValue(username);
            this.Hide();
            x.Show();
        }


        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login keluar = new Login();
            this.Hide();
            keluar.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InputData entryData = new InputData();
            entryData.passValue(username);
            this.Hide();
            entryData.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Home beranda = new Home();
            beranda.passValue(username);
            beranda.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Home beranda = new Home();
            beranda.passValue(username);
            beranda.Show();
            this.Hide();
        }

        private void simpan_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                MyConn();
                string query = "UPDATE admin SET Nama=@name, Password=@pass, Username=@user WHERE Nip=@nip";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nip", textBox1.Text);
                cmd.Parameters.AddWithValue("@user", textBox4.Text);
                cmd.Parameters.AddWithValue("@name", textBox5.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);
                // cmd.Parameters.AddWithValue("@nip", nip);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil disimpan");
                    Login log = new Login();
                    log.Show();
                    this.Hide();
                
            }

            else
            {
                MessageBox.Show("Password Tidak Sesuai");

            }
            conn.Close();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {
            Login outt = new Login();
            this.Hide();
            outt.Show();
        }
    }
}
