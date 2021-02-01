using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureMQClient;
using FutureMQClient.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;

namespace FutureClient.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            UserName = XmlHelper.GetInnerText(G.doc, "ClientName");
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

    }
}
