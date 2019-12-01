using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KGB_BAKESBANGPOL
{
    public partial class pemberitahuan : Form
    {
        public pemberitahuan()
        {
            InitializeComponent();
            masakerja();
        }

        MySqlConnection conn = new MySqlConnection();

        public void MyConn()
        {
            String connString;
            connString = "server=127.0.0.1;uid=root;pwd=;database=bang;SslMode=none";
            conn.ConnectionString = connString;
            conn.Open();
        }

        public void masakerja()
        {
            MyConn();
            DateTime tgl;
            int totalBulan, xx;
            //String name = "inayah";
            DateTime Day = DateTime.Now;
            string queryy = "SELECT a.nip, b.nama, a.tglmsk, a.pangkat, a.jabatan, b.status FROM datask a JOIN datapegawai b ON a.nip = b.nip where b.status = @peg";
            MySqlCommand cmd = new MySqlCommand(queryy, conn);
            cmd.Parameters.AddWithValue("@peg", "Pegawai");
            //cmd.Parameters.AddWithValue("@n", name);
            MySqlDataReader r = cmd.ExecuteReader();
            if (r.HasRows)
            {
                while (r.Read())
                {
                    tgl = Convert.ToDateTime(r[2]);
                    TimeSpan span = Day.Subtract(tgl);
                    xx = (int)span.TotalDays;
                    totalBulan = xx / 30;
                    int hasil = totalBulan % 24;
                    textBox3.Text = hasil.ToString();
                    //textBox2.Text = hasil.ToString();
                    textBox2.Text = hasil.ToString();
                    if (hasil == 22)
                    {
                        ListViewItem li = new ListViewItem(r[0].ToString());
                        li.SubItems.Add(r[1].ToString());
                        li.SubItems.Add(r[3].ToString());
                        li.SubItems.Add(r[4].ToString());
                        li.SubItems.Add(r[5].ToString());
                        listKGB.Items.Add(li);
                    }
                    //else
                    //{
                    //    MessageBox.Show("Data tidak ditemukan");
                    //    break;
                    //}

                }
                r.Close();
                
                
            }
           
        }

        private void listKGB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home beranda = new Home();
            beranda.passValue(username);
            this.Hide();
            beranda.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login keluar = new Login();
            this.Hide();
            keluar.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tambahAdmin tambah = new tambahAdmin();
            tambah.passValue(username);
            this.Hide();
            tambah.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            caridata cari = new caridata();
            cari.passValue(username);
            this.Hide();
            cari.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InputData entryData = new InputData();
            entryData.passValue(username);
            this.Hide();
            entryData.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pemberitahuan x = new pemberitahuan();
            x.passValue(username);
            x.Show();
            this.Hide();

        }

        private void pemberitahuan_Load(object sender, EventArgs e)
        {
            listKGB.Columns.Add("NIP", 150);
            listKGB.Columns.Add("NAMA", 150);
            listKGB.Columns.Add("PANGKAT", 150);
            listKGB.Columns.Add("JABATAN", 150);
            listKGB.Columns.Add("STATUS", 150);
        }

        private void tampilkan_Click(object sender, EventArgs e)
        {

            if (listKGB.SelectedItems.Count > 0)
            {
                ListViewItem pilih = listKGB.SelectedItems[0];
                //textBox6.Text = pilih.SubItems[1].Text;
                textBox4.Text = pilih.SubItems[0].Text;
                //textBox1.Text = pilih.SubItems[1].Text;
                
                //masakerja(); parameter
                FormKGB kgb = new FormKGB();
                //kgb.view(textBox1.Text);
                kgb.passValue(textBox4.Text, label4.Text);
                kgb.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Pilih salah satu data untuk di proses");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Login outt = new Login();
            outt.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pengaturan x = new pengaturan();
            x.passValue(username);
            x.view();
            this.Hide();
            x.Show();
        }

        String username;

        public void passValue(String username)
        {
            this.username = username;
            label4.Text = username;
        }

        private void listKGB_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
    //    foreach (ListViewItem item in listView1.Items)

    //{

    //     //Do whatever

    //}
}
