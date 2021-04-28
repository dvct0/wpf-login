using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;

namespace wpf_login
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin(object sender, RoutedEventArgs e) //АВТОРИЗАЦИЯ
        {
            using (dbUsersEntities db = new dbUsersEntities())
            {
                try
                {
                    if (Authorization.Checklogin(sLogin.Text)) //проверка на существование пользователя
                    {
                        if (Authorization.CheckPass(sPass.Password))//проверка на совпадение пароля
                        {
                            sStatus.Content = "Авторизация успешна";
                            List<int?> userRole = (from user in db.users where user.login == sLogin.Text select user.role).ToList();
                            if (userRole[0] == 2)//если аккаунт админа, то открытие панели администратора
                            {
                                AdminPanel adminPanel = new AdminPanel();
                                adminPanel.Owner = this;
                                adminPanel.Title += " - " + sLogin.Text;
                                adminPanel.Show();
                                adminPanel.Login.Text = sLogin.Text;
                            }
                            else if (userRole[0] == 1)//если аккаунт пользователя, то открытие панели пользователя
                            {
                                UserPanel userPanel = new UserPanel();
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
                catch
                {
                    sStatus.Content = "Ошибка! Возможно проблемы с сетью.";
                }
            }
        }

        private void BtnReg(object sender, RoutedEventArgs e)//РЕГИСТРАЦИЯ
        {
            try
            {
                using (dbUsersEntities db = new dbUsersEntities())
                {
                    if (Authorization.Checklogin(sLoginReg.Text)) //проверка на существование пользователя
                    {
                        sStatus.Content = "Такой пользователь уже существует!";
                    }
                    else if (Authorization.ValidInput(sLoginReg.Text, "login") && Authorization.ValidInput(sPassReg.Password, "sPass") && Authorization.ValidInput(sNameReg.Text, "name")) //проверка введенных данных, если все верно, то создается новый пользователь.
                    {
                        users user = new users { login = sLoginReg.Text, pass = sPassReg.Password, role = 1 }; //создаем объект user
                        person person1 = new person { login_user = sLoginReg.Text, name = sNameReg.Text, phone = sPhoneReg.Text };
                        db.users.Add(user); //добавление объекта в бд
                        db.person.Add(person1);
                        db.SaveChanges(); //сохранение данных
                        sStatus.Content = "Регистрация успешна.";
                        CancelReg();
                    }
                    else
                    {
                        sStatus.Content = "Поля заполнены неверно";
                        MessageBox.Show("Для логина и пароля используйте\nлатинские заглавные и прописные буквы и цифры,\nдля имени - только буквы");
                    }
                }
            }
            catch
            {
                sStatus.Content = "Ошибка! Возможно проблемы с сетью.";
            }
        }

        private void OpenRegForm(object sender, RoutedEventArgs e) //ОТКРЫТИЕ ФОРМЫ РЕГИСТРАЦИИ ПРИ НАЖАТИИ КНОПКИ
        {
            OpenRegForm();
        }

        private void OpenRegForm() //ОТКРЫТИЕ ФОРМЫ РЕГИСТРАЦИИ
        {
            bckgrnd1.Visibility = Visibility.Hidden;
            loginForm.Visibility = Visibility.Hidden;
            bckgrnd2.Visibility = Visibility.Visible;
            regForm.Visibility = Visibility.Visible;
            ClearRegData();
        }

        private void CancelReg(object sender, RoutedEventArgs e)//ОТМЕНА И ЗАКРЫТИЕ ФОРМЫ РЕГИСТРАЦИИ ПРИ НАЖАТИИ КНОПКИ
        {
            CancelReg();
        }

        private void CancelReg()//ОТМЕНА И ЗАКРЫТИЕ ФОРМЫ РЕГИСТРАЦИИ
        {
            bckgrnd1.Visibility = Visibility.Visible;
            loginForm.Visibility = Visibility.Visible;
            bckgrnd2.Visibility = Visibility.Hidden;
            regForm.Visibility = Visibility.Hidden;
            ClearRegData();
        }

        private void ClearRegData()//ОЧИСТКА ПОЛЕЙ В РЕГИСТРАЦИОННОЙ ФОРМЕ
        {
            sStatus.Content = "";
            sLoginReg.Text = "";
            sPassReg.Password = "";
            sNameReg.Text = "";
            sPhoneReg.Text = "";
        }
    }

    public class Authorization
    {
        MainWindow mainWindow = new MainWindow();

        public static bool ValidInput(string userData, string type) //ПРОВЕРИТЬ ВВОДИМЫЕ ДАННЫЕ
        {
            if (type == "login")
            {
                string upList = "ABCDEFGHIJKLMNOPRSTUVWXYZ-";
                string lowList = "abcdefghijklmnoprstuvwxyz_";
                string numericList = "1234567890";
                return (userData.IndexOfAny(upList.ToCharArray()) > -1)
                || (userData.IndexOfAny(lowList.ToCharArray()) > -1)
                || (userData.IndexOfAny(numericList.ToCharArray()) > -1);
            }
            if (type == "pass")
            {
                string upList = "ABCDEFGHIJKLMNOPRSTUVWXYZ-";
                string lowList = "abcdefghijklmnoprstuvwxyz_";
                string numericList = "1234567890";
                return (userData.IndexOfAny(upList.ToCharArray()) > -1)
                && (userData.IndexOfAny(lowList.ToCharArray()) > -1)
                && (userData.IndexOfAny(numericList.ToCharArray()) > -1);
            }
            if (type == "name")
            {
                string upList = "ABCDEFGHIJKLMNOPRSTUVWXYZАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
                string lowList = "abcdefghijklmnoprstuvwxyzабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
                return (userData.IndexOfAny(upList.ToCharArray()) > -1)
                && (userData.IndexOfAny(lowList.ToCharArray()) > -1);
            }
            return true;
        }

        public static bool Checklogin(string userData)  //ПРОВЕРИТЬ, СУЩЕСТВУЕТ ЛИ ПОЛЬЗОВАТЕЛЬ
        {
            using (dbUsersEntities db = new dbUsersEntities())
            {
                return db.users.Any(x => x.login == userData);
            }
        }

        public static bool CheckPass(string userData) //ПРОВЕРИТЬ, СОВПАДАЮТ ЛИ ПАРОЛИ
        {
            using (dbUsersEntities db = new dbUsersEntities())
            {
                return db.users.Any(x => x.pass == userData);
            }
        }
    }

    public class SessionTimer//ТАЙМЕР СЕССИИ
    {
        public DateTime t1, t2;
        Timer timer1 = new Timer();

        public SessionTimer() //включить таймер
        {
            timer1.Interval = 1000;
            timer1.Start();
            t1 = DateTime.Now;
        }

        public void SaveLog(string login)//ОСТАНОВИТЬ ТАЙМЕР И ЗАПИСАТЬ В ЛОГ ИНФОРМАЦИЮ О СЕССИИ
        {
            t2 = DateTime.Now;
            TimeSpan ts = t2 - t1;
            DateTime Date = DateTime.Now;
            using (dbUsersEntities db = new dbUsersEntities())
            {
                int codePerson = db.person.Where(c => c.login_user == login).Select(c => c.code_person).FirstOrDefault();
                logsData log = new logsData
                {
                    date_session = Date,
                    code_person = codePerson,
                    time = Convert.ToString(ts.Hours.ToString() + ":" + ts.Minutes.ToString() + ":" + ts.Seconds.ToString())
                };
                db.logsData.Add(log);
                db.SaveChanges();
            }
            timer1.Stop();
        }

        public static string GetLast(string login)//ПОЛУЧИТЬ ДАННЫЕ О ПОСЛЕДНЕЙ СЕССИИ
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
