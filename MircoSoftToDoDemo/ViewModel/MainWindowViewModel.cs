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
        private MeumModel _meumItem;

        public MeumModel MeumItem
        {
            get { return _meumItem; }
            set { SetProperty(ref _meumItem, value); }
        }


        public MainWindowViewModel()
        {
            if (_meums == null)
            {
                _meums = new ObservableCollection<MeumModel>();
                _meums.Add(new MeumModel() { Icon = "\xe623", Menu = "我的一天" ,BackColor="Green",Display=false});
                _meums.Add(new MeumModel() { Icon = "\xe662", Menu = "重要", BackColor = "Red" });
                _meums.Add(new MeumModel() { Icon = "\xe662", Menu = "已计划日程", BackColor = "#218868" });
                _meums.Add(new MeumModel() { Icon = "\xe662", Menu = "任务", BackColor = "Blue" });
                MeumItem = _meums[0];
            }
        }

    }
}
