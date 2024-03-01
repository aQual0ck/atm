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
using System.Windows.Threading;

namespace atm.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageBlocked.xaml
    /// 12.02.2024
    /// Андрей Изутов
    /// </summary>
    public partial class PageBlocked : Page
    {
        public PageBlocked()
        {
            InitializeComponent();
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            timer.Start();
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                FrameApp.frmObj.Navigate(new PageCheck());
            };
        }
    }
}
