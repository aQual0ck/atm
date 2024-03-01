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
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace atm.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageCheck.xaml
    /// 12.02.2024
    /// Андрей Изутов
    /// </summary>
    public partial class PageCheck : Page
    {
        private int i = 0;

        private static readonly Regex _regex = new Regex("[^0-9]+");

        ///<summary>
        ///Проверка введенного значения
        ///</summary>
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        public PageCheck()
        { 
            InitializeComponent();
            txtBox.MaxLength = 4;

            if (string.IsNullOrEmpty(txtBox.Text))
            {
                txtBox.Text = "Введите ПИН-код";
                txtBox.Foreground = Brushes.Gray;
                txtBox.GotFocus += RemoveText;
                txtBox.LostFocus += AddText;
            }
        }
        private void txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBox.Foreground = Brushes.Black;
        }
        /// <summary>
        /// Убирает текст-подсказку при фокусе
        /// </summary>
        public void RemoveText(object sender, EventArgs e)
        {
            if (txtBox.Text == "Введите ПИН-код")
                txtBox.Text = "";
        }
        /// <summary>
        /// Добавляет текст-подсказку при потере фокуса
        /// </summary>
        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBox.Text))
            {
                txtBox.Text = "Введите ПИН-код";
                txtBox.Foreground = Brushes.Gray;
            }
        }

        private void txtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtBox.Text != "" && txtBox.Text != "Введите ПИН-код")
                {
                    var userObj = dbstuff.dbConnectHelper.entObj.pin.FirstOrDefault(x => x.pincode == txtBox.Text);
                    if (userObj == null)
                    {
                        tbWarning.Visibility = Visibility.Visible;
                        i++;
                    }
                    else
                    {
                        FrameApp.frmObj.Navigate(new PageMain(userObj));
                    }
                    if (i > 3)
                    {
                        FrameApp.frmObj.Navigate(new PageBlocked());
                    }
                }
                else
                {
                    tbNoText.Visibility = Visibility.Visible;
                    var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
                    timer.Start();
                    timer.Tick += (sender1, args) =>
                    {
                        timer.Stop();
                        tbNoText.Visibility = Visibility.Hidden;
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message.ToString(), "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
