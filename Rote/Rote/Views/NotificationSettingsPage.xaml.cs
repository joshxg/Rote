﻿using Rote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rote.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Rote.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationSettingsPage : ContentPage
	{
  

		public NotificationSettingsPage(Deck Deck)
		{
			InitializeComponent ();
            BindingContext = new NotificationSettingsViewModel(Deck);
		}
	}
}
