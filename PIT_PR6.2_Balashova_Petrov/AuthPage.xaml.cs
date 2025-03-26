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

namespace PIT_PR6._2_Balashova_Petrov
{
    public partial class AuthPage : Page
    {
        private int failedAttempts = 0;
        private string currentCaptcha = "";

        public AuthPage()
        {
            InitializeComponent();
            GenerateCaptcha();
        }

        private void GenerateCaptcha()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var random = new Random();

            currentCaptcha = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            CaptchaText.Text = currentCaptcha;
            CaptchaText.Foreground = new SolidColorBrush(Color.FromRgb(
                (byte)random.Next(50, 200),
                (byte)random.Next(50, 200),
                (byte)random.Next(50, 200)));

            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(new RotateTransform(random.Next(-10, 10)));
            transformGroup.Children.Add(new SkewTransform(random.Next(-5, 5), random.Next(-5, 5)));
            CaptchaText.RenderTransform = transformGroup;
        }

        private void RefreshCaptcha_Click(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaPanel.Visibility == Visibility.Visible)
            {
                if (CaptchaInput.Text != currentCaptcha)
                {
                    MessageBox.Show("Неверно введена капча!", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                    GenerateCaptcha();
                    return;
                }
            }

            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new UserAuthDBEntities())
            {
                var user = db.User.FirstOrDefault(u =>
                    u.Login == TextBoxLogin.Text &&
                    u.Password == PasswordBox.Password);

                if (user != null)
                {
                    failedAttempts = 0;
                    CaptchaPanel.Visibility = Visibility.Collapsed;

                    MessageBox.Show($"Здравствуйте, {user.Role} {user.FIO}!","Успешная авторизация",MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    failedAttempts++;

                    if (failedAttempts >= 3)
                    {
                        CaptchaPanel.Visibility = Visibility.Visible;
                        GenerateCaptcha();
                    }

                    MessageBox.Show("Неверный логин или пароль! Попыток: " + (3 - failedAttempts),"Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}
