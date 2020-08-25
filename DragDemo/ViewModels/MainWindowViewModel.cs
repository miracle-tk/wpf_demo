using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace DragDemo.ViewModels
{
    class MainWindowViewModel:BindableBase
    {
        private List<Test> tests;

        public MainWindowViewModel()
        {
            Tests = new List<Test>();
            Tests.Add(
                new Test { Code = 1 }

                );
            Tests.Add(new Test { Code = 2 });
            Tests.Add(new Test { Code = 3 });
            Tests.Add(new Test { Code = 4 });
            Tests.Add(new Test { Code = 5 });
            Tests.Add(new Test { Code = 6 });
            Tests.Add(new Test { Code = 7 });
            Tests.Add(new Test { Code = 8 });
            Tests.Add(new Test { Code = 9 });

        }

        public List<Test> Tests
        {
            get { return tests; }
            set { SetProperty(ref tests,value); }
        }


    }

    public class Test
    {
        public int Code { get; set; }
    }
}
