using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KGB_BAKESBANGPOL
{
    public partial class caridata : Form
    {
        public caridata()
        {
            InitializeComponent();
            showAll();

        }

        MySqlConnection conn = new MySqlConnection();

        public void MyConn()
        {
            String connString;
            connString = "server=127.0.0.1;uid=root;pwd=;database=bang;SslMode=none";
            conn.ConnectionString = connString;
            conn.Open();
        }

        private void cari_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                listPegawai.Items.Clear();
                string query = "SELECT a.nip, b.nama, a.pangkat, b.pendidikan, a.jabatan, a.gajipokok, b.tmptlhr, b.tgllhr, b.status FROM datask a JOIN datapegawai b ON b.nip = a.nip where a.nip like '%" + cari.Text + "%'";
                //string query = "SELECT nip, golongan, pangkat FROM datask Where Nip like '%" + cari.Text + "%'";
                try
                {
                    MyConn();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem listViewItem = new
                        ListViewItem(reader[0].ToString());
                        listViewItem.SubItems.Add(reader[1].ToString());
                        listViewItem.SubItems.Add(reader[2].ToString());
                        listViewItem.SubItems.Add(reader[3].ToString());
                        listViewItem.SubItems.Add(reader[4].ToString());
                        listViewItem.SubItems.Add(reader[5].ToString());
                        listViewItem.SubItems.Add(reader[6].ToString());
                        listViewItem.SubItems.Add(reader[7].ToString());
                        listViewItem.SubItems.Add(reader[8].ToString());
                        listPegawai.Items.Add(listViewItem);
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (radioButton2.Checked == true)
            {
                listPegawai.Items.Clear();
                string query1 = "SELECT a.nip, b.nama, a.pangkat, b.pendidikan, a.jabatan, a.gajipokok, b.tmptlhr, b.tgllhr, b.status FROM datask a JOIN datapegawai b ON b.nip = a.nip where b.nama like '%" + cari.Text + "%'";
                //"SELECT datask.nip, datapegawai.nama, datask.golongan, datask.pangkat FROM datask, datapegawai where datask.nip = like '%" + cari.Text + "%' AND datapegawai.nip = like '%" + cari.Text + "%'"
                try
                {
                    MyConn();
                    MySqlCommand cmd = new MySqlCommand(query1, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem listViewItem = new
                        ListViewItem(reader[0].ToString());
                        listViewItem.SubItems.Add(reader[1].ToString());
                        listViewItem.SubItems.Add(reader[2].ToString());
                        listViewItem.SubItems.Add(reader[3].ToString());
                        listViewItem.SubItems.Add(reader[4].ToString());
                        listViewItem.SubItems.Add(reader[5].ToString());
                        listViewItem.SubItems.Add(reader[6].ToString());
                        listViewItem.SubItems.Add(reader[7].ToString());
                        listViewItem.SubItems.Add(reader[8].ToString());
                        listPegawai.Items.Add(listViewItem);
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Pilih Pencarian Berdasarkan Nip atau Nama");
            }
        }

        private void showAll()
        {
            listPegawai.Items.Clear();
            string query = "SELECT a.nip, b.nama, a.pangkat, b.pendidikan, a.jabatan, a.gajipokok, b.tmptlhr, b.tgllhr, b.status FROM datask a JOIN datapegawai b ON b.nip = a.nip GROUP BY b.nama";// INNER JOIN nama ON Nip like '%" + cari.Text + "%'";
            try
            {
                // Open the database
                MyConn();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ListViewItem listViewItem = new
                        ListViewItem(reader[0].ToString());
                        listViewItem.SubItems.Add(reader[1].ToString());
                        listViewItem.SubItems.Add(reader[2].ToString());
                        listViewItem.SubItems.Add(reader[3].ToString());
                        listViewItem.SubItems.Add(reader[4].ToString());
                        listViewItem.SubItems.Add(reader[5].ToString());
                        listViewItem.SubItems.Add(reader[6].ToString());
                        listViewItem.SubItems.Add(reader[7].ToString());
                        listViewItem.SubItems.Add(reader[8].ToString());
                        listPegawai.Items.Add(listViewItem);
                    }

                }
                reader.Close();
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


        private void cari_Enter(object sender, EventArgs e)
        {
            cari.Text = "";
            cari.ForeColor = Color.Black;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InputData add = new InputData();
            add.passValue(username);
            this.Hide();
            add.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tambahAdmin tambah = new tambahAdmin();
            tambah.passValue(username);
            this.Hide();
            tambah.Show();
        }

        String username;

        public void passValue(String username)
        {
            this.username = username;
            //label2.Text = username;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.passValue(username);
            this.Hide();
            home.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login outt = new Login();
            this.Hide();
            outt.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            caridata cari = new caridata();
            cari.passValue(username);
            this.Hide();
            cari.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPegawai.SelectedItems.Count > 0)
            {
                ListViewItem pilih = listPegawai.SelectedItems[0];
                textBox1.Text = pilih.SubItems[0].Text;
                //string BarisEdit = listPegawai.FocusedItem.Index;
            }
        }

        //String username;

        //public void passValue(String username)
        //{
        //    this.username = username;
        //    label2.Text = username;
        //}

        private void hapus_Click(object sender, EventArgs e)
        {
            if (listPegawai.SelectedItems.Count > 0)
            {
                ListViewItem pilih = listPegawai.SelectedItems[0];
                textBox1.Text = pilih.SubItems[0].Text;
                DialogResult result = MessageBox.Show("Apakah anda yakin pegawai dipidahkan?", "Warning",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string query = "Update datapegawai SET status = @status WHERE nip=@nip";
                    MyConn();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nip", textBox1.Text);
                    cmd.Parameters.AddWithValue("@status", "Pindah");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil");
                    conn.Close();
                    showAll();
                }

                else if (result == DialogResult.No)
                {
                    MessageBox.Show("Proses dibatalkan");
                }
            }
            else
            {
                MessageBox.Show("Harap pilih salah satu data");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            pemberitahuan notif = new pemberitahuan();
            notif.passValue(username);
            notif.Show();
            this.Hide();
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

        private void edit_Click(object sender, EventArgs e)
        {
            editData edit = new editData();
            if (listPegawai.SelectedItems.Count > 0)
            {
                ListViewItem pilih = listPegawai.SelectedItems[0];
                textBox1.Text = pilih.SubItems[0].Text;
                edit.allData(textBox1.Text);
                edit.passValue(username);
                edit.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Pilih salah satu data untuk mengedit");
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            pemberitahuan x = new pemberitahuan();
            x.passValue(username);
            x.Show();
            this.Hide();
        }

        private void caridata_Load(object sender, EventArgs e)
        {
            listPegawai.Columns.Add("NIP", 120);
            listPegawai.Columns.Add("NAMA", 120);
            listPegawai.Columns.Add("PANGKAT", 120);
            listPegawai.Columns.Add("PENDIDIKAN", 120);

            listPegawai.Columns.Add("JABATAN", 120);
            listPegawai.Columns.Add("GAJI POKOK", 120);
            listPegawai.Columns.Add("TEMPAT LAHIR", 120);
            listPegawai.Columns.Add("TANGGAL LAHIR", 120);
            listPegawai.Columns.Add("STATUS", 120);
        }
    }

}

