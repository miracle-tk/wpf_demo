using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace FileWatcher.Model
{
    [DataContract]
    public class FindFileResult
    {
        [DataMember(Name = "virtualRoot")]
        public int VirtualRoot { get; set; }
        [DataMember(Name = "currentMask")]
        public string CurrentMask { get; set; }
        [DataMember(Name = "files")]
        public List<FindFileModel> Files { get; set; }
    }
    [DataContract]
    public class FindFileModel : INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "path")]
        private string _path;

        public string Path
        {
            get { return _path; }
            set { _path = value;
                OnPropertyChanged("Path");
            }
        }

        [DataMember(Name = "dir")]
        public bool Dir { get; set; }
        [DataMember(Name = "size")]
        public int Size { get; set; }
        [DataMember(Name = "modified")]
        public string Modified { get; set; }
        [DataMember(Name = "icon")]
        public string Icon { get; set; }
        [DataMember(Name = "mask")]
        public string Mask { get; set; }
        [DataMember(Name = "guestMask")]
        public string GuestMask { get; set; }
        [DataMember(Name = "hasSubDir")]
        public bool HasSubDir { get; set; }
    }
}
