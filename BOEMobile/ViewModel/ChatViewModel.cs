using MyMobile.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMobile.ViewModel
{
    public class ChatViewModel:BindableBase
    {
        public ChatViewModel()
        {
            ContactInfos = new List<ContactInfo>
            {
                new ContactInfo
                {
                    Name="张烨",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="哦",
                    IsOnline=true,
                    WorkStation="智造推进技术部_企划管理岗",
                    PersonalSignature="出差到10月份，白天进线，有事请留言",
                    MessageList=new List<MessageClass>
                    {
                        new MessageClass{ IsMe=true,Message="send1"},
                        new MessageClass{ IsMe=false,Message="reply1"},

                    }

                },
                new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                    ,
                    IsOnline=false,
                    WorkStation="CIM科_信息系统建设岗位",
                    PersonalSignature="座机2590"
                    
                },
                 new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                },
                  new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                },
                   new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                },
                    new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                },
                     new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                },
                      new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                },
                       new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                },
                        new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                },
                         new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                },
                          new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                },
                           new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                },
                            new ContactInfo
                {
                    Name="陈强",
                    ImagePath="../Image/gungun.jpg",
                    LastMessage="毕竟手头上的版本不同"
                }
            };
            SelectedContactPerson = ContactInfos[0];
        }
        private List<ContactInfo> contactInfos;

        public List<ContactInfo> ContactInfos
        {
            get { return contactInfos; }
            set {SetProperty(ref contactInfos , value); }
        }
        private ContactInfo selectedContactPerson;

        public ContactInfo SelectedContactPerson
        {
            get { return selectedContactPerson; }
            set {
                ToUserImagePath = value.ImagePath;
                SetProperty(ref selectedContactPerson , value); }
        }
        private string toUserImagePath;

        public string ToUserImagePath
        {
            get { return toUserImagePath; }
            set {SetProperty(ref toUserImagePath , value); }
        }


    }
}
