using fgeek.Entities.Interfaces;
using SQLite;
using System.Text;

namespace fgeek.Entities
{
    [Table("accounts")]
    public class User : IEntity
    {
        [PrimaryKey]
        [Column("id")]
        public string Id       { get; set; }
        [Column("email")]
        public string Email    { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("password")]
        public string Password { get; set; }
        public static Func<User, bool> CompareId(string id)
        {
            return item => { return item.Id == id; };
        }
        public static Func<User, bool> CompareEmail(string email)
        {
            return item => { return item.Email == email; };
        }
        public static Func<User, bool> CompareUsername(string username)
        {
            return item => { return item.Username == username; };
        }
        public static Func<User, bool> ComparePassword(string password)
        {
            return item => { return item.Password == password; };
        }

        public User()
        {
            Id = Email = Username = Password = "Undefiened";
        }
        public User(string id, string email, string username, string password)
        {
            Id = id;
            Email = email;
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return new StringBuilder().
                       Append($"Id: {this.Id}; ").
                       Append($"EMail: {this.Email}; ").
                       Append($"Username {this.Username}; ").
                       Append($"Password {this.Password}; ").
                       ToString();
        }
    }
}
