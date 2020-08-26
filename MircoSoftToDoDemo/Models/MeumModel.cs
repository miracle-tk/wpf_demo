using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MircoSoftToDoDemo.Models
{
    public class MeumModel
    {
        public string Icon { get; set; }
        public string Menu { get; set; }
        public string BackColor { get; set; }
        public bool Display { get; set; } = true;
        private ObservableCollection<TaskInfo> _taskinfos=new ObservableCollection<TaskInfo>();

        public ObservableCollection<TaskInfo> TaskInfos
        {
            get { return _taskinfos; }
            set { _taskinfos = value; }
        }

    }
}
