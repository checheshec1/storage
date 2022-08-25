using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Windows.Forms;

namespace Курсовой_проект_4_сем
{
    class Order
    {
        private static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
        private static SqlDataAdapter adapter;
        private static SqlCommand cmd;

        private static int num, capacity;
        private static string name, brand, date, cat, status;
        private static double costt;

        public static void DisplayOrderData(RadioButton rb1, RadioButton rb2, RadioButton rb3, DataGridView data)
        {
            if(rb1.Checked)
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("SELECT ID_Order AS Номер, ProductName AS Название, BrandName AS Бренд, Cost AS Стоимость, Quantity AS Количество, OrderDate AS Дата, Adress AS Адрес, CategoryName AS Категория, StatuseName AS Статус FROM MarketOrder INNER JOIN Product ON MarketOrder.ID_Product = Product.ID_Product INNER JOIN Brand ON MarketOrder.ID_Brand = Brand.ID_Brand INNER JOIN ProductCategory ON MarketOrder.ID_Category = ProductCategory.ID_Category INNER JOIN OrderStatus ON MarketOrder.ID_Statusa = OrderStatus.ID_Status INNER JOIN Shop ON MarketOrder.Shop = Shop.ID_Shop;", connection);
                adapter.Fill(dt);
                data.DataSource = dt;
                connection.Close();
            }
            if(rb2.Checked)
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("SELECT ID_Order AS Номер, ProductName AS Название, BrandName AS Бренд, Cost AS Стоимость, Quantity AS Количество, OrderDate AS Дата, CategoryName AS Категория, StatuseName AS Статус FROM MarketOrder INNER JOIN Product ON MarketOrder.ID_Product = Product.ID_Product INNER JOIN Brand ON MarketOrder.ID_Brand = Brand.ID_Brand INNER JOIN ProductCategory ON MarketOrder.ID_Category = ProductCategory.ID_Category INNER JOIN OrderStatus ON MarketOrder.ID_Statusa = OrderStatus.ID_Status AND OrderStatus.ID_Status = 1 INNER JOIN Shop ON MarketOrder.Shop = Shop.ID_Shop;", connection);
                adapter.Fill(dt);
                data.DataSource = dt;
                connection.Close();
            }
            if(rb3.Checked)
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("SELECT ID_Order AS Номер, ProductName AS Название, BrandName AS Бренд, Cost AS Стоимость, Quantity AS Количество, OrderDate AS Дата, CategoryName AS Категория, StatuseName AS Статус FROM MarketOrder INNER JOIN Product ON MarketOrder.ID_Product = Product.ID_Product INNER JOIN Brand ON MarketOrder.ID_Brand = Brand.ID_Brand INNER JOIN ProductCategory ON MarketOrder.ID_Category = ProductCategory.ID_Category INNER JOIN OrderStatus ON MarketOrder.ID_Statusa = OrderStatus.ID_Status AND OrderStatus.ID_Status = 2 INNER JOIN Shop ON MarketOrder.Shop = Shop.ID_Shop;", connection);
                adapter.Fill(dt);
                data.DataSource = dt;
                connection.Close();
            }
        }
        public static void DisplayOrderData1(DataGridView data)
        {
            connection.Open();
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter("SELECT ID_Order AS Номер, ProductName AS Название, BrandName AS Бренд, Cost AS Стоимость, Quantity AS Количество, OrderDate AS Дата, Adress AS Адрес, CategoryName AS Категория, StatuseName AS Статус FROM MarketOrder INNER JOIN Product ON MarketOrder.ID_Product = Product.ID_Product INNER JOIN Brand ON MarketOrder.ID_Brand = Brand.ID_Brand INNER JOIN ProductCategory ON MarketOrder.ID_Category = ProductCategory.ID_Category INNER JOIN OrderStatus ON MarketOrder.ID_Statusa = OrderStatus.ID_Status AND OrderStatus.ID_Status = 1 INNER JOIN Shop ON MarketOrder.Shop = Shop.ID_Shop;", connection);
            adapter.Fill(dt);
            data.DataSource = dt;
            connection.Close();
        }

        public static void ChangeStatus(string nameLabel, string numLabel, string brandLabel, string date, string categoryLabel, string statusLabel, double cost, int capacity)
        {
            connection.Open();
            if (statusLabel.Equals("Активен"))
            {
                cmd = new SqlCommand("UPDATE MarketOrder SET ID_Statusa = 2 WHERE ID_Order = " + numLabel + ";", connection);
                SqlCommand command = new SqlCommand("UPDATE Product SET Capacity = Capacity - (SELECT Quantity FROM MarketOrder WHERE ID_Order = " + numLabel + ") WHERE ID_Product = (SELECT ID_Product FROM MarketOrder WHERE ID_Order = " + numLabel + ");", connection);
                command.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
            } else
            {
                cmd = new SqlCommand("UPDATE MarketOrder SET ID_Statusa = 1 WHERE ID_Order = " + numLabel + ";", connection);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
            MessageBox.Show("Статус заказа изменен");
        }

        public static void CreateOrder(int id, string product, string brand, string capasity, string adress)
        {
            double Price;
            int st = 1;
            connection.Open();
            cmd = new SqlCommand("INSERT INTO MarketOrder (ID_Order, ID_Product, ID_Brand, Cost, Quantity, OrderDate, Shop, ID_Category, ID_Statusa) VALUES (@ido, @id, @brand, @cost, @capacity, @date, @address, @cat, @status);", connection);
            SqlCommand brandCommand = new SqlCommand("SELECT ID_Brand FROM Product WHERE ID_Product = " + id + ";", connection);
            SqlCommand costCommand = new SqlCommand("SELECT Price FROM Product WHERE ID_Product = " + id + ";", connection);
            SqlCommand adCommand = new SqlCommand("SELECT ID_Shop FROM Shop WHERE Adress LIKE \'%" + adress + "%\';", connection);
            SqlCommand catCommand = new SqlCommand("SELECT ID_Category FROM Product WHERE ID_Product = " + id + ";", connection);
            SqlCommand idoCommand = new SqlCommand("SELECT MAX (ID_Order) FROM MarketOrder", connection);
            Price = Convert.ToDouble(costCommand.ExecuteScalar());
            cmd.Parameters.AddWithValue("@ido", Convert.ToInt32(idoCommand.ExecuteScalar()) + 1);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@brand", Convert.ToInt32(brandCommand.ExecuteScalar()));
            cmd.Parameters.AddWithValue("@cost", Price * Convert.ToInt32(capasity));
            cmd.Parameters.AddWithValue("@capacity", Convert.ToInt32(capasity));
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.Parameters.AddWithValue("@address", Convert.ToInt32(adCommand.ExecuteScalar()));
            cmd.Parameters.AddWithValue("@cat", Convert.ToInt32(catCommand.ExecuteScalar()));
            cmd.Parameters.AddWithValue("@status", st);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
