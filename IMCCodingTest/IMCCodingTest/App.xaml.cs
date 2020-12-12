using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IMCCodingTest.Views;

namespace IMCCodingTest
{
    /// <summary>
    /// Launch class for the app
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Beginning of execution
        /// </summary>
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new FrontPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
