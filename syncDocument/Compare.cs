using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace syncDocument
{

    class Compare
    {
        const long MAX_COMPARE_MD5_SIZE = 1024 * 1024 * 100;
        static public bool fastCompare = true;
        static public bool ifSame(FileItem a, FileItem b)
        {
            if (a.getFileSize() == b.getFileSize())
            {
                if (a.getFileSize() < MAX_COMPARE_MD5_SIZE)
                    return fastCompare||ifFileMD5Same(a, b);
                else
                    return true;
            }
            return false;

        }
        static public bool ifFileMD5Same(FileItem a, FileItem b)
        {
            return a.getMD5_value() == b.getMD5_value();
        }
        CmpDealer cmpDealer;
        public Compare(CmpDealer travel)
        {
            cmpDealer = travel;
        }
        public void compare(BaseItem sourcefile, BaseItem targetfile)
        {
            if (sourcefile.type == FileType.T_File)
            {
                if (ifSame((FileItem)sourcefile, (FileItem)targetfile))
                {
                    cmpDealer.same(sourcefile, targetfile);
                    sourcefile.state = DealState.Synced;
                    targetfile.state = DealState.Synced;
                }
                else
                {
                    cmpDealer.notSame(sourcefile, targetfile);
                    sourcefile.state = DealState.Synced;
                }

            }
            else  // if folder directly into next level
            {
                var s_subfiles = ((FolderItem)sourcefile).getSubFiles();
                var t_subfiles = ((FolderItem)targetfile).getSubFiles();

                foreach (var pair in s_subfiles)
                {
                    String indexName = pair.Key;
                    //if has this folder/file
                    if (t_subfiles.ContainsKey(indexName))
                    {
                        compare(s_subfiles[indexName], t_subfiles[indexName]);
                        s_subfiles[indexName].state = DealState.Synced;
                        t_subfiles[indexName].state = DealState.Synced;
                    }
                    //if doesn't has this folder
                    else
                    {
                        cmpDealer.notExist(s_subfiles[indexName], (FolderItem)targetfile);
                        s_subfiles[indexName].state = DealState.Synced;
                    }

                }
                if(s_subfiles.Count==117)
                {
                    int i = 0;
                    i++;
                }
                //delete files
                foreach(var pair  in t_subfiles)
                {
                    if (pair.Value.state == DealState.WaitingForDeal)
                    {
                        cmpDealer.redundancyFile(pair.Value);
                        pair.Value.state = DealState.Synced;
                    }
                }
            }
        }
    }
}
