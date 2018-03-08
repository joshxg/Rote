using Rg.Plugins.Popup.Pages;
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
	public partial class PlayPopUp : PopupPage
	{
		public PlayPopUp (Deck deck, int Type) 
		{
            BindingContext = new PlayPopUpViewModel(deck, Type);
			InitializeComponent ();
		}
	}
}