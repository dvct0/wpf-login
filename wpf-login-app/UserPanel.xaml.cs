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
using System.Data.Entity;
using System.Timers;

namespace wpf_login
{
    public partial class UserPanel : Window
    {
        bool IsActiveWindow = true;
        SessionTimer Timer = new SessionTimer();//включение таймера

        public UserPanel()
        {
            InitializeComponent();
            /* Task.Run(() => //обновление таймера
             {
                 while (IsActiveWindow == true)
                 {
                     Dispatcher.Invoke(new Action(() =>
                     {
                         Timer.t2 = DateTime.Now;
                         TimeSpan ts = Timer.t2 - Timer.t1;
                         sTimer.Content = ts.Hours.ToString() + ":" + ts.Minutes.ToString() + ":" + ts.Seconds.ToString();
                     }), null);
                 }
             });*/
        }

        private void SaveLog(object sender, EventArgs e)//остановить таймер и записать в лог информацию о сессии. Срабатывание при закрытии окна
        {
            Timer.SaveLog(sLogin.Text);
            IsActiveWindow = false;
        }

        private void Change(object sender, RoutedEventArgs e)//изменение своих данных
        {
                using (dbUsersEntities db = new dbUsersEntities())
                {
                    if (Authorization.ValidInput(sPass.Text))
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

        private void Edit(object sender, RoutedEventArgs e)//открыть форму редактирования данных
        {
            editForm.Visibility = Visibility.Visible;
            viewForm.Visibility = Visibility.Hidden;
            sStatus.Content = "";
        }

        private void ToReturn(object sender, RoutedEventArgs e)//вернуться к форме просмотра данных
        {
            editForm.Visibility = Visibility.Hidden;
            viewForm.Visibility = Visibility.Visible;
            sStatus.Content = "";
        }

        private void ViewData(object sender, RoutedEventArgs e)//посмотреть данные
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
    }
}
