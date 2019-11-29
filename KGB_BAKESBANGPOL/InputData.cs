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
    public partial class InputData : Form
    {
        // private static string connectionString = "server=localhost;port=3306;username=root;password='';database=kgb_kesbang; sslmode=none";
       // pemberitahuan notif = new pemberitahuan();
        // private MySqlConnection databaseConnection = new MySqlConnection(connectionString);
        MySqlConnection conn = new MySqlConnection();
        public void MyConn()
        {
            String connString;
            connString = "server=127.0.0.1;uid=root;pwd=;database=bang;SslMode=none";
            conn.ConnectionString = connString;
            conn.Open();
        }
        public InputData()
        {
            InitializeComponent();
            //opTanggal();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPEGs|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label5.Text = openFileDialog1.FileName;
            }
            else
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
            }
        }

        private void simpan_Click(object sender, EventArgs e)
        {
            if (nama.Text == "" || nip.Text == "" || tempat_lahir.Text == "" || pendidikan.Text == "" || pangkat.Text == "" || jabatan.Text == "" || gaji.Text == "" || terbilang.Text == "")
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
            }
            else
            {
                string query1 = "INSERT INTO datapegawai (nama, nip, tmptlhr, tgllhr, pendidikan) VALUES (@Nama, @Nip, @tempat_lahir, @tanggal_lahir, @pendidikan)";
                string query2 = "INSERT INTO datask (nip, tglmsk, pangkat, jabatan, tmptkerja, gajipokok, terbilang, scan, tglsk) VALUES (@Nip, @tanggal_masuk, @pangkat, @jabatan, @tempat_kerja, @gaji_pokok, @Terbilang, @scan_sk, @tglsk)";
                try
                {
                    // Open the database
                    //databaseConnection.Open();
                    MyConn();
                    MySqlCommand cmd1 = new MySqlCommand(query1, conn);
                    cmd1.CommandTimeout = 60;
                    cmd1.Parameters.AddWithValue("@Nama", nama.Text);
                    cmd1.Parameters.AddWithValue("@Nip", nip.Text);
                    cmd1.Parameters.AddWithValue("@tempat_lahir", tempat_lahir.Text);
                    var tgl1 = this.tgl_lhr.Value.ToString("yyyy-MM-dd");
                    cmd1.Parameters.AddWithValue("@tanggal_lahir", tgl1);
                    cmd1.Parameters.AddWithValue("@pendidikan", pendidikan.Text);
                    cmd1.ExecuteNonQuery();
                    cmd1.Parameters.Clear();
                    MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                    cmd2.CommandTimeout = 60;
                    cmd2.Parameters.AddWithValue("@Nip", nip.Text);
                    var tgl2 = this.tgl_mulai.Value.ToString("yyyy-MM-dd");
                    cmd2.Parameters.AddWithValue("@tanggal_masuk", tgl2);
                    cmd2.Parameters.AddWithValue("@pangkat", pangkat.Text);
                    cmd2.Parameters.AddWithValue("@jabatan", jabatan.Text);
                    cmd2.Parameters.AddWithValue("@tempat_kerja", tempat_Kerja.Text);
                    cmd2.Parameters.AddWithValue("@gaji_pokok", this.gaji.Text);
                    cmd2.Parameters.AddWithValue("@Terbilang", terbilang.Text);
                    cmd2.Parameters.AddWithValue("@scan_sk", System.IO.File.ReadAllBytes(openFileDialog1.FileName));
                    var tgl3 = this.tgl_sk.Value.ToString("yyyy-MM-dd");
                    cmd2.Parameters.AddWithValue("@tglsk", tgl3);
                    cmd2.ExecuteNonQuery();
                    cmd2.Parameters.Clear();

                    //MessageBox.Show("Data berhasil disimpan");
                    //caridata c = new caridata();
                    //c.Show();
                    //this.Hide();
                    //notif.opTanggal(dateTimePicker1);
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        String username;

        public void passValue(String username)
        {
            this.username = username;
            //label18.Text = username;
        }

        private void button8_Click(object sender, EventArgs e)
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


        private void button2_Click(object sender, EventArgs e)
        {
            InputData xx = new InputData();
            xx.passValue(username);
            this.Hide();
            xx.Show();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            nama.Text = "";
            nip.Text = "";
            tempat_lahir.Text = "";
            pendidikan.Text = "";
            pangkat.Text = "";
            gaji.Text = "";
            tempat_Kerja.Text = "Badan Kesatuan Bangsa dan Politik";
            gaji.Text = "Rp. ,-";
            terbilang.Text = "";


        }

        private void label13_Click(object sender, EventArgs e)
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

    }
}
