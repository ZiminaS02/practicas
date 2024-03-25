using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {

            var login = loginTB.Text;
            var pass = passTB.Text;
            var context = new AppaDbContext();
            var user_exists = context.Users.FirstOrDefault(x => x.Login == login);
            if (user_exists != null)
            {
                MessageBox.Show("Такой пользователь уже в клубе крутышек");
                return;
            }
            var user = new User { Login = login, Password = pass };
            context.Users.Add(user);
            context.SaveChanges();
            MessageBox.Show("Welcome to the club, buddy");
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var login = loginTB.Text;
            var password = passTB.Text;

            var context = new AppaDbContext();

            var user = context.Users.SingleOrDefault(x => x.Login == login && x.Password == password);
            if (user != null)
            {
                MessageBox.Show("Неправильный логин или пароль");
                return;
            }
            MessageBox.Show("Вы успешно вошли в аккаунт");
        }

        private void loginTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var login = loginTB.Text;
            var password = passTB.Text;
            var email = mailTB;
            var context = new AppaDbContext();
            var user_exist = context.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            if ((!Regex.IsMatch(email, @"[a-zA-z0-9_.+-]+@(mail\.ru|gmail.com|yandex\.ru)$")))
            {
                MessageBox.Text = "указан неверный email!";
                error1.Visibility = Visibility.Visible;
            }
            else if (((!Regex.IsMatch(password, @"[!,&%+_"))))
            {
                error1.Visibility = Visibility.Collapsed;
                error2.Visibility = Visibility.Visible;
                MessageBox.Text = "";
                MessageBox.Text = "В пароле требуются спец. символы!";
            }
            else if (PasswordBox.Text.Length < 8) 
            {
                error2.Visibility = Visibility.Collapsed;

            }
            
        }
    }

}
