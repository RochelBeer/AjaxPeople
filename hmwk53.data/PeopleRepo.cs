using System.Data.SqlClient;

namespace hmwk53.data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
    public class PeopleRepo
    {
        private string _connectionString;

        public PeopleRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM PeopleTable";
            connection.Open();
            var list = new List<Person>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]

                });
            }

            return list;
        }

        public void Add(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO PeopleTable (FirstName, LastName, Age) " +
                "VALUES (@first, @last, @age)";
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public void DeletePerson(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM PeopleTable WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);         
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public void UpdatePerson(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE PeopleTable " +
                "SET FirstName = @first, " +
                "LastName = @last, " +
                "Age = @age " +
                "WHERE Id = @id";
                
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.Parameters.AddWithValue("@id", person.Id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public Person GetPerson(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 * FROM PeopleTable " +
                "WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if(!reader.Read())
            {
                return null;
            }
            Person person = new()
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Age = (int)reader["Age"]
            };
            return person;
            
        }

    }
}