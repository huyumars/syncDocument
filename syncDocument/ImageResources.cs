using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Resources;
using System.Reflection;
namespace syncDocument
{
    class ImageResources
    {
        ImageResources() {
            imagePool = new Dictionary<string, System.Drawing.Image>();
        }
        public static ImageResources instance = new ImageResources();
        Dictionary<String, System.Drawing.Image> imagePool;
        System.Drawing.Image GetImageFromPath(string path)
        {
           // ResourceManager rm = new ResourceManager("Resources", Assembly.GetExecutingAssembly());
            // 获取当前类库的程序集  
            Assembly assembly = Assembly.GetExecutingAssembly();
            String projectName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
            // 从程序集中读取插件的菜单和工具栏配置信息(xml文件内容)  syncDocument.Resources.DeleteIcon.png
            var s = assembly.GetManifestResourceStream(projectName + ".Resources."  + path);
            return System.Drawing.Image.FromStream(s);
           
            //System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            //System.Drawing.Image result = System.Drawing.Image.FromStream(fs);
            //fs.Close();
            //return result;
        }
        public System.Drawing.Image GetImage(String str)
        {
            if (imagePool.ContainsKey(str))
                return imagePool[str];
            return GetImageFromPath(str);
        }
    }
}
