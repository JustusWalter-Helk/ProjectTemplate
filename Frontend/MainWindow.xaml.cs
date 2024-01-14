﻿using System;
using System.Runtime.InteropServices;
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

namespace Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("Backend.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern void printTest();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Tet");
            await Task.Run(() => { printTest(); });
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Console.Write("clicked");
        }
    }
}