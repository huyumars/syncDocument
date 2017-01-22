using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncDocument
{
   interface CmpDealer
    {
        void same(BaseItem a, BaseItem b);
        void notSame(BaseItem a, BaseItem b);
        void notExist(BaseItem sourceFile, FolderItem targetFolder);
        void redundancyFile(BaseItem targetFile);

    }


}
