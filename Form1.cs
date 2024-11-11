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
    public partial class Form1 : Form
    {
        private static string databaseFile = "samples.sqlite";
        private static string connectionString = $"Data Source={databaseFile};Version=3;";
        public Form1()
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
                    CREATE TABLE IF NOT EXISTS samples (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        sample TEXT NOT NULL
                    )";
                SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("テーブルを初期化しました。");
            }
        }

        // データの挿入
        private static void InsertData(string sample)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO samples (sample) VALUES (@sample)";
                SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@sample", sample);

                command.ExecuteNonQuery();
                Console.WriteLine($"Inserted: {sample}");
            }
        }

        // データの削除
        private void removeData(string sample)
        {  
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string removeQuery = "DELETE FROM samples WHERE sample = @sample";
                SQLiteCommand command = new SQLiteCommand(removeQuery, connection);
                command.Parameters.AddWithValue("@sample", sample);
                
                command.ExecuteNonQuery();
                Console.WriteLine($"Deleted: {sample}");
            }
        }

        private void sarchBtn_Click(object sender, EventArgs e)
        {
            if ("" == searchTxt.Text)
            {
                MessageBox.Show("検索ワードを入力してください．", "検索エラー", MessageBoxButtons.OK);
                searchTxt.Focus();
                return;
            }
            List<string> result = searchData(searchTxt.Text);
            if(result.Count == 0)
            {
                MessageBox.Show("検索ワードが見つかりませんでした．", "検索エラー", MessageBoxButtons.OK);
                searchTxt.Focus();
                return;
            }
            //Console.WriteLine(result);
            searchList.Items.Clear();
            foreach (string r in result)
            {
                searchList.Items.Add(r);
            }
        }

        // データの検索
        private static List<string> searchData(string line)
        {
            List<string> samples = new List<string>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM samples WHERE sample LIKE @Line";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Line", "%" + line + "%");

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    samples.Add(reader["sample"].ToString());
                    //Console.WriteLine($"ID: {reader["id"]}, sample: {reader["sample"]}");
                }
            }
            return samples;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            InsertData(searchTxt.Text);
        }

        private void rmBtn_Click(object sender, EventArgs e)
        {
            if (searchList.SelectedItem == null)
            {
                MessageBox.Show("削除対象を選択してください．", "削除エラー", MessageBoxButtons.OK);
                return;
            }
            string selectedItem = searchList.SelectedItem.ToString();
            if (searchData(selectedItem).Count == 0)
            {
                MessageBox.Show("削除対象が見つかりませんでした．", "削除エラー", MessageBoxButtons.OK);
                return;
            }
            if (rmCheckbox.Checked)
            {
                DialogResult msgRes = MessageBox.Show(selectedItem + " を削除します．\nよろしいですか？", "削除確認", MessageBoxButtons.OKCancel);
                if(msgRes == DialogResult.Cancel)
                {
                    searchTxt.Focus();
                    return;
                }
            }
            removeData(selectedItem);
            searchList.Items.Clear();
            List<string> result = searchData(searchTxt.Text);
            foreach(string r in result)
            {
                searchList.Items.Add(r);
            }
        }
    }
}
