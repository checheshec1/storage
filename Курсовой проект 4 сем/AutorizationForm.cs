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
using System.Data.Sql;
using System.Configuration;

namespace Курсовой_проект_4_сем
{
    public partial class AutorizationForm : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString;

        public AutorizationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand autorizationCommand = new SqlCommand("SELECT UserParol FROM Users WHERE UserLogin = '" + textBox1.Text + "';", connection);
                    string password = (string)autorizationCommand.ExecuteScalar();
                    if (textBox2.Text.Equals(password))
                    {
                        SqlCommand command = new SqlCommand("SELECT RoleName FROM Roles WHERE ID_Role = (SELECT ID_Role FROM Users WHERE UserLogin = '" + textBox1.Text + "');", connection);
                        if (command.ExecuteScalar().Equals("Администратор"))
                        {
                            Form mainAdminForm = new MainFormAdmin();
                            this.SetVisibleCore(false);
                            mainAdminForm.ShowDialog();
                            this.SetVisibleCore(true);
                        } else
                        {
                            Form MainUserForm = new MainUserForm();
                            this.SetVisibleCore(false);
                            MainUserForm.ShowDialog();
                            this.SetVisibleCore(true);
                        }
                    } else
                    {
                        MessageBox.Show("Неверные данные для входа в систему");
                    }
                }
            } else
            {
                MessageBox.Show("Введите данные для входа в систему");
            }
        }

        private void AutorizationForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form orderForm = new OrderForm();
            this.SetVisibleCore(false);
            orderForm.ShowDialog();
            this.SetVisibleCore(true);
        }
    }
}
