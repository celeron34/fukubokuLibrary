namespace nori
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.searchTxt = new System.Windows.Forms.TextBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.searchList = new System.Windows.Forms.ListBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.rmBtn = new System.Windows.Forms.Button();
            this.rmCheckbox = new System.Windows.Forms.CheckBox();
            this.sampleView = new System.Windows.Forms.ListView();
            this.sample = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.current = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // searchTxt
            // 
            this.searchTxt.Location = new System.Drawing.Point(12, 14);
            this.searchTxt.Name = "searchTxt";
            this.searchTxt.Size = new System.Drawing.Size(75, 19);
            this.searchTxt.TabIndex = 2;
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(93, 12);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 6;
            this.searchBtn.Text = "検索";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.sarchBtn_Click);
            // 
            // searchList
            // 
            this.searchList.FormattingEnabled = true;
            this.searchList.ItemHeight = 12;
            this.searchList.Location = new System.Drawing.Point(142, 41);
            this.searchList.Name = "searchList";
            this.searchList.Size = new System.Drawing.Size(107, 136);
            this.searchList.TabIndex = 7;
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(174, 12);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 8;
            this.addBtn.Text = "追加";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // rmBtn
            // 
            this.rmBtn.Location = new System.Drawing.Point(255, 12);
            this.rmBtn.Name = "rmBtn";
            this.rmBtn.Size = new System.Drawing.Size(75, 23);
            this.rmBtn.TabIndex = 9;
            this.rmBtn.Text = "削除";
            this.rmBtn.UseVisualStyleBackColor = true;
            this.rmBtn.Click += new System.EventHandler(this.rmBtn_Click);
            // 
            // rmCheckbox
            // 
            this.rmCheckbox.AutoSize = true;
            this.rmCheckbox.Checked = true;
            this.rmCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rmCheckbox.Location = new System.Drawing.Point(257, 41);
            this.rmCheckbox.Name = "rmCheckbox";
            this.rmCheckbox.Size = new System.Drawing.Size(100, 16);
            this.rmCheckbox.TabIndex = 10;
            this.rmCheckbox.Text = "削除を確認する";
            this.rmCheckbox.UseVisualStyleBackColor = true;
            // 
            // sampleView
            // 
            this.sampleView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sample,
            this.current});
            this.sampleView.HideSelection = false;
            this.sampleView.Location = new System.Drawing.Point(12, 41);
            this.sampleView.Name = "sampleView";
            this.sampleView.Size = new System.Drawing.Size(124, 136);
            this.sampleView.TabIndex = 11;
            this.sampleView.UseCompatibleStateImageBehavior = false;
            this.sampleView.View = System.Windows.Forms.View.Details;
            // 
            // sample
            // 
            this.sample.Text = "お手本";
            this.sample.Width = 82;
            // 
            // current
            // 
            this.current.Text = "向き";
            this.current.Width = 33;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 191);
            this.Controls.Add(this.sampleView);
            this.Controls.Add(this.rmCheckbox);
            this.Controls.Add(this.rmBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.searchList);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.searchTxt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "noriLibrary";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox searchTxt;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.ListBox searchList;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button rmBtn;
        private System.Windows.Forms.CheckBox rmCheckbox;
        private System.Windows.Forms.ListView sampleView;
        private System.Windows.Forms.ColumnHeader sample;
        private System.Windows.Forms.ColumnHeader current;
    }
}

