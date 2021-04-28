using System;
using System.Linq;
using System.Windows;

namespace wpf_login
{
    public partial class UserPanel : Window
    {
        SessionTimer Timer = new SessionTimer();//включение таймера

        public UserPanel()
        {
            InitializeComponent();
        }

        private void SaveLog(object sender, EventArgs e)//ОСТАНОВИТЬ ТАЙМЕР И ЗАПИСАТЬ ИНФОРМАЦИЮ О СЕССИИ
        {
            try
            {
                Timer.SaveLog(sLogin.Text);
            }
            catch
            {
                sStatus.Content = "Ошибка! Возможно проблемы с сетью.";
            }
        }

        private void Change(object sender, RoutedEventArgs e)//ИЗМЕНИТЬ СВОИ ДАННЫЕ (ДАННЫЕ ТЕКУЩЕЙ УЧЕТНОЙ ЗАПИСИ)
        {
            try
            {
                using (dbUsersEntities db = new dbUsersEntities())
                {
                    if (Authorization.ValidInput(sPass.Text, "pass"))
                    {
                        var changedUser = db.users.Where(c => c.login == sLogin.Text).FirstOrDefault();
                        var changedPerson = db.person.Where(c => c.login_user == sLogin.Text).FirstOrDefault();
                        changedUser.pass = sPass.Text;
                        changedPerson.name = sName.Text;
                        changedPerson.phone = sPhone.Text;
                        db.SaveChanges();
                    }
                    else
                    {
                        sStatus.Content = "Используйте латинские заглавные\nи прописные буквы и цифры\nдля пароля!";
                    }
                }
                sStatus.Content = "Данные изменены";
            }
            catch
            {
                sStatus.Content = "Ошибка! Возможно проблемы с сетью.";
            }
        }

        private void Edit(object sender, RoutedEventArgs e)//ОТКРЫТЬ ФОРМУ РЕДАКТИРОВАНИЯ ДАННЫХ
        {
            editForm.Visibility = Visibility.Visible;
            viewForm.Visibility = Visibility.Hidden;
            sStatus.Content = "";
        }

        private void ToReturn(object sender, RoutedEventArgs e)//ВЕРНУТЬСЯ К ФОРМЕ ПРОСМОТРА ДАННЫХ
        {
            editForm.Visibility = Visibility.Hidden;
            viewForm.Visibility = Visibility.Visible;
            sStatus.Content = "";
        }

        private void ViewData(object sender, RoutedEventArgs e)//ПОСМОТРЕТЬ ДАННЫЕ
        {
            try
            {
                if (Authorization.Checklogin(sLogin.Text))
                {
                    using (dbUsersEntities db = new dbUsersEntities())
                    {
                        int codePerson = db.person.Where(c => c.login_user == sLogin.Text).Select(c => c.code_person).FirstOrDefault();
                        string name = db.person.Where(c => c.login_user == sLogin.Text).Select(c => c.name).FirstOrDefault();
                        string phone = db.person.Where(c => c.login_user == sLogin.Text).Select(c => c.phone).FirstOrDefault();
                        int? codeRole = db.users.Where(c => c.login == sLogin.Text).Select(c => c.role).FirstOrDefault();
                        string role = db.role.Where(c => c.code_role == codeRole).Select(c => c.role1).FirstOrDefault();
                        sNameView.Content = "Имя:  " + name;
                        sPhoneView.Content = "Телефон:  " + phone;
                        sRoleView.Content = "Роль:  " + role;
                        sStatus.Content = "Данные о пользователе получены";
                    }
                }
                else
                {
                    sNameView.Content = "";
                    sPhoneView.Content = "";
                    sRoleView.Content = "";
                    sStatus.Content = "Такого пользователя не существует!";
                }
            }
            catch
            {
                sStatus.Content = "Ошибка! Возможно проблемы с сетью.";
            }
        }
    }
}
