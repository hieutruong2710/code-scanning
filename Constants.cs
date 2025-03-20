using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace SecurityTestApp
{
    class Program
    {
        // Hardcoded credentials (Vấn đề bảo mật)
        static string username = "admin";
        static string password = "admin123"; // Hardcoded password

        static void Main(string[] args)
        {
            Console.WriteLine("Enter your username:");
            string userInput = Console.ReadLine(); // Nhập vào từ người dùng

            // SQL Injection (Vấn đề bảo mật)
            string query = "SELECT * FROM Users WHERE Username = '" + userInput + "' AND Password = '" + password + "'"; // Dễ bị tấn công SQL Injection

            SqlConnection connection = new SqlConnection("Server=myserver;Database=mydb;User Id=myuser;Password=mypassword;");
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("Welcome, " + reader["Username"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // Insecure data storage (Lưu trữ dữ liệu không an toàn)
            string sensitiveData = "Sensitive data";
            SaveDataInPlaintext(sensitiveData); // Dữ liệu nhạy cảm được lưu trữ không mã hóa
        }

        // Hàm lưu trữ dữ liệu không an toàn (Insecure storage)
        static void SaveDataInPlaintext(string data)
        {
            System.IO.File.WriteAllText("sensitiveData.txt", data); // Lưu dữ liệu nhạy cảm vào tệp mà không mã hóa
        }

        // Mã hóa không an toàn (Insecure encryption)
        static string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); // Mã hóa bằng SHA256 (không phải là phương pháp an toàn để mã hóa mật khẩu)
                return Convert.ToBase64String(hash);
            }
        }
    }
}
