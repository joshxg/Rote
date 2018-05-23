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
	public partial class ByScorePage : ContentPage
	{
		public ByScorePage(Deck Deck)
		{
			InitializeComponent ();
            BindingContext = new ByScoreViewModel(Deck);
		}

        public ByScorePage(int DeckID)
        {
            InitializeComponent();
            BindingContext = new ByScoreViewModel(DeckID);
        }
    }
}