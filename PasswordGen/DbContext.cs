using SQLite;
using System.Collections.Generic;
using System.IO;

namespace PasswordGen
{
    public class DbContext
    {
        private List<PasswordModel> passwords;

        public DbContext()
        {
            passwords = new List<PasswordModel>();
        }


        public List<PasswordModel> ReadDatabase()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(App.databasePath))
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
                using (SQLiteConnection cnn = new SQLiteConnection(App.databasePath))
                {
                    cnn.CreateTable<PasswordModel>();

                    cnn.Insert(newPassword);
                }
            }
            // reload list
            ReadDatabase();
        }

        public void DeleteEntry(PasswordModel passwordToDelete)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(App.databasePath))
            {
                cnn.CreateTable<PasswordModel>();

                cnn.Delete(passwordToDelete);
            }

            ReadDatabase();
        }

        public void DeleteAll() // deletes the entire database (BE CARFUL!!)
        {
            if (File.Exists(App.databasePath))
            {
                File.Delete(App.databasePath);
            }
        }

    }
}
