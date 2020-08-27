using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MircoSoftToDoDemo.Models;
using Prism.Commands;
using Prism.Mvvm;
namespace MircoSoftToDoDemo.ViewModel
{
    public class MainWindowViewModel:BindableBase
    {
        private string _addstring;

        public string AddString
        {
            get { return _addstring; }
            set { SetProperty(ref _addstring, value); }
        }
        private TaskInfo _taskitem;

        public TaskInfo TaskItem
        {
            get { return _taskitem; }
            set { SetProperty(ref _taskitem, value); }
        }
        private Visibility _expandVisibility=Visibility.Collapsed;

        public Visibility ExpandVisibility
        {
            get { return _expandVisibility; }
            set { SetProperty(ref _expandVisibility, value); ; }
        }

        public DelegateCommand SelectedCommand { get; set; }
        public DelegateCommand AddTaskCommand { get; set; }
        public DelegateCommand<TaskInfo> DeleteTaskCommand { get; set; }
        public DelegateCommand ClickTask { get; set; }
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
                var taskinfo = new TaskInfo()
                {
                    Detail = "aaaaaaaaa"
                };
                
                _meums = new ObservableCollection<MeumModel>();
                _meums.Add(new MeumModel() { Icon = "\xe623", Menu = "我的一天" ,BackColor="Green",Display=false});
                _meums.Add(new MeumModel() { Icon = "\xe662", Menu = "重要", BackColor = "Red" });
                _meums.Add(new MeumModel() { Icon = "\xe662", Menu = "已计划日程", BackColor = "#218868" });
                _meums.Add(new MeumModel() { Icon = "\xe662", Menu = "任务", BackColor = "Blue" });
                _meums[3].TaskInfos.Add(taskinfo);
                MeumItem = _meums[0];
            }
            if (SelectedCommand == null)
            {
                SelectedCommand = new DelegateCommand(Select);
            }
            if (AddTaskCommand == null)
            {
                AddTaskCommand = new DelegateCommand(AddTask);
                DeleteTaskCommand = new DelegateCommand<TaskInfo>(DeleteTask);
                ClickTask = new DelegateCommand(clicktask);
            }
        }

        private void clicktask()
        {
            ExpandVisibility = Visibility.Visible;
        }

        private void DeleteTask(TaskInfo ti)
        {
           int index =  Meums.IndexOf(MeumItem);
            Meums[index].TaskInfos.Remove(ti);
           // TaskItem = null;
        }

        private void AddTask()
        {
            MeumItem.TaskInfos.Add(new TaskInfo()
            {
                Detail = AddString
            });
            AddString = "";
        }

        private bool canExecuteMethod()
        {
            return true;
        }

        private void Select()
        {
            AddString = "";
        }
    }
}
