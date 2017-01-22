using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;

namespace syncDocument
{
    enum CmdType
    {
        Copy,
        Delete,
    }
    class CmdItem
    {
        public BaseItem source;
        public BaseItem target;
        public CmdType type;
        public bool enable;
        static public CmdItem CopyCmd(BaseItem s,FolderItem targetFolder){
            return new CmdItem(s, targetFolder, CmdType.Copy);
        }
        static public CmdItem DeleteCmd(BaseItem t)
        {
            return new CmdItem(null, t, CmdType.Delete);
        }
        CmdItem(BaseItem s, BaseItem t, CmdType tp)
        {
            source = s;
            target = t;
            type = tp;
            enable = true;
        }
        public void execute(){
            if (enable == false) return;
            if (type == CmdType.Delete)
            {
                target.delete();
            }
            else if(type == CmdType.Copy)
            {
                ((FolderItem)target).copy(source);
            }
        }
        public void outPut()
        {
            if (type == CmdType.Delete)
            {
                Console.WriteLine("Delete {0}", target.getFullFileName());
            }
            else if (type == CmdType.Copy)
            {
                Console.WriteLine("Copy {0} {1}", source.getFullFileName(), target.getFullFileName());
            }
        }

    }
    class CmdList
    {
        List<CmdItem> cmdList;
        public   List<CmdItem> getList(){return cmdList;}
        public CmdList()
        {
            cmdList = new List<CmdItem>();
        }
        public void addDelete(BaseItem t)
        {
            CmdItem ci =  CmdItem.DeleteCmd(t);
            CompareReporter.addResult(ci,cmdList.Count);
            cmdList.Add(ci);
        }
        public void addCopy(BaseItem source, FolderItem toFolder)
        {
            CmdItem ci = CmdItem.CopyCmd(source, toFolder);
            CompareReporter.addResult(ci,cmdList.Count);
            cmdList.Add(ci);
        }
        public void execute()
        {
            foreach (var c in cmdList)
            {
                c.execute();
                ProgressReporter.addProgress(c);
            }
        }

        public void outPut()
        {
            foreach (var cmd in cmdList)
            {
                cmd.outPut();
            }
        }

        public CmdItem getCmdItem(int index) { return cmdList[index]; }

    }
}
