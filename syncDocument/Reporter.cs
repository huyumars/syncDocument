using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace syncDocument
{
    class ProgressReporter :  Reporter
    {
        //interface
        static public String getStr()
        {
            return instance.cacheStr;
        }
        static public void report(String message)
        {
            instance.reportDetail(message);
        }
        static public void setup(BackgroundWorker _worker, TravelSync ts)
        {
            instance.allTask = ts.getCmdListCount();
            instance.currentProgress = 0;
            instance.worker = _worker;
        }
        static public void addProgress(CmdItem exeC)
        {
            instance.currentProgress++;
            if (instance.currentProgress ==instance.allTask)
            {
                instance.cacheStr = instance.reportStr;
            }
            instance.updateViews();

        }

        ProgressReporter() { }
        static  ProgressReporter instance = new ProgressReporter();
        BackgroundWorker worker;
        double allTask;
        double currentProgress;

        protected override void updateViews()
        {
            int p = (int)((100 * currentProgress) / allTask);
            worker.ReportProgress(p);
        }
    }
    class CompareReporter : Reporter
    {
        static DataRow createRow(CmdItem ci,int index)
        {
            if (instance.cmdTable != null)
            {
                var dr = instance.cmdTable.NewRow();
                dr["序号"] = index;
                dr["选择"] = true;
                dr["操作目标"] = ci.target.getFullFileName();

                if (ci.type == CmdType.Copy)
                {
                    dr["源目标"] = ci.source.getFullFileName();
                    dr["操作类型"] = ImageResources.instance.GetImage("copyIcon.png");
                    dr["目标格式"] = System.IO.Path.GetExtension(ci.source.getFullFileName());
                }
                else if(ci.type==CmdType.Delete)
                {
                    dr["操作类型"] = ImageResources.instance.GetImage("DeleteIcon.png");
                    dr["目标格式"] = System.IO.Path.GetExtension(ci.target.getFullFileName());
                }
                return dr;
            }
            return null;
        }
        public static void updateCmdList(CmdList cl)
        {
            foreach (DataRow data in instance.cmdTable.Rows)
            {
                CmdItem ci = cl.getCmdItem((int)data["序号"]);
                ci.enable = (bool)data["选择"];
            }
        }
        public static void report(String message)
        {
            instance.reportDetail(message);
        }
        CompareReporter() { }
        static CompareReporter instance = new CompareReporter();
        static public void setup(DataGridView v)
        {
            instance.dataView = v;
            //v.ColumnCount = 4;
            //v.Columns[0].Name = "选择";
            //v.Columns[1].Name = "操作类型";
            //v.Columns[2].Name = "操作目标";
            //v.Columns[3].Name = "源目标";
            instance.cmdTable = new DataTable();
            instance.cmdTable.Columns.Add("序号", typeof(int));
            instance.cmdTable.Columns.Add("选择", System.Type.GetType("System.Boolean"));
            instance.cmdTable.Columns.Add("操作类型", typeof(System.Drawing.Image));
            instance.cmdTable.Columns.Add("目标格式");
            instance.cmdTable.Columns.Add("操作目标");
            instance.cmdTable.Columns.Add("源目标");

            v.DataSource= instance.cmdTable;
        }
        private delegate void UpdateDataGridView(DataTable dt);
        private void UpdateGV(DataTable dt)
        {
            if (dataView.InvokeRequired)
            {
                dataView.BeginInvoke(new UpdateDataGridView(UpdateGV), new object[] { dt });
            }
            else
            {
                dataView.DataSource = dt;
                dataView.Refresh();
            }
        }
        public static void addResult(CmdItem result, int index)
        {
          
            instance.cmdTable.Rows.Add(createRow(result,index));
            instance.UpdateGV(instance.cmdTable);
            
        }
        DataTable cmdTable;
        DataGridView dataView;
    }
    abstract class Reporter
    {
        protected String reportStr;
        protected String cacheStr;
        public Reporter() {  reportStr = ""; }
        private static readonly object lockHelper = new object();
        DateTime lastUpdateTime;
        protected void reportDetail(String details)
        {
            reportStr += details + "\r\n";
            if (lastUpdateTime == null) lastUpdateTime = System.DateTime.Now;
            var currentTime = System.DateTime.Now;
            if (currentTime.Subtract(lastUpdateTime).TotalMilliseconds > 20)
            {
                cacheStr = reportStr;
                reportStr = "";
                updateViews();
                lastUpdateTime = currentTime;

            }
        }
        protected virtual void updateViews() { }
    }
}
