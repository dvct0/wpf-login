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
using System.Data.Entity;
using System.Timers;

namespace wpf_login
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin(object sender, RoutedEventArgs e) //авторизация
        {
            using (dbUsersEntities db = new dbUsersEntities())
            {
                if (Authorization.Checklogin(sLogin.Text)) //проверка на существование пользователя
                {
                    if (Authorization.CheckPass(sPass.Password))//проверка на совпадение пароля
                    {
                        sStatus.Content = "Авторизация успешна";
                        List<int?> userRole = (from user in db.users where user.login == sLogin.Text select user.role).ToList();
                        if (userRole[0] == 2)//если аккаунт админа, то открытие администраторской панели
                        {
                            AdminPanel adminPanel = new AdminPanel();
                            //this.Close(); //закрывает первое окно, только при независимых окнах
                            adminPanel.Owner = this;
                            adminPanel.Title += " - " + sLogin.Text;
                            adminPanel.Show();
                        }
                        else if (userRole[0] == 1)//если аккаунт пользователя, то открытие пользовательской панели
                        {
                            UserPanel userPanel = new UserPanel();
                            //this.Close(); //закрывает первое окно, только при независимых окнах
                            userPanel.Owner = this;
                            userPanel.Title += " - " + sLogin.Text;
                            userPanel.Show();
                            userPanel.sLogin.Text = sLogin.Text;
                        }
                    }
                    else
                    {
                        sStatus.Content = "Пароль неверный!";
                    }
                }
                else
                {
                    sStatus.Content = "Такого пользователя не существует!";
                }
            }
        }
        
        private void BtnReg(object sender, RoutedEventArgs e)//регистрация
        {
            using (dbUsersEntities db = new dbUsersEntities())
            {
                if (Authorization.Checklogin(sLoginReg.Text)) //проверка на существование пользователя
                {
                    sStatus.Content = "Такой пользователь уже существует!";
                }
                else if (Authorization.ValidInput(sLoginReg.Text) && Authorization.ValidInput(sPassReg.Password)) //проверка введенных данных, если все верно, то создается новый пользователь.
                {
                    users user = new users { login = sLoginReg.Text, pass = sPassReg.Password, role = 1 }; //создаем объект user
                    person person1 = new person { login_user = sLoginReg.Text, name = sNameReg.Text, phone = sPhoneReg.Text };
                    db.users.Add(user); //добавление объекта в бд
                    db.person.Add(person1);
                    db.SaveChanges(); //сохранение данных
                    sStatus.Content = "Регистрация успешна.";
                    bckgrnd1.Visibility = Visibility.Visible;
                    loginForm.Visibility = Visibility.Visible;
                    bckgrnd2.Visibility = Visibility.Hidden;
                    regForm.Visibility = Visibility.Hidden;
                }
                else
                {
                    sStatus.Content = "Используйте латинские заглавные\nи прописные буквы и цифры!";
                }
            }
        }

        private void OpenRegForm(object sender, RoutedEventArgs e) //открытие формы регистрации
        {
            bckgrnd1.Visibility = Visibility.Hidden;
            loginForm.Visibility = Visibility.Hidden;
            bckgrnd2.Visibility = Visibility.Visible;
            regForm.Visibility = Visibility.Visible;
            sStatus.Content = "";
        }

        private void CancelReg(object sender, RoutedEventArgs e)//отмена и закрытие формы регистрации
        {
            bckgrnd1.Visibility = Visibility.Visible;
            loginForm.Visibility = Visibility.Visible;
            bckgrnd2.Visibility = Visibility.Hidden;
            regForm.Visibility = Visibility.Hidden;
            sStatus.Content = "";
        }
    }

    public class Authorization
    {
        MainWindow mainWindow = new MainWindow();

        public static bool ValidInput(string userData) //Проверка вводимых данных на соотвествие требованиям: латинские заглавные и прописные буквы, цифры.
        {
            string upList = "ABCDEFGHIJKLMNOPRSTUVWXYZ";
            string lowList = "abcdefghijklmnoprstuvwxyz";
            string numericList = "1234567890";
            return (userData.IndexOfAny(upList.ToCharArray()) > -1)
            && (userData.IndexOfAny(lowList.ToCharArray()) > -1)
            && (userData.IndexOfAny(numericList.ToCharArray()) > -1);
        }

        public static bool Checklogin(string userData)  //проверка на существование пользователя
        {
            using (dbUsersEntities db = new dbUsersEntities())
            {
                return db.users.Any(x => x.login == userData);
            }
        }

        public static bool CheckPass(string  userData) //проверка на совпадение пароля
        {
            using (dbUsersEntities db = new dbUsersEntities())
            {
                return db.users.Any(x => x.pass == userData);
            }
        }
    }

    public class SessionTimer//Таймер сессии
    {
        public DateTime t1, t2;
        Timer timer1 = new Timer();
        public SessionTimer() //включить таймер
        {
            timer1.Interval = 1000;
            timer1.Start();
            t1 = DateTime.Now;
            Task.Delay(100);//обновление таймера каждую секунду
        }

        public void SaveLog(string login)//остановить таймер и записать в лог информацию о сессии.//ПЕРЕДЕЛАТЬ БД (таблицу logsData) и код!!!
        {
            t2 = DateTime.Now;
            TimeSpan ts = t2 - t1;

            using (dbUsersEntities db = new dbUsersEntities())
            {
                int codePerson = db.person.Where(c => c.login_user == login).Select(c => c.code_person).FirstOrDefault();
                logsData log = new logsData
                {
                    code_person = codePerson,
                    time = Convert.ToString(ts.Hours.ToString() + ":" + ts.Minutes.ToString() + ":" + ts.Seconds.ToString())
                };
                db.logsData.Add(log);
                db.SaveChanges();
            }   

            timer1.Stop();
        }

        public static string GetLast(string login)//получение данных о последней сессии
        {
            using (dbUsersEntities db = new dbUsersEntities())
            {
                if (Authorization.Checklogin(login))
                {
                    int codePerson = db.person.Where(c => c.login_user == login).Select(c => c.code_person).FirstOrDefault();
                    string time = db.logsData.Where(c => c.code_person == codePerson).OrderByDescending(c => c.id_session).Select(c => c.time).FirstOrDefault();
                    string date = Convert.ToString(db.logsData.Where(c => c.code_person == codePerson).Select(c => c.date_session).FirstOrDefault());
                    return login + "  --  Дата: " + date + "  Время сессии: " + time;
                }
                else
                {
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.sStatus.Content = "Такого пользователя не существует";
                    return "";
                }
            }
        }
    }
}
