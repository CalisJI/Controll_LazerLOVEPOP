using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System.Diagnostics;

namespace ControlLazerApp
{
    public static class DatabaseExcute_Main
    {
        #region Local Host
        //private static string Password = "12345678";
        //private static string Host = "localhost";
        //private static string User = "root";
        //private static string Database = "workstation";
        #endregion

        #region Server Host
        private static string Password = "Fwdvina@2024";
        private static string Host = "103.138.88.14";
        private static string User = "fwd63823_FWDdemo";
        private static string Database = "fwd63823_database";
        #endregion

        private static string acounts_tbl = "accounts_tbl";
        private static string lazer_tbl = "lazer_configuration";
        private static string total_plan = "total_plan";
        private static string Str_connection = "Server = " + Host + ";Database =" + Database + "; UId = " + User + "; Pwd = " + Password + "; Pooling = false; Character Set=utf8; SslMode=none";


        

        private static string Machine_rumtime_tbl = "machine_data_runtime";
        private static string Password_MCN = "12345678";
        private static string Host_MCN = "localhost";
        private static string User_MCN = "root";
        private static string Database_MCN = "workstation";
        private static string MCN_connection = "Server = " + Host_MCN + ";Database =" + Database_MCN + "; UId = " + User_MCN + "; Pwd = " + Password_MCN + "; Pooling = false; Character Set=utf8; SslMode=none";

        #region Technican Department Database action

        public static DataTable LoadConfig() 
        {
            var datatable = new DataTable();
            
            MySqlConnection connection = new MySqlConnection(Str_connection);
            try
            {
                if(connection.State == ConnectionState.Closed) 
                {
                    connection.Open();
                    string query = $"SELECT * FROM {lazer_tbl}";
                    using(MySqlCommand command = new MySqlCommand(query, connection)) 
                    {
                        using (MySqlDataReader reader = command.ExecuteReader()) 
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                datatable.Columns.Add(columnName);
                            }

                            while (reader.Read()) 
                            {
                                object[] values = new object[reader.FieldCount];
                                reader.GetValues(values);
                                datatable.Rows.Add(values);
                            }
                        }
                    }
                    return datatable;
                }
                else 
                {
                    return datatable = new DataTable()
                    {
                        Columns =
                        {
                            new DataColumn("id",typeof(int)),
                            new DataColumn("color",typeof(string)),
                            new DataColumn("tanso",typeof(double)),
                            new DataColumn("nangluong",typeof(double)),
                            new DataColumn("step_size",typeof(double)),
                            new DataColumn("dotre_trunggian",typeof(double)),
                            new DataColumn("dotre_tat",typeof(double)),
                            new DataColumn("delay",typeof(double)),
                            new DataColumn("ucche_nangluong",typeof(double)),
                            new DataColumn("ucche_soluong",typeof (double)),
                            new DataColumn("thoigian_tamdung",typeof(double)),
                            new DataColumn("solan_laplai",typeof(int))
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return datatable = new DataTable()
                {
                    Columns =
                    {
                        new DataColumn("id",typeof(int)),
                        new DataColumn("color",typeof(string)),
                        new DataColumn("tanso",typeof(double)),
                        new DataColumn("nangluong",typeof(double)),
                        new DataColumn("step_size",typeof(double)),
                        new DataColumn("dotre_trunggian",typeof(double)),
                        new DataColumn("dotre_tat",typeof(double)),
                        new DataColumn("delay",typeof(double)),
                        new DataColumn("ucche_nangluong",typeof(double)),
                        new DataColumn("ucche_soluong",typeof (double)),
                        new DataColumn("thoigian_tamdung",typeof(double)),
                        new DataColumn("solan_laplai",typeof(int))
                    }
                };
            }
            finally 
            {
                connection.Close();
            }
        }

        public static void UpdateLazerconfig(List<Rowobject> rowobjects) 
        {
            MySqlConnection connection = new MySqlConnection(Str_connection);
            try
            {
                if(connection.State == ConnectionState.Closed) 
                {
                    connection.Open();
                    foreach(Rowobject rowobject in rowobjects) 
                    {
                        string query = $"UPDATE {lazer_tbl} SET color = @Color , " +
                            $"tanso = @Tanso , " +
                            $"nangluong = @Nangluong , " +
                            $"step_size = @Step_size , " +
                            $"dotre_trunggian = @Dotre_trunggian , " +
                            $"dotre_tat = @Dotre_tat , " +
                            $"delay = @Delay , " +
                            $"ucche_nangluong = @Ucche_nangluong , " +
                            $"ucche_soluong = @Ucche_soluong , " +
                            $"thoigian_tamdung = @Thoigian_tamdung , " +
                            $"solan_laplai = @Solan_laplai " +
                            $"WHERE id = @Id";
                        using (MySqlCommand command = new MySqlCommand(query, connection)) 
                        {
                            command.Parameters.AddWithValue("@Color", rowobject.color);
                            command.Parameters.AddWithValue("@Tanso", rowobject.tanso);
                            command.Parameters.AddWithValue("@Nangluong", rowobject.nangluong);
                            command.Parameters.AddWithValue("@Step_size", rowobject.step_size);
                            command.Parameters.AddWithValue("@Dotre_trunggian", rowobject.dotre_trunggian);
                            command.Parameters.AddWithValue("@Dotre_tat", rowobject.dotre_tat);
                            command.Parameters.AddWithValue("@Delay", rowobject.delay);
                            command.Parameters.AddWithValue("@Ucche_nangluong", rowobject.ucche_nangluong);
                            command.Parameters.AddWithValue("@Ucche_soluong", rowobject.ucche_soluong);
                            command.Parameters.AddWithValue("@Thoigian_tamdung", rowobject.thoigian_tamdung);
                            command.Parameters.AddWithValue("@Solan_laplai", rowobject.solan_laplai);
                            command.Parameters.AddWithValue("@Id", rowobject.id);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally 
            {
                connection.Close();
            }
        }
        #endregion
    }
    public class Rowobject
    {
        public int id { get; set; }
        public string color { get; set; }
        public double tanso { get; set; }
        public double nangluong { get; set; }
        public double step_size { get; set; }
        public double dotre_trunggian { get; set; }
        public double dotre_tat { get; set; }
        public double delay { get; set; }
        public double ucche_nangluong { get; set; }
        public double ucche_soluong { get; set; }
        public double thoigian_tamdung { get; set; }
        public int solan_laplai { get; set; }
    }
}
