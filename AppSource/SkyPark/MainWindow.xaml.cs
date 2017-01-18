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
//new libs
using MySql;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace SkyPark
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //проверка логов
            if (!Directory.Exists(@"logs")) { Directory.CreateDirectory(@"logs"); }
            if (!File.Exists(@"logs/global_log.log")) { FileStream fs = new FileStream(@"logs/global_log.log", FileMode.Create); fs.Close(); }

            MySqlConnection connStr = new MySqlConnection("Server=localhost;Database=SkyPark;User=root;Password=;");
            try
            {
                connStr.Open();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Возникла ошибка подключения, повторите попытку позже.", "Ошибка подключения");
            }
            finally
            {
                connStr.Close();
            }
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connStr = new MySqlConnection("Server=localhost;Database=SkyPark;User=root;Password=;");
            try
            {
                connStr.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(`account type`) FROM `Accounts` WHERE BINARY username='" + textBox.Text + "' AND BINARY password='" + textBox1.Password + "';", connStr);
                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();
                if (Convert.ToInt32(rdr[0]) == 1)
                {
                    //close old reader ex
                    rdr.Close();
                    //new logic
                    cmd = new MySqlCommand("SELECT `account type` FROM `Accounts` WHERE BINARY username='" + textBox.Text + "' AND BINARY password='" + textBox1.Password + "';", connStr);
                    rdr = cmd.ExecuteReader();
                    rdr.Read();

                    if (rdr[0].ToString() == "Оператор")
                    {
                        //пишем лог
                        DateTime log_date = DateTime.Now;
                        string current_date = log_date.ToString("yyyy-MM-dd H:mm:ss");
                        StreamWriter log = new StreamWriter(@"logs/global_log.log", true);
                        log.WriteLine(current_date + " Выполнен вход оператора " + textBox.Text );
                        log.Close();

                        rdr.Close();
                        new Operator_window().Show();
                        this.Close();
                    }
                    else if (rdr[0].ToString() == "Менеджер")
                    {
                        //пишем лог
                        DateTime log_date = DateTime.Now;
                        string current_date = log_date.ToString("yyyy-MM-dd H:mm:ss");
                        StreamWriter log = new StreamWriter(@"logs/global_log.log", true);
                        log.WriteLine(current_date + " Выполнен вход управляющего (Менеджер) " + textBox.Text);
                        log.Close();

                        rdr.Close();
                        new Manager_window().Show();
                        this.Close();
                    }

                }
                else
                {
                    //пишем лог
                    DateTime log_date = DateTime.Now;
                    string current_date = log_date.ToString("yyyy-MM-dd H:mm:ss");
                    StreamWriter log = new StreamWriter(@"logs/global_log.log", true);
                    log.WriteLine(current_date + " === Попытка авторизации под записью " + textBox.Text + " === (FAILED)");
                    log.Close();

                    rdr.Close();
                    MessageBox.Show("Не верный логин/пароль", "Ошибка");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Возникла ошибка", "Ошибка");
            }
            finally
            {
                connStr.Close();
            }
        }


    }
}
