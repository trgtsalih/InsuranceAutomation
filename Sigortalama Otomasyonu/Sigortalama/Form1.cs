using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sigortalama
{
    public partial class Form1 : Form
    {
        SqlCommand komut;
        SqlConnection baglanti;
        SqlDataReader oku;
        int musteri_id = 0;
        int sirket_id = 0;
        int police_id = 0;
        int sigorta_id = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void musteri_kaydet_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Insert Into tblMusteri (tc,ad_soyad,tel,adres,plaka) values (@TC, @Musteri, @Tel, @Adres, @Plaka)", baglanti);
            komut.Parameters.AddWithValue("@TC", musteri_tc.Text);
            komut.Parameters.AddWithValue("@Musteri", musteri.Text);
            komut.Parameters.AddWithValue("@Tel", musteri_tel.Text);
            komut.Parameters.AddWithValue("@Adres", musteri_adres.Text);
            komut.Parameters.AddWithValue("@Plaka", musteri_plaka.Text);

            try
            {
                if(baglanti.State==ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.ExecuteNonQuery();
                baglanti.Close();
                yenile1();
                temizle1();
                MessageBox.Show("Müşteri Kaydedildi !");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void musteri_sil_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Delete From tblMusteri where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", musteri_id);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                baglanti.Close();
                yenile1();
                temizle1();
                MessageBox.Show("Müşteri Silindi !");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void musteri_guncelle_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Update tblMusteri set tc=@TC, ad_soyad=@Musteri, tel=@Tel, adres=@Adres, plaka=@Plaka where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", musteri_id);
            komut.Parameters.AddWithValue("@TC", musteri_tc.Text);
            komut.Parameters.AddWithValue("@Musteri", musteri.Text);
            komut.Parameters.AddWithValue("@Tel", musteri_tel.Text);
            komut.Parameters.AddWithValue("@Adres", musteri_adres.Text);
            komut.Parameters.AddWithValue("@Plaka", musteri_plaka.Text);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.ExecuteNonQuery();
                baglanti.Close();
                yenile1();
                temizle1();
                MessageBox.Show("Müşteri Güncellendi !");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void musteri_temizle_Click(object sender, EventArgs e)
        {
            temizle1();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            musteri_id = int.Parse(row.Cells[0].Value.ToString());
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblMusteri where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", musteri_id);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                oku.Read();
                musteri_tc.Text = oku["tc"].ToString();
                musteri.Text = oku["ad_soyad"].ToString();
                musteri_tel.Text = oku["tel"].ToString();
                musteri_plaka.Text = oku["plaka"].ToString();
                musteri_adres.Text = oku["adres"].ToString();
                baglanti.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sirket_temizle_Click(object sender, EventArgs e)
        {
            temizle2();
        }

        private void police_temizle_Click(object sender, EventArgs e)
        {
            temizle3();
        }

        private void sigorta_temizle_Click(object sender, EventArgs e)
        {
            temizle4();
        }

        public void temizle1()
        {
            musteri_tc.Text = "";
            musteri.Text = "";
            musteri_tel.Text = "";
            musteri_plaka.Text = "";
            musteri_adres.Text = "";
        }

        public void temizle2()
        {
            sirket.Text = "";
            sirket_tel.Text = "";
            sirket_adres.Text = "";
        }

        public void temizle3()
        {
            police_turu.Text = "";

        }

        public void temizle4()
        {
            sigorta_ucret.Text = "";
            sigorta_aciklama.Text = "";
        }

        private void yenile1()
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblMusteri", baglanti);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                DataTable tablo1 = new DataTable();
                tablo1.Load(oku);
                dataGridView1.DataSource = tablo1;
                baglanti.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void yenile2()
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblSirket", baglanti);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                DataTable tablo2 = new DataTable();
                tablo2.Load(oku);
                dataGridView2.DataSource = tablo2;
                baglanti.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void yenile3()
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblPolice", baglanti);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                DataTable tablo3 = new DataTable();
                tablo3.Load(oku);
                dataGridView3.DataSource = tablo3;
                baglanti.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void yenile4()
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblSigortalama", baglanti);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                DataTable tablo4 = new DataTable();
                tablo4.Load(oku);
                dataGridView4.DataSource = tablo4;
                baglanti.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[rowIndex];
            sirket_id = int.Parse(row.Cells[0].Value.ToString());
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblSirket where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", sirket_id);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                oku.Read();
                sirket.Text = oku["sirket_adi"].ToString();
                sirket_tel.Text = oku["tel"].ToString();
                sirket_adres.Text = oku["adres"].ToString();
                baglanti.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView3.Rows[rowIndex];
            police_id = int.Parse(row.Cells[0].Value.ToString());
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblPolice where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", police_id);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                oku.Read();
                police_turu.Text = oku["police_turu"].ToString();
                baglanti.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView4_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView4.Rows[rowIndex];
            sigorta_id = int.Parse(row.Cells[0].Value.ToString());
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblSigortalama where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", sigorta_id);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                oku.Read();
                p_id.Text = oku["police_id"].ToString();
                s_id.Text = oku["sirket_id"].ToString();
                m_id.Text = oku["musteri_id"].ToString();
                sigorta_baslangic.Text = oku["baslangic"].ToString();
                sigorta_bitis.Text = oku["bitis"].ToString();
                sigorta_ucret.Text = oku["ucret"].ToString();
                sigorta_aciklama.Text = oku["aciklama"].ToString();
                baglanti.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sirket_kaydet_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Insert Into tblSirket (sirket_adi,tel,adres) values (@Sirket, @Tel, @Adres)", baglanti);
            komut.Parameters.AddWithValue("@Sirket", sirket.Text);
            komut.Parameters.AddWithValue("@Tel", sirket_tel.Text);
            komut.Parameters.AddWithValue("@Adres", sirket_adres.Text);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.ExecuteNonQuery();
                baglanti.Close();
                yenile2();
                temizle2();
                MessageBox.Show("Yeni Şirket Eklendi!");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void police_kaydet_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Insert Into tblPolice (police_turu) values (@Police)", baglanti);
            komut.Parameters.AddWithValue("@Police", police_turu.Text);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.ExecuteNonQuery();
                baglanti.Close();
                yenile3();
                temizle3();
                MessageBox.Show("Yeni Poliçe Türü Eklendi!");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sigorta_kaydet_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Insert Into tblSigortalama (police_id,sirket_id,musteri_id,baslangic,bitis,ucret,aciklama) values (@Police, @Sirket, @Musteri, @Baslangic, @Bitis, @Ucret, @Aciklama)", baglanti);
            komut.Parameters.AddWithValue("@Police", Convert.ToInt16(p_id.Text));
            komut.Parameters.AddWithValue("@Sirket", Convert.ToInt16(s_id.Text));
            komut.Parameters.AddWithValue("@Musteri", Convert.ToInt16(m_id.Text));
            komut.Parameters.AddWithValue("@Baslangic", sigorta_baslangic.Text);
            komut.Parameters.AddWithValue("@Bitis", sigorta_bitis.Text);
            komut.Parameters.AddWithValue("@Ucret", sigorta_ucret.Text);
            komut.Parameters.AddWithValue("@Aciklama", sigorta_aciklama.Text);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.ExecuteNonQuery();
                baglanti.Close();
                yenile4();
                temizle4();
                MessageBox.Show("Yeni Sigorta Eklendi!");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sirket_sil_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Delete From tblSirket where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", sirket_id);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                baglanti.Close();
                yenile2();
                temizle2();
                MessageBox.Show("Şirket Silindi !");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void police_sil_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Delete From tblPolice where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", police_id);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                baglanti.Close();
                yenile3();
                temizle3();
                MessageBox.Show("Poliçe Türü Silindi !");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void sigorta_sil_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Delete From tblSigortalama where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", sigorta_id);


            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                oku = komut.ExecuteReader();
                baglanti.Close();
                yenile4();
                temizle4();
                MessageBox.Show("Sigorta Silindi !");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void sirket_guncelle_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Update tblSirket set sirket_adi=@Sirket,tel=@Tel, adres=@Adres where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", sirket_id);
            komut.Parameters.AddWithValue("@Sirket", sirket.Text);
            komut.Parameters.AddWithValue("@Tel", sirket_tel.Text);
            komut.Parameters.AddWithValue("@Adres", sirket_adres.Text);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.ExecuteNonQuery();
                baglanti.Close();
                yenile2();
                temizle2();
                MessageBox.Show("Şirket Güncellendi !");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void police_guncelle_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Update tblPolice set police_turu=@Police where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", police_id);
            komut.Parameters.AddWithValue("@Police", police_turu.Text);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.ExecuteNonQuery();
                baglanti.Close();
                yenile3();
                temizle3();
                MessageBox.Show("Poliçe Türü Güncellendi !");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void sigorta_guncelle_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Update tblSigortalama set police_id=@Police, sirket_id=@Sirket, musteri_id=@Musteri, baslangic=@Baslangic, bitis=@Bitis, ucret=@Ucret, aciklama=@Aciklama where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", sigorta_id);
            komut.Parameters.AddWithValue("@Police", police_id);
            komut.Parameters.AddWithValue("@Sirket", sirket_id);
            komut.Parameters.AddWithValue("@Musteri", musteri_id);
            komut.Parameters.AddWithValue("@Baslangic", sigorta_baslangic.Text);
            komut.Parameters.AddWithValue("@Bitis", sigorta_bitis.Text);
            komut.Parameters.AddWithValue("@Ucret", sigorta_ucret.Text);
            komut.Parameters.AddWithValue("@Aciklama", sigorta_aciklama.Text);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.ExecuteNonQuery();
                baglanti.Close();
                yenile4();
                temizle4();
                MessageBox.Show("Sigorta Güncellendi !");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void sigorta_police_turu_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblPolice where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", police_id);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                p_id.Text = sigorta_police_turu.SelectedIndex.ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void sigorta_sirketi_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblSirket where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", sirket_id);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                s_id.Text = sigorta_sirketi.SelectedIndex.ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void sigorta_musteri_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblMusteri where id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", musteri_id);

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                m_id.Text = sigorta_musteri.SelectedIndex.ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source = SALİH; Initial Catalog = dbSigortalama; Integrated Security = true");
            komut = new SqlCommand("Select * From tblMusteri", baglanti);
            
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            oku = komut.ExecuteReader();
            DataTable tablo5 = new DataTable();
            tablo5.Load(oku);
            sigorta_musteri.ValueMember = "id";
            sigorta_musteri.DisplayMember = "ad_soyad";
            sigorta_musteri.DataSource = tablo5;

            komut = new SqlCommand("Select * From tblSirket", baglanti);
            oku = komut.ExecuteReader();
            DataTable tablo6 = new DataTable();
            tablo6.Load(oku);
            sigorta_sirketi.ValueMember = "id";
            sigorta_sirketi.DisplayMember = "sirket_adi";
            sigorta_sirketi.DataSource = tablo6;

            komut = new SqlCommand("Select * From tblPolice", baglanti);
            oku = komut.ExecuteReader();
            DataTable tablo7 = new DataTable();
            tablo7.Load(oku);
            sigorta_police_turu.ValueMember = "id";
            sigorta_police_turu.DisplayMember = "police_turu";
            sigorta_police_turu.DataSource = tablo7;

            baglanti.Close();
            yenile1();
            yenile2();
            yenile3();
            yenile4();
        }

    }
}
