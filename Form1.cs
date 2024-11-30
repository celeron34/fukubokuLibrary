using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace nori
{
    public partial class Form1 : Form
    {
        private readonly static string databaseFile = "samples.sqlite";
        private readonly static string connectionString = $"Data Source={databaseFile};Version=3;";
        public Form1()
        {
            InitializeComponent();

            this.MinimumSize = new System.Drawing.Size(282, 247);
            this.MaximumSize = new System.Drawing.Size(282, int.MaxValue); ;
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

            // テーブルが存在しない場合,作成
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS samples (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        sample TEXT NOT NULL UNIQUE
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
        private void RemoveData(string sample)
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

        private void SarchBtn_Click(object sender, EventArgs e)
        {
            searchList.Items.Clear();
            if ("" == searchTxt.Text)
            {
                MessageBox.Show("検索ワードを入力してください．", "検索エラー", MessageBoxButtons.OK);
                searchTxt.Focus();
                return;
            }

            List<string> result = SearchData(searchTxt.Text);
            if (result.Count == 0)
            { // 部分一致がない
                searchTxt.Focus();
                addBtn.Enabled = true;
                return;
            }
            
            //Console.WriteLine(result);
            foreach (string r in result) { searchList.Items.Add(r); }

            if (!result.Contains(searchTxt.Text))
            {  // 完全一致がない
                addBtn.Enabled = true;
            }
            else
            {
                searchList.SelectedItem = searchTxt.Text;
            }
        }

        private void searchTxt_TextChanged(object sender, EventArgs e)
        {
            addBtn.Enabled = false;
        }
        private void searchTxt_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) { SarchBtn_Click(sender, e); }
        }

        private void searchList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (searchList.SelectedItems != null)
            {
                Console.WriteLine("rmBtn.Enabled = true");
                rmBtn.Enabled = true;
            }
            else 
            {
                Console.WriteLine("rmBtn.Enabled = false");
                rmBtn.Enabled = false;
            }
        }

        // データの検索
        private static List<string> SearchData(string line)
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

        private void AddBtn_Click(object sender, EventArgs e)
        {
            InsertData(searchTxt.Text);
            // リスト更新
            List<string> result = SearchData(searchTxt.Text);
            searchList.Items.Clear();
            foreach (string r in result) { searchList.Items.Add(r); }

            searchTxt.Focus();
            addBtn.Enabled = false;
            searchList.SelectedItem = searchTxt.Text;
            MessageBox.Show(searchTxt.Text + " を追加しました．", "追加完了", MessageBoxButtons.OK);
        }

        private void RmBtn_Click(object sender, EventArgs e)
        {
            if (searchList.SelectedItem == null)
            {
                MessageBox.Show("削除対象を選択してください．", "削除エラー", MessageBoxButtons.OK);
                return;
            }
            string selectedItem = searchList.SelectedItem.ToString();
            if (SearchData(selectedItem).Count == 0)
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
            RemoveData(selectedItem);
            searchList.Items.Clear();
            List<string> result = SearchData(searchTxt.Text);
            foreach(string r in result)
            {
                searchList.Items.Add(r);
            }
        }

    }
}
