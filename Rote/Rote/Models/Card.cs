using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Rote.Models
{
    [Table("Card")]
    public class Card : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [PrimaryKey, AutoIncrement]
        public int? ID { get; set; }
        public string DeckName { get; set; }

        private int _Score;
        public int Score
        {
            get { return _Score; }
            set { _Score = value; if (PropertyChanged != null) { OnPropertyChanged("Score"); } }
        }

        private string _Question;
        public string Question
        {
            get { return _Question; }
            set { _Question = value; OnPropertyChanged("Question"); }
        }

        private string _Answer;
        public string Answer
        {
            get { return _Answer; }
            set { _Answer = value; if(PropertyChanged != null){ OnPropertyChanged("Answer"); } }
        }

        public Card() { }

        public Card(string Question, string Answer, string DeckName)
        {
            this.DeckName = DeckName;
            this.Question = Question;
            this.Answer = Answer;
        }




        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
