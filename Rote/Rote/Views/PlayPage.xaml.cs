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
	public partial class PlayPage : ContentPage
	{
		public PlayPage (Deck deck)
		{
            BindingContext = new PlayViewModel(deck);
            InitializeComponent ();
            

        }

        public PlayPage(int DeckID)
        {
            BindingContext = new PlayViewModel(DeckID);
            InitializeComponent();
        }
    }
}