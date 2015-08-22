using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevLake.MasterDetail.UI.ViewModel;
using System.Collections.ObjectModel;

namespace DevLake.MasterDetail.UI.View
{
    /// <summary>
    /// Interaction logic for OrderListView.xaml
    /// </summary>
    public partial class OrderListView : UserControl
    {
        public OrderListView()
        {
            InitializeComponent();
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderView view = new OrderView();
            OrderViewModel order = new OrderViewModel();
            //make sure the OrderViewModel has reference to the CustomerViewModel so that it can perform the AddOrder operation
            order.Customer = (CustomerViewModel)this.DataContext;  //the OrderViewModel's container is the CustomerViewModel
            order.Mode = Mode.Add;
            view.DataContext = order;  //the view's dataContext is the OrderViewModel
            view.ShowDialog();
        }

        private void btnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderView view = new OrderView();
            OrderViewModel order = (OrderViewModel)((Button)sender).DataContext;
            order.Mode = Mode.Edit;
            view.DataContext = order;
            view.ShowDialog();
        }
    }
}
