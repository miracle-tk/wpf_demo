using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher.Model
{
    [DataContract]
    public class FileResult
    {
        [DataMember(Name = "virtualRoot")]
        public int VirtualRoot { get; set; }
        [DataMember(Name ="currentMask")]
        public string CurrentMask { get; set; }
        [DataMember(Name ="files")]
        public List<FileModel> Files { get; set; }
    }
    [DataContract]
    public class FileModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "dir")]
        public bool Dir { get; set; }
        [DataMember(Name = "size")]
        public int Size { get; set; }
        [DataMember(Name = "modified")]
        public string Modified { get; set; }
        [DataMember(Name = "icon")]
        public string Icon{ get; set; }
        [DataMember(Name = "mask")]
        public string Mask { get; set; }
        [DataMember(Name = "guestMask")]
        public string GuestMask { get; set; }
        [DataMember(Name = "hasSubDir")]
        public bool HasSubDir { get; set; }
    }
}
