using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Rote.Models;
using Rote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rote.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RenamePopup : PopupPage
	{
		public RenamePopup (Deck deck)
		{
            BindingContext = new RenameViewModel(deck);
			InitializeComponent ();

		}
	}
}