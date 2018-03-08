using Rg.Plugins.Popup.Services;
using Rote.Models;
using Rote.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rote.ViewModels
{
    public class PlayPopUpViewModel
    {
        public ICommand PlayAgain { get; set; }
        public ICommand GoBack { get; set; }
        INavigation navigation;
        Deck deck;
        int Type; // 1 = PlayPage, 2 = MultiChoice, 3 = ByScore

        public PlayPopUpViewModel(Deck deck, int Type)
        {
            PlayAgain = new Command(Play);
            GoBack = new Command(BackAsync);
            this.navigation = navigation;
            this.deck = deck;
            this.Type = Type;
        }

        private async void BackAsync(object obj)
        {
            await PopupNavigation.PopAsync();
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopToRootAsync();
        }
            
        private void Play(object obj)
        {
            PopupNavigation.PopAsync();

            if(Type == 1)
            {
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PlayPage(deck));
            }
            else if (Type == 2)
            {
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new MultiChoicePage(deck));
            }
            else
            {
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ByScorePage(deck));
            }
        }
    }
}

