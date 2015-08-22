using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;
using UPC.HRNPCI.DesktopApplication._Common;
using UPC.HRNPCI.DesktopApplication._View;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;


namespace DevLake.MasterDetail.UI.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        //private string firstName;
        //private string lastName;
        ////private ObservableCollection<OrderViewModel> orders = null;        

        ////for opening up the Edit Customer window
        //private ICommand showEditCommand;

        ////for adding/saving customer information
        //private ICommand updateCommand;

        ////for deleting a customer
        //private ICommand deleteCommand;             
  
        ////for cancel an Edit
        //private ICommand cancelCommand;

        //private CustomerViewModel originalValue;

        //public int CustomerId
        //{
        //    get;
        //    set;
        //}

        //public string FirstName
        //{
        //    get { return firstName; }
        //    set 
        //    { 
        //        firstName = value;
        //        OnPropertyChanged("FirstName");
        //    }
        //}

        //public string LastName
        //{
        //    get { return lastName; }
        //    set 
        //    { 
        //        lastName = value;
        //        OnPropertyChanged("LastName");
        //    }
        //}

        ///// <summary>
        ///// For determining if you are in Add Customer or Edit Customer mode
        ///// </summary>
        //public Mode Mode
        //{
        //    get;
        //    set;
        //}

        ////for accessing the CustomerListViewModel that holds this CustomerViewModel
        //public CustomerListViewModel Container
        //{
        //    get { return CustomerListViewModel.Instance(); }
        //}

        ////list of orders from the customer
        //public ObservableCollection<OrderViewModel> Orders
        //{
        //    get { return GetOrders(); }  
        //    set
        //    {
        //        orders = value;
        //        OnPropertyChanged("Orders");
        //    }
        //}

        //public ICommand ShowEditCommand
        //{
        //    get
        //    {
        //        if (showEditCommand == null)
        //        {
        //            showEditCommand = new CommandBase(i => this.ShowEditDialog(), null);
        //        }
        //        return showEditCommand;
        //    }
        //}

        //public ICommand UpdateCommand
        //{
        //    get
        //    {
        //        if (updateCommand == null)
        //        {
        //            updateCommand = new CommandBase(i => this.Update(), null);
        //        }
        //        return updateCommand;
        //    }
        //}

        //public ICommand DeleteCommand
        //{
        //    get
        //    {
        //        if (deleteCommand == null)
        //        {
        //            deleteCommand = new CommandBase(i => this.Delete(), null);
        //        }
        //        return deleteCommand;
        //    }
        //}

        //public ICommand CancelCommand
        //{
        //    get
        //    {
        //        if (cancelCommand == null)
        //        {
        //            cancelCommand = new CommandBase(i => this.Undo(), null);
        //        }
        //        return cancelCommand;
        //    }
        //} 

        //public CustomerViewModel(svcCustomer.Customer c)
        //{
        //    CustomerId = c.CustomerId;
        //    firstName = c.FirstName;
        //    lastName = c.LastName;
        //    //copy the current value so in case cancel you can undo
        //    this.originalValue = (CustomerViewModel)this.MemberwiseClone();
        //}

        //internal CustomerViewModel()
        //{
        //}

        //internal ObservableCollection<OrderViewModel> GetOrders()
        //{
        //    orders = new ObservableCollection<OrderViewModel>();
        //    //get the orders from the service tier
        //    svcOrder.OrderServiceClient c = new svcOrder.OrderServiceClient();
        //    foreach (svcOrder.Order i in c.GetOrderByCustomer(this.CustomerId))
        //    {
        //        OrderViewModel order = new OrderViewModel(i);
        //        order.Customer = this;
        //        orders.Add(order);
        //    }
        //    return orders;
        //}

        //private void ShowEditDialog()
        //{
        //    this.Mode = ViewModel.Mode.Edit;
            
        //    IModalDialog dialog = ServiceProvider.Instance.Get<IModalDialog>();
        //    dialog.BindViewModel(this); //bind to this viewModel
        //    dialog.ShowDialog();
        //}

        //private void Update()
        //{
        //    svcCustomer.CustomerServiceClient c = new svcCustomer.CustomerServiceClient();
        //    if (this.Mode == ViewModel.Mode.Add)  //if adding a customer
        //    {
        //        c.AddCustomer(this.FirstName, this.LastName);
        //        //refresh the view
        //        this.Container.CustomerList = this.Container.GetCustomers();
        //    }
        //    else if (this.Mode == ViewModel.Mode.Edit)  //if editing a customer
        //    {
        //        c.UpdateCustomer(new svcCustomer.Customer
        //                                {
        //                                    CustomerId = this.CustomerId,
        //                                    FirstName = this.FirstName,
        //                                    LastName = this.LastName
        //                                });
        //        //copy the current value so in case cancel you can undo
        //        this.originalValue = (CustomerViewModel)this.MemberwiseClone();
        //    }
        //}

        //private void Delete()
        //{
        //    svcCustomer.CustomerServiceClient c = new svcCustomer.CustomerServiceClient();
        //    c.DeleteCustomer(this.CustomerId);
        //    //refresh the view
        //    this.Container.CustomerList = this.Container.GetCustomers();
        //}

        //private void Undo()
        //{
        //    if (this.Mode == ViewModel.Mode.Edit)
        //    {
        //        this.FirstName = originalValue.FirstName;
        //        this.LastName = originalValue.LastName;
        //    }
        //}
    }
}
