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
using System.IO;

namespace KGB_BAKESBANGPOL
{
    public partial class editData : Form
    {
        public editData()
        {
            InitializeComponent();
            //allData();
        }

        MySqlConnection conn = new MySqlConnection();

        public void MyConn()
        {
            String connString;
            connString = "server=127.0.0.1;uid=root;pwd=;database=bang;SslMode=none";
            conn.ConnectionString = connString;
            conn.Open();
        }

        public void allData(String nipp)
        {
            MyConn();
            string que = "SELECT a.nama, a.nip, a.tmptlhr, a.pendidikan, b.pangkat, b.jabatan, b.tmptkerja, b.gajipokok, b.terbilang, b.tglmsk, a.tgllhr, b.tglsk FROM datask b JOIN datapegawai a ON b.nip = a.nip where a.nip = @nipp";
            MySqlCommand cmd2 = new MySqlCommand(que, conn);
            cmd2.Parameters.AddWithValue("@nipp", nipp);
            MySqlDataReader rdr = cmd2.ExecuteReader();
            while (rdr.Read())
            {
                nama.Text = rdr[0].ToString();
                nip.Text = rdr[1].ToString();
                tempat_lahir.Text = rdr[2].ToString();
                pendidikan.Text = rdr[3].ToString();
                pangkat.Text = rdr[4].ToString();
                jabatan.Text = rdr[5].ToString();
                tempat_Kerja.Text = rdr[6].ToString();
                gaji.Text = rdr[7].ToString();
                terbilang.Text = rdr[8].ToString();
                tgl_mulai.Value = (DateTime)rdr[9];
                tgl_lhr.Value = (DateTime)rdr[10];
                tgl_sk.Value = (DateTime)rdr[11];
            }
            rdr.Close();
            conn.Close();
        }

        String username;

        public void passValue(String username)
        {
            this.username = username;
            //label12.Text = username;
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
            InputData entryData = new InputData();
            entryData.passValue(username);
            this.Hide();
            entryData.Show();
        }
        
        private void pictureBox7_Click_1(object sender, EventArgs e)
        {
            Login outt = new Login();
            outt.Show();
            this.Hide();
        }
        byte[] a;
        private void simpan_Click(object sender, EventArgs e)
        {

            String name = nama.Text;
            String nipp = nip.Text;
            String tmpt = tempat_lahir.Text;
            //String tgl = tgl_lhr.Text;
            String pend = pendidikan.Text;
            //String tglM = tgl_mulai.Text;
            String pangkatt = pangkat.Text;
            String jabatann = jabatan.Text;
            String tmptK = tempat_Kerja.Text;
            String gajii = gaji.Text;
            String terbil = terbilang.Text;
            var tgl3 = this.tgl_sk.Value.ToString("yyyy-MM-dd");
            var tgl2 = this.tgl_lhr.Value.ToString("yyyy-MM-dd");
            var tgl1 = this.tgl_mulai.Value.ToString("yyyy-MM-dd");


            //if (openFileDialog1.FileName == null)
            //{
            MyConn();
            String q = "UPDATE datask a join datapegawai b ON a.nip = b.nip  SET b.nama=@n, b.tmptlhr=@lhr, b.tgllhr=@tgllhr, b.pendidikan=@pend, a.scan=@scan, a.nip=@nip, a.tglmsk=@tglmsk, a.pangkat=@pangkat, a.jabatan=@jabatan, a.tmptkerja=@tmptkrja, a.gajipokok=@gaji, a.terbilang=@terbilang, a.tglsk=@tgl_sk where a.nip=@NIP ";
            MySqlCommand cmd = new MySqlCommand(q, conn);
            try
            {
                cmd.Parameters.AddWithValue("@Scan", System.IO.File.ReadAllBytes(openFileDialog1.FileName));
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@lhr", tmpt);
                cmd.Parameters.AddWithValue("@tgllhr", tgl2);
                cmd.Parameters.AddWithValue("@pend", pend);
                cmd.Parameters.AddWithValue("@nip", nipp);
                cmd.Parameters.AddWithValue("@tglmsk", tgl1);
                cmd.Parameters.AddWithValue("@pangkat", pangkatt);
                cmd.Parameters.AddWithValue("@jabatan", jabatann);
                cmd.Parameters.AddWithValue("@tmptkrja", tmptK);

                cmd.Parameters.AddWithValue("@gaji", gajii);
                cmd.Parameters.AddWithValue("@terbilang", terbil);
                cmd.Parameters.AddWithValue("@tgl_sk", tgl3);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Edit profil berhasil!");
                caridata c = new caridata();
                c.Show();
                this.Hide();
            }
            catch(Exception)
            {
                MessageBox.Show("Data SK belum diinputkan");
            }
            
           
            //cmd.Parameters.AddWithValue("@tglmsk", tglM);
            conn.Close();

            //}
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPEGs|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label5.Text = openFileDialog1.FileName;

            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Home home = new Home();
            home.passValue(username);
            this.Hide();
            home.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            InputData add = new InputData();
            add.passValue(username);
            this.Hide();
            add.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pengaturan x = new pengaturan();
            x.passValue(username);
            x.view();
            this.Hide();
            x.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            caridata cari = new caridata();
            this.Hide();
            cari.Show();
        }
    }

}
