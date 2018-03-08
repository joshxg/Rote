using Rote.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rote.ViewModels;

namespace Rote.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupNewDeck : PopupPage
	{
		public PopupNewDeck(ObservableCollection<Deck> Decks)
		{
            BindingContext = new ViewModelNewDeck(Decks);
			InitializeComponent ();
		}
	}
}