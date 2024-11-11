using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.SQLite;
using System.IO;

namespace nori
{
    public partial class searchForm : Form
    {
        private static string databaseFile = "samples.sqlite";
        private static string connectionString = $"Data Source={databaseFile};Version=3;";
        public searchForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeDatabase();
            

        }


        // データベースとテーブルの初期化
        private static void InitializeDatabase()
        {
            // データベースファイルが存在しない場合は作成
            if (!File.Exists(databaseFile))
            {
                SQLiteConnection.CreateFile(databaseFile);
                Console.WriteLine("新しいデータベースファイルを作成しました。");
            }

            // テーブルが存在しない場合のみ作成
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Samples (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Text TEXT NOT NULL,
                        
                    )";
                SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("テーブルを初期化しました。");
            }
        }

        // データの挿入
        private static void InsertData(string name, int age, string city)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO People (Text) VALUES (@Name, @Age, @City)";
                SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@City", city);

                command.ExecuteNonQuery();
                Console.WriteLine($"Inserted: {name}, {age}, {city}");
            }
        }

        // データの検索
        private static string SearchData(string name)
        {
            string city = null;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string searchQuery = "SELECT * FROM People WHERE Name LIKE @Name";
                SQLiteCommand command = new SQLiteCommand(searchQuery, connection);
                command.Parameters.AddWithValue("@Name", "%" + name + "%");

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    city = reader["Age"].ToString();
                    Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Age: {reader["Age"]}, City: {reader["City"]}");   
                }
            }
            return city;
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
