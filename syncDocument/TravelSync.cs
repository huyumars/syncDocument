using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace syncDocument
{

    
    public class TravelSync: CmpDealer
    {
        FolderItem sourceFolder;
        FolderItem targetFolder;
        Compare fileCmp;
        CmdList syncCmdList;
        public int getCmdListCount() { return syncCmdList.getList().Count; }
        public TravelSync(FolderItem from, FolderItem to)
        {
            sourceFolder = from;
            targetFolder = to;
            fileCmp = new Compare(this);
            syncCmdList = new CmdList();
        }
        public void sync()
        {           
            syncCmdList.outPut();
            syncCmdList.execute();
        }
        public void compare()
        {
            fileCmp.compare(sourceFolder, targetFolder);
        }
        public void modifyCmdList()
        {
            CompareReporter.updateCmdList(syncCmdList);
        }
        //CmpDealer interface
        public void same(BaseItem a, BaseItem b)
        {

        }
        public void notSame(BaseItem a, BaseItem b)
        {
            syncCmdList.addCopy(a, b.getRootPath());
            syncCmdList.addDelete(b);
        }
        public void notExist(BaseItem sourceFile, FolderItem targetFolder)
        {
            syncCmdList.addCopy(sourceFile, targetFolder);
        }
        public void redundancyFile(BaseItem targetFile)
        {
            syncCmdList.addDelete(targetFile);
        }
       
    }
}
