using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism;
using Prism.Commands;
using Prism.Mvvm;

namespace Binding_demo.VIewModels
{
    class MainWindowViewModel: BindableBase
    {
        private List<Color> colors ;

        public List<Color> Colors
        {
            get {
                if (colors == null)
                {
                    colors = new List<Color>();
                }
                return colors; }
            set {
                if (colors == null)
                {
                    colors = new List<Color>();
                }
                SetProperty(ref colors, value); }
        }

      
        private string test;
       
        public string Test
        {
            get { return test; }
            set { SetProperty(ref test,value); }
        }

        public DelegateCommand Change { get; set; }
        //public MainWindowViewModel()
        //{
        //    Test = "name";
        //    Change = new DelegateCommand(change);
        //}
        public MainWindowViewModel()
        {
            Test = "hahaaaa";
            Change = new DelegateCommand(change);

            Colors.Add(new Color { Code = "#1111" });
            Colors.Add(new Color { Code = "#2222" });
            Colors.Add(new Color { Code = "#3333" });
            Colors.Add(new Color { Code = "#4444" });

        }
        public MainWindowViewModel(string s)
        {
            Test = s;
            Change = new DelegateCommand(change);
           
            Colors.Add(new Color { Code = "#1111" });
            Colors.Add(new Color { Code = "#2222" });
            Colors.Add(new Color { Code = "#3333" });
            Colors.Add(new Color { Code = "#4444" });
            
        }
        private void change()
        {
            Test = "tk";
        }
    }
    public class Color
    {
        public string Code { get; set; }
    }
}
