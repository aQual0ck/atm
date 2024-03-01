using atm.dbstuff;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace atm
{
    /// <summary>
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        private pin a;

        public PageMain(pin userObj)
        {
            InitializeComponent();
            a = userObj;
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            
            FrameApp.frmObj.Navigate(new Pages.PageWithdraw(a));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new Pages.PageCheck());
        }

        private void btnReference_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new Pages.PageReference());
        }
    }
}
