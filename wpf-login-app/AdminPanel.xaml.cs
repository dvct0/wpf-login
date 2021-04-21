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
using System.Windows.Shapes;
using System.Timers;
using System.Data.Entity;

namespace wpf_login
{
    public partial class AdminPanel : Window
    {
        SessionTimer Timer = new SessionTimer();//включение таймера

        public AdminPanel()
        {
            InitializeComponent();
        }

        private void SaveLog(object sender, EventArgs e)//остановить таймер и записать в лог информацию о сессии. Срабатывание при закрытии окна
        {
            Timer.SaveLog(Login.Text);
        }

        private void Change(object sender, RoutedEventArgs e)//изменение данных выбранного пользователя
        {
            if (Authorization.Checklogin(sLogin.Text))
            {
                if (Authorization.ValidInput(sPass.Text))
                {
                    using (dbUsersEntities db = new dbUsersEntities())
                    {
                        var changedUser = db.users.Where(c => c.login == sLogin.Text).FirstOrDefault();
                        var changedPerson = db.person.Where(c => c.login_user == sLogin.Text).FirstOrDefault();
                        changedUser.pass = sPass.Text;
                        changedUser.role = Convert.ToInt32(sRole.Text);
                        changedPerson.name = sName.Text;
                        changedPerson.phone = sPhone.Text;
                        db.SaveChanges();
                    }

                    sStatus.Content = "Данные изменены";
                }
                else
                {
                    sStatus.Content = "Используйте латинские заглавные\nи прописные буквы и цифры\nдля пароля!";
                }
            }
            else
            {
                sStatus.Content = "Такого пользователя не существует";
            }
        }

        private void Edit (object sender, RoutedEventArgs e)//открыть форму редактирования данных
        {
            editForm.Visibility = Visibility.Visible;
            viewForm.Visibility = Visibility.Hidden;
            sLastSession.Visibility = Visibility.Hidden;
            sStatus.Content = "";
        }

        private void ToReturn (object sender, RoutedEventArgs e)//вернуться к форме просмотра данных
        {
            editForm.Visibility = Visibility.Hidden;
            viewForm.Visibility = Visibility.Visible;
            sLastSession.Visibility = Visibility.Visible;
            sStatus.Content = "";
        }

        private void ViewData (object sender, RoutedEventArgs e)//посмотреть данные
        {
            if (Authorization.Checklogin (sLoginView.Text))
            {
                using (dbUsersEntities db = new dbUsersEntities())
                {
                    int codePerson = db.person.Where(c => c.login_user == sLoginView.Text).Select(c => c.code_person).FirstOrDefault();
                    string name = db.person.Where(c => c.login_user == sLoginView.Text).Select(c => c.name).FirstOrDefault();
                    string phone = db.person.Where(c => c.login_user == sLoginView.Text).Select(c => c.phone).FirstOrDefault();
                    int? codeRole = db.users.Where(c => c.login == sLoginView.Text).Select(c => c.role).FirstOrDefault();
                    string role = db.role.Where(c => c.code_role == codeRole).Select(c => c.role1).FirstOrDefault();
                    sLastSession.Content = SessionTimer.GetLast(sLoginView.Text); //данные о последней сессии
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
    }
}
