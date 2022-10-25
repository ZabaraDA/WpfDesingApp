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
using WpfDesingApp.databases;
namespace WpfDesingApp.pages
{
    /// <summary>
    /// Логика взаимодействия для DataProductPage.xaml
    /// </summary>
    public partial class DataProductPage : Page
    {
        TradeEntities tradeEntities = new TradeEntities();
        public DataProductPage()
        {
           

            InitializeComponent();
            DataContext = tradeEntities;
            ProductListView.ItemsSource = tradeEntities.Product.ToList();
            
            
        }
    }
}
