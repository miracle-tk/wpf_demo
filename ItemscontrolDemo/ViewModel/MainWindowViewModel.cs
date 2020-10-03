using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemscontrolDemo.ViewModel
{
    public class MainWindowViewModel:BindableBase
    {
        private List<dataClass> data =new List<dataClass>();

        public List<dataClass> Data
        {
            get { return data; }
            set { SetProperty(ref data,value); }
        }

        private int col;

        public int Col
        {
            get { return col; }
            set { SetProperty(ref col,value); }
        }

        public MainWindowViewModel()
        {
            col = 5;
            data.Add(new dataClass { num=1,content=new UserControl1()});
            data.Add(new dataClass { num = 2 });
            data.Add(new dataClass { num = 3 });
            data.Add(new dataClass { num = 4 });
            data.Add(new dataClass { num = 5 }); data.Add(new dataClass { num = 6 }); data.Add(new dataClass { num =9 });
        }

    }
    public class dataClass
    {
        public int num { get; set; }
        public Object content { get; set; }
    }
}
