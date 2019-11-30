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
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace KGB_BAKESBANGPOL
{
    public partial class FormKGB : Form
    {
        public FormKGB()
        {
            InitializeComponent();
            viewData();

        }

        MySqlConnection conn = new MySqlConnection();

        public void MyConn()
        {
            String connString;
            connString = "server=127.0.0.1;uid=root;pwd=;database=bang;SslMode=none";
            conn.ConnectionString = connString;
            conn.Open();
        }
        String userame,nip;

        public void passValue(String Nip, String Admin)
        {
            userame = Admin;
            
            nip = Nip;
            View(Nip);

        }

        //"select nama, nip, pangkat, tgllhr, pendidikan from datapegawai joint datask where nip=@NIP"

        public void View(String username)
        {
            try
            {
                //nama.Text = use;
                //label6.Text = use;
                MyConn();
                String sql = "select a.nama, b.tglmsk, b.tglsk from datapegawai a join datask b  ON a.nip = b.nip where a.nip=@NIP";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NIP", username);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    nama.Text = rdr[0].ToString();
                    label6.Text = rdr[0].ToString();
                    tanggal_mulai.Value = (DateTime)rdr[1];
                    tanggal_sk_terakhir.Value = (DateTime)rdr[2];
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public void viewData()
        {
            try
            {
                MyConn();
                String ql = "select nama_badan, nomor_surat, landasan_surat, keterangan, nama_penandatangan, nip_penandatangan, pangkat_penandatangan, jabatan_penandatangan from printsk where keterangan=@ket";
                MySqlCommand cmd = new MySqlCommand(ql, conn);
                cmd.Parameters.AddWithValue("@ket", "Pegawai Negeri Sipil Daerah");
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    pejabat.Text = rdr[0].ToString();
                    nmr_surat.Text = rdr[1].ToString();
                    landasan.Text = rdr[2].ToString();
                    keterangan.Text = rdr[3].ToString();
                    nama_petinggi.Text = rdr[4].ToString();
                    NIP_petinggi.Text = rdr[5].ToString();
                    pangkat_petinggi.Text = rdr[6].ToString();
                    petinggi.Text = rdr[7].ToString();
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void previewSK_Click(object sender, EventArgs e)
        {
            if (pageSetupDialog1.PageSettings == null)
            {

                MessageBox.Show("Ukuran kertas belum di atur \n Pengaturan kertas berada pada Page Setup", "Warning",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                printPreviewDialog1.Document = printDocument1;

                printPreviewDialog1.ShowDialog();
            }

        }
        private void printDocument1_print(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = pageSetupDialog1.PageSettings.PaperSize.Width;
            int y = pageSetupDialog1.PageSettings.PaperSize.Height;
            MyConn();
            string sql = "select scan from datask where nip=@NIP";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@NIP", nip);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                byte[] img = (byte[])(rdr["Scan"]);
                MemoryStream mstream = new MemoryStream(img);
                Image nw = System.Drawing.Image.FromStream(mstream);
                e.Graphics.DrawImage(nw, AutoScrollPosition.X, AutoScrollPosition.Y, x, y);
            }

            conn.Close();
        }
        //595, 842
        private void page_Setup_Click(object sender, EventArgs e)
        {
            //PaperSize CustomSize1 = new PaperSize("F4", 813 , 1247);
            pageSetupDialog1.AllowMargins = true;
            pageSetupDialog1.AllowOrientation = true;
            pageSetupDialog1.AllowPaper = true;
            pageSetupDialog1.AllowPrinter = true;
            // pageSetupDialog1.PageSettings.PaperSize = CustomSize1;
            pageSetupDialog1.Reset();

            pageSetupDialog1.Document = printDocument1;
            pageSetupDialog1.Document = printDocument2;

            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.DefaultPageSettings = pageSetupDialog1.PageSettings;
                printDocument1.PrinterSettings = pageSetupDialog1.PrinterSettings;
                printDocument2.DefaultPageSettings = pageSetupDialog1.PageSettings;
                printDocument2.PrinterSettings = pageSetupDialog1.PrinterSettings;
            }

        }


        private void SK_Click(object sender, EventArgs e)
        {
            if (pageSetupDialog1.PageSettings == null)
            {

                MessageBox.Show("Ukuran kertas belum di atur \n Pengaturan kertas berada pada Page Setup", "Warning",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                printDialog1.Document = printDocument1;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
        }

        private void priview_Click(object sender, EventArgs e)
        {
            if (pageSetupDialog1.PageSettings == null)
            {

                MessageBox.Show("Ukuran kertas belum di atur \n Pengaturan kertas berada pada Page Setup", "Warning",
                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                PrintDocument pd = new PrintDocument();
                pd = printDocument2;
                printPreviewDialog2.Document = pd;

                printPreviewDialog2.ShowDialog();
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            if (pageSetupDialog1.PageSettings == null)
            {

                MessageBox.Show("Ukuran kertas belum di atur \n Pengaturan kertas berada pada Page Setup", "Warning",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                PrintDocument pd = new PrintDocument();
                pd = printDocument2;
                printDialog2.Document = pd;

                if (printDialog2.ShowDialog() == DialogResult.OK)
                {
                    //if () {
                    pd.Print();
                    savePrintSK();
                    updateData();
                    // }
                }
            }

        }

        private void printDocument2_print(object sender, PrintPageEventArgs e)
        {
            int x = pageSetupDialog1.PageSettings.PaperSize.Width;
            int y = pageSetupDialog1.PageSettings.PaperSize.Height;
            int f = pageSetupDialog1.PageSettings.Margins.Right;
            int g = pageSetupDialog1.PageSettings.Margins.Left;
            int h = pageSetupDialog1.PageSettings.Margins.Top;
            int i = pageSetupDialog1.PageSettings.Margins.Bottom;
            StringFormat form = new StringFormat(StringFormatFlags.NoClip);
            form.LineAlignment = StringAlignment.Near;
            form.Alignment = StringAlignment.Near ;
            Bitmap bpm = Properties.Resources.logo_bakesbangpol;
            Image nw = bpm;

            string code = "ID";
            var d = dateTimePicker2.Value.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo(code));
            var a = tanggal_sk_terakhir.Value.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo(code));
            var b = terhitung_tanggal.Value.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo(code));
            var c = tanggal_mulai.Value.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo(code));
            //tanggal_sk_terakhir.Format = DateTimePickerFormat.Custom;
            //tanggal_sk_terakhir.CustomFormat = "dd MMMM yyyy";
            //terhitung_tanggal.Format = DateTimePickerFormat.Custom;
            //terhitung_tanggal.CustomFormat = "dd MMMM yyyy";
            //tanggal_mulai.Format = DateTimePickerFormat.Custom;
            //tanggal_mulai.CustomFormat = "dd MMMM yyyy";
            e.Graphics.DrawImage(nw, 35, 25, 140, 150);
            e.Graphics.DrawString("PEMERINTAH KOTA MALANG", new Font("Times New Roman", 18, FontStyle.Regular), Brushes.Black, new RectangleF(310, 45, x, y));
            e.Graphics.DrawString("BADAN KESATUAN BANGSA DAN POLITIK", new Font("Times New Roman", 18, FontStyle.Bold), Brushes.Black, new RectangleF(230, 75, x, y));
            e.Graphics.DrawString("Jl. A. Yani No. 98 Telp. (0341) 491180 Fax. (0341) 474254", new Font("Times New Roman", 13, FontStyle.Regular), Brushes.Black, new RectangleF(270, 107, x, y));
            e.Graphics.DrawString("M A L A N G", new Font("Times New Roman", 16, FontStyle.Regular), Brushes.Black, new RectangleF(430, 133, x, y));
            e.Graphics.DrawString("Kode Pos 65125", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(660, 150, x, y));
            Pen black = new Pen(Color.Black);
            e.Graphics.DrawLine(black, 40, 170, 800, 170);
            e.Graphics.DrawLine(black, 40, 173, 800, 173);
            e.Graphics.DrawLine(black, 40, 174, 800, 174);
            e.Graphics.DrawLine(black, 40, 175, 800, 175);
            e.Graphics.DrawString("Malang, "+ b, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(480, 190, x, y));
            e.Graphics.DrawString("Nomor", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(40, 210, x, y));
            e.Graphics.DrawString("Sifat", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(40, 230, x, y));
            e.Graphics.DrawString("Lampiran", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(40, 250, x, y));
            e.Graphics.DrawString("Perihal", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(40, 270, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(120, 210, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(120, 230, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(120, 250, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(120, 270, x, y));
            e.Graphics.DrawString("Penting", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(140, 230, x, y));
            e.Graphics.DrawString("-", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(140, 250, x, y));
            e.Graphics.DrawString("Kenaikan Gaji Berkala", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(140, 270, x, y));
            e.Graphics.DrawString("Kepada", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(480, 210, x, y));
            e.Graphics.DrawString("Yth. Sdr. Kepala Badan Pengelola Keuangan", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(440, 230, x, y));
            e.Graphics.DrawString("dan Aset Daerah Kota Malang", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(510, 250, x, y));
            e.Graphics.DrawString("di", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(510, 270, x, y));
            e.Graphics.DrawString("M A L A N G", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new RectangleF(580, 290, x, y));
            e.Graphics.DrawString("                  Dengan ini diberitahukan bahwa berhubung telah dipenuhinya masa kerja dan syarat-syarat lainnya kepada : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 340, x - 150 - f, y));
            e.Graphics.DrawString("1.   Nama", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 380, x, y));
            e.Graphics.DrawString("2.   NIP", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 400, x, y));
            e.Graphics.DrawString("3.   Pangkat", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 420, x, y));
            e.Graphics.DrawString("4.   Jabatan", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 440, x, y));
            e.Graphics.DrawString("5.   Kantor / Tempat Kerja", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 460, x, y));
            e.Graphics.DrawString("6.   Gaji pokok lama ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 480, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 380, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 400, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 420, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 440, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 460, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 480, x, y));
            e.Graphics.DrawString("(atas dasar Surat Keputusan terakhir tentang gaji / pangkat yang diterapkan)", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(200, 520, x, y));
            e.Graphics.DrawString("a. Oleh Pejabat", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(200, 540, x, y));
            e.Graphics.DrawString("b. Tanggal", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(200, 560, x, y));
            e.Graphics.DrawString("Nomor ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(220, 580, x, y));
            e.Graphics.DrawString("c. Tanggal Mulai", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(200, 600, x, y));
            e.Graphics.DrawString("berlakunya gaji tersebut", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(220, 620, x, y));
            e.Graphics.DrawString("d. Masa kerja golongan ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(200, 640, x, y));
            e.Graphics.DrawString("pada tanggal tersebut", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(220, 660, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 540, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 560, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 580, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 620, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 640, x, y));
            e.Graphics.DrawString("Diberikan kenaikan gaji berkala hingga memperoleh : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(200, 680, x, y));
            e.Graphics.DrawString("7.   Gaji pokok baru ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 700, x, y));
            e.Graphics.DrawString("8.   Berdasarkan masa kerja ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 720, x, y));
            e.Graphics.DrawString("9.   Dalam golongan / ruang ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 740, x, y));
            e.Graphics.DrawString("10.  Terhitung mulai tanggal ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 760, x, y));
            e.Graphics.DrawString("11.  Keterangan ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 780, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 700, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 720, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 740, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 760, x, y));
            e.Graphics.DrawString(" : ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(390, 780, x, y));
            e.Graphics.DrawString("TEMBUSAN", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(40, 1120, x, y));
            e.Graphics.DrawString("1. Sdr. Kepala Badan Kepegawaian Daerah Kota Malang", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(40, 1140, x, y));
            e.Graphics.DrawString("2. Yang bersangkutan", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(40, 1160, x, y));

            try
            {
                MyConn();
                String sql = "select a.nama, a.nip, b.pangkat, b.jabatan, b.tmptkerja, b.gajipokok, b.terbilang from datapegawai a join datask b  ON a.nip = b.nip where a.nip=@NIP";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NIP", nip);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    e.Graphics.DrawString(nmr_surat.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new RectangleF(140, 210, x, y));
                    e.Graphics.DrawString("a.n " + nama.Text, new Font("Times New Roman", 12, FontStyle.Underline | FontStyle.Bold), Brushes.Black, new RectangleF(140, 290, x, y));
                    e.Graphics.DrawString(rdr[0].ToString(), new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 380, x, y));
                    e.Graphics.DrawString(rdr[1].ToString(), new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 400, x, y));
                    e.Graphics.DrawString(rdr[2].ToString(), new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 420, x, y));
                    e.Graphics.DrawString(rdr[3].ToString(), new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 440, x, y));
                    e.Graphics.DrawString(rdr[4].ToString(), new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 460, x, y));
                    e.Graphics.DrawString(rdr[5].ToString(), new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 480, x, y));
                    e.Graphics.DrawString("(" + rdr[6].ToString() + ")", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 500, x, y));
                    e.Graphics.DrawString(pejabat.Text, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 540, x, y));
                    e.Graphics.DrawString(a, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 560, x, y));
                    e.Graphics.DrawString(nomor.Text, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 580, x, y));
                    e.Graphics.DrawString(c, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 620, x, y));
                    e.Graphics.DrawString(thn_sk.Text + " tahun " + bln_sk.Text + " bulan ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 660, x, y));
                    e.Graphics.DrawString(gaji.Text + "(" + terbilang.Text + ")", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 700, x - 200 - f, y));
                    e.Graphics.DrawString(thn_masa_kerja.Text + " tahun " + bln_masa_kerja.Text + " bulan ", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 720, x, y));
                    e.Graphics.DrawString(golongan_ruang.Text, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 740, x, y));
                    e.Graphics.DrawString(b, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 760, x, y));
                    e.Graphics.DrawString(keterangan.Text, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(410, 780, x, y));
                    e.Graphics.DrawString(landasan.Text, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(170, 820, (x - 150 - f), y), form);
                    // Graphics sd = new System.Drawing.Graphics();
                    //    Font font = new Font("Times New Roman", 12, FontStyle.Regular);
                    //    Brush brush = Brushes.Black;
                    //    RectangleF rect = new RectangleF(170, 860, (x - g - f), y);
                    //    String text = landasan.Text;
                    //    String[] word = text.Split(' ');
                    //    float[] word_width = new float[word.Length];
                    //    float total_width = 0;
                    //    for (int u = 0; u < word.Length; u++)
                    //    {
                    //        SizeF size = this.CreateGraphics().MeasureString(word[u],font);
                    //        word_width[u] = size.Width;
                    //        total_width += word_width[u];
                    //    }
                    //    float extra_space = rect.Width - total_width;
                    //    int num_space = word.Length - 1;
                    //    if (word.Length > 1) extra_space /= num_space;
                    //    float xg = rect.Left;
                    //    float yg = rect.Top;
                    //    for (int u = 0; u < word.Length; u++)
                    //    {
                    //        e.Graphics.DrawString(word[u], font, brush, xg, yg);
                    //        xg += word_width[u] + extra_space;
                    //    }
                    //    // Drawjustify(Graphics.FromHdcInternal, new RectangleF( 170, 860, (x - 150 - f), y), new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, landasan.Text);
                    e.Graphics.DrawString(petinggi.Text, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(480, 930, x, y));
                    e.Graphics.DrawString("KOTA MALANG", new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(500, 950, x, y));
                    e.Graphics.DrawString(nama_petinggi.Text, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(480, 1050, x, y));
                    e.Graphics.DrawString(pangkat_petinggi.Text, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(480, 1070, x, y));
                    e.Graphics.DrawString("NIP. " + NIP_petinggi.Text, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new RectangleF(480, 1090, x, y));
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //public void Drawjustify(Graphics gr, RectangleF rect, Font font, Brush brush, String text)
        //{

        //    String[] word = text.Split(' ');
        //    float[] word_width = new float[word.Length];
        //    float total_width = 0;
        //    for (int i = 0; i < word.Length; i++)
        //    {
        //        SizeF size = gr.MeasureString(word[i], font);
        //        word_width[i] = size.Width;
        //        total_width += word_width[i];
        //    }
        //    float extra_space = rect.Width - total_width;
        //    int num_space = word.Length - 1;
        //    if (word.Length > 1) extra_space /= num_space;
        //    float x = rect.Left;
        //    float y = rect.Top;
        //    for (int i = 0; i < word.Length; i++)
        //    {
        //        gr.DrawString(word[i], font, brush, x, y);
        //        x += word_width[i] + extra_space;
        //    }

        //}

        public void savePrintSK()
        {
            MyConn();
            String q = "UPDATE printsk SET nama_badan=@nm_bd, nomor_surat=@nmr_srt, landasan_surat=@lndsn_srt, nama_penandatangan=@nm_pnd, nip_penandatangan=@nip_pnd, pangkat_penandatangan=@pngkt_pnd, jabatan_penandatangan=@jbt_pnd where keterangan=@ket";
            MySqlCommand cmd1 = new MySqlCommand(q, conn);
            cmd1.CommandTimeout = 60;
            cmd1.Parameters.AddWithValue("@ket", keterangan.Text);
            cmd1.Parameters.AddWithValue("@NIP", NIP_petinggi.Text);
            cmd1.Parameters.AddWithValue("@nm_bd", pejabat.Text);
            cmd1.Parameters.AddWithValue("@nmr_srt", nmr_surat.Text);
            cmd1.Parameters.AddWithValue("@lndsn_srt", landasan.Text);
            cmd1.Parameters.AddWithValue("@nm_pnd", nama_petinggi.Text);
            cmd1.Parameters.AddWithValue("@nip_pnd", NIP_petinggi.Text);
            cmd1.Parameters.AddWithValue("@pngkt_pnd", pangkat_petinggi.Text);
            cmd1.Parameters.AddWithValue("@jbt_pnd", petinggi.Text);
            cmd1.ExecuteNonQuery();
            MessageBox.Show("simpan data print berhasil!");
            conn.Close();

        }

        public void updateData()
        {
            MyConn();
            String q = "UPDATE datask set gajipokok=@gaji, terbilang=@terbilng, tglmsk=@tgl where nip=@Nip";
            MySqlCommand cmd = new MySqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@NIP", nip);
            cmd.Parameters.AddWithValue("@gaji", "Rp. " + gaji.Text);
            cmd.Parameters.AddWithValue("@terbilng", terbilang.Text);
            var c = terhitung_tanggal.Value.ToString("yyyy-MM-dd");
            cmd.Parameters.AddWithValue("@tgl", c);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("simpan data print berhasil!");
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Home x = new Home();
            x.passValue(userame);
            x.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Home x = new Home();
            x.Show();
            x.passValue(userame);
            this.Hide();
        }

    }
}
