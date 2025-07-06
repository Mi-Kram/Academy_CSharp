using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FolderItem;

namespace FolderItemsSearcher
{
    public class Files
    {
        public delegate void NewDirItem(DirItemInfo item);
        public event NewDirItem OnNewItem;

        public delegate void SetProgressBarMaximum(double max);
        public event SetProgressBarMaximum OnSetProgressBarMaximum;

        public delegate void TakeAwayProgressBarMaximum(double val);
        public event TakeAwayProgressBarMaximum OnTakeAwayProgressBarMaximum;

        public void Search(string path, string folderSearch, string fileSearch)
        {
            GetDirsAndFiles(path, out List<DirectoryInfo> dirs, out List<FileInfo> files, folderSearch, fileSearch);

            int maximum = dirs.Count() + files.Count();
            OnSetProgressBarMaximum?.Invoke(maximum);

            foreach (DirectoryInfo dir in dirs)
            {
                OnNewItem?.Invoke(new DirItemInfo { Name = dir.Name, Path = dir.FullName, CreationDate = dir.CreationTime });
                //Thread.Sleep(50);
            }
            foreach (FileInfo file in files)
            {
                try
                {
                    string icoPath = "Resources/Icons/" + file.Extension.Trim('.') + ".ico";
                    if (!File.Exists(icoPath))
                    {
                        FileStream stream = new FileStream(icoPath, FileMode.Create, FileAccess.Write, FileShare.None);
                        System.Drawing.Icon formIcon = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                        formIcon.Save(stream);
                        stream.Close();
                    }

                    OnNewItem?.Invoke(new DirItemInfo { Img = Environment.CurrentDirectory + "\\" + icoPath, Name = System.IO.Path.GetFileNameWithoutExtension(file.Name), Ext = file.Extension, Path = file.FullName, Size = file.Length, CreationDate = file.CreationTime });
                }
                catch
                {
                    OnTakeAwayProgressBarMaximum?.Invoke(1);
                }
                //Thread.Sleep(50);
            }
        }

        void GetDirsAndFiles(string path, out List<DirectoryInfo> dirs, out List<FileInfo> files, string folderSearch = "*", string fileSearch = "*")
        {
            dirs = new List<DirectoryInfo>();
            files = new List<FileInfo>();

            ReadFolder(path, dirs, files, folderSearch, fileSearch);
        }

        void ReadFolder(string path, List<DirectoryInfo> dirs, List<FileInfo> files, string folderSearch, string fileSearch)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);
            try
            {
                files.AddRange(dInfo.GetFiles(fileSearch));
            }
            catch { }
            try
            {
                DirectoryInfo[] allDirs = dInfo.GetDirectories();

                foreach (DirectoryInfo dir in allDirs)
                {
                    ReadFolder(dir.FullName, dirs, files, folderSearch, fileSearch);
                }
                dirs.AddRange(dInfo.GetDirectories(folderSearch));
            }
            catch { }
        }

    }
}
