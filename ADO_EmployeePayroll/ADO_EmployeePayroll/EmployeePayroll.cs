using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_EmployeePayroll
{
    internal class EmployeePayroll
    {
        public static string connectionString = "Data Source=DESKTOP-V5LU9FE\\SQLEXPRESS;Initial Catalog=PAYROLL;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        public void DatabseConnection()
        {
            try
            {
                connection.Open();
                using (connection)
                {
                    Console.WriteLine("Connectivity successful.");
                }
                connection.Close();
            }
            catch
            {
                Console.WriteLine("Connectivity failed");
            }
        }

        public void GetAllEmployees()
        {
            ModelClass model = new ModelClass();
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                string query = @"SELECT * FROM employee_payroll;";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("id\t\tname\t\tbasic_pay\t\t\tstart\n");
                    while (reader.Read())
                    {
                        model.id = reader.GetInt32(0);
                        model.name = reader.GetString(1);
                        model.basic_pay = reader.GetDouble(2);
                        model.start = reader.GetDateTime(3);

                        Console.WriteLine(model.id + "\t\t" + model.name + "\t\t" + model.basic_pay + "\t\t" + model.start);
                    }
                }
                else
                {
                    Console.WriteLine("Records not found in Database.");
                }
                reader.Close();
            }
            connection.Close();
        }

        public bool AddEmployee(ModelClass model)
        {
            connection.Open();
            using (connection)
            {
                SqlCommand command = new SqlCommand("spAddEmployee", this.connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", model.name);
                command.Parameters.AddWithValue("@basic_pay", model.basic_pay);
                command.Parameters.AddWithValue("@start", model.start);
                command.Parameters.AddWithValue("@gender", model.gender);

                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            connection.Close();
        }

        public void UpdateValue()
        {

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    Console.WriteLine("Enter name of employee to update basic pay:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter basic pay to update:");
                    decimal salary = Convert.ToDecimal(Console.ReadLine());
                    connection.Open();
                    string query = "UPDATE employee_payroll set basic_pay =" + salary + "where NAME='" + name + "'";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Records updated successfully.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("-----\nError:Records are not updated.\n-------------");
            }
        }
    }
}
