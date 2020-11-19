using MyBOEMobile.View;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBOEMobile.ViewModel
{
    public class MainWindowViewModel: BindableBase
    {
        public int MyProperty { get; set; }
        private List<WorkTab> _workTabs;

        public List<WorkTab> WorkTabs
        {
            get { return _workTabs; }
            set {SetProperty(ref _workTabs , value); }
        }

        private WorkTab _selectWorkTab;

        public WorkTab SelectWorkTab
        {
            get { return _selectWorkTab; }
            set { SetProperty(ref  _selectWorkTab , value); }
        }

        public MainWindowViewModel()
        {
            _workTabs = new List<WorkTab>()
            {
                new WorkTab{Text="\xe62f",Content=new ChatView()},
                new WorkTab{Text="\xe667",Content=null},
                new WorkTab{Text="\xe60a",Content=null},
                new WorkTab{Text="\xe63e",Content=null}
            };
            SelectWorkTab = WorkTabs[0];
        }
    }

    public class WorkTab
    {
        public string Text { get; set; }
        public object Content { get; set; }
    }
}
