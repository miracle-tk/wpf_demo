using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace test1
{
   public static class GolbalValue
    {
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;


        private static ObservableCollection<LineInformation> datalist = new ObservableCollection<LineInformation>();

        public static ObservableCollection<LineInformation> DataList
        {
            get { return datalist; }
            set
            {
                datalist = value;

            }
        }
        private static  int maxCol=3;

        public static int MaxCol
        {
            get { return maxCol; }
            set { maxCol = value; StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(MaxCol))); }
        }

        static GolbalValue()
        {
            for (int i = 0; i < 5; i++)
            {
                var line = new LineInformation { LineName = $"G{ i}" };
                line.EQPList.Add(new EQPInformation { EQPName = "GEGIS" });
                line.EQPList.Add(new EQPInformation { EQPName = "GEGIS1" });
                DataList.Add(line);
            }

            Task.Run(async () => {
                var list = new List<LineInformation>();
                while (true)
                {
                    // Thread.Sleep(1000);
                    await Task.Delay(1000);
                    MaxCol++;
                    list = DataList.ToList();
                    LineInformation line = new LineInformation { LineName = $"0" };
                    line.EQPList.Add(new EQPInformation { EQPName = "DDEGIS" });
                    line.EQPList.Add(new EQPInformation { EQPName = "DDEGIS1" });
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        DataList.Add(line);
                        //  DataList= new ObservableCollection<LineInformation>(list);

                    }));
                }


            });
        }
    }
}
