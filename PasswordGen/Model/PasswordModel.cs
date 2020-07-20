using SQLite;

namespace PasswordGen
{
    public class PasswordModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Password { get; set; }
    }
}
