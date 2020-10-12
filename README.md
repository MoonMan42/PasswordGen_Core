# PasswordGen_Core
Password Generator Program to assist in generating new passwords


1. install the sqlite-net-pcl package

1. Setup the Database connction

        public static string passwordDb = "Passwords.db";
        public static string folderPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string passwordDbPath = System.IO.Path.Combine(folderPath, passwordDb);
        
1. Setup the model of the database

               public class PasswordModel
                {
                [PrimaryKey, AutoIncrement]
                public int Id { get; set; } // primary key, Auto increment integer
                public string Password { get; set; }
                }
        
1. Setup the basic connections for the Sqlite data table

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
               }
