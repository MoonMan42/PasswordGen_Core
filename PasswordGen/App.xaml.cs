using System.Reflection;
using System.Windows;

namespace PasswordGen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string passwordDb = "Passwords.db";
        public static string folderPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string passwordDbPath = System.IO.Path.Combine(folderPath, passwordDb);
    }
}
