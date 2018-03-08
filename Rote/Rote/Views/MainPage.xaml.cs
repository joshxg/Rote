using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Rote.ViewModels;
using Rote.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rote
{

	public partial class MainPage : ContentPage
	{

        public MainPage()

		{
            
            BindingContext = new MainViewModel(Navigation);
			InitializeComponent();

		}

    }

}
