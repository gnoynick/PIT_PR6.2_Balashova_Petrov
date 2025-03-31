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
        public int failedAttempts = 0;
        public string currentCaptcha = "";


        public AuthPage()
        {
            InitializeComponent();
            GenerateCaptcha();
            CaptchaPanel.Visibility = Visibility.Collapsed;
        }

        public void GenerateCaptcha()
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

        public void RefreshCaptcha_Click(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Auth(TextBoxLogin.Text, PasswordBox.Text, CaptchaInput.Text);
        }

        public bool Auth(string login, string password, string captchaInput = null)
        {
            if (CaptchaPanel.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(captchaInput) || captchaInput != currentCaptcha)
                {
                    MessageBox.Show("Неверно введена капча!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    GenerateCaptcha();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            using (var db = new UserAuthDBEntities1())
            {
                var userExists = db.User.Any(u => u.Logins == login);

                if (!userExists)
                {
                    MessageBox.Show("Такого пользователя не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    failedAttempts++;

                    if (failedAttempts >= 3)
                    {
                        CaptchaPanel.Visibility = Visibility.Visible;
                        GenerateCaptcha();
                    }
                    return false;
                }

                var user = db.User.FirstOrDefault(u => u.Logins == login && u.Passwords == password);

                if (user != null)
                {
                    failedAttempts = 0;
                    CaptchaPanel.Visibility = Visibility.Collapsed;
                    ClearInputFields();

                    MessageBox.Show($"Здравствуйте, {user.Roles} {user.FIO}!", "Успешная авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    failedAttempts++;

                    if (failedAttempts >= 3)
                    {
                        CaptchaPanel.Visibility = Visibility.Visible;
                        GenerateCaptcha();
                    }

                    MessageBox.Show("Неверный пароль! Осталось попыток: " + (3 - failedAttempts), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        private void ClearInputFields()
        {
            TextBoxLogin.Clear();
            PasswordBox.Clear();
            CaptchaInput.Clear();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}