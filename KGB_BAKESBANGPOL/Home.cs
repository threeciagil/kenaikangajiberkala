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

        private void label2_Click(object sender, EventArgs e)
        {
            //MyConn();
            //String sql = "SELECT Nama FROM admin WHERE username=@usern";
            //MySqlCommand cmd = new MySqlCommand(sql, conn);
            //cmd.Parameters.AddWithValue("@usern", label2.Text);
            //MySqlDataReader rdr = cmd.ExecuteReader();
            //while (rdr.Read())
            //{
            //    username = rdr[0].ToString();
            //}
            //rdr.Close();
            //conn.Close();
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

        private void caridata_Click(object sender, EventArgs e)
        {
            caridata cari = new caridata();
            cari.passValue(username);
            this.Hide();
            cari.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tambahAdmin add = new tambahAdmin();
            add.passValue(username);
            this.Hide();
            add.Show();
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

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //MyConn();
            //string shortDateString = DateTime.Now.Date.ToShortDateString();
            //String name = "inayah";

            ////label18.Text = shortDateString;
            //string sql = "SELECT a.tglmsk, b.nama, b.nip FROM datask a JOIN datapegawai b ON a.nip = b.nip where b.nama = @user";
            //MySqlCommand cmd = new MySqlCommand(sql, conn);
            //cmd.Parameters.AddWithValue("@user", name);
            //MySqlDataReader rdr = cmd.ExecuteReader();
            //while (rdr.Read())
            //{
            //    label16.Text = rdr[0].ToString();
            //    label18.Text = rdr[1].ToString();
            //    DateTime start = DateTime.Parse(label16.Text);
            //    DateTime end = DateTime.Parse(shortDateString);
            //    TimeSpan dtt = end.Date - start.Date;
            //    double total = dtt.TotalDays;
            //    int a = Convert.ToInt32(total);
            //    int totall = (a / 30);
            //    int x = totall % 24;
            //    label3.Text = x.ToString();
            //    if (x == 23)
            //    {
            //        DataTable dt = new DataTable();
            //        dt.Load(rdr);
            //        datakgb.DataSource = dt;

            //    }

            //}
            //conn.Close();
        }

        void main()
        {
            MyConn();
            string shortDateString = DateTime.Now.Date.ToShortDateString();
            
            //label18.Text = shortDateString;
            string sql = "SELECT b.nama, b.nip, a.pangkat, a.tglmsk FROM datask a JOIN datapegawai b ON a.nip = b.nip where b.status = @peg";
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

        private void label5_Click(object sender, EventArgs e)
        {


        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        public void nana()
        {
            MyConn();
            //int index = 1;
            string sql = "SELECT Count(*) id_pegawai from datapegawai where status = @peg";
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
        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pemberitahuan_Click(object sender, EventArgs e)
        {
            pemberitahuan x = new pemberitahuan();
            x.passValue(username);
            x.Show();
            this.Hide();
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

        

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}




