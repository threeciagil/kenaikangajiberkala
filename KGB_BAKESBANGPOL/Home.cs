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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            nana();
            main();
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

        public void passValue(String username)
        {
            this.username = username;
            //label2.Text = username;
            MyConn();
            String sql = "SELECT Nama FROM admin WHERE username=@usern";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usern", this.username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                label2.Text = rdr[0].ToString();
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home x = new Home();
            x.passValue(username);
            this.Hide();
            x.Show();
        }

        private void InputData_Click(object sender, EventArgs e)
        {
            InputData xx = new InputData();
            xx.passValue(username);
            this.Hide();
            xx.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login outt = new Login();
            this.Hide();
            outt.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pengaturan x = new pengaturan();
            x.passValue(username);
            x.view();
            this.Hide();
            x.Show();
        }

        void main()
        {
            MyConn();
            string shortDateString = DateTime.Now.Date.ToShortDateString();
            
            //label18.Text = shortDateString;
            string sql = "SELECT b.nama, b.nip, a.pangkat, a.tglmsk FROM datask a JOIN datapegawai b ON a.nip = b.nip";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@peg", "Pegawai");
            //cmd.Parameters.AddWithValue("@user", name);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                    {
                    label16.Text = rdr[3].ToString();
                    //label18.Text = rdr[1].ToString();
                    DateTime start = DateTime.Parse(label16.Text);
                    DateTime end = DateTime.Parse(shortDateString);
                    TimeSpan dtt = end.Date - start.Date;
                    double total = dtt.TotalDays;
                    int a = Convert.ToInt32(total);
                    int totall = (a / 30);
                    int x = totall % 24;
                    label7.Text = dtt.ToString();
                    //label7.Text = x.ToString();
                    if (x == 22)
                    {
                        ListViewItem li = new ListViewItem(rdr[0].ToString());
                        li.SubItems.Add(rdr[1].ToString());
                        li.SubItems.Add(rdr[2].ToString());
                        kgb.Items.Add(li);
                    }
                    
                }
                rdr.Close();

            }
            
            conn.Close();
        }

        public void nana()
        {
            MyConn();
            //int index = 1;
            string sql = "SELECT Count(*) id_pegawai from datapegawai";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@peg", "Pegawai");
            //cmd.Parameters.AddWithValue("@usern", index);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                label6.Text = rdr[0].ToString();
            }
            rdr.Close();
            conn.Close();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            Login outt = new Login();
            this.Hide();
            outt.Show();
        }

        private void kgb_SelectedIndexChanged(object sender, EventArgs e)
        {
            kgb.Columns.Add("NIP", 150);
            kgb.Columns.Add("NAMA", 150);
        }

    }
}




