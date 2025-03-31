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

        public bool Register(string fio, string login, string password, string gender, string role,
                            string phone, string photoUrl, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(fio) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(gender) ||
                string.IsNullOrWhiteSpace(role) ||
                string.IsNullOrWhiteSpace(phone))
            {
                errorMessage = "Заполните все обязательные поля!";
                return false;
            }

            if (fio.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 3)
            {
                errorMessage = "Введите полное ФИО (Фамилия Имя Отчество)!";
                return false;
            }

            if (login.Length < 4 || login.Length > 20)
            {
                errorMessage = "Логин должен содержать от 4 до 20 символов!";
                return false;
            }

            if (!Regex.IsMatch(login, @"^[a-zA-Z0-9]+$"))
            {
                errorMessage = "Логин может содержать только буквы латинского алфавита и цифры!";
                return false;
            }

            if (password.Length < 6)
            {
                errorMessage = "Пароль должен содержать минимум 6 символов!";
                return false;
            }

            if (!Regex.IsMatch(password, @"[0-9]") || !Regex.IsMatch(password, @"[a-zA-Z]"))
            {
                errorMessage = "Пароль должен содержать хотя бы одну букву и одну цифру!";
                return false;
            }

            if (!Regex.IsMatch(phone, @"^\+7\s?\(\d{3}\)\s?\d{3}-\d{2}-\d{2}$")) 
            {
                errorMessage = "Введите телефон в формате: +7(XXX) XXX-XX-XX или +7 (XXX) XXX-XX-XX";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(photoUrl) &&
                !Regex.IsMatch(photoUrl, @"^(http|https):\/\/[^\s/$.?#].[^\s]*$", RegexOptions.IgnoreCase))
            {
                errorMessage = "Введите корректный URL для фотографии (начинается с http:// или https://)!";
                return false;
            }

            try
            {
                using (var db = new UserAuthDBEntities1())
                {
                    if (db.User.Any(u => u.Logins == login))
                    {
                        errorMessage = "Такой логин уже существует! Пожалуйста, выберите другой.";
                        return false;
                    }

                    var newUser = new User
                    {
                        FIO = fio.Trim(),
                        Logins = login.Trim(),
                        Passwords = password,
                        Roles = role,
                        Gender = gender,
                        Phone = phone,
                        Photo = string.IsNullOrWhiteSpace(photoUrl) ? null : photoUrl.Trim()
                    };

                    db.User.Add(newUser);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Ошибка при регистрации: {ex.Message}";
                return false;
            }
        }

        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage;
            bool success = Register(
                TextBoxFIO.Text,
                TextBoxLogin.Text,
                PasswordBox.Text,
                (CmbGender.SelectedItem as ComboBoxItem)?.Content.ToString(),
                (CmbRole.SelectedItem as ComboBoxItem)?.Content.ToString(),
                TextBoxPhone.Text,
                TextBoxPhoto.Text,
                out errorMessage);

            if (success)
            {
                MessageBox.Show("Пользователь успешно зарегистрирован!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                CancelButton_Click(sender, e);
            }
            else
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void TextBoxPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            string text = textBox.Text;
            int caretPos = textBox.CaretIndex;

            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
                return;
            }

            if (text.Length == 4)
            {
                textBox.Text = "+7 (" + e.Text;
                textBox.CaretIndex = textBox.Text.Length;
                e.Handled = true;
            }
            else if (text.Length == 8)
            {
                textBox.Text += ") " + e.Text;
                textBox.CaretIndex = textBox.Text.Length;
                e.Handled = true;
            }
            else if (text.Length == 13 || text.Length == 16)
            {
                textBox.Text += "-" + e.Text;
                textBox.CaretIndex = textBox.Text.Length;
                e.Handled = true;
            }

            if (text.Length >= 18)
            {
                e.Handled = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxFIO.Clear();
            TextBoxLogin.Clear();
            PasswordBox.Clear();
            CmbGender.SelectedIndex = -1;
            CmbRole.SelectedIndex = -1;
            TextBoxPhone.Text = "+7 (";
            TextBoxPhoto.Clear();
            NavigationService.Navigate(new AuthPage());
        }
    }
}
