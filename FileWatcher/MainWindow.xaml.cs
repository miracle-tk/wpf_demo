using FileWatcher.CHFSApi;
using FileWatcher.Model;
using Microsoft.Win32;

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
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Runtime.Serialization.Json;
using WinForm = System.Windows.Forms;
using System.ComponentModel;

namespace FileWatcher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        private bool flag = false;
        private object lockobj =new object();
        private List<String> list = new List<string>();
     //   private RestResponseCookie cookie;
        private ObservableCollection<FindFileModel> findfilelist = new ObservableCollection<FindFileModel>();
        private ObservableCollection<NodeItem> itemlist = new ObservableCollection<NodeItem>();
        private DoubleAnimation searchBoxXposAni;
        private DoubleAnimation searchBoxOpciaty;
        private NodeItem selectedNodeItem;
        //public int TotalFileCount { get; set; }
        //public int CurrentFileCount { get; set; }
        private int _totalFileCount=0;

        public int TotalFileCount
        {
            get { return _totalFileCount; }
            set { _totalFileCount = value;
                OnPropertyChanged("TotalFileCount");
            }
        }
        private int _currentFileCount;
        private  bool canScroll=false;
        private bool isSearching=false;

        public int CurrentFileCount
        {
            get { return _currentFileCount; }
            set
            {
                _currentFileCount = value;
                OnPropertyChanged("CurrentFileCount");
            }
        }


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            searchBoxXposAni = new DoubleAnimation();
            searchBoxXposAni.Duration = TimeSpan.FromSeconds(2);
            searchBoxXposAni.To = 0;
            CHFSServer.Start();
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
           var response= CHFSApi.CHFSApi.Login();
            if (response.code != 201)
            {
                MessageBox.Show("登陆失败！");
                Application.Current.Shutdown();
            }
            var node = new NodeItem { Name = "/", IsDir = true, ImagePath = "folder",IsExpanded=true ,IsSelected=true};
            selectedNodeItem = node;
            itemlist.Add(node);
            tree.ItemsSource = itemlist;
            lv_findresult.ItemsSource = findfilelist;
        }

        private async void  Button_Click(object sender, RoutedEventArgs e)
        {  
           
          

        }

        


        //private async Task<bool> login()
        //{
        //    //await Task.Delay(6000);
        //    var client = new RestClient("http://10.80.145.8:989/chfs/session");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //  //  request.AddHeader("Cookie", "JWT=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2MiOiJhZG1pbiIsImV4cCI6MTYxMDQyNDUyN30.6Wz8zSagZ-1xAc5zQ7khRWQlTfjd-9Z1-7bsP5WNBro; user=admin");
        //    request.AlwaysMultipartFormData = true;
        //    request.AddParameter("user", "FileWatcher");
        //    request.AddParameter("pwd", "123456");
        //    IRestResponse response =await client.ExecuteAsync(request);
        //    if (response.StatusCode != HttpStatusCode.Created)
        //    {
        //        MessageBox.Show("登陆失败！");
        //        return false;
        //    }
        //     cookie=response.Cookies.FirstOrDefault(x => x.Name == "JWT");
        //    if (cookie == null)
        //    {
        //        MessageBox.Show("Cookie获取失败！");
        //        return false; ;
        //    }
        //    return true;
           
        //}
        private async Task<FileResult> getFile(string path)
        {
            // var client = new RestClient("http://10.80.145.8:989/chfs/files");
            // client.Timeout = -1;
            // var request = new RestRequest(Method.GET);
            //// request.AddHeader("Cookie", "user=admin; JWT=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2MiOiJhZG1pbiIsImV4cCI6MTYxMDQyODI5OX0.hYZ95USG2MVUggBQlbsu3HqJbajB35Eg5eYUKDrjTAE");
            // request.AddCookie(cookie.Name, cookie.Value);
            // request.AddQueryParameter("filepath", path);
            // request.AlwaysMultipartFormData = true;
            //  var response =await client.ExecuteAsync<FileResult>(request);
            // return response.Data;
            lock (lockobj)
            {
                ResponseResult r = CHFSApi.CHFSApi.GetFiles(path);
                if (r.code != 200)
                {
                    MessageBox.Show("获取文件列表失败！");
                    return null;
                }
                FileResult model;
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(r.data)))
                {
                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(FileResult));
                    model = (FileResult)deseralizer.ReadObject(ms);// //反序列化ReadObject

                }
                return model;
            }
           
        }

        private void TreeView_Selected(object sender, RoutedEventArgs e)
        {
            var tv=sender as TreeViewItem;
            MessageBox.Show(tv.Header.ToString());
        }

        private async void Tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
             
          
        }

        private async Task AddTreeChildren(NodeItem node,bool isRoot)
        {
           
            
            if (!node.IsDir) { return; }
            if (isRoot)
            {
                var rootnode=  itemlist.FirstOrDefault(x=>x.Name=="/");
                if (rootnode != null)
                {
                    rootnode.Children.Clear();
                }
                var rootresult = await getFile(rootnode.Name);
                foreach (var file in rootresult.Files)
                {
                    var newnode = new NodeItem { Name = file.Name, ParentPath = rootnode.Name, IsDir = file.Dir ,ImagePath=file.Icon };
                    rootnode.Children.Add(newnode);
                }
            }
            else
            {
                node.Children.Clear();
                var result = await getFile(node.ParentPath +"/"+ node.Name);
                if (result == null)
                {
                    return;
                }
                foreach (var file in result.Files)
                {
                    node.Children.Add(new NodeItem {Parent=node, Name = file.Name, ParentPath = (node.ParentPath+"/" + node.Name).Replace("//","/") , IsDir = file.Dir , ImagePath =file.Icon});
                }
            }

            
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // /chfs/search
            if(tb_search.Text==null|| tb_search.Text == string.Empty)
            {
                return;
            }
            var r = await FindFile(tb_search.Text);
            if (r==null|| r.Files == null || r.Files.Count() <= 0)
            {
                MessageBox.Show("查询到的文件太多！请精确查找");
                return;
            }
            // var r = new List<FindFileModel> { new FindFileModel { Name = "ftp.txt", Modified = "2020.11.11",Path="/var" } };

            findfilelist.Clear();
            // foreach (var file in r.Files)
            foreach (var file in r.Files)
            {
                findfilelist.Add(file);
            }
            //xpos.BeginAnimation(TranslateTransform.XProperty, searchBoxXposAni);
            MenuToggleButton.IsChecked = true;
        }
        private async Task<FindFileResult> FindFile(string name)
        {
           var r= CHFSApi.CHFSApi.SearchFile(name);
             if (r.code != 200)
            {
                MessageBox.Show("搜索失败，请精确查找！");
                return null;
            }
            FindFileResult model;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(r.data)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(FindFileResult));
                model = (FindFileResult)deseralizer.ReadObject(ms);// //反序列化ReadObject

            }
            return model;
        }

        private void Tree_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject source = e.OriginalSource as DependencyObject;
            while (source != null && source.GetType() != typeof(TreeViewItem))
                source = System.Windows.Media.VisualTreeHelper.GetParent(source);
            if(!(source is TreeViewItem))
            {
                return;
            }
            var treeitem = source as TreeViewItem;
            treeitem.Focus();
        }

        private void Btn_upload_Click(object sender, RoutedEventArgs e)
        {
            
          // CHFSApi.CHFSApi.MkDir()

        }

        private void Uploadbtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedNodeItem.IsDir)
            {
            UploadFile(tb_currentPath.Text,true);

            }
            else
            {
                var s = System.IO.Path.GetDirectoryName(tb_currentPath.Text);
                UploadFile(s,false);
            }
        }

        private async void UploadFile(string dst,bool isDir)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            
            var open_result = ofd.ShowDialog();

            if (open_result ==true)
            {
                try
                {
                    var s = ofd.FileName;
                    CurrentFileCount = 0;
                    TotalFileCount = 1;
                    ChangeMask(true);
                  var r=  await upload(s, dst);
                    if (r.code != 201)
                    {
                        MessageBox.Show("文件上传失败！(特殊字符或者文件已存在)" + s);
                        return;
                    }
                    CurrentFileCount++;
                   
                }
                catch
                {
                    MessageBox.Show("文件太大！");
                    
                }
                finally
                {
                    ChangeMask(false);
                    var node = isDir ? selectedNodeItem : selectedNodeItem.Parent;
                    node.Children.Clear();
                    AddTreeChildren(node, false);
                    foreach (var child in node.Children)
                    {
                        if (child.IsDir)
                        {
                            await AddTreeChildren(child, false);
                        }
                    }
                    GC.Collect();
                }
                
            }
        }
        private void ChangeMask(bool isShow)
        {
            bd_mask.IsHitTestVisible = isShow;
            bd_mask.Opacity =isShow? 1.0:0d;
        }
        private async Task<ResponseResult> upload(string file,string dst)
        {
                    
            ResponseResult r=new ResponseResult();
            try
            {
                r = CHFSApi.CHFSApi.Exist(file, dst + "/");
                if (r.code != 404)
                {
                    //  MessageBox.Show(file + "已存在！");
                    r.code = 500;
                    return r;
                }
                r = await CHFSApi.CHFSApi.Upload(file, dst);
                return r;
            }
            catch
            {
                r.code = 500;
                return r;
            }
            
        }
        //private async Task<bool> upload(byte[] file, string dst)
        //{
        //    var client = new RestClient("http://10.80.145.8:989/chfs/upload");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    //  request.AddHeader("Cookie", "JWT=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2MiOiJhZG1pbiIsImV4cCI6MTYxMDQyNDUyN30.6Wz8zSagZ-1xAc5zQ7khRWQlTfjd-9Z1-7bsP5WNBro; user=admin");
        //    request.AlwaysMultipartFormData = true;
        //    request.AddCookie(cookie.Name, cookie.Value);
        //    request.AddParameter("file", file);
        //    request.AddParameter("folder", dst);
        //    IRestResponse response = await client.ExecuteAsync(request);
        //    if (response.StatusCode != HttpStatusCode.Created)
        //    {
        //        MessageBox.Show("登陆失败！");
        //        return false;
        //    }
        //    cookie = response.Cookies.FirstOrDefault(x => x.Name == "JWT");
        //    if (cookie == null)
        //    {
        //        MessageBox.Show("Cookie获取失败！");
        //        return false; ;
        //    }
        //    return true;
        //}

        private byte[] FileContent(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    byte[] buffur = new byte[fs.Length];
                    fs.Read(buffur, 0, (int)fs.Length);
                    return buffur;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private async void UploadCatabtn_Click(object sender, RoutedEventArgs e)
        {
            WinForm.FolderBrowserDialog dialog = new WinForm.FolderBrowserDialog();
            
            var result = dialog.ShowDialog();
            if (result == WinForm.DialogResult.OK)
            {
             var s=   dialog.SelectedPath;
                DirectoryInfo di = new DirectoryInfo(s);
                TotalFileCount = 0;
                CurrentFileCount = 0;
                GetAllFileCount(di);
                ChangeMask(true);
                try
                {
                await CreateFolderAndUploadfile(di,true);
                    MessageBox.Show("上传完成：" + CurrentFileCount + "/" + TotalFileCount);
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                ChangeMask(false);
                    
                }
            }

        }

        private void GetAllFileCount(DirectoryInfo di)
        {
           var files = di.GetFiles();
            if (files != null && files.Count() > 0)
            {
                TotalFileCount += files.Count();
            }
            var dis = di.GetDirectories();

            if (dis == null || dis.Count() <= 0)
            {
                return;
            }
            foreach (var childDI in dis)
            {
                GetAllFileCount(childDI);
            }
        }

        private async Task CreateFolderAndUploadfile(DirectoryInfo di,bool isFirst=false,string dirPrefix="")
        {
            if (di.Name.Contains("+")|| di.Name.Contains("&")|| di.Name.Contains("@"))
            {
                MessageBox.Show("文件夹包含非法字符！跳过" + di.FullName + "所有内容");
                return;
            }
            var dirBool = await mkdir(dirPrefix+ di.Name);
           if (isFirst && !dirBool) { MessageBox.Show("创建初始目录失败，请检查目录是否存在！"); return; }
            var files = di.GetFiles();
            string filecata;
            if (selectedNodeItem.IsDir)
            {
                filecata = selectedNodeItem.ParentPath + "/" + selectedNodeItem.Name + "/"+ dirPrefix + di.Name+"/";
            }
            else
            {
                filecata = selectedNodeItem.ParentPath + "/" + dirPrefix + di.Name + "/";

            }
            filecata.Replace("///", "/").Replace("//", "/");
            foreach (var file in files)
            {
                
               var r = await upload(file.FullName,filecata);
                if (r.code != 201)
                {
                    MessageBox.Show(file.FullName + "上传失败，该文件夹结束上传(特殊字符或者文件已存在)");
                    return;
                }
                CurrentFileCount++;
            }
            var dis= di.GetDirectories();
            if (dis.Count() <= 0 || dis == null) return;
            foreach (var childdi in dis)
            {
              await CreateFolderAndUploadfile(childdi,false,dirPrefix+ di.Name + "/");
            }
       
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var cm = FindResource("TreeItemContextMenu") as ContextMenu;
            cm.Visibility = Visibility.Collapsed;
        }

        private async void cmBtn_createCata(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_newDIrName.Text == string.Empty) return;
                if (tb_newDIrName.Text.Contains("@") || tb_newDIrName.Text.Contains("&") ||tb_newDIrName.Text.Contains("+")){
                    MessageBox.Show("包含非法字符");
                    return;
                }
                var name = tb_newDIrName.Text;
                var b =await mkdir(name);
                if (!b)
                {
                    MessageBox.Show("创建文件夹失败");
                }
            }
            catch
            {

            }
           
           
            finally
            {
                var cm = FindResource("TreeItemContextMenu") as ContextMenu;
                cm.Visibility = Visibility.Visible;
            }
           
           
        }
        private async Task<bool> mkdir(string name)
        {
            try
            {
                ResponseResult result;
                if (selectedNodeItem.IsDir)
                {
                    result = CHFSApi.CHFSApi.MkDir((selectedNodeItem.ParentPath + "/" + selectedNodeItem.Name + "/" + name).Replace("//", "/"));
                }
                else
                {
                    result = CHFSApi.CHFSApi.MkDir((selectedNodeItem.ParentPath + "/" + name).Replace("//", "/"));

                }
                if (result.code == 201) return true;
                else return false;

            }
            catch
            {
                return false;
            }
        }



        private void Lv_findresult_CurrentCellChanged(object sender, EventArgs e)
        {
           var o= lv_findresult.CurrentCell.Item;
        }

 

        private void Lv_findresult_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject source = e.OriginalSource as DependencyObject;
            while (source != null && source.GetType() != typeof(DataGridRow))
                source = System.Windows.Media.VisualTreeHelper.GetParent(source);
            if(!(source is DataGridRow))
            {
                return;
            }
            var dgr = source as DataGridRow;
            ShowInTree((FindFileModel)dgr.Item);
        }

        private async void ShowInTree(FindFileModel item)
        {
            if (item == null||item.Path=="") return;
            var tempArry = item.Path.Split(new char[] { '/'}, StringSplitOptions.RemoveEmptyEntries);
            var root = itemlist[0];
       //     root.IsExpanded = false;
           
     //     await  AddTreeChildren(root,false);
             root.IsExpanded= true;
            var tempNode = root;
            isSearching = true;
            foreach (var path in tempArry)
            {
                if (!tempNode.IsDir)
                {
                    tempNode.IsExpanded = true;
                    break;
                }
                var result = tempNode.Children.Where(x => x.Name == path).FirstOrDefault();
                if (result == null) { return; }
                flag = false;
                result.IsSelected = true;
               //result.IsExpanded = true;

                await Task.Run(() =>
                {
                    while (!flag)
                    {

                    }
                });
                flag = false;
                result.IsExpanded = true;
               
               // await Task.Delay(1000);
                tempNode = result;
            }
            tempNode.IsExpanded = true;
            tempNode.IsSelected = true;
            isSearching = false;
        }

       

        private async void Tree_Selected(object sender, RoutedEventArgs e)
        {
            var treeview = sender as TreeView;
            selectedNodeItem = treeview.SelectedItem as NodeItem;
            var node = treeview.SelectedItem as NodeItem;
            if (node == null)
            {
                return;
            }
            if (node.IsDir)
            {
                //    if (node.Name == @"/")
                //    {
                //     await   AddTreeChildren(node, true);
                //    }
                //    else
                //    {
                await FillChildren(node);
                
            }
            flag = true;

            tb_currentPath.Text = (node.ParentPath + "/" + node.Name).Replace("//", "/");
            
        }
        private async Task FillChildren(NodeItem node)
        {
            await AddTreeChildren(node, false);
            // }
            foreach (var child in node.Children)
            {
                if (child.IsDir)
                {
                    await AddTreeChildren(child, false);
                }
            }

        }

       
      

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (isSearching)
            {
                var element = sender as FrameworkElement;
                element.BringIntoView();
              //  e.Handled = true;
            }
            
        }

        private void TreeViewItem_Drop(object sender, DragEventArgs e)
        {
           

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            e.Handled = true;
            
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            (sender as TreeViewItem).IsSelected = true;
            e.Handled = true;
        }
    }
}
