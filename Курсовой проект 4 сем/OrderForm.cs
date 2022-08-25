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
    public partial class OrderForm : Form
    {
        private static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
        public int id;
        public OrderForm()
        {
            InitializeComponent();
            Product.DisplayClientData(dataGridView1);
        }

        public void CleanData()
        {
            id = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(id != 0 && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                Order.CreateOrder(id, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            }
            SqlCommand cmd = new SqlCommand("SELECT MAX (ID_Order) FROM MarketOrder;", connection);
            connection.Open();
            MessageBox.Show("Заказ №" + Convert.ToInt32(cmd.ExecuteScalar()) + " создан");
            connection.Close();
            CleanData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Product.DisplayClientData(dataGridView1);
            CleanData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
    }
}
