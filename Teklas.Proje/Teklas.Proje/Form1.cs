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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            string kullanici_adi="admin";
            string sifre="admin";
            if (textBox1.Text==kullanici_adi&textBox2.Text==sifre)
            {
                MessageBox.Show("Başarılı!");
                this.Hide();
                Form2 frm = new Form2();
                frm.Show();
            }
            else if (textBox1.Text!=kullanici_adi&textBox2.Text==sifre)
            {
                MessageBox.Show("Kullanıcı Adınız Hatalı!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
            }
            else if (textBox1.Text==kullanici_adi&textBox2.Text!=sifre)
            {
                MessageBox.Show("Şifreniz Hatalı!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı ve Şifreniz Hatalı!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
            }

        }
    }
}
