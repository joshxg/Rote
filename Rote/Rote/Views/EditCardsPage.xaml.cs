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
	public partial class EditCardsPage : ContentPage
	{
		public EditCardsPage(Deck deck)
		{
            
			InitializeComponent ();
            BindingContext = new EditCardsViewModel(deck);
        }
	}
}