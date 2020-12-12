using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using IMCCodingTest.Views;
using System.Threading.Tasks;

namespace IMCCodingTest.ViewModels
{
    /// <summary>
    /// ViewModel for the front page
    /// </summary>
    public class FrontPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Constructor for front page
        /// </summary>
        public FrontPageViewModel()
        {
            Title = "Calculate Taxes";
            CreateOrderCommand = new Command(async () => await CreateOrder());
        }

        #region Comamnds

        public ICommand CreateOrderCommand { get; }
        public async Task CreateOrder()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new CreateOrderPage());
        }

        #endregion
    }
}
