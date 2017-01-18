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
//new libs
using MySql;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace SkyPark
{
    /// <summary>
    /// Логика взаимодействия для Manager_window.xaml
    /// </summary>
    public partial class Manager_window
    {
        public Manager_window()
        {
            InitializeComponent();
            Update_profit();
        }

        private void button_update_money_Click(object sender, RoutedEventArgs e)
        {
            Update_profit();
        }

        private string Update_profit()
        {
            //выполняется подключение
            MySqlConnection connStr = new MySqlConnection("Server=localhost;Database=SkyPark;User=root;Password=;");
            try
            {
                connStr.Open();

                //за все время
                MySqlCommand cmd = new MySqlCommand("SELECT SUM(`price`) FROM `logs`;", connStr);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                label1.Content = rdr[0] + " р.";
                rdr.Close();

                //за текущий месяц
                cmd = new MySqlCommand("SELECT SUM(`price`) FROM `logs` WHERE MONTH(`departure`) = MONTH(NOW()) AND YEAR(`departure`) = YEAR(NOW());", connStr);
                rdr = cmd.ExecuteReader();
                rdr.Read();
                label2.Content = rdr[0] + " р.";
                rdr.Close();

                //за предыдущий месяц
                cmd = new MySqlCommand("SELECT SUM(`price`) FROM `logs` WHERE MONTH(`departure`) = MONTH(DATE_ADD(NOW(), INTERVAL -1 MONTH)) AND YEAR(`departure`) = YEAR(NOW())", connStr);
                rdr = cmd.ExecuteReader();
                rdr.Read();
                label3.Content = rdr[0] + " р.";
                rdr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connStr.Close();
            }
            return "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //кнопка открывающая глобальный лог
            Process.Start("logs\\global_log.log");
        }

        private void button_delete_user_Click(object sender, RoutedEventArgs e)
        {
            //выполняется подключение
            MySqlConnection connStr = new MySqlConnection("Server=localhost;Database=SkyPark;User=root;Password=;");
            try
            {
                connStr.Open();

                //за все время
                MySqlCommand cmd = new MySqlCommand("DELETE FROM `accounts` WHERE `username` = '" + textBox2.Text + "';", connStr);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Запрос выполнен");
                connStr.Close();
            }
        }

        private void button_add_user_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex != -1 && textBox.Text != "" && textBox1.Text != "")
            {
                //выполняется подключение
                MySqlConnection connStr = new MySqlConnection("Server=localhost;Database=SkyPark;User=root;Password=;");
                try
                {
                    connStr.Open();

                    string type;
                    if (comboBox.SelectedIndex == 0) { type = "Оператор"; } else { type = "Менеджер"; }

                    //за все время
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `accounts` (`username`, `password`, `account type`) VALUES ('" + textBox.Text + "', '" + textBox1.Text + "', '" + type + "');", connStr);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Запрос выполнен");
                    connStr.Close();
                }
            }
            else
            {
                MessageBox.Show("Одно из полей не заполнено");
            }



        }

        private void button_report_Click(object sender, RoutedEventArgs e)
        {
            //формируем отчет
            //выполняется подключение
            MySqlConnection connStr = new MySqlConnection("Server=localhost;Database=SkyPark;User=root;Password=;");
            try
            {
                connStr.Open();

                //первый SELECT блок (информация о активных машинах на стоянке)
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `active`;", connStr);
                MySqlDataReader rdr = cmd.ExecuteReader();
                string report = default(string);
                report = "<html lang=\"en\"><head><meta charset=\"utf-8\" /><title>SkyPark™ Глобальный отчет</title><meta name=\"viewport\" content=\"initial-scale=1.0; maximum-scale=1.0; width=device-width;\"><style>@import url(http://fonts.googleapis.com/css?family=Roboto:400,500,700,300,100);body {background-color: #3e94ec;font-family: \"Roboto\", helvetica, arial, sans-serif;font-size: 16px;font-weight: 400;text-rendering: optimizeLegibility;}div.table-title {display: block;margin: auto;max-width: inherit;padding:5px;width: 100%;}.table-title h3 {color: #fafafa;font-size: 30px;font-weight: 400;font-style:normal;font-family: \"Roboto\", helvetica, arial, sans-serif;text-shadow: -1px -1px 1px rgba(0, 0, 0, 0.1);text-transform:uppercase;}/*** Table Styles **/.table-fill {background: white;border-radius:3px;border-collapse: collapse;height: 320px;margin: auto;max-width: inherit;padding:5px;width: 100%;box-shadow: 0 5px 10px rgba(0, 0, 0, 0.1);animation: float 5s infinite;}th {color:#D5DDE5;;background:#1b1e24;border-bottom:4px solid #9ea7af;border-right: 1px solid #343a45;font-size:23px;font-weight: 100;padding:24px;text-align:left;text-shadow: 0 1px 1px rgba(0, 0, 0, 0.1);vertical-align:middle;}th:first-child {border-top-left-radius:3px;}th:last-child {border-top-right-radius:3px;border-right:none;}tr {border-top: 1px solid #C1C3D1;border-bottom-: 1px solid #C1C3D1;color:#666B85;font-size:16px;font-weight:normal;text-shadow: 0 1px 1px rgba(256, 256, 256, 0.1);}tr:hover td {background:#4E5066;color:#FFFFFF;border-top: 1px solid #22262e;border-bottom: 1px solid #22262e;}tr:first-child {border-top:none;}tr:last-child {border-bottom:none;}tr:nth-child(odd) td {background:#EBEBEB;}tr:nth-child(odd):hover td {background:#4E5066;}tr:last-child td:first-child {border-bottom-left-radius:3px;}tr:last-child td:last-child {border-bottom-right-radius:3px;}td {background:#FFFFFF;padding:20px;text-align:left;vertical-align:middle;font-weight:300;font-size:18px;text-shadow: -1px -1px 1px rgba(0, 0, 0, 0.1);border-right: 1px solid #C1C3D1;}td:last-child {border-right: 0px;}th.text-left {text-align: left;}th.text-center {text-align: center;}th.text-right {text-align: right;}td.text-left {text-align: left;}td.text-center {text-align: center;}td.text-right {text-align: right;}</style></head><body>";
                report += "<div class=\"table-title\"><h3>В данный момент на стоянке</h3></div><table class=\"table-fill\"><thead><tr><th class=\"text-left\">Код</th><th class=\"text-left\">Тип</th><th class=\"text-left\">Гос. номер</th><th class=\"text-left\">Дата въезда</th></tr></thead><tbody class=\"table-hover\">";
                while (rdr.Read())
                {
                    report += "<tr>";
                    report += "<td class=\"text-left\">" + rdr[1] + "</td>" + "<td class=\"text-left\">" + rdr[2] + "</td>" + "<td class=\"text-left\">" + rdr[3] + "</td>" + "<td class=\"text-left\">" + rdr[4] + "</td>";
                    report += "</tr>";
                }
                report += "</tbody></table>";
                rdr.Close();

                //Второй SELECT блок (информация о машинах в архиве)
                cmd = new MySqlCommand("SELECT * FROM `logs`;", connStr);
                rdr = cmd.ExecuteReader();
                report += "<div class=\"table-title\"><h3>В архиве</h3></div><table class=\"table-fill\"><thead><tr><th class=\"text-left\">Код</th><th class=\"text-left\">Тип</th><th class=\"text-left\">Гос. номер</th><th class=\"text-left\">Дата въезда</th><th class=\"text-left\">Дата выезда</th><th class=\"text-left\">Оплачено</th></tr></thead><tbody class=\"table-hover\">";
                while (rdr.Read())
                {
                    report += "<tr>";
                    report += "<td class=\"text-left\">" + rdr[1] + "</td>" + "<td class=\"text-left\">" + rdr[2] + "</td>" + "<td class=\"text-left\">" + rdr[3] + "</td>" + "<td class=\"text-left\">" + rdr[4] + "</td>" + "<td class=\"text-left\">" + rdr[5] + "</td>" + "<td class=\"text-left\">" + rdr[6] + "</td>";
                    report += "</tr>";
                }
                report += "</tbody></table>";
                rdr.Close();

                //Третий SELECT блок (информация о учетках персонала)
                cmd = new MySqlCommand("SELECT * FROM `accounts`;", connStr);
                rdr = cmd.ExecuteReader();
                report += "<div class=\"table-title\"><h3>Учетные записи персонала</h3></div><table class=\"table-fill\"><thead><tr><th class=\"text-left\">Имя пользователя</th><th class=\"text-left\">Пароль</th><th class=\"text-left\">Оператор</th></tr></thead><tbody class=\"table-hover\">";
                while (rdr.Read())
                {
                    report += "<tr>";
                    report += "<td class=\"text-left\">" + rdr[1] + "</td>" + "<td class=\"text-left\">" + rdr[2] + "</td>" + "<td class=\"text-left\">" + rdr[3];
                    report += "</tr>";
                }
                report += "</tbody></table>";
                rdr.Close();


                //конец файла
                report += "</body>";

                StreamWriter sw = new StreamWriter(@"reports/global_report.html", false);
                sw.WriteLine(report);
                sw.Close();

                //открываем свежий отчет
                Process.Start(@"reports\\global_report.html");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Запрос выполнен");
                connStr.Close();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //открываем последний отчет
            Process.Start(@"reports\\global_report.html");
        }
    }
}
