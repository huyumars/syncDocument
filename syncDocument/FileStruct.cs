using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace syncDocument
{
    public enum FileType
    {
        T_Folder = 1,
        T_File = 2,
    }
    public enum WRAttribution
    {
        None=0,
        ReadOnly,
        ReadWrite,
    }

    public enum DealState
    {
        WaitingForDeal = 0,
        Synced,
        Modified,
    }
    public abstract class BaseItem
    {
        protected WRAttribution wrAttrib;   //读写权限
        protected String name;
        protected FolderItem rootPath;
        public FileType type;
        public DealState state;
        public String getName()
        {
            if (rootPath != null) return name;
            else if (type == FileType.T_File) return System.IO.Path.GetFileName(getFullFileName());
            else
            {
                return name.Substring(name.LastIndexOf('\\')+1);
            }
        }
        public FolderItem getRootPath()
        {
            return rootPath;
        }
        public BaseItem(String _name, FolderItem root)
        {
            rootPath = root;
            name = _name;
            state = DealState.WaitingForDeal;
            if (root != null)      //防止读写操作
                wrAttrib = root.wrAttrib;   
            else wrAttrib = WRAttribution.None;
        }
        public virtual String getFullFileName()
        {
            if (rootPath != null)
                return rootPath.getFullFileName() + getIndexName();
            else return name+"\\";
        }
        public virtual String getIndexName() { return getName(); } // "abc/" is folder  "abc" is file
        public virtual bool ifValid()
        {
            if (type == FileType.T_File && File.Exists(getFullFileName()))
                return true;
            else if (type == FileType.T_Folder && Directory.Exists(getFullFileName()))
                return true;
            return false;
        }
        protected virtual void copyImplement(BaseItem sourceFile) { }
        protected virtual void deleteImplement() { }
        public static bool ifCanceled;
        //static int count = 0;
        public void delete()
        {
            if (wrAttrib == WRAttribution.ReadWrite&&!ifCanceled)
            {
                deleteImplement();
                ProgressReporter.report("delete  " + getFullFileName());

            }
        }
        public void copy(BaseItem sourceFile)
        {
            if (wrAttrib == WRAttribution.ReadWrite&&!ifCanceled)
            {
               copyImplement(sourceFile);
                if(sourceFile.type==FileType.T_File)
                    ProgressReporter.report("copy  " + sourceFile.getFullFileName() + " to " + getFullFileName());            
            }
        }

    }

   public  class FolderItem : BaseItem
    {
        Dictionary<String, BaseItem> subFiles;  // "abc/" is folder  "abc" is file
        public override String getIndexName()
        {
            return getName()+"\\";
        }
        public FolderItem(String _name, FolderItem root)
            : base(_name, root)
        {
            type = FileType.T_Folder;
        }
        public static FolderItem createSourceFolder(String rootPath)
        {
            var f = new FolderItem(rootPath, null);
            f.wrAttrib = WRAttribution.ReadOnly;
            return f;
        }
       public static FolderItem createTargetFolder(String rootPath)
        {
            var f = new FolderItem(rootPath, null);
            f.wrAttrib = WRAttribution.ReadWrite;
            return f;
        }
        public Dictionary<String, BaseItem> getSubFiles()
       {
           if (subFiles == null&&ifValid())
           {
               subFiles = new Dictionary<string, BaseItem>();
               DirectoryInfo dif = new DirectoryInfo(getFullFileName());
               System.IO.FileSystemInfo[] flist = dif.GetFileSystemInfos();
               foreach (FileSystemInfo finfo in flist)
               {
                   if (finfo is FileInfo)
                   {
                       FileItem f = new FileItem(finfo.Name, this);                   
                       subFiles[f.getIndexName()] = f;
                   }
                   else
                   {
                       FolderItem f = new FolderItem(finfo.Name, this);
                       subFiles[f.getIndexName()] = f;
                   }
               }
           }
           return subFiles;
       }

        protected override void deleteImplement()
        {
            System.Collections.IEnumerator it = getSubFiles().GetEnumerator();
            foreach (var pair in getSubFiles())
            {
                pair.Value.delete();
            }
            String path = getFullFileName();
            System.IO.Directory.Delete(getFullFileName());
        }

        protected override void copyImplement(BaseItem sourceFile)
        {

            if (sourceFile.type == FileType.T_File)
            {
                System.IO.File.Copy(sourceFile.getFullFileName(), getFullFileName() + sourceFile.getIndexName(), true);
            }
            else
            {
                    
                FolderItem folder = new FolderItem( sourceFile.getName(), this);
                folder.wrAttrib = this.wrAttrib;
                System.IO.Directory.CreateDirectory(folder.getFullFileName());
                foreach (var pair in ((FolderItem)sourceFile).getSubFiles())
                {
                    folder.copy(pair.Value);
                }

            }
            
        }
    }
    public class FileItem : BaseItem
    {
        String MD5_value;
        long  fileSize;
        public FileItem(String _name, FolderItem root)
            : base(_name, root)
        {
            type = FileType.T_File;
            fileSize =-1;
        }
        public static String GetMD5HashFromFile(String fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open,FileAccess.Read);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                file.Dispose();
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
        public String getMD5_value()
        {
            if (MD5_value == null)
            {
                MD5_value = GetMD5HashFromFile(getFullFileName());
            }
            return MD5_value;
        }

        public long  getFileSize(){
            if(fileSize==-1){
                FileInfo  fileInfo = new FileInfo(getFullFileName());
                fileSize = fileInfo.Length;
            }
            return fileSize;
        }
        protected override void deleteImplement()
        {
            System.IO.File.Delete(getFullFileName());
        }
    }
}
