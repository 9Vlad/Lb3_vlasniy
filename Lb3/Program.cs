using System;
using System.Data.Common;
using System.Security.Cryptography;
using Lab3_Primer;
using MySql.Data.MySqlClient;

namespace Lb3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Отримую дані...");
            MySqlConnection conn = DBUtils.GetDBConnection();

            try
            {
                Console.WriteLine("Підключаюсь...");

                conn.Open();
                Console.WriteLine("Пiдключення успiшне!");

                while (true)
                {
                    Console.WriteLine("1.Переглянути хирургів");
                    Console.WriteLine("2.Додати хирурга");
                    Console.WriteLine("3.Видалити хирурга");
                    Console.WriteLine("4.Переглянути тарифи");
                    Console.WriteLine("5.Додати тарифи");
                    Console.WriteLine("6.Видалити тарифи");
                    Console.WriteLine("7.Переглянути паціентів");
                    Console.WriteLine("8.Додати паціентів");
                    Console.WriteLine("9.Видалити паціентів");
                    Console.WriteLine("10.Переглянути записи");
                    Console.WriteLine("11.Додати записи");
                    Console.WriteLine("12.Видалити записи");

                    Console.Write("Виберіть опцію: ");
                    string vib = Console.ReadLine();

                    switch (vib)
                    {
                        case "1":
                            ViewSurgeons(conn);
                            break;
                        case "2":
                            AddSurgeons(conn);
                            break;
                        case "3":
                            DeleteSurgeon(conn);
                            break;
                        case "4":
                            ViewRates(conn);
                            break;
                        case "5":
                            AddRates(conn);
                            break;
                        case "6":
                            DeleteRates(conn);
                            break;
                        case "7":
                            ViewPatients(conn);
                            break;
                        case "8":
                            AddPatient(conn);
                            break;
                        case "9":
                            DeletePatient(conn);
                            break;
                        case "10":
                            ViewRecords(conn);
                            break;
                        case "11":
                            AddRecord(conn);
                            break;
                        case "12":
                            DeleteRecord(conn);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Помилка: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            Console.Read();
        }
        private static void ViewSurgeons(MySqlConnection conn)
        {
            string surgeon_code, surgeon_surname, surgeon_name, surgeon_birtday, surgeon_category, surgeon_sex, surgeon_phone;
            string sql = "SELECT * FROM mydb.surgeons";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        surgeon_code = reader["surgeon_code"].ToString();
                        surgeon_surname = reader["surgeon_surname"].ToString();
                        surgeon_name = reader["surgeon_name"].ToString();
                        surgeon_birtday = reader["surgeon_birtday"].ToString();
                        surgeon_category = reader["surgeon_category"].ToString();
                        surgeon_sex = reader["surgeon_sex"].ToString();
                        surgeon_phone = reader["surgeon_phone"].ToString();
                        Console.WriteLine("---------------------------------------------------------");
                        Console.WriteLine("Код хірурга:" + surgeon_code + "   Прізвище хірурга:" + surgeon_surname + "   Ім'я хірурга" + surgeon_name + "   Дата народження хірурга:" + surgeon_birtday + "   Категорія хірурга:" + surgeon_category + "   Пол хірурга:" + surgeon_sex + "   Номер телефону хірурга:" + surgeon_phone);
                        Console.WriteLine("---------------------------------------------------------");
                    }
                }
            }
        }
        private static void AddSurgeons(MySqlConnection conn)
        {
            Console.Write("Введіть код хірурга: ");
            string surgeon_code = Console.ReadLine();
            Console.Write("Введіть прізвище хірурга: ");
            string surgeon_surname = Console.ReadLine();
            Console.Write("Введіть ім'я хірурга: ");
            string surgeon_name = Console.ReadLine();
            Console.Write("Введіть дату народження хірурга: ");
            string surgeon_birtday = Console.ReadLine();
            Console.Write("Введіть категорію хірурга: ");
            string surgeon_category = Console.ReadLine();
            Console.Write("Введіть стать хірурга: ");
            string surgeon_sex = Console.ReadLine();
            Console.Write("Введіть номер телефону хірурга: ");
            string surgeon_phone = Console.ReadLine();

            string sql = "INSERT INTO mydb.surgeons (surgeon_code, surgeon_surname, surgeon_name, surgeon_birtday, surgeon_category, surgeon_sex, surgeon_phone) " +
                         "VALUES (@surgeon_code, @surgeon_surname, @surgeon_name, @surgeon_birtday, @surgeon_category, @surgeon_sex, @surgeon_phone)";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@surgeon_code", surgeon_code);
            cmd.Parameters.AddWithValue("@surgeon_surname", surgeon_surname);
            cmd.Parameters.AddWithValue("@surgeon_name", surgeon_name);
            cmd.Parameters.AddWithValue("@surgeon_birtday", surgeon_birtday);
            cmd.Parameters.AddWithValue("@surgeon_category", surgeon_category);
            cmd.Parameters.AddWithValue("@surgeon_sex", surgeon_sex);
            cmd.Parameters.AddWithValue("@surgeon_phone", surgeon_phone);

            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Хірург успішно доданий.");
                }
                else
                {
                    Console.WriteLine("Не вдалося додати хірурга.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при додаванні хірурга: " + ex.Message);
            }
        }
        private static void DeleteSurgeon(MySqlConnection conn)
        {
            Console.Write("Введіть код хірурга для видалення: ");
            string surgeon_code = Console.ReadLine();

            string sql = "DELETE FROM surgeons WHERE surgeon_code = @surgeon_code";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@surgeon_code", surgeon_code);

            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Хірург успішно видалений.");
                }
                else
                {
                    Console.WriteLine("Не вдалося знайти хірурга з вказаним кодом.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при видаленні хірурга: " + ex.Message);
            }
        }
        private static void ViewRates(MySqlConnection conn)
        {
            string operation_category, rates_name, rates_price, rehabilitation_term, cost_day;
            string sql = "SELECT * FROM mydb.rates";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        operation_category = reader["operation_category"].ToString();
                        rates_name = reader["rates_name"].ToString();
                        rates_price = reader["rates_price"].ToString();
                        rehabilitation_term = reader["rehabilitation_term"].ToString();
                        cost_day = reader["cost_day"].ToString();
                        Console.WriteLine("---------------------------------------------------------");
                        Console.WriteLine("Категорія операції:" + operation_category + "   Назва операції:" + rates_name + "   Ціна операції:" + rates_price + "   Термін реабілітації:" + rehabilitation_term + "   Ціна реабілітації за день:" + cost_day);
                        Console.WriteLine("---------------------------------------------------------");
                    }
                }
            }
        }
        private static void AddRates(MySqlConnection conn)
        {
            Console.Write("Введіть категорію операції: ");
            string operation_category = Console.ReadLine();
            Console.Write("Введіть назву операції: ");
            string rates_name = Console.ReadLine();
            Console.Write("Введіть ціну операції: ");
            string rates_price = Console.ReadLine();
            Console.Write("Введіть термін реабілітації: ");
            string rehabilitation_term = Console.ReadLine();
            Console.Write("Введіть ціну реабілітації за день: ");
            string cost_day = Console.ReadLine();

            string sql = "INSERT INTO rates (operation_category, rates_name, rates_price, rehabilitation_term, cost_day) " +
                         "VALUES (@operation_category, @rates_name, @rates_price, @rehabilitation_term, @cost_day)";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@operation_category", operation_category);
            cmd.Parameters.AddWithValue("@rates_name", rates_name);
            cmd.Parameters.AddWithValue("@rates_price", rates_price);
            cmd.Parameters.AddWithValue("@rehabilitation_term", rehabilitation_term);
        }
        private static void DeleteRates(MySqlConnection conn)
        {
            Console.Write("Введіть категорію операції тарифу для видалення: ");
            string operation_category = Console.ReadLine();

            string sql = "DELETE FROM rates WHERE operation_category = @operation_category";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@operation_category", operation_category);

            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Тариф успішно видалений.");
                }
                else
                {
                    Console.WriteLine("Не вдалося знайти тариф з вказаною категорією операції.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при видаленні тарифу: " + ex.Message);
            }
        }
        private static void ViewPatients(MySqlConnection conn)
        {
            string patient_code, patient_name, patient_birtday, patient_sex, patient_category, patient_date, patient_rehabilitation;
            string sql = "SELECT * FROM mydb.patients";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        patient_code = reader["patient_code"].ToString();
                        patient_name = reader["patient_name"].ToString();
                        patient_birtday = reader["patient_birtday"].ToString();
                        patient_sex = reader["patient_sex"].ToString();
                        patient_category = reader["patient_category"].ToString();
                        patient_date = reader["patient_date"].ToString();
                        patient_rehabilitation = reader["patient_rehabilitation"].ToString();
                        Console.WriteLine("---------------------------------------------------------");
                        Console.WriteLine("Код пацієнта:" + patient_code + "   Ім'я пацієнта:" + patient_name + "   Дата народження пацієнта" + patient_birtday + "   Пол пацієнта:" + patient_sex + "   Категорія пацієнта:" + patient_category + "   Дата операції:" + patient_date + "   Час реабілітації:" + patient_rehabilitation);
                        Console.WriteLine("---------------------------------------------------------");
                    }
                }
            }
        }
        private static void AddPatient(MySqlConnection conn)
        {
            Console.Write("Введіть код пацієнта: ");
            string patient_code = Console.ReadLine();
            Console.Write("Введіть ім'я пацієнта: ");
            string patient_name = Console.ReadLine();
            Console.Write("Введіть дату народження пацієнта: ");
            string patient_birtday = Console.ReadLine();
            Console.Write("Введіть стать пацієнта: ");
            string patient_sex = Console.ReadLine();
            Console.Write("Введіть категорію пацієнта: ");
            string patient_category = Console.ReadLine();
            Console.Write("Введіть дату операції пацієнта: ");
            string patient_date = Console.ReadLine();
            Console.Write("Введіть час реабілітації пацієнта: ");
            string patient_rehabilitation = Console.ReadLine();

            string sql = "INSERT INTO patients (patient_code, patient_name, patient_birtday, patient_sex, patient_category, patient_date, patient_rehabilitation) " +
                         "VALUES (@patient_code, @patient_name, @patient_birtday, @patient_sex, @patient_category, @patient_date, @patient_rehabilitation)";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@patient_code", patient_code);
            cmd.Parameters.AddWithValue("@patient_name", patient_name);
            cmd.Parameters.AddWithValue("@patient_birtday", patient_birtday);
            cmd.Parameters.AddWithValue("@patient_sex", patient_sex);
            cmd.Parameters.AddWithValue("@patient_category", patient_category);
            cmd.Parameters.AddWithValue("@patient_date", patient_date);
            cmd.Parameters.AddWithValue("@patient_rehabilitation", patient_rehabilitation);

            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Пацієнт успішно доданий.");
                }
                else
                {
                    Console.WriteLine("Не вдалося додати пацієнта.");
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                Console.WriteLine("Помилка: Код пацієнта вже існує. Будь ласка, введіть інший код.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при додаванні пацієнта: " + ex.Message);
            }
        }
        private static void DeletePatient(MySqlConnection conn)
        {
            Console.Write("Введіть код пацієнта для видалення: ");
            string patient_code = Console.ReadLine();

            string sql = "DELETE FROM patients WHERE patient_code = @patient_code";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@patient_code", patient_code);

            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Пацієнт успішно видалений.");
                }
                else
                {
                    Console.WriteLine("Не вдалося знайти пацієнта з вказаним кодом.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при видаленні пацієнта: " + ex.Message);
            }
        }
        private static void ViewRecords(MySqlConnection conn)
        {
            string record_id, record_date, patients_patient_code, surgeons_surgeon_code, rates_operation_category;
            string sql = "SELECT * FROM mydb.records";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        record_id = reader["record_id"].ToString();
                        record_date = reader["record_date"].ToString();
                        patients_patient_code = reader["patients_patient_code"].ToString();
                        surgeons_surgeon_code = reader["surgeons_surgeon_code"].ToString();
                        rates_operation_category = reader["rates_operation_category"].ToString();
                        Console.WriteLine("---------------------------------------------------------");
                        Console.WriteLine("Код запису:" + record_id + "   Дата запису:" + record_date + "   Код паціента" + patients_patient_code + "   Код хірурга:" + surgeons_surgeon_code + "   Код операції:" + rates_operation_category);
                        Console.WriteLine("---------------------------------------------------------");
                    }
                }
            }
        }
        private static void AddRecord(MySqlConnection conn)
        {
            Console.Write("Введіть код запису: ");
            string record_id = Console.ReadLine();
            Console.Write("Введіть дату запису (yyyy-MM-dd): ");
            string record_date = Console.ReadLine();
            Console.Write("Введіть код пацієнта: ");
            string patients_patient_code = Console.ReadLine();
            Console.Write("Введіть код хірурга: ");
            string surgeons_surgeon_code = Console.ReadLine();
            Console.Write("Введіть код операції: ");
            string rates_operation_category = Console.ReadLine();

            string sql = "INSERT INTO records (record_id, record_date, patients_patient_code, surgeons_surgeon_code, rates_operation_category) " +
                         "VALUES (@record_id, @record_date, @patients_patient_code, @surgeons_surgeon_code, @rates_operation_category)";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@record_id", record_id);
            cmd.Parameters.AddWithValue("@record_date", record_date);
            cmd.Parameters.AddWithValue("@patients_patient_code", patients_patient_code);
            cmd.Parameters.AddWithValue("@surgeons_surgeon_code", surgeons_surgeon_code);
            cmd.Parameters.AddWithValue("@rates_operation_category", rates_operation_category);

            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Запис успішно додана.");
                }
                else
                {
                    Console.WriteLine("Не вдалося додати запис.");
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                Console.WriteLine("Помилка: Код запису вже існує. Будь ласка, введіть інший код.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при додаванні запису: " + ex.Message);
            }
        }
        private static void DeleteRecord(MySqlConnection conn)
        {
            Console.Write("Введіть код запису для видалення: ");
            string record_id = Console.ReadLine();

            string sql = "DELETE FROM records WHERE record_id = @record_id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@record_id", record_id);

            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Запис успішно видалена.");
                }
                else
                {
                    Console.WriteLine("Не вдалося знайти запис з вказаним кодом.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при видаленні запису: " + ex.Message);
            }
        }
    }

}
