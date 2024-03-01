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
using System.Text.RegularExpressions;

namespace atm.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageWithdraw.xaml
    /// 12.02.2024
    /// Андрей Изутов
    /// </summary>
    public partial class PageWithdraw : Page
    {
        private static readonly Regex _regex = new Regex("[^0-9]+");

        ///<summary>
        ///Проверка введенного значения
        ///</summary>
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private pin a;

        private users b;

        public PageWithdraw(pin userObj)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(txtBox.Text))
            {
                txtBox.Text = "Введите сумму";
                txtBox.Foreground = Brushes.Gray;
                txtBox.GotFocus += RemoveText;
                txtBox.LostFocus += AddText;
            }
            a = userObj;
            b = dbConnectHelper.entObj.users.FirstOrDefault(x => x.pincode_id == a.id);
            txbBalance.Text = $"Ваш баланс: {b.balance}";
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
            if (txtBox.Text == "Введите сумму")
                txtBox.Text = "";
        }

        /// <summary>
        /// Добавляет текст-подсказку при потере фокуса
        /// </summary>
        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBox.Text))
            {
                txtBox.Text = "Введите сумму";
                txtBox.Foreground = Brushes.Gray;
            }
        }

        private void txtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int wthdrw = Convert.ToInt32(txtBox.Text);
            if(b.balance < wthdrw)
            {
                tbWarning.Visibility = Visibility.Visible;
            }
            else
            {
                b.balance -= wthdrw;
                dbConnectHelper.entObj.SaveChanges();
                FrameApp.frmObj.Navigate(new PageWithdrawalComplete());
            }
        }
    }
}
