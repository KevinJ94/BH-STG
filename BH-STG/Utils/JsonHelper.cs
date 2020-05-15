using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH_STG.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BH_STG.Utils
{
    public class JsonHelper
    {
        public static void ObjToJsonFile(Data obj)
        {
            DirectoryInfo rootDir = Directory.GetParent(Environment.CurrentDirectory);
            string root = rootDir.Parent.Parent.Parent.FullName;
            string configPath = root + @"\Config\Config.json";
            string config = JsonConvert.SerializeObject(obj);
            File.WriteAllText(configPath, config);
        }

        public static Data JsonFileToObj()
        {
            DirectoryInfo rootDir = Directory.GetParent(Environment.CurrentDirectory);
            string root = rootDir.Parent.Parent.Parent.FullName;
            string configPath = root + @"\Config\Config.json";
            var obj = JsonConvert.DeserializeObject<Data>(File.ReadAllText(configPath));
            return obj;
        }


    }
}
