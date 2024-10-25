using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using SabreTools.IO;

namespace Headerer
{
    internal static class Database
    {
        #region Constants

        /// <summary>
        /// Default location for the database
        /// </summary>
        private static readonly string DbFileName = Path.Combine(PathTool.GetRuntimeDirectory(), "Headerer.sqlite");

        /// <summary>
        /// Connection string for the database
        /// </summary>
        private static readonly string DbConnectionString = $"Data Source={DbFileName};";

        #endregion

        /// <summary>
        /// Add a header to the database
        /// </summary>
        /// <param name="header">String representing the header bytes</param>
        /// <param name="SHA1">SHA-1 of the deheadered file</param>
        /// <param name="type">Name of the source skipper file</param>
        /// <param name="debug">Enable additional log statements for debugging</param>
        public static void AddHeader(string header, string SHA1, string source, bool debug)
        {
            // Ensure the database exists
            EnsureDatabase();

            // Open the database connection
            SqliteConnection dbc = new(DbConnectionString);
            dbc.Open();

            string query = $"SELECT * FROM data WHERE sha1='{SHA1}' AND header='{header}'";
            var slc = new SqliteCommand(query, dbc);
            SqliteDataReader sldr = slc.ExecuteReader();
            bool exists = sldr.HasRows;

            if (!exists)
            {
                query = $"INSERT INTO data (sha1, header, type) VALUES ('{SHA1}', '{header}', '{source}')";
                slc = new SqliteCommand(query, dbc);
                if (debug) Console.WriteLine($"Result of inserting header: {slc.ExecuteNonQuery()}");
            }

            // Dispose of database objects
            slc.Dispose();
            sldr.Dispose();
            dbc.Dispose();
        }

        /// <summary>
        /// Retrieve headers from the database
        /// </summary>
        /// <param name="SHA1">SHA-1 of the deheadered file</param>
        /// <param name="debug">Enable additional log statements for debugging</param>
        /// <returns>List of strings representing the headers to add</returns>
        public static List<string> RetrieveHeaders(string SHA1, bool debug)
        {
            // Ensure the database exists
            EnsureDatabase();

            // Open the database connection
            var dbc = new SqliteConnection(DbConnectionString);
            dbc.Open();

            // Create the output list of headers
            List<string> headers = [];

            string query = $"SELECT header, type FROM data WHERE sha1='{SHA1}'";
            var slc = new SqliteCommand(query, dbc);
            SqliteDataReader sldr = slc.ExecuteReader();

            if (sldr.HasRows)
            {
                while (sldr.Read())
                {
                    if (debug) Console.WriteLine($"Found match with rom type '{sldr.GetString(1)}'");
                    headers.Add(sldr.GetString(0));
                }
            }
            else
            {
                Console.Error.WriteLine("No matching header could be found!");
            }

            // Dispose of database objects
            slc.Dispose();
            sldr.Dispose();
            dbc.Dispose();

            return headers;
        }

        /// <summary>
        /// Ensure that the database exists and has the proper schema
        /// </summary>
        private static void EnsureDatabase()
        {
            // Make sure the file exists
            if (!File.Exists(DbFileName))
                File.Create(DbFileName);

            // Open the database connection
            SqliteConnection dbc = new(DbConnectionString);
            dbc.Open();

            // Make sure the database has the correct schema
            string query = @"
CREATE TABLE IF NOT EXISTS data (
    'sha1'		TEXT		NOT NULL,
    'header'	TEXT		NOT NULL,
    'type'		TEXT		NOT NULL,
    PRIMARY KEY (sha1, header, type)
)";
            SqliteCommand slc = new(query, dbc);
            slc.ExecuteNonQuery();
            slc.Dispose();
            dbc.Dispose();
        }
    }
}