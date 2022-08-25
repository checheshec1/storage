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
    class Personal
    {
        private static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
        private static SqlDataAdapter adapter;
        private static SqlCommand cmd;

        public static void DisplayPersonalData(DataGridView data)
        {
            connection.Open();
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter("SELECT ID_User AS Номер, SecondName AS Фамилия, FirstName AS Имя, LastName AS Отчество, Age AS Возраст, Sex AS Пол, Post AS Должность, Salary AS Зарплата, RoleName AS Роль, UserLogin AS Логин, UserParol AS Пароль FROM Users INNER JOIN Roles ON Users.ID_Role = Roles.ID_Role;", connection);
            adapter.Fill(dt);
            data.DataSource = dt;
            connection.Close();
        }

        public static void AddPersonal(string login, string parol, string name, string surname, string dadname, string age, string sex, string post, string salary, string role)
        {
            if (name != "" && surname != "" && dadname != "" && age != "" && sex != "" && post != "" && salary != "" && role != "")
            {
                cmd = new SqlCommand("INSERT INTO Users (UserLogin, UserParol, FirstName, SecondName, LastName, Age, Sex, Post, Salary, ID_Role) VALUES (@login, @parol, @name, @surname, @dadname, @age, @sex, @post, @salary, @role);", connection);
                SqlCommand roleCommand = new SqlCommand("SELECT ID_Role FROM Roles WHERE RoleName = \'" + role + "\';",connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@parol", parol);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@surname", surname);
                cmd.Parameters.AddWithValue("@dadname", dadname);
                cmd.Parameters.AddWithValue("@age", Convert.ToInt32(age));
                cmd.Parameters.AddWithValue("@sex", sex);
                cmd.Parameters.AddWithValue("@post", post);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(salary));
                cmd.Parameters.AddWithValue("@role", roleCommand.ExecuteScalar());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Данные сотрудника добавлены");
                connection.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        public static void ChangePersonal(string login, string parol, string name, string surname, string dadname, string age, string sex, string post, string salary, string role, int id)
        {
            if (name != "" && surname != "" && dadname != "" && age != "" && sex != "" && post != "" && salary != "" && role != "")
            {
                cmd = new SqlCommand("UPDATE Product SET UserLogin = @login, UserParol = @parol, FirstName = @name, SecondName = @surname, LastName = @dadname, Age = @age, Post = @post, Salary = @salary, ID_Role = @role WHERE ID_User = " + id + ";", connection);
                SqlCommand roleCommand = new SqlCommand("SELECT ID_Role FROM Roles WHERE RoleName = \'" + role + "\';");
                connection.Open();
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@parol", parol);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@surname", surname);
                cmd.Parameters.AddWithValue("@dadname", dadname);
                cmd.Parameters.AddWithValue("@age", Convert.ToInt32(age));
                cmd.Parameters.AddWithValue("@sex", sex);
                cmd.Parameters.AddWithValue("@post", post);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(salary));
                cmd.Parameters.AddWithValue("@role", roleCommand.ExecuteScalar());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Данные сотрудника изменены");
                connection.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        public static void DeletePersonal(int id, string name)
        {
            if (name != "")
            {
                cmd = new SqlCommand("DELETE Users WHERE ID_user = " + id + ";", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Данные сотрудника удалены");
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }
    }
}
