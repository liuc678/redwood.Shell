using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace redwood.Shell
{
    [Serializable]
    public class CustomConfig
    {
        static CustomConfig _current;
        public static CustomConfig Current
        {
            get
            {
                if(_current == null)
                {
                    _current = Load();
                }
                return _current;
            }
        }
        
        public string URL { get; private set; }

        public bool SetURL(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                this.URL = url;
                return true;
            }
            else
                return false;
        }

        const string Report_Path = "fastreports";
        public static string GetReport_Path(bool bcreate = false)
        {
            string path = System.IO.Path.Combine(Application.StartupPath, Report_Path);
            if (bcreate)
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            return path;
        }

        static string GetFileName()
        {
            return Application.StartupPath + "\\data.dat";
        }

        public void SaveToFile()
        {
            ObjectSerialize.SaveToFile(GetFileName(), this);
        }

        public static CustomConfig Load()
        {
            var obj = ObjectSerialize.LoadFromFile<CustomConfig>(GetFileName());
            if (obj == null)
            {
                obj = new CustomConfig();
            }
            return obj;
        }
    }

    public static class ObjectSerialize
    {
        public static void SaveToFile(string fileName, object obj)
        {
            using (FileStream file = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter br = new BinaryFormatter();
                br.Serialize(file, obj);
                file.Close();
            }
        }

        public static T LoadFromFile<T>(string fileName) where T : class
        {
            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (file == null)
                    return null;
                if(file.Length <= 0)
                {
                    file.Close();
                    return null;
                }

                try
                {
                    BinaryFormatter br = new BinaryFormatter();
                    var data = (T)br.Deserialize(file);
                    file.Close();
                    return data;
                }
                catch (Exception E)
                {
                    file.Close();
                }
            }
            return null;
        }
    }
}
