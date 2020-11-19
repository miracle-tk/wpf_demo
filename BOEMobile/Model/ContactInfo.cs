using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBOEMobile.Model
{
    public class ContactInfo:BindableBase
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name ,value); }
        }

        private string lastMessage;

        public string LastMessage
        {
            get { return lastMessage; }
            set {SetProperty(ref lastMessage , value); }
        }

        private string imagePath;

        public string ImagePath
        {
            get { return imagePath; }
            set {SetProperty(ref imagePath , value); }
        }
        private bool isOnline;

        public bool IsOnline
        {
            get { return isOnline; }
            set {SetProperty(ref isOnline , value); }
        }
        private string workStation;

        public string WorkStation
        {
            get { return workStation; }
            set {SetProperty(ref workStation ,value); }
        }

        private string personalSignature;

        public string PersonalSignature
        {
            get { return personalSignature; }
            set { SetProperty(ref personalSignature, value); }
        }
        private List<MessageClass> messageList;

        public List<MessageClass> MessageList
        {
            get { return messageList; }
            set {SetProperty(ref messageList , value); }
        }


    }
}
