using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass
{
    [Serializable]
    public class FileInfo
    {
        public string FileName { get; set; }

        public string SHA { get; set; }

        //设置文件的SHA
        public void SetFileSHA(string path, string fullFileName)
        {
            this.SetFileName(path, fullFileName);
            this.SHA = GetFileSHA(fullFileName);
        }

        public void SetFileName(string path, string fullFileName)
        {
            this.FileName = fullFileName.Replace(path, "");
            if (FileName[0] == '\\')
                this.FileName = this.FileName.Substring(1);
        }
        
        public bool CheckValid(string path)
        {
            string fullFileName = System.IO.Path.Combine(path, FileName);           
            var local_sha = GetFileSHA(fullFileName);
            
            return this.SHA == local_sha;
        }

        string GetFileSHA(string fullFileName)
        {
            return Utils.ComputeFileHash(fullFileName);
        }
    }

    [Serializable]
    public class FileInfoManage
    {
        public FileInfoManage()
        {
            
        }

        public string Version { get; set; }

        public string SystemName { get; set; }

        public string extFileName { get; set; }

        public string StartExe_Name { get; set; }

        public string StartExe_Args { get; set; }
        
        List<FileInfo> _list = new List<FileInfo>();
                
        public FileInfo[] List
        {
            get
            {
                return _list.ToArray();
            }
            set {
                _list.Clear();
                _list.AddRange(value);
            }
        }

        public void Make(string rootPath,string[] excludeFileNameList)
        {
            _list.Clear();
            var dir = new DirectoryInfo(rootPath);
            FindFile(dir, rootPath,excludeFileNameList);
        }

        /// <summary>
        /// 将文件发布到指定目录
        /// </summary>
        /// <param name="publishPath"></param>
        /// <param name="extFileName"></param>
        public void Publish(string rootPath, string publishPath,bool bclear)
        {
            if(bclear)
            {
                if (Directory.Exists(publishPath))
                    Directory.Delete(publishPath, true);
                Directory.CreateDirectory(publishPath);
            }
            foreach(var f in this._list)
            {    
                string src = Path.Combine(rootPath, f.FileName);
                string desc = Path.Combine(publishPath, f.FileName + extFileName);
                string path = System.IO.Path.GetDirectoryName(desc);
                if(!Directory.Exists(desc))
                {
                    Directory.CreateDirectory(path);
                }
                File.Copy(src, desc,true);
            }
            this.extFileName = extFileName;
        }

        public FileInfo[]  BiJiao(FileInfoManage src)
        {
            var list = new List<FileInfo>();
            if(src == null)
            {
                return _list.ToArray();
            }

            foreach(var f in _list)
            {
                bool bFind = false;
                foreach(var df in src.List)
                {
                    if (string.Compare(f.FileName, df.FileName, true) == 0)
                    {
                        bFind = true;
                        if (f.SHA != df.SHA)
                            list.Add(f);
                        break;
                    }
                }
                if (!bFind)
                    list.Add(f);
            }
            return list.ToArray();
        }

        void FindFile(System.IO.DirectoryInfo path,string rootPath, string[] excludeFileNameList)
        {
            var list = path.GetFileSystemInfos();
            foreach (var systeminfo in list)
            {                
                if (systeminfo is DirectoryInfo)
                {
                    FindFile(systeminfo as DirectoryInfo, rootPath,excludeFileNameList);
                }
                else
                {
                    System.IO.FileInfo file = systeminfo as System.IO.FileInfo;
                    if ((file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        continue;
                    var fileInfo = new FileInfo();
                    fileInfo.SetFileName(rootPath, file.FullName);
                    bool bskip = false;
                    foreach (string excludeF in excludeFileNameList)
                    {                        
                        if (string.Compare(fileInfo.FileName, excludeF,true)==0)
                        {
                            bskip = true;
                            break;
                        }
                    }
                    if (bskip)
                        continue;
                    
                    fileInfo.SetFileSHA(rootPath, file.FullName);
                    _list.Add(fileInfo);                   
                }
            }
        }

        public void SaveToFile(string fileName)
        {
            var s = new SerializerXML();
            s.SerializeToXml(fileName,this);
        }

        public static FileInfoManage LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                return null;
            var s = new SerializerXML();
            return s.DeserializeFromXml<FileInfoManage>(fileName);
        }
    }
}
