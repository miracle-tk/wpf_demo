using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemscontrolDemo.ViewModel
{
    public class User_Control1ViewModel:BindableBase
    {
        private string input;

        public string Input
        {
            get { return input; }
            set { SetProperty(ref input,value); }
        }

    }
}
