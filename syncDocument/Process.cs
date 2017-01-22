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
    public partial class Process : Form
    {
        TravelSync travelSync;
        public Process(TravelSync ts):
            this()
        {
            travelSync = ts;
        }

        Process()
        {
            InitializeComponent();
        }

        private void Process_Shown(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ProgressReporter.setup(backgroundWorker1,travelSync);
            BaseItem.ifCanceled = false;
            travelSync.sync();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            String str = ProgressReporter.getStr();
            progressBar1.Value = e.ProgressPercentage;
            AppendMessage(ProgressReporter.getStr());
        }

        delegate void AppendMessageCallback(string text);

        public void AppendMessage(string text)
        {
            if (this.InvokeRequired)
            {
                AppendMessageCallback d = new AppendMessageCallback(AppendMessage);
                this.Invoke(d, text);
            }
            else
            {
                richTextBox1.AppendText(ProgressReporter.getStr());
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value =100;
            richTextBox1.AppendText("完成!  ");
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
            cancelBtn.Text = "完成";
            cancelBtn.Click -= new System.EventHandler(this.cancelBtn_Click);
            cancelBtn.Click += new System.EventHandler(this.endBtnClick);
        }

        private void endBtnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            BaseItem.ifCanceled = true;
        }
    }
}
