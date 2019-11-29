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
    public partial class tambahAdmin : Form
    {
        public tambahAdmin()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection();

        public void MyConn()
        {
            String connString;
            connString = "server=127.0.0.1;uid=root;pwd=;database=bang;SslMode=none";
            conn.ConnectionString = connString;
            conn.Open(); // Open DB connection 
                         //MessageBox.Show("Database connection successful");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox4.ForeColor = Color.Black;
            textBox4.PasswordChar = '*';
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //tambahAdmin tambah = new tambahAdmin();
            //this.Hide();
            //tambah.Show();
        }

        String user;

        public void passValue(String username)
        {
            this.user = username;
            label5.Text = username;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home beranda = new Home();
            beranda.passValue(user);
            this.Hide();
            beranda.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InputData entryData = new InputData();
            entryData.passValue(user);
            this.Hide();
            entryData.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            caridata cari = new caridata();
            cari.passValue(user);
            this.Hide();
            cari.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pemberitahuan notif = new pemberitahuan();
            notif.passValue(user);
            notif.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login outt = new Login();
            outt.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pengaturan x = new pengaturan();
            x.passValue(user);
            x.view();
            this.Hide();
            x.Show();
        }

        private void textBox1_Enter_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void textBox2_Enter_1(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
        }

        private void textBox3_Enter_1(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
        }

        private void textBox4_Enter_1(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox4.ForeColor = Color.Black;
            textBox4.PasswordChar = '*';
        }

        private void tambah_Click(object sender, EventArgs e)
        {
            MyConn();
            Boolean usern = false;
            Boolean nip = false;
            String username = textBox3.Text;
            String sql = "select Username, Nip From admin";
            MySqlCommand cmd1 = new MySqlCommand(sql, conn);
            MySqlDataReader rdr1 = cmd1.ExecuteReader();
            while (rdr1.Read())
            {
                if (rdr1[0].ToString() == textBox3.Text)
                {
                    usern = true;
                }
                if (rdr1[1].ToString() == textBox2.Text)
                {
                    nip = true;
                }
            }
            rdr1.Close();

            if (usern == false && nip == false)
            {
                String sql2 = "INSERT INTO admin (Nama, NIP, Username, Password) VALUES (@nama, @nip, @user, @pass)";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@nama", textBox1.Text);
                cmd2.Parameters.AddWithValue("@nip", textBox2.Text);
                cmd2.Parameters.AddWithValue("@user", textBox3.Text);
                cmd2.Parameters.AddWithValue("@pass", textBox4.Text);

                cmd2.ExecuteNonQuery();
                MessageBox.Show("Akun Berhasil Ditambah");
                this.Hide();
                Login baru = new Login();
                baru.Show();
            }

            else
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
            }
            conn.Close();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            pemberitahuan x = new pemberitahuan();
            x.passValue(user);
            x.Show();
            this.Hide();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            Login outt = new Login();
            outt.Show();
            this.Hide();
        }
    }
}

