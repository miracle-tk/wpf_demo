using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher.Model
{
    public class NodeItem : INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private bool isExpanded = false;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set { isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }
        private bool isSelected=false;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public NodeItem Parent;
        public string ImagePath { get; set; }
        public string ParentPath { get; set; }
        public string Name { get; set; }
        public bool IsDir { get; set; }
        public ObservableCollection<NodeItem> Children { get; set; }
        public NodeItem()
        {
            Children = new ObservableCollection<NodeItem>();
        }
    }
}
