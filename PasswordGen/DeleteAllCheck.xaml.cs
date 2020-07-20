using System.Windows;

namespace PasswordGen
{
    /// <summary>
    /// Interaction logic for DeleteAllCheck.xaml
    /// </summary>
    public partial class DeleteAllCheck : Window
    {
        private PasswordDbContext _context;

        public DeleteAllCheck()
        {
            InitializeComponent();

            _context = new PasswordDbContext();
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
