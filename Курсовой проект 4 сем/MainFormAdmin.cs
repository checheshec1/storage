using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовой_проект_4_сем
{
    public partial class MainFormAdmin : Form
    {
        private static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
        public static int id;

        public string nameLabel, numLabel, brandLabel, date, categoryLabel, statusLabel;
        public int capacity, adress;
        public double cost;

        public MainFormAdmin()
        {
            InitializeComponent();
            Product.DisplayClientData(dataGridView1);
            radioButton1.Checked = true;
            Order.DisplayOrderData(radioButton1, radioButton2, radioButton3, dataGridView2);
            Personal.DisplayPersonalData(dataGridView3);
        }
        public void CleanData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox8.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Product.AddProduct(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
            Product.DisplayClientData(dataGridView1);
            CleanData();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Product.ChangeProduct(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
            Product.DisplayClientData(dataGridView1);
            CleanData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Order.ChangeStatus(nameLabel, numLabel, brandLabel, date, categoryLabel, statusLabel, cost, capacity);
            Order.DisplayOrderData(radioButton1, radioButton2, radioButton3, dataGridView2);
            CleanData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Personal.AddPersonal(textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text, textBox15.Text, textBox16.Text);
            Personal.DisplayPersonalData(dataGridView3);
            CleanData();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Personal.ChangePersonal(textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text, textBox15.Text, textBox16.Text, id);
            Personal.DisplayPersonalData(dataGridView3);
            CleanData();
        }

        private void dataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[0].Value);
            textBox7.Text = dataGridView3.Rows[e.RowIndex].Cells[9].Value.ToString();
            textBox8.Text = dataGridView3.Rows[e.RowIndex].Cells[10].Value.ToString();
            textBox9.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox10.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox11.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox12.Text = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox13.Text = dataGridView3.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox14.Text = dataGridView3.Rows[e.RowIndex].Cells[6].Value.ToString();
            textBox15.Text = dataGridView3.Rows[e.RowIndex].Cells[7].Value.ToString();
            textBox16.Text = dataGridView3.Rows[e.RowIndex].Cells[8].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Personal.DeletePersonal(id, textBox9.Text);
            Personal.DisplayPersonalData(dataGridView3);
            CleanData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Product.DeleteProduct(textBox1.Text);
            Product.DisplayClientData(dataGridView1);
            CleanData();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Order.DisplayOrderData(radioButton1, radioButton2, radioButton3, dataGridView2);
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Order.DisplayOrderData(radioButton1, radioButton2, radioButton3, dataGridView2);
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Order.DisplayOrderData(radioButton1, radioButton2, radioButton3, dataGridView2);
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            SqlCommand c = new SqlCommand("SELECT ID_Product WHERE ProductName = \'" + textBox1.Text + "\' AND Price = " + Convert.ToInt32(textBox5.Text) + " AND Capacity = " + Convert.ToInt32(textBox6.Text) + ";", connection);
            connection.Open();
            id = (int)c.ExecuteScalar();
            connection.Close();
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            numLabel = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            nameLabel = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            brandLabel = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            cost = (double)dataGridView2.Rows[e.RowIndex].Cells[3].Value;
            capacity = (int)dataGridView2.Rows[e.RowIndex].Cells[4].Value;
            date = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
            adress=(int)dataGridView2.Rows[e.RowIndex].Cells[6].Value;
            categoryLabel = dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
            statusLabel = dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString();
        }
    }
}