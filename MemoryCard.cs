using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Memory
{
    /// <summary>
    /// Die Klasse fuer eine Karte des Memory-Spiels
    /// Sie erbt von Button
    /// </summary>
    class MemoryCard : Button
    {
        // Fields
        // eine eindeutige ID zur Identifikation des Bildes
        int picId;
        // fuer Vorder- und Rueckseite
        Image picFront, picBack;

        // wo liegt die Karte im Spielfeld
        int picPos;

        // ist die Karte umgedreht?
        bool isTourned;
        // ist die Karte noch im Spiel?
        bool inGame;

        // fuer das Spielfeld fuer die Karte
        MemoryPlayground game;


        /// <summary>
        /// Der Konstruktor
        /// </summary>
        /// <param name="front">Dateiname des Bildes</param>
        /// <param name="picId">Eindeutige Nummer fuer die Karte</param>
        public MemoryCard(string front, int picId, MemoryPlayground game)
        {
            // die Vorderseite, der Dateiname des Bildes wird an den Kosntruktor uebergeben
            picFront = new Image();
            picFront.Source = new BitmapImage(new Uri(front, UriKind.Relative));

            // die Rueckseite, sie wird fest gesetzt
            picBack = new Image();
            picBack.Source = new BitmapImage(new Uri("pics/verdeckt.bmp", UriKind.Relative));

            // die Eigenschaften zuweisen
            Content = picBack;

            // die Bild-Id
            this.picId = picId;

            // die Karte ist erst einmal umgedreht und noch im Feld
            isTourned = false;
            inGame = true;

            // mit dem Spielfeld verbinden
            this.game = game;

            // die Methode mit dem Ereginis verbinden
            Click += new RoutedEventHandler(ButtonClick);
        }


        // die Methode fuer das anklicken
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // ist die Karte ueberhaupt noch im Spiel?
            if (!inGame || !game.PickAllowed())
            {
                return;
            }
            // wenn die Rueckseite zu sehen ist -> Vorderseite anzeigen
            if (!isTourned)
            {
                Content = picFront;
                isTourned = true;
                // die Methode OpenCard() im Spielfeld aufrufen, uebergeben wird dabei die Karte(this)
                game.OpenCard(this);
            }
        }


        // die Methode zeigt die Rueckseite der Karte an
        public void ShowBackside(bool goOut)
        {
            // soll die Karte komplett aus dem Spiel genommen werden?
            if (goOut)
            {
                // das Bild aufgedeckt zeigen und die Karte aus dem Spiel nehmen
                Image picOut = new Image();
                picOut.Source = new BitmapImage(new Uri("pics/aufgedeckt.bmp", UriKind.Relative));
                Content = picOut;
                inGame = false;
            }
            else
            {
                // sonst nur die Rueckseite zeigen
                Content = picBack;
                isTourned = false;
            }
        }


        // die Methode zeigt die Vorderseite der Karte an
        public void ShowFrontSide()
        {
            Content = picFront;
            isTourned = true;
        }

        // die Methode liefert die Bild-ID einer Karte
        public int GetPicID()
        {
            return picId;
        }

        // die Methode leifert die Position einer Karte
        public int GetPicPos()
        {
            return picPos;
        }

        // die Methode setzt die Position einer Karte
        public void SetPicPos(int picPos)
        {
            this.picPos = picPos;
        }

        // die Methode liefert den Wert des Feldes umgedreht
        public bool GetIsTourned()
        {
            return isTourned;
        }

        // die Methode liefert den Wert des Felds inGame
        public bool GetInGame()
        {
            return inGame;
        }
    }
}
