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
    public partial class MainUserForm : Form
    {
        private static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
        public string nameLabel, numLabel, brandLabel, date, categoryLabel, statusLabel;
        public int id;

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            numLabel = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            nameLabel = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            brandLabel = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cost = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            capacity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            date = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            //adress = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            categoryLabel = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            statusLabel = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            //SqlCommand cmd = new SqlCommand("SELECT ID_Order FROM MarketOrder WHERE OrderDate = \'" + date + "\' AND Cost = " + cost + ";", connection);
            //connection.Open();
            //id = Convert.ToInt32(cmd.ExecuteScalar());
            //connection.Close();
        }

        public int cost, capacity, adress;
        public MainUserForm()
        {
            InitializeComponent();
            Order.DisplayOrderData1(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order.ChangeStatus(nameLabel, numLabel, brandLabel, date, categoryLabel, statusLabel, cost, capacity);
            Order.DisplayOrderData1(dataGridView1);
        }
    }
}
