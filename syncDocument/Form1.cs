using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace syncDocument
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog folderDlg;
        String sourcePath;
        String targetPath;
        TravelSync travelSync;
        void setSourcePath(String newp)
        {
            textBox1.Text = newp;
            sourcePath = newp;
        }
        void setTargetPath(String newp)
        {
            targetPath = newp;
            textBox2.Text = newp;
        }
        public Form1()
        {
            InitializeComponent();
            folderDlg = new FolderBrowserDialog();
            CompareReporter.setup(dataGridView1);
            pictureBox1.Visible = false;
            dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler (this.Grid_CellFormatting);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            folderDlg.Description = "123123";
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
            setSourcePath("E:\\1234");
            setTargetPath("E:\\123456");
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (System.Windows.Forms.CheckState.Checked == checkBox1.CheckState)
            {
                Compare.fastCompare = true;
            }
            else if (System.Windows.Forms.CheckState.Unchecked == checkBox1.CheckState)
            {
                Compare.fastCompare = false;
            }
        }

        private void sourceSltBtn_Click(object sender, EventArgs e)
        {
            folderDlg.ShowDialog();
            setSourcePath(folderDlg.SelectedPath);
        }

        private void targetSltBtn_Click(object sender, EventArgs e)
        {
            folderDlg.ShowDialog();
            setTargetPath(folderDlg.SelectedPath);
        }

        private void startSyncBtn_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            if (travelSync == null)
            {
                travelCompare.RunWorkerAsync();
                startSyncBtn.Enabled = false;
            }
            else
            {
                travelSync.modifyCmdList();
                Process pc = new Process(travelSync);
                pc.ShowDialog();
            }
        }

        private void travelCompare_DoWork(object sender, DoWorkEventArgs e)
        {
            
            travelSync = new TravelSync(FolderItem.createSourceFolder(sourcePath), FolderItem.createTargetFolder(targetPath));
            travelSync.compare();
            
        }

        private void travelCompare_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Visible = false;
            startSyncBtn.Enabled = true;
            startSyncBtn.Text = "开始同步";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var i = dataGridView1.Columns[e.ColumnIndex].CellType;
            if (((DataTable)dataGridView1.DataSource).Columns[e.ColumnIndex].DataType == System.Type.GetType("System.Boolean"))
            {
                var dr =( (DataTable)dataGridView1.DataSource  ).Rows[e.RowIndex];
                dr[e.ColumnIndex] = !(Boolean)dr[e.ColumnIndex];
                bool newValue = (Boolean)dr[e.ColumnIndex];
                foreach (DataGridViewRow c in dataGridView1.SelectedRows)
                {
                    DataRowView drv = (DataRowView)c.DataBoundItem;
                    ((DataTable)dataGridView1.DataSource).Rows[(int)drv[0]][e.ColumnIndex] = newValue;
                }
                dataGridView1.Refresh();
            }
          
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("操作类型"))
            //{
            //    string path = e.Value.ToString();
            //    e.Value = ImageResources.instance.GetImage(path);
            //}
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.Index < 3)
            {
                e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                e.Column.Width = 40;
            }
        } 
    }
}
