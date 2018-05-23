using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Rote
{
	public partial class App : Application
	{
		public App (int DeckID = -1, int Game = -1)
		{
			InitializeComponent();
            MainPage = new NavigationPage(new Rote.MainPage());

            if (DeckID > 0)
            {
                switch (Game)
                {
                    case 1:
                        Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Rote.Views.PlayPage(DeckID));
                        break;
                    case 2:
                        Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Rote.Views.MultiChoicePage(DeckID));
                        break;
                    case 3:
                        Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Rote.Views.ByScorePage(DeckID));
                        break;
                }
            }
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}




