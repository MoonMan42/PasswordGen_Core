using System;
using System.Collections.Generic;
using System.Windows;

namespace PasswordGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<PasswordModel> passwords;

        private DbContext _context;

        private string[] specialCharList = { "!", "$", "#", "%", "&" };

        public MainWindow()
        {
            InitializeComponent();

            passwords = new List<PasswordModel>();

            _context = new DbContext();

            // load data list
            LoadItemList();
        }

        private void LoadItemList()
        {
            var itemList = _context.ReadDatabase();
            Random random = new Random();

            foreach (var item in itemList)
            {
                item.Password = item.Password.Replace("*", $"{random.Next(10, 100)}");
                item.Password = item.Password.Replace("!", $"{specialCharList[random.Next(specialCharList.Length - 1)]}");
            }

            passwordListView.ItemsSource = itemList;
        }

        private void SaveEntry_Click(object sender, RoutedEventArgs e)
        {
            PasswordModel newPassword = new PasswordModel
            {
                Password = newEntryTextBox.Text
            };

            // save Entry
            _context.SaveEntry(newPassword);
            LoadItemList();

            newEntryTextBox.Text = string.Empty;
        }

        private void SaveEntry_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            PasswordModel newPassword = new PasswordModel
            {
                Password = newEntryTextBox.Text
            };

            if (e.Key == System.Windows.Input.Key.Return)
            {
                _context.SaveEntry(newPassword);
                LoadItemList();
                newEntryTextBox.Text = string.Empty;
            }
        }

        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            LoadItemList();
        }

        private void DeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            PasswordModel passwordToDelete = (PasswordModel)passwordListView.SelectedItem;
            _context.DeleteEntry(passwordToDelete);
            LoadItemList();

        }

        private void CopyEntry_Click(object sender, RoutedEventArgs e)
        {
            PasswordModel password = (PasswordModel)passwordListView.SelectedItem;

            Clipboard.SetText(password.Password);
        }

        private void CopyEntry_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PasswordModel password = (PasswordModel)passwordListView.SelectedItem;

            Clipboard.SetText(password.Password);
        }

        private void Exit_Program(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeleteAllCheck_Click(object sender, RoutedEventArgs e)
        {
            DeleteAllCheck deleteAllWindow = new DeleteAllCheck();

            deleteAllWindow.ShowDialog();

            LoadItemList();
        }
    }
}
