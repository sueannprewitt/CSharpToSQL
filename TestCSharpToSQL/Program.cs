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
			List<Student> students = new List<Student>();
			while(reader.Read()) {
				var id = reader.GetInt32(reader.GetOrdinal("Id"));
				var firstName = reader.GetString(reader.GetOrdinal("FirstName"));
				var lastName = reader.GetString(reader.GetOrdinal("LastName"));
				var birthday = reader.GetDateTime(reader.GetOrdinal("Birthday"));
				var address = reader.GetString(reader.GetOrdinal("Address"));
				var city = reader.GetString(reader.GetOrdinal("City"));
				var state = reader.GetString(reader.GetOrdinal("State"));
				var zipcode = reader.GetString(reader.GetOrdinal("Zipcode"));
				var phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
				var email = reader.GetString(reader.GetOrdinal("Email"));
				var sat = reader.GetInt32(reader.GetOrdinal("SAT"));
				var gpa = reader.GetDouble(reader.GetOrdinal("GPA"));
				//set major id to null value before reading the database value
				var majorID = 0;
				//check the value in the database for null
				//if it is NOT NULL
				if (!reader.GetValue(reader.GetOrdinal("MajorId")).Equals(DBNull.Value)) {
					//then do this
					majorID = reader.GetInt32(reader.GetOrdinal("MajorId"));
				}
				Console.WriteLine($"{id}, {firstName} {lastName}, born on {birthday}");
				Student student = new Student();
				student.Id = id;
				student.FirstName = firstName;
				student.LastName = lastName;
				student.Birthday = birthday;
				student.Address = address;
				student.City = city;
				student.State = state;
				student.Zipcode = zipcode;
				student.PhoneNumber = phoneNumber;
				student.Email = email;
				student.SAT = sat;
				student.GPA = gpa;
				student.MajorId = majorID;
				students.Add(student);

				}


			reader.Close(); //close reader before connection
			connection.Close(); //this is a method



		}
		static void Main(string[] args) {
			new Program().Run();
		}
	}
}
