using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher.Model
{
    public class NodeItem
    {
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
