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

namespace Teklas.Proje
{
    public partial class Form2 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        public Form2()
        {
            InitializeComponent();
        }
        void KayıtGetir()
        {
            baglanti = new SqlConnection("server=.;Initial Catalog=TeklasKayitlar;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM TeklasUser", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void alert(string s)
            {
        DialogResult dr = MessageBox.Show(s, "Message", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                
            }
            else
            { } 
                }

        private void Form2_Load(object sender, EventArgs e)
        {
            KayıtGetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to exit.", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
            { } 
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtEposta.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtKimlikNo.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtEposta.Text == "" || txtKimlikNo.Text == "" || txtSoyad.Text == "" || txtTelefon.Text == "")
            {
                alert("Lütfen girilmesi gerekli alanları (*) doldurunuz.");
                return;
            }
            
            string sorgu = "INSERT INTO TeklasUser(UserName,UserSurname,UserMail,UserIdentityNo,UserBirthday,UserPhoneNumber,UserGender) VALUES (@Username,@UserSurname,@UserMail,@UserIdentityNo,@UserBirthday,@UserPhoneNumber,@UserGender)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@UserName", txtAd.Text);
            komut.Parameters.AddWithValue("@UserSurname", txtSoyad.Text);
            komut.Parameters.AddWithValue("@UserMail", txtEposta.Text);
            komut.Parameters.AddWithValue("@UserIdentityNo", txtKimlikNo.Text);
            komut.Parameters.AddWithValue("@UserBirthDay", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@UserPhoneNumber", txtTelefon.Text);
            if (radioButton1.Checked)
                komut.Parameters.AddWithValue("@UserGender", "E");
            else if(radioButton2.Checked)
            {
                komut.Parameters.AddWithValue("@UserGender", "K");
            }
            else
            {
                komut.Parameters.AddWithValue("@UserGender", "");
            }
            MessageBox.Show("Kayıt Başarılı");
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            KayıtGetir();
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtEposta.Text = "";
            txtKimlikNo.Text = "";
            txtTelefon.Text = "";
            
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text=="")
            {
                alert("Lütfen silmek istediğiniz satırı seçin.");
                return;
            }
            string sorgu = "DELETE FROM TeklasUser WHERE Id=@Id";
            
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Id",Convert.ToInt16(txtID.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            KayıtGetir();
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtEposta.Text = "";
            txtKimlikNo.Text = "";
            txtTelefon.Text = "";
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                alert("Lütfen güncellemek istediğiniz satırı seçin.");
                return;
            }
            string sorgu = "UPDATE TeklasUser SET UserName=@UserName,UserSurname=@UserSurname,UserMail=@UserMail,UserIdentityNo=@UserIdentityNo,UserBirthday=@UserBirthday,UserPhoneNumber=@UserPhoneNumber,UserGender=@UserGender WHERE Id=@Id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Id", Convert.ToInt32(txtID.Text));
            komut.Parameters.AddWithValue("@UserName",txtAd.Text);
            komut.Parameters.AddWithValue("@UserSurname", txtSoyad.Text);
            komut.Parameters.AddWithValue("@UserMail", txtEposta.Text);
            komut.Parameters.AddWithValue("@UserIdentityNo", txtKimlikNo.Text);
            komut.Parameters.AddWithValue("@UserBirthday", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@UserPhoneNumber", txtTelefon.Text);
            if (radioButton1.Checked)
                komut.Parameters.AddWithValue("@UserGender", "E");
            else
                komut.Parameters.AddWithValue("@UserGender", "K");
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            KayıtGetir();
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtEposta.Text = "";
            txtKimlikNo.Text = "";
            txtTelefon.Text = "";
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void txtAd_Enter(object sender, EventArgs e)
        {
            if (txtAd.Text=="Ör. Alper")
            {
                txtAd.Text = "";
                txtAd.ForeColor = Color.Black;
            }
        }

        private void txtAd_Leave(object sender, EventArgs e)
        {
            if (txtAd.Text=="")
            {
                txtAd.Text = "Ör. Alper";
                txtAd.ForeColor = Color.Silver;
            }
        }

        private void txtSoyad_Enter(object sender, EventArgs e)
        {
            if (txtSoyad.Text=="Ör. Çalışkan")
            {
                txtSoyad.Text = "";
                txtSoyad.ForeColor = Color.Black;
            }
        }

        private void txtSoyad_Leave(object sender, EventArgs e)
        {
            if (txtSoyad.Text=="")
            {
                txtSoyad.Text = "Ör. Çalışkan";
                txtSoyad.ForeColor = Color.Silver;
            }
        }

        private void txtEposta_Enter(object sender, EventArgs e)
        {
            if (txtEposta.Text=="Ör. acaliskan@teklas.com.tr")
            {
                txtEposta.Text = "";
                txtEposta.ForeColor = Color.Black;
            }
        }

        private void txtEposta_Leave(object sender, EventArgs e)
        {
            if (txtEposta.Text=="")
            {
                txtEposta.Text = "Ör. acaliskan@teklas.com.tr";
                txtEposta.ForeColor = Color.Silver;
            }
        }

        private void txtKimlikNo_Enter(object sender, EventArgs e)
        {
            if (txtKimlikNo.Text=="Kimlik Numarası Giriniz")
            {
                txtKimlikNo.Text = "";
                txtKimlikNo.ForeColor = Color.Black;
            }
        }

        private void txtKimlikNo_Leave(object sender, EventArgs e)
        {
            if (txtKimlikNo.Text=="")
            {
                txtKimlikNo.Text = "Kimlik Numarası Giriniz";
                txtKimlikNo.ForeColor = Color.Silver;
            }
        }

        private void txtTelefon_Enter(object sender, EventArgs e)
        {
            if (txtTelefon.Text=="Ör. 01234567890")
            {
                txtTelefon.Text = "";
                txtTelefon.ForeColor = Color.Black;
            }
        }

        private void txtTelefon_Leave(object sender, EventArgs e)
        {
            if (txtTelefon.Text=="")
            {
                txtTelefon.Text = "Ör. 01234567890";
                txtTelefon.ForeColor = Color.Silver;
            }
        }

        /*private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtEposta.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtKimlikNo.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }*/

    }
}
