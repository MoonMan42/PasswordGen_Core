using System.Windows;

namespace PasswordGen
{
    /// <summary>
    /// Interaction logic for DeleteAllCheck.xaml
    /// </summary>
    public partial class DeleteAllCheck : Window
    {
        private DbContext _context;

        public DeleteAllCheck()
        {
            InitializeComponent();

            _context = new DbContext();
        }

        private void DeleteDatabase_Click(object sender, RoutedEventArgs e)
        {
            _context.DeleteAll();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
