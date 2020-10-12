﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace PasswordGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private PasswordDbContext _context;

        private string[] specialCharList = { "!", "$", "#", "%", "&" };

        public MainWindow()
        {
            InitializeComponent();

            _context = new PasswordDbContext();

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
                item.Password = item.Password.Replace("~", $"{specialCharList[random.Next(specialCharList.Length - 1)]}");
            }


            // shuffle list and take the first 5
            passwordListView.ItemsSource = Shuffle(itemList).Take(5);
        }

        public List<PasswordModel> Shuffle(List<PasswordModel> list)
        {
            Random gen = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = gen.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
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

            Clipboard.SetDataObject(password.Password);
        }

        private void CopyEntry_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PasswordModel password = (PasswordModel)passwordListView.SelectedItem;

            try
            {
                if (password != null)
                {
                    Clipboard.SetDataObject(password.Password);
                }
            }
            catch (Exception ex)
            {
            }
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

        private void OpenRules_Click(object sender, RoutedEventArgs e)
        {
            PasswordRules passwordRulesWindow = new PasswordRules();

            passwordRulesWindow.ShowDialog();
        }
    }
}
