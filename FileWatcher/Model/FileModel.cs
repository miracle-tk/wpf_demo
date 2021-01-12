using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher.Model
{
    public class FileResult
    {
        public int VirtualRoot { get; set; }
        public string CurrentMask { get; set; }
        public List<FileModel> Files { get; set; }
    }
    public class FileModel
    {
        public string Name { get; set; }
        public bool Dir { get; set; }
        public int Size { get; set; }
        public string Modified { get; set; }
        public string Icon{ get; set; }
        public string Mask { get; set; }
        public string GuestMask { get; set; }
        public bool HasSubDir { get; set; }
    }
}
