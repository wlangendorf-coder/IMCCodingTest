using IMCCodingTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IMCCodingTest.ViewModels
{
    /// <summary>
    /// ViewModel for looking at server output.  User can use back to return and try again, or ok to return to start, ready to enter another order.
    /// </summary>
    public class DisplayOutcomeViewModel : BaseViewModel
    {
        #region Variables

        private string inputString;
        private string outputString;
        private string responseCode;
        private string responsePhrase;

        #endregion

        #region Commands
        public ICommand OKCommand { get; }

        /// <summary>
        /// This command pops off the stack, brings us back to square one
        /// </summary>
        /// <returns></returns>
        private async Task PopStack()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        #endregion

        #region Properties
        public string InputString
        {
            get => inputString;
            set => SetProperty(ref inputString, value);
        }

        public string OutputString
        {
            get => outputString;
            set => SetProperty(ref outputString, value);
        }

        public string ResponseCode
        {
            get => responseCode;
            set => SetProperty(ref responseCode, value);
        }

        public string ResponsePhrase
        {
            get => responsePhrase;
            set => SetProperty(ref responsePhrase, value);
        }

        #endregion

        /// <summary>
        /// Constructor which sets data into place to be viewed, initializes command
        /// </summary>
        /// <param name="seed"></param>
        public DisplayOutcomeViewModel(ServerCallResult seed)
        {
            Title = "Display Outcome";
            InputString = seed.InputString;
            OutputString = seed.OutputString;
            ResponseCode = seed.ResponseCode;
            ResponsePhrase = seed.ResponsePhrase;

            OKCommand = new Command(async () => await PopStack());
        }

        

    }
}
