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
    /// Логика взаимодействия для Operator_window.xaml
    /// </summary>
    public partial class Operator_window
    {
        public Operator_window()
        {
            InitializeComponent();
        }

        private void reload_code_button_Click(object sender, RoutedEventArgs e)
        {
            code_box.Text = CodeGen();
        }

        private string CodeGen()
        {
            string code = "";
            Random ran = new Random();
            int dice = 0; //результат броска кости

            //генерация кода из 16 символов

            //первая линейка (линейный алгоритм)
            code += (char)ran.Next('A', 'Z' + 1);
            code += ran.Next(0, 9);
            code += (char)ran.Next('A', 'Z' + 1);
            code += ran.Next(0, 9);
            //разделитель
            code += "-";

            //вторая линейка (алгоритм броска)
            dice = ran.Next(1, 2); //грани кости
            if (dice == 1) { code += (char)ran.Next('A', 'Z' + 1); } else { code += ran.Next(0, 9); }
            dice = ran.Next(1, 4);
            if (dice <= 2) { code += ran.Next(0, 9); } else { code += (char)ran.Next('A', 'Z' + 1); }
            dice = ran.Next(1, 6);
            if (dice <= 3) { code += (char)ran.Next('A', 'Z' + 1); } else { code += ran.Next(0, 9); }
            dice = ran.Next(1, 16);
            if (dice <= 8) { code += ran.Next(0, 9); } else { code += (char)ran.Next('A', 'Z' + 1); }
            //разделитель
            code += "-";

            //третья линейка (результат мат.вычислений)
            if (ran.Next(0, 10) - ran.Next(0, 10) < 0) { code += ran.Next(0, 9); } else { code += (char)ran.Next('A', 'Z' + 1); }
            if (ran.Next(-25, 35) - ran.Next(-25, 35) < 0) { code += ran.Next(0, 9); } else { code += (char)ran.Next('A', 'Z' + 1); }
            if (ran.Next(0, 100) - ran.Next(0, 100) < 0) { code += (char)ran.Next('A', 'Z' + 1); } else { code += ran.Next(0, 9); }
            if (ran.Next(-100, 100) - ran.Next(-100, 100) < 0) { code += (char)ran.Next('A', 'Z' + 1); } else { code += ran.Next(0, 9); }
            //разделитель
            code += "-";

            //четвертая линейка (дифференция)
            if (char.IsNumber(code[10])) { code += (char)ran.Next('A', 'Z' + 1); } else { code += ran.Next(0, 9); }
            code += (char)ran.Next('A', 'Z' + 1);
            if (char.IsNumber(code[12])) { code += (char)ran.Next('A', 'Z' + 1); } else { code += ran.Next(0, 9); }
            dice = ran.Next(1, 16);
            if (dice <= 8) { code += ran.Next(0, 9); } else { code += (char)ran.Next('A', 'Z' + 1); }

            return code;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            code_box.Text = CodeGen();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex != -1 && textBox.Text != "")
            {
                //выполняется подключение
                MySqlConnection connStr = new MySqlConnection("Server=localhost;Database=SkyPark;User=root;Password=;");
                try
                {
                    connStr.Open();

                    //переменные для записи
                    string type_clear = comboBox.SelectedValue.ToString();
                    type_clear = type_clear.Remove(0, 38);
                    string sql_date = "";
                    DateTime date = DateTime.Now;
                    sql_date = date.ToString("yyyy-MM-dd H:mm:ss");

                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `active` (`code`, `type`, `gosnumber_auto`, `entry`) VALUES ('" + code_box.Text + "', '" + type_clear + "', '" + textBox.Text + "', '" + sql_date + "');", connStr);
                    cmd.ExecuteNonQuery();

                    //вызываем метод генерации чека
                    generate_cheque(code_box.Text, type_clear, sql_date);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //MessageBox.Show("Возникла ошибка", "Ошибка");
                }
                finally
                {
                    connStr.Close();

                    comboBox.SelectedIndex = -1;
                    textBox.Text = "";
                    code_box.Text = CodeGen();
                    //if ex.message == null { code }
                    MessageBox.Show("Запрос выполнен");
                }
            }
            else
            {
                MessageBox.Show("Одно из полей не заполнено");
            }
        }

        //тут типа метод HTML-генерации чека
        private void generate_cheque(string code, string type, string date)
        {
            string cheque = default(string);

            //строка 1
            cheque = "<!----><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title> Order confirmation </title><meta name=\"robots\" content=\"noindex,nofollow\" /><meta name=\"viewport\" content=\"width=device-width; initial-scale=1.0;\" /><style type=\"text/css\">@import url(https://fonts.googleapis.com/css?family=Open+Sans:400,700);body { margin: 0; padding: 0; background: #e1e1e1; }div, p, a, li, td { -webkit-text-size-adjust: none; }.ReadMsgBody { width: 100%; background-color: #ffffff; }.ExternalClass { width: 100%; background-color: #ffffff; }body { width: 100%; height: 100%; background-color: #e1e1e1; margin: 0; padding: 0; -webkit-font-smoothing: antialiased; }html { width: 100%; }p { padding: 0 !important; margin-top: 0 !important; margin-right: 0 !important; margin-bottom: 0 !important; margin-left: 0 !important; }.visibleMobile { display: none; }.hiddenMobile { display: block; }@media only screen and (max-width: 600px) {body { width: auto !important; }table[class=fullTable] { width: 96% !important; clear: both; }table[class=fullPadding] { width: 85% !important; clear: both; }table[class=col] { width: 45% !important; }.erase { display: none; }}@media only screen and (max-width: 420px) {table[class=fullTable] { width: 100% !important; clear: both; }table[class=fullPadding] { width: 85% !important; clear: both; }table[class=col] { width: 100% !important; clear: both; }table[class=col] td { text-align: left !important; }.erase { display: none; font-size: 0; max-height: 0; line-height: 0; padding: 0; }.visibleMobile { display: block !important; }.hiddenMobile { display: none !important; }}</style><!-- Header --><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"fullTable\" bgcolor=\"#e1e1e1\"><tr><td height=\"20\"></td></tr><tr><td><table width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"fullTable\" bgcolor=\"#ffffff\" style=\"border-radius: 10px 10px 0 0;\"><tr class=\"hiddenMobile\"><td height=\"40\"></td></tr><tr class=\"visibleMobile\"><td height=\"30\"></td></tr><tr><td><table width=\"480\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"fullPadding\"><tbody><tr><td><table width=\"220\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"left\" class=\"col\"><tbody><tr><td align=\"left\"> <img src=\"sky_logo.jpg\" width=\"82\" height=\"52\" alt=\"logo\" border=\"0\" /></td></tr><tr class=\"hiddenMobile\"><td height=\"40\"></td></tr><tr class=\"visibleMobile\"><td height=\"20\"></td></tr><tr><td style=\"font-size: 12px; color: #5b5b5b; font-family: 'Open Sans', sans-serif; line-height: 18px; vertical-align: top; text-align: left;\">SkyPark order<br> skyboxpark@skybox.com</td></tr></tbody></table><table width=\"220\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"right\" class=\"col\"><tbody><tr class=\"visibleMobile\"><td height=\"20\"></td></tr><tr><td height=\"5\"></td></tr><tr><td style=\"font-size: 21px; color: #efc729; letter-spacing: -1px; font-family: 'Open Sans', sans-serif; line-height: 1; vertical-align: top; text-align: right;\">Invoice</td></tr><tr><tr class=\"hiddenMobile\"><td height=\"50\"></td></tr><tr class=\"visibleMobile\"><td height=\"20\"></td></tr><tr><td style=\"font-size: 12px; color: #5b5b5b; font-family: 'Open Sans', sans-serif; line-height: 18px; vertical-align: top; text-align: right;\">";
            //строка 2
            cheque += "<small> LACK ORDER </small> DV1 <br/>";
            //строка 3
            cheque += "<small>" + DateTime.Now + "</small>";
            //строка 4
            cheque += "</td></tr></tbody></table></td></tr></tbody></table></td></tr></table></td></tr></table><!-- /Header --><!-- Order Details --><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"fullTable\" bgcolor=\"#e1e1e1\"><tbody><tr><td><table width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"fullTable\" bgcolor=\"#ffffff\"><tbody><tr><tr class=\"hiddenMobile\"><td height=\"60\"></td></tr><tr class=\"visibleMobile\"><td height=\"40\"></td></tr><tr><td><table width=\"480\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"fullPadding\"><tbody><tr><th style=\"font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 10px 7px 0;\" width=\"52%\" align=\"left\">Код</th><th style=\"font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;\" align=\"left\"><small>Тип</small></th><th style=\"font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;\" align=\"center\">Дата въезда</th></tr><tr><td height=\"1\" style=\"background: #bebebe;\" colspan=\"4\"></td></tr><tr><td height=\"10\" colspan=\"4\"></td></tr><tr><td style=\"font-size: 12px; font-family: 'Open Sans', sans-serif; color: #2971ef;  line-height: 18px;  vertical-align: top; padding:10px 0;\" class=\"article\">";
            //строка 5
            cheque += code;
            //строка 6
            cheque += "</td><td style=\"font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e;  line-height: 18px;  vertical-align: top; padding:10px 0;\"><small>";
            //строка 7
            cheque += type;
            //строка 8
            cheque += "</small></td><td style=\"font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e;  line-height: 18px;  vertical-align: top; padding:10px 0;\" align=\"center\">";
            //строка 9
            cheque += date;
            //строка 10
            cheque += "</td></tr><tr><td height=\"1\" colspan=\"4\" style=\"border-bottom:1px solid #e4e4e4\"></td></tr><tr><td height=\"1\" colspan=\"4\" style=\"border-bottom:1px solid #e4e4e4\"></td></tr></tbody></table></td></tr><tr><td height=\"20\"></td></tr></tbody></table></td></tr></tbody></table><!-- /Order Details --><!-- Total --><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"fullTable\" bgcolor=\"#e1e1e1\"><tbody><tr><td><table width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"fullTable\" bgcolor=\"#ffffff\"><tbody><tr><td><!-- Table Total --><!-- /Table Total --></td></tr></tbody></table></td></tr></tbody></table><!-- /Total --><!-- Information --></td></tr><tr class=\"hiddenMobile\"><td height=\"60\"></td></tr><tr class=\"visibleMobile\"><td height=\"30\"></td></tr></tbody></table></td></tr></tbody></table><!-- /Information --><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"fullTable\" bgcolor=\"#e1e1e1\"><tr><td><table width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"fullTable\" bgcolor=\"#ffffff\" style=\"border-radius: 0 0 10px 10px;\"><tr><td><table width=\"480\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"fullPadding\"><tbody><tr><td style=\"font-size: 12px; color: #5b5b5b; font-family: 'Open Sans', sans-serif; line-height: 18px; vertical-align: top; text-align: left;\">Удачного дня \\ Have a nice day.</td></tr></tbody></table></td></tr><tr class=\"spacer\"><td height=\"50\"></td></tr></table></td></tr><tr><td height=\"20\"></td></tr></table>";

            StreamWriter sw = new StreamWriter(@"cheque/cheque.html", false);
            sw.WriteLine(cheque);
            sw.Close();
            Process.Start(@"cheque\\cheque.html");
        }

        private void check_button_Click(object sender, RoutedEventArgs e)
        {
            if (check_box.Text.Length == 19)
            {
                bool IfNotExeption = false;
                //выполняется подключение
                MySqlConnection connStr = new MySqlConnection("Server=localhost;Database=SkyPark;User=root;Password=;");
                
                try
                {
                    
                    connStr.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT `IsPaid` FROM `active` WHERE `code`= '" + check_box.Text + "';", connStr);

                    MySqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();

                    if (Convert.ToString(rdr[0]) != "")
                    {
                        rdr.Close();

                        string code = check_box.Text;
                        string type = "";
                        string gosnumber = "";
                        string entry = "";
                        string ispaid = "";
                        string departure = "";

                        cmd = new MySqlCommand("SELECT `type` FROM `active` WHERE `code`= '" + check_box.Text + "';", connStr);
                        rdr = cmd.ExecuteReader();
                        rdr.Read();
                        type = rdr[0].ToString();
                        rdr.Close();

                        cmd = new MySqlCommand("SELECT `gosnumber_auto` FROM `active` WHERE `code`= '" + check_box.Text + "';", connStr);
                        rdr = cmd.ExecuteReader();
                        rdr.Read();
                        gosnumber = rdr[0].ToString();
                        rdr.Close();

                        //с датой немного заморочек (sql select возвращает ее в обратном порядке) fixed
                        cmd = new MySqlCommand("SELECT `entry` FROM `active` WHERE `code`= '" + check_box.Text + "';", connStr);
                        rdr = cmd.ExecuteReader();
                        rdr.Read();
                        entry = rdr[0].ToString();
                        string[] substring_sqldate = entry.Split(' ');
                        //минутка бредовщины: делим substring на substring'и
                        string[] substring_mini = substring_sqldate[0].Split('.');
                        entry = substring_mini[2] + "-" + substring_mini[1] + "-" + substring_mini[0] + " " + substring_sqldate[1];
                        rdr.Close();

                        cmd = new MySqlCommand("SELECT `IsPaid` FROM `active` WHERE `code`= '" + check_box.Text + "';", connStr);
                        rdr = cmd.ExecuteReader();
                        rdr.Read();
                        ispaid = rdr[0].ToString();
                        rdr.Close();

                        DateTime date = DateTime.Now;
                        departure = date.ToString("yyyy-MM-dd H:mm:ss");
                        cmd = new MySqlCommand("INSERT INTO `logs` (`code`, `type`, `gosnumber_auto`, `entry`, `departure`, `price`) VALUES ('" + code + "', '" + type + "', '" + gosnumber + "', '" + entry + "', '" + departure + "', '" + ispaid + "' );", connStr);
                        cmd.ExecuteNonQuery();
                        //выполнена запись в logs

                        //удаление из active
                        cmd = new MySqlCommand("DELETE FROM `active` WHERE `code` = '" + check_box.Text + "';", connStr);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        IfNotExeption = true;
                        MessageBox.Show("Данный заказ не оплачен!");
                    }

                    rdr.Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    IfNotExeption = true;
                    //MessageBox.Show("Возникла ошибка", "Ошибка");
                   
                }
                finally
                {
                    connStr.Close();
                    if (IfNotExeption != true) { MessageBox.Show("Запрос выполнен"); }  
                }
                
            }
            else
            {
                MessageBox.Show("Введенный код не соответствует правилам");
            }
        }


        static string ReverseStringLinq(string originalString)
        {
            return new string(originalString.Reverse().ToArray());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //кнопка открывающая глобальный лог
            Process.Start("logs\\global_log.log");
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //открываем последний чек
            Process.Start(@"cheque\\cheque.html");
        }


        //полностью рабочий композит даты!!!!!!!!
        //DateTime date = DateTime.Now;
        //Console.WriteLine(date.ToLongDateString());
        //Console.ReadKey(true);
        //textBox1.Text = ("Today is " + date.ToString("yyyy-MM-dd H:mm:ss") + ".");

    }
}
