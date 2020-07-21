using SQLite;
using System.Collections.Generic;
using System.IO;

namespace PasswordGen
{
    public class PasswordDbContext
    {
        private List<PasswordModel> passwords;

        public PasswordDbContext()
        {
            passwords = new List<PasswordModel>();
        }


        public List<PasswordModel> ReadDatabase()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(App.passwordDbPath))
            {
                cnn.CreateTable<PasswordModel>();
                passwords = (cnn.Table<PasswordModel>()).ToList();
            }

            return passwords;
        }

        public void SaveEntry(PasswordModel newPassword)
        {

            if (newPassword.Password != null)
            {
                using (SQLiteConnection cnn = new SQLiteConnection(App.passwordDbPath))
                {
                    cnn.CreateTable<PasswordModel>();

                    cnn.Insert(newPassword);
                }
            }

        }

        public void DeleteEntry(PasswordModel passwordToDelete)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(App.passwordDbPath))
            {
                cnn.CreateTable<PasswordModel>();

                cnn.Delete(passwordToDelete);
            }

        }

        public void DeleteAll() // deletes the entire database (BE CARFUL!!)
        {
            if (File.Exists(App.passwordDbPath))
            {
                File.Delete(App.passwordDbPath);
            }
        }
    }
}
