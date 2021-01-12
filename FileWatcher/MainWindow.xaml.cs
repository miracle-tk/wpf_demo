using FileWatcher.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileWatcher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<String> list = new List<string>();
        private RestResponseCookie cookie;
        private ObservableCollection<FindFileModel> findfilelist = new ObservableCollection<FindFileModel>();
        private ObservableCollection<NodeItem> itemlist = new ObservableCollection<NodeItem>();
        private DoubleAnimation searchBoxXposAni;
        private DoubleAnimation searchBoxOpciaty;
        public MainWindow()
        {
            InitializeComponent();
            searchBoxXposAni = new DoubleAnimation();
            searchBoxXposAni.Duration = TimeSpan.FromSeconds(2);
            searchBoxXposAni.To = 0;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
            //var overTimeTask = Task.Delay(5000);
            //var loginTask = login();
            //var t = await Task.WhenAny(loginTask, overTimeTask);
            //if (t == overTimeTask)
            //{
            //    MessageBox.Show("连接超时");
            //    Application.Current.Shutdown();
            //}
            itemlist.Add(new NodeItem { Name = "/", IsDir = true, ParentPath = "/", ImagePath = "folder" });
            tree.ItemsSource = itemlist;
            lv_findresult.ItemsSource = findfilelist;
        }

        private async void  Button_Click(object sender, RoutedEventArgs e)
        {
           
           


        }

        


        private async Task<bool> login()
        {
            //await Task.Delay(6000);
            var client = new RestClient("http://10.80.145.8:989/chfs/session");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
          //  request.AddHeader("Cookie", "JWT=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2MiOiJhZG1pbiIsImV4cCI6MTYxMDQyNDUyN30.6Wz8zSagZ-1xAc5zQ7khRWQlTfjd-9Z1-7bsP5WNBro; user=admin");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("user", "FileWatcher");
            request.AddParameter("pwd", "123456");
            IRestResponse response =await client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                MessageBox.Show("登陆失败！");
                return false;
            }
             cookie=response.Cookies.FirstOrDefault(x => x.Name == "JWT");
            if (cookie == null)
            {
                MessageBox.Show("Cookie获取失败！");
                return false; ;
            }
            return true;
           
        }
        private async Task<FileResult> getFile(string path)
        {
            var client = new RestClient("http://10.80.145.8:989/chfs/files");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
           // request.AddHeader("Cookie", "user=admin; JWT=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2MiOiJhZG1pbiIsImV4cCI6MTYxMDQyODI5OX0.hYZ95USG2MVUggBQlbsu3HqJbajB35Eg5eYUKDrjTAE");
            request.AddCookie(cookie.Name, cookie.Value);
            request.AddQueryParameter("filepath", path);
            request.AlwaysMultipartFormData = true;
             var response =await client.ExecuteAsync<FileResult>(request);
            return response.Data;
        }

        private void TreeView_Selected(object sender, RoutedEventArgs e)
        {
            var tv=sender as TreeViewItem;
            MessageBox.Show(tv.Header.ToString());
        }

        private async void Tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tvi = e.OriginalSource as TreeViewItem;
            var node = e.NewValue as NodeItem;
            if (node.IsDir)
            {
                if (node.Name == @"/")
                {
                    AddTreeChildren(node, true);
                }
                else
                {
                    AddTreeChildren(node, false);
                }
                foreach (var child in node.Children)
                {
                    if (child.IsDir)
                    {
                        AddTreeChildren(child, true);
                    }
                }
                
            }
            tb_currentPath.Text = node.ParentPath + node.Name;
           
        }

        private async void AddTreeChildren(NodeItem node,bool isRoot)
        {
            if (isRoot)
            {
                var rootnode=  itemlist.FirstOrDefault(x=>x.Name=="/");
                if (rootnode != null)
                {
                    rootnode.Children.Clear();
                }
                var rootresult = await getFile(rootnode.ParentPath);
                foreach (var file in rootresult.Files)
                {
                    var newnode = new NodeItem { Name = file.Name, ParentPath = rootnode.ParentPath, IsDir = file.Dir ,ImagePath=file.Icon };
                    rootnode.Children.Add(newnode);
                }
            }
            else
            {
                node.Children.Clear();
                var result = await getFile(node.ParentPath + node.Name);
                foreach (var file in result.Files)
                {
                    node.Children.Add(new NodeItem { Name = file.Name, ParentPath = node.ParentPath + node.Name + "/", IsDir = file.Dir , ImagePath =file.Icon});
                }
            }
            
           
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // /chfs/search
            // var r = await FindFile(tb_search.Text);
            //if (r.Files == null || r.Files.Count() <= 0)
            //{
            //    MessageBox.Show("查询到的文件太多！请精确查找");
            //        return;
            //}
           var r = new List<FindFileModel> { new FindFileModel { Name = "ftp.txt", Modified = "2020.11.11",Path="/var" } };
           
            // findfilelist.Clear();
            // foreach (var file in r.Files)
            foreach (var file in r)
            {
                findfilelist.Add(file);
            }
            //xpos.BeginAnimation(TranslateTransform.XProperty, searchBoxXposAni);
            MenuToggleButton.IsChecked = true;
        }
        private async Task<FindFileResult> FindFile(string name)
        {
            var client = new RestClient("http://10.80.145.8:989/chfs/search");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            // request.AddHeader("Cookie", "user=admin; JWT=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2MiOiJhZG1pbiIsImV4cCI6MTYxMDQyODI5OX0.hYZ95USG2MVUggBQlbsu3HqJbajB35Eg5eYUKDrjTAE");
            request.AddCookie(cookie.Name, cookie.Value);
            request.AddQueryParameter("str", name);
            request.AlwaysMultipartFormData = true;
            var response = await client.ExecuteAsync<FindFileResult>(request);
            return response.Data;
        }
    }
}
