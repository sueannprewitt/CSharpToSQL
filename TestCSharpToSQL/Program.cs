using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CSharpToSQL;

namespace TestCSharpToSQL {
	class Program {

		void Run() {

			string connStr = "Server=Student05;Database=DotNetDatabase;Trusted_Connection=yes";
			SqlConnection connection = new SqlConnection(connStr);
			connection.Open();
			if(connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("SQL Connection did not open");
				return; //this would get out of the run routine and shut the program down
			}
			Console.WriteLine("SQL connection opened successfully");
			//makes sure you have a good connection

			var sql = "select * from Student";
			SqlCommand cmd = new SqlCommand(sql, connection);
			SqlDataReader reader = cmd.ExecuteReader();
			while(reader.Read()) {
				var id = reader.GetInt32(0);
				var firstName = reader.GetString(1);
				var lastName = reader.GetString(2);
				var birthday = reader.GetDateTime(9);
				Console.WriteLine($"{id}, {firstName} {lastName}, born on {birthday}");
				}


			reader.Close(); //close reader before connection
			connection.Close(); //this is a method



		}
		static void Main(string[] args) {
			new Program().Run();
		}
	}
}
