﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrackFit_Project
{
    /// <summary>
    /// Interaction logic for Main_Page.xaml
    /// </summary>
    public partial class Main_Page : Page
    {
        public Main_Page()
        {
            InitializeComponent();
        }

        private void profileButtonClick(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            mainWindow.Content = new Profile_Page();
        }
    }
}
