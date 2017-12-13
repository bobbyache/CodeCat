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
using Xceed.Wpf.AvalonDock.Layout;

namespace DockGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            layoutRoot.ElementAdded += (s, e) => { AddEvent($"{(e.Element as LayoutDocument).Title} added"); };
            layoutRoot.ElementRemoved += (s, e) => { AddEvent($"{(e.Element as LayoutDocument).Title} removed"); };
        }

        private void AddEvent(string eventText)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = eventText;
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = textBlock;

            EventsListBox.Items.Add(listBoxItem);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DocumentArea.Children.Add(new LayoutDocument() { Title = Guid.NewGuid().ToString() });
        }
    }
}
