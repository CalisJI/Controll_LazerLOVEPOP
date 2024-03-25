using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlLazerApp
{
    public static class DBControl
    {
        static string pwd { get { return "12345678"; } }
        //public static string pwd = "12345678";
        
        //public static string Server = "127.0.0.1";
        static string Server { get { return "localhost"; } }
        static string UId { get { return "root"; } }
        static string Database { get { return "iotserver"; } }
        static string connection_string = "Server=" + Server + ";Database=" + Database + ";Uid=" + UId + ";Pwd=" + pwd + ";Pooling = false; Character Set=utf8; SslMode=none";

        public static void ApplyTable(DataTable dataTable) 
        {
            using(MySqlConnection connection = new MySqlConnection(connection_string)) 
            {
                connection.Open();
                CreateTableAndInsertData(connection, dataTable, DateTime.Now);
            }
        }

        static void CreateTableAndInsertData(MySqlConnection connection, DataTable dataTable, DateTime dateTime)
        {
            // Construct table name using DateTime (you can format it as needed)
            string tableName = "Plantbl_" + dateTime.ToString("yyyyMMdd"); // Example format: Table_20240108

            // Create SQL command to create table with a primary key column
            string createTableQuery = $"CREATE TABLE IF NOT EXISTS {tableName} (";

            // Add primary key column (change "ID" and "INT" to match your column name and data type)
            createTableQuery += "ID INT AUTO_INCREMENT PRIMARY KEY, ";

            // Add other columns based on the DataTable schema
            foreach (DataColumn column in dataTable.Columns)
            {
                createTableQuery += $"`{column.ColumnName}` VARCHAR(250), "; // Change the data type and size as needed
            }

            createTableQuery = createTableQuery.TrimEnd(',', ' ') + ")"; // Remove the last comma and space, complete the query

            // Execute the create table command
            MySqlCommand createTableCmd = new MySqlCommand(createTableQuery, connection);
            createTableCmd.ExecuteNonQuery();

            Console.WriteLine($"Table '{tableName}' created successfully.");

            // Insert data into the dynamically created table
            foreach (DataRow row in dataTable.Rows)
            {
                // Construct INSERT query without the primary key column (it's auto-incremented)
                string insertQuery = $"INSERT INTO {tableName} (";

                // Add columns to the insert query except the primary key column
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName != "ID") // Exclude the primary key column from insertion
                    {
                        insertQuery += $"`{column.ColumnName}`, ";
                    }
                }

                insertQuery = insertQuery.TrimEnd(',', ' ') + ") VALUES (";

                // Add parameter placeholders for the insert query
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName != "ID") // Exclude the primary key column
                    {
                        insertQuery += $"@{column.ColumnName.Replace(" ","")}, ";
                    }
                }

                insertQuery = insertQuery.TrimEnd(',', ' ') + ")";

                // Create MySqlCommand
                MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection);

                // Add parameters and their values except for the primary key column
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName != "ID") // Exclude the primary key column
                    {
                        insertCmd.Parameters.AddWithValue($"@{column.ColumnName.Replace(" ", "")}", row[column.ColumnName]);
                    }
                }

                // Execute the INSERT command
                insertCmd.ExecuteNonQuery();
            }

            Console.WriteLine($"Data inserted into table '{tableName}' successfully.");
        }
    }
}
