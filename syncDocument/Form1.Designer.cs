namespace syncDocument
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.sourceSltBtn = new System.Windows.Forms.Button();
            this.targetSltBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.startSyncBtn = new System.Windows.Forms.Button();
            this.travelCompare = new System.ComponentModel.BackgroundWorker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // sourceSltBtn
            // 
            this.sourceSltBtn.Location = new System.Drawing.Point(18, 18);
            this.sourceSltBtn.Margin = new System.Windows.Forms.Padding(4);
            this.sourceSltBtn.Name = "sourceSltBtn";
            this.sourceSltBtn.Size = new System.Drawing.Size(112, 34);
            this.sourceSltBtn.TabIndex = 0;
            this.sourceSltBtn.Text = "源文件夹";
            this.sourceSltBtn.UseVisualStyleBackColor = true;
            this.sourceSltBtn.Click += new System.EventHandler(this.sourceSltBtn_Click);
            // 
            // targetSltBtn
            // 
            this.targetSltBtn.Location = new System.Drawing.Point(18, 62);
            this.targetSltBtn.Margin = new System.Windows.Forms.Padding(4);
            this.targetSltBtn.Name = "targetSltBtn";
            this.targetSltBtn.Size = new System.Drawing.Size(112, 34);
            this.targetSltBtn.TabIndex = 1;
            this.targetSltBtn.Text = "目标文件夹";
            this.targetSltBtn.UseVisualStyleBackColor = true;
            this.targetSltBtn.Click += new System.EventHandler(this.targetSltBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(141, 20);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(398, 28);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(141, 60);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(400, 28);
            this.textBox2.TabIndex = 3;
            // 
            // startSyncBtn
            // 
            this.startSyncBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startSyncBtn.Location = new System.Drawing.Point(194, 516);
            this.startSyncBtn.Margin = new System.Windows.Forms.Padding(4);
            this.startSyncBtn.Name = "startSyncBtn";
            this.startSyncBtn.Size = new System.Drawing.Size(176, 92);
            this.startSyncBtn.TabIndex = 4;
            this.startSyncBtn.Text = "开始比较";
            this.startSyncBtn.UseVisualStyleBackColor = true;
            this.startSyncBtn.Click += new System.EventHandler(this.startSyncBtn_Click);
            // 
            // travelCompare
            // 
            this.travelCompare.DoWork += new System.ComponentModel.DoWorkEventHandler(this.travelCompare_DoWork);
            this.travelCompare.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.travelCompare_RunWorkerCompleted);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 133);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(524, 230);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::syncDocument.Properties.Resources.waiting;
            this.pictureBox1.Location = new System.Drawing.Point(262, 472);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 37);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(60, 104);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(106, 22);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "快速同步";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 626);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.startSyncBtn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.targetSltBtn);
            this.Controls.Add(this.sourceSltBtn);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sourceSltBtn;
        private System.Windows.Forms.Button targetSltBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button startSyncBtn;
        private System.ComponentModel.BackgroundWorker travelCompare;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

