using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Prism;
using Prism.Commands;
using Prism.Mvvm;

namespace Binding_demo.VIewModels
{
    class MainWindowViewModel: BindableBase
    {
        public DelegateCommand LoadCommand;
        private List<Color> colors ;
        private string content;

        public string Content
        {
            get { return content; }
            set { 
                SetProperty(ref content,value);
            }
        }

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
            LoadCommand = new DelegateCommand(Load);

            Content = "0";
            Task.Run(async () => {
                int i = 0;
                while (true)
                {
                    Thread.Sleep(2000);
                    Content += i.ToString();
                    i++;
                }

            });
        }

        public void Load()
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
            LoadCommand = new DelegateCommand(Load);
            //Test = s;
            //Change = new DelegateCommand(change);
           
            //Colors.Add(new Color { Code = "#1111" });
            //Colors.Add(new Color { Code = "#2222" });
            //Colors.Add(new Color { Code = "#3333" });
            //Colors.Add(new Color { Code = "#4444" });
            
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
