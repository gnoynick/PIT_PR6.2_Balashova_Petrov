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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PIT_PR6._2_Balashova_Petrov
{
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void TextBoxPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            string text = textBox.Text;

            if (!Regex.IsMatch(e.Text, @"^[\d\(\)\+\-]$"))
            {
                e.Handled = true;
                return;
            }

            if (char.IsDigit(e.Text[0]))
            {
                if (text.Length == 0)
                    text = "+7 (";
                else if (text.Length == 7)
                    text += ") ";
                else if (text.Length == 12 || text.Length == 15)
                    text += "-";

                textBox.Text = text;
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxFIO.Clear();
            TextBoxLogin.Clear();
            PasswordBox.Clear();
            CmbGender.SelectedIndex = -1;
            CmbRole.SelectedIndex = -1;
            TextBoxPhone.Text = "+7 ("; ;
            TextBoxPhoto.Clear();

            NavigationService.Navigate(new AuthPage());
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxFIO.Text) ||
                string.IsNullOrEmpty(TextBoxLogin.Text) ||
                string.IsNullOrEmpty(PasswordBox.Text) ||
                CmbGender.SelectedItem == null ||
                CmbRole.SelectedItem == null ||
                string.IsNullOrEmpty(TextBoxPhone.Text))
            {
                MessageBox.Show("Заполните все поля для данных!", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Regex.IsMatch(TextBoxPhone.Text, @"^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$"))
            {
                MessageBox.Show("Введите телефон в формате: +7(XXX)XXX-XX-XX", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var db = new UserAuthDBEntities())
                {
                    if (db.User.Any(u => u.Login == TextBoxLogin.Text))
                    {
                        MessageBox.Show("Такой логин уже существует!", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var newUser = new User
                    {
                        FIO = TextBoxFIO.Text,
                        Login = TextBoxLogin.Text,
                        Password = PasswordBox.Text,
                        Role = (CmbRole.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Gender = (CmbGender.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Phone = TextBoxPhone.Text,
                        Photo = TextBoxPhoto.Text
                    };

                    db.User.Add(newUser);
                    db.SaveChanges();

                    MessageBox.Show("Пользователь успешно зарегистрирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    CancelButton_Click(sender, e);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
