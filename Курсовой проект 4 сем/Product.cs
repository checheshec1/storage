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
    class Product
    {
        private static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
        private static SqlDataAdapter adapter;
        private static SqlCommand cmd;

        public static void DisplayClientData(DataGridView data)
        {
            connection.Open();
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter("SELECT ID_Product AS Номер, ProductName AS Название, BrandName AS Бренд, Price AS Цена, Capacity AS Количество, CountryName AS Производитель, CategoryName AS Категория FROM Product INNER JOIN Brand ON Product.ID_Brand = Brand.ID_Brand INNER JOIN Country ON Product.ID_Country = Country.ID_Country INNER JOIN ProductCategory ON Product.ID_Category = ProductCategory.ID_Category;", connection);
            adapter.Fill(dt);
            data.DataSource = dt;
            connection.Close();
        }

        public static void AddProduct(string name, string brand, string cat, string country, string price, string count)
        {
            if (name != "" && brand != "" && cat != "" && country != "" && price != "" && count != "")
            {
                cmd = new SqlCommand("INSERT INTO Product (ProductName, ID_Brand, ID_Category, ID_Country, Price, Capacity) VALUES (@name, @brand, @cat, @country, @money, @count);", connection);
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT ID_Brand FROM Brand WHERE BrandName LIKE \'%" + brand + "%\';", connection);
                SqlCommand command1 = new SqlCommand("SELECT ID_Category FROM ProductCategory WHERE CategoryName LIKE \'%" + cat + "%\';", connection);
                SqlCommand countryCommand = new SqlCommand("SELECT ID_Country FROM Country WHERE CountryName LIKE \'%" + country + "%\';", connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@brand", Convert.ToInt32(command.ExecuteScalar()));
                cmd.Parameters.AddWithValue("@cat", Convert.ToInt32(command1.ExecuteScalar()));
                cmd.Parameters.AddWithValue("@country", Convert.ToInt32(countryCommand.ExecuteScalar()));
                cmd.Parameters.AddWithValue("@money", Convert.ToDouble(price));
                cmd.Parameters.AddWithValue("@count", Convert.ToInt32(count));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Товар добавлен");
                connection.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        public static void ChangeProduct(string name, string brand, string cat, string country, string price, string count)
        {
            if (name != "" && brand != "" && cat != "" && country != "" && price != "" && count != "")
            {
                cmd = new SqlCommand("UPDATE Product SET ProductName = @name, ID_Brand = @brand, ID_Category = @cat, ID_Country = @country, Price = @money, Capacity = @count; WHERE ProductName = \'" + name + "\'", connection);
                SqlCommand brandCommand = new SqlCommand("SELECT ID_Brand FROM Brand WHERE BrandName LIKE \'%" + brand + "%\';", connection);
                SqlCommand categoryCommand = new SqlCommand("SELECT ID_Category FROM ProductCategory WHERE CategoryName LIKE \'%" + cat + "%\';", connection);
                SqlCommand countryCommand = new SqlCommand("SELECT ID_Country FROM Country WHERE CountryName LIKE \'%" + country + "%\';", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@brand", Convert.ToInt32(brandCommand.ExecuteScalar()));
                cmd.Parameters.AddWithValue("@cat", Convert.ToInt32(categoryCommand.ExecuteScalar()));
                cmd.Parameters.AddWithValue("@country", Convert.ToInt32(countryCommand.ExecuteScalar()));
                cmd.Parameters.AddWithValue("@money", Convert.ToDouble(price));
                cmd.Parameters.AddWithValue("@count", Convert.ToInt32(count));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Товар изменен");
                connection.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        public static void DeleteProduct(string name)
        {
            if (name != "")
            {
                cmd = new SqlCommand("DELETE Product WHERE ProductName = \'" + name + "\'", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Товар удален");
            }
            else
            {
                MessageBox.Show("Заполните поле \"Название товара\"");
            }
        }

    }
}
