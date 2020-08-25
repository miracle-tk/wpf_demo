using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MircoSoftToDoDemo.Models;
using Prism.Mvvm;
namespace MircoSoftToDoDemo.ViewModel
{
    public class MainWindowViewModel:BindableBase
    {

        private ObservableCollection<MeumModel> _meums;

        public ObservableCollection<MeumModel> Meums
        {
            get { return _meums; }
            set { SetProperty(ref _meums,value); }
        }

        public MainWindowViewModel()
        {
            if (_meums == null)
            {
                _meums = new ObservableCollection<MeumModel>();
                _meums.Add(new MeumModel() { Icon = "\xe662", Menu = "我的一天" });
                _meums.Add(new MeumModel() { Icon = "\xe662", Menu = "重要" });
                _meums.Add(new MeumModel() { Icon = "\xe662", Menu = "已计划日程" });
                _meums.Add(new MeumModel() { Icon = "\xe662", Menu = "任务" });

            }
        }

    }
}
