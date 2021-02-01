using FutureClient.ViewModel;
using FutureMQClient;
using FutureMQClient.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace FutureClient
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public static UserInfo USER;
        SqlSugarClient LoginDb;
        public Login()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
            LoginDb = G.GetInstance("SqlConnString");
        }

        private void AzureLoginButton_Click(object sender, RoutedEventArgs e)
        {
            login.IsEnabled = false;
            //var User = LoginDb.Queryable<UserInfo>().Where(o => o.USERNAME == XmlHelper.GetInnerText(G.doc, "ClientName")).ToList();
            var User = LoginDb.Queryable<UserInfo>().Where(o => o.combi_no == UserName.Text).ToList();
            if (User.Count == 1)
            {
                if(User[0].PASSWORD == PasswordBox.Password)
                {
                    XmlNode xn = G.doc.SelectSingleNode("//ClientName");
                    xn.InnerText = UserName.Text;
                    G.doc.Save(@"config.xml");
                    USER = User[0];
                    var MyMainWindow = new MainWindow();
                    MyMainWindow.Show();
                    this.Close();
                }
                else
                {
                    login.IsEnabled = true;
                    MessageBox.Show("密碼輸入錯誤");
                }
            }
            else
            {
                login.IsEnabled = true;
                MessageBox.Show("未註冊的用戶，請聯繫管理員開通權限");
            }
        }

        private void PasswordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AzureLoginButton_Click(sender,e);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pop.IsPopupOpen = false;
        }

        private void ModifyPsw(object sender, RoutedEventArgs e)
        {
            var User = LoginDb.Queryable<UserInfo>().Where(o => o.asset_no == XmlHelper.GetInnerText(G.doc, "ClientName")).ToList();
            if (User.Count == 1)
            {
                if (User[0].PASSWORD == OldPsw.Password)
                {
                    if(NewPsw.Password == ConfirmPsw.Password)
                    {
                        UserInfo newPswInfo = new UserInfo();
                        newPswInfo.USERNAME = User[0].USERNAME;
                        newPswInfo.PASSWORD = NewPsw.Password;
                        LoginDb.Updateable(newPswInfo).ExecuteCommand();
                        MessageBox.Show("密碼修改成功");
                        pop.IsPopupOpen = false;
                    }
                    else
                    {
                        MessageBox.Show("兩次密碼輸入不一致");
                    }
                }
                else
                {
                    MessageBox.Show("原密碼輸入錯誤");
                }
            }
            else
            {
                MessageBox.Show("未註冊的用戶，請聯繫管理員開通權限");
            }
        }



    }
}
