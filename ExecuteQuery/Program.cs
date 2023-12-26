using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace ExecuteQuery
{
	internal class Program
	{
		static void Main(string[] args)
		{

			var anProgramInstance  = new Program();

			string queryString = ConfigurationManager.AppSettings["SP_INDEX"];
			string data_source = ConfigurationManager.AppSettings["DATA_SOURCE"];
			string catalog = ConfigurationManager.AppSettings["DATABASE"];
			string user = ConfigurationManager.AppSettings["USER"];
			string password = ConfigurationManager.AppSettings["PASSWORD"];

			try {

				string connectionString = "Data Source=" + data_source +
						";Initial Catalog=" + catalog +
						";User ID=" + user +
						";Password=" + password;
				SqlConnection connection = new SqlConnection(connectionString);
				

				SqlCommand command = new SqlCommand(queryString, connection);

				command.CommandType = CommandType.StoredProcedure;

				connection.Open();

				command.ExecuteNonQuery();

				connection.Close();

				anProgramInstance.GenerateLog();

			} catch(SqlException e) {

				Console.WriteLine(e);

			}

		}

		public void GenerateLog() {

			string path_log = ConfigurationManager.AppSettings["PATH_LOG"];
			DateTime thisDay = DateTime.Now;
			var text = "";

			if (path_log != null)
			{
				try 
				{

					if (File.Exists(path_log))
					{
						Console.WriteLine($"{path_log} already exists!");
					}
					text += "------------------------------\n";
					text += "Fecha: " + thisDay.ToString() + "\n";
					text += "------------------------------\n";
					File.AppendAllLines(path_log, new String[] { text });

				} catch(Exception e) 
				{
					Console.WriteLine(e); 
				}

			}
		}

	}
}
