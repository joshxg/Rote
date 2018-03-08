using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
/* TODO: Events into commands
 * 
 * 
 * 
 * 
 * 
 */

namespace Rote
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage( new Rote.MainPage());
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

