﻿using Prism.Mvvm;
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

        public MainWindowViewModel()
        {
            _workTabs = new List<WorkTab>()
            {
                new WorkTab{Text="&#xe621;",Content=null},
                new WorkTab{Text="&#xe621;",Content=null}
            };
        }
    }

    public class WorkTab
    {
        public string Text { get; set; }
        public object Content { get; set; }
    }
}
