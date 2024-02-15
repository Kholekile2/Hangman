using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HangmanAssignment
{
    public partial class HangmanGamePage : ContentPage
    {
        private List<string> Words = new List<string>() { "Play", "NETMAUI", "Madrid", "Kiddo" };
        private string secretWord;
        private int turns;
        private int hangman;
        public string GuessedWord { get; set; }
        public string TurnsLeft { get; set; }

        public HangmanGamePage()
        {
            InitializeComponent();
            ResetGame();
            Random rand = new Random();
            turns = 8;
            hangman = 1;
            UpdateDisplay();
            BindingContext = this;
        }

        private void Guess_Clicked(object sender, EventArgs e)
        {
            
            char guess = guessWordEntry.Text.ToLower()[0];
            guessWordEntry.Text = "";

            bool correctGuess = false;
            for (int i = 0; i < secretWord.Length; i++)
            {
                if (secretWord[i] == guess)
                {
                    GuessedWord = GuessedWord.Substring(0, i) + guess + GuessedWord.Substring(i + 1);
                    correctGuess = true;
                }
            }

            if (!correctGuess)
            {
                turns--;
                hangman++;
                UpdateDisplay();
            }
            else if (GuessedWord == secretWord)
            {
                DisplayAlert("Congratulations!", "You guessed the word correctly!", "OK");
                ResetGame();
            }

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            string spacedGuessedWord = string.Join(" ", GuessedWord.ToCharArray());

            guessedWordLabel.Text = spacedGuessedWord;

            string imageName = $"hang{hangman}.png";

            HangmanImage.Source = imageName;

            if(turns > 0)
            {
                turnsLabel.Text = $"Turns left: {turns}";
            }
            else
            {
                turnsLabel.Text = "Game over";
                DisplayAlert("Game Over", "The word was: " + secretWord, "OK");
                ResetGame();
            }
        }

        private void ResetGame()
        {
            secretWord = Words[new Random().Next(Words.Count)].ToLower();
            GuessedWord = new string('_', secretWord.Length);
            hangman =1;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResetGame();
        }

    }
}