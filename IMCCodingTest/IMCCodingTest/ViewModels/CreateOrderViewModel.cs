using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using IMCCodingTest.Models;
using IMCCodingTest.Views;
using IMCCodingTest.Services;
using Xamarin.Forms;


namespace IMCCodingTest.ViewModels
{
    /// <summary>
    /// ViewModel for the CreateOrderPage
    /// </summary>
    public class CreateOrderViewModel : BaseViewModel
    {
        #region Variables
        private string fromCountry;
        private string fromZip;
        private string fromState;
        private string fromCity;
        private string fromStreet;

        private string toCountry;
        private string toZip;
        private string toState;
        private string toCity;
        private string toStreet;

        private string amount;
        private string shipping;

        public List<NexusAddress> Addresses = new List<NexusAddress>();
        public List<LineItem> LineItems = new List<LineItem>();
        #endregion

        /// <summary>
        /// Constructor, intializes commands and vars that need initializing
        /// </summary>
        public CreateOrderViewModel()
        {
            Title = "Create Order";
            CalculateTaxCommand = new Command(()=>CalculateTax());
            AddAddressCommand = new Command(() => AddAddress());
            ClearAddressesCommand = new Command(() => ClearAddresses());
            AddLineItemCommand = new Command(() => AddLineItem());
            ClearLineItemsCommand = new Command(() => ClearLineItems());

            AutoPopulateCommand = new Command(() => AutoPopulate());

            Addresses = new List<NexusAddress>();
            LineItems = new List<LineItem>();
        }

        #region Commands

        public ICommand CalculateTaxCommand { get; }
        public ICommand AddAddressCommand { get; }
        public ICommand ClearAddressesCommand { get; }
        public ICommand AddLineItemCommand { get; }
        public ICommand ClearLineItemsCommand { get; }

        public ICommand AutoPopulateCommand { get; }

        /// <summary>
        /// Calls the Service's Calculate Tax method with the information stored in this model as input
        /// Then gives the response to a DisplayOutComeViewModel and pushes it's page onto the stack
        /// </summary>
        public async void CalculateTax()
        {
            Order toBeCalculated = GenerateOrder();

            TaxService ts = TaxService.Singleton;
            ServerCallResult result = await ts.CalculateTaxes(toBeCalculated);

            DisplayOutcomeViewModel outputVM = new DisplayOutcomeViewModel(result);
            DisplayOutcomePage outputPage = new DisplayOutcomePage();
            outputPage.BindingContext = outputVM;

            await Application.Current.MainPage.Navigation.PushAsync(outputPage);
        }

        /// <summary>
        /// Helper method for CalculateTax, generates an Order from the information stored in this model
        /// </summary>
        /// <returns></returns>
        public Order GenerateOrder()
        {
            Order toBeCalculated = new Order(FromCountry, FromZip, FromState, FromCity, FromStreet, ToCountry, ToZip, ToState, ToCity, ToStreet, float.Parse(Amount), float.Parse(Shipping));
            toBeCalculated.line_items.AddRange(LineItems);
            toBeCalculated.nexus_addresses.AddRange(Addresses);
            return toBeCalculated;
        }

        /// <summary>
        /// Helper method for developer/testing, not accessible to user's.  Saves data entry time
        /// </summary>
        public void AutoPopulate()
        {
            FromStreet = "350 5th Avenue";
            FromCity = "New York";
            FromState = "NY";
            FromZip = "10118";
            FromCountry = "US";
            LineItem temp = new LineItem();
            temp.id = "1";
            temp.quantity = 1;
            temp.unit_price = 19.95f;
            temp.product_tax_code = "20010";
            LineItems.Add(temp);
            ToStreet = "668 Route Six";
            ToCity = "Mahopac";
            ToState = "NY";
            ToZip = "10541";
            ToCountry = "US";
            Amount = "19.95";
            Shipping = "10";

        }

        /// <summary>
        /// Sends the user to an AddAddress page where info is gathered, then stores that info in an Address and adds it to Addresses
        /// </summary>
        public async void AddAddress()
        {
            NexusAddress tempAddress = new NexusAddress();
            AddAddressPage tempPage = new AddAddressPage();
            tempPage.BindingContext = tempAddress;
            await Application.Current.MainPage.Navigation.PushAsync(tempPage);
            Addresses.Add(tempAddress);
            OnPropertyChanged("AddressCount");
        }

        /// <summary>
        /// Clears the Addresses collection
        /// </summary>
        public void ClearAddresses()
        {
            Addresses.Clear();
            OnPropertyChanged("AddressCount");
        }

        /// <summary>
        /// Sends the user to an AddLineItem page where info is gathered, then stores that info in a LineItem and adds it to LineItems
        /// </summary>
        public async void AddLineItem()
        {
            LineItem tempLineItem = new LineItem();
            AddLineItemPage tempPage = new AddLineItemPage();
            tempPage.BindingContext = tempLineItem;
            await Application.Current.MainPage.Navigation.PushAsync(tempPage);
            LineItems.Add(tempLineItem);
            OnPropertyChanged("LineItemCount");
        }

        /// <summary>
        /// Clears the LineItems collection
        /// </summary>
        public void ClearLineItems()
        {
            LineItems.Clear();
            OnPropertyChanged("LineItemCount");
        }

        #endregion

        #region Properties
        public string FromCountry
        {
            get => fromCountry;
            set => SetProperty(ref fromCountry, value);
        }

        public string FromZip
        {
            get => fromZip;
            set => SetProperty(ref fromZip, value);
        }

        public string FromState
        {
            get => fromState;
            set => SetProperty(ref fromState, value);
        }

        public string FromCity
        {
            get => fromCity;
            set => SetProperty(ref fromCity, value);
        }

        public string FromStreet
        {
            get => fromStreet;
            set => SetProperty(ref fromStreet, value);
        }

        public string ToCountry
        {
            get => toCountry;
            set => SetProperty(ref toCountry, value);
        }

        public string ToZip
        {
            get => toZip;
            set => SetProperty(ref toZip, value);
        }

        public string ToState
        {
            get => toState;
            set => SetProperty(ref toState, value);
        }

        public string ToCity
        {
            get => toCity;
            set => SetProperty(ref toCity, value);
        }

        public string ToStreet
        {
            get => toStreet;
            set => SetProperty(ref toStreet, value);
        }

        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public string Shipping
        {
            get => shipping;
            set => SetProperty(ref shipping, value);
        }

        public int AddressCount
        {
            get => Addresses.Count;
        }

        public int LineItemCount
        {
            get => LineItems.Count;
        }

    }
    #endregion

}
