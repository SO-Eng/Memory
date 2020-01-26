using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Memory
{
    /// <summary>
    /// Interaktionslogik für GameEnd.xaml
    /// </summary>
    public partial class GameEnd : Window
    {
        // Fields
        //MemoryPlayground mP;

        public GameEnd(int playerPoints, int computerPoints)
        {
            InitializeComponent();

            // GameResult anzeigen
            ResultGame(playerPoints, computerPoints);
        }


        private void ResultGame(int pP, int cP)
        {
                        //string over = "Möchtest Du nocheinmal spielen?";
            string playerWon = $"Herzlichen Glückwunsch! \nDu hast mit { pP.ToString() } Punkten gewonnen!";
            string computerWon = $"Schade! \nDu hast gegen den Computer mit { cP.ToString() } Punkten verloren!";
            string noWinner = "Das war ein sehr knappes Spiel! \nBei gleichem Punktestand gab es keinen Gewinner!";


            if (pP > cP)
            {
                imageResult.Source = new BitmapImage(new Uri("gameEnd/trophy.png", UriKind.Relative));
                textBlockCustomtext.Text = playerWon;
                labelPlayerpointsText.Content = pP.ToString();
                labelComputerpointsText.Content = cP.ToString();
            }
            else if (pP < cP)
            {
                imageResult.Source = new BitmapImage(new Uri("gameEnd/lose.png", UriKind.Relative));
                textBlockCustomtext.Text = computerWon;
                labelPlayerpointsText.Content = pP.ToString();
                labelComputerpointsText.Content = cP.ToString();
            }
            else
            {
                imageResult.Source = new BitmapImage(new Uri("gameEnd/undecided.png", UriKind.Relative));
                textBlockCustomtext.Text = noWinner;
                labelPlayerpointsText.Content = pP.ToString();
                labelComputerpointsText.Content = cP.ToString();
            }


        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
