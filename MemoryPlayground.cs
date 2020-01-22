using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Memory
{
    /// <summary>
    /// Diese Klasse baut das Spielfeld und die Logik dafuer auf.
    /// </summary>
    class MemoryPlayground
    {
        // Fields
        MemoryCard[] cards;

        // das Array fuer die Namen der pics
        string[] pics =
        {
            "pics/apfel.bmp", "pics/birne.bmp",
            "pics/blume.bmp", "pics/blume2.bmp",
            "pics/ente.bmp", "pics/fisch.bmp",
            "pics/fuchs.bmp", "pics/igel.bmp",
            "pics/kaenguruh.bmp", "pics/katze.bmp",
            "pics/kuh.bmp", "pics/maus1.bmp",
            "pics/maus2.bmp", "pics/maus3.bmp",
            "pics/melone.bmp", "pics/pilz.bmp",
            "pics/ronny.bmp", "pics/schmetterling.bmp",
            "pics/sonne.bmp", "pics/wolke.bmp",
            "pics/maus4.bmp"
        };

        // fuer die Punkte
        int playerPoints, computerPoints;
        Label playerPointsLabel, computerPointsLabel;

        // wie viele Karten sind aktuell umgedreht?
        int tournedCards;

        // fuer das aktuell umgedrehte Paar
        MemoryCard[] pair;

        // fuer den aktuellen Spieler
        int player;

        // das "Gedaechtnis" fuer den Computer, er speichert hier, wo das Gegenstueck liegt
        int[,] rememberCards;

        // fuer das eigentliche Spielfeld
        UniformGrid field;


        /// <summary>
        /// Der Kosntruktor fuer das Spielfeld.
        /// </summary>
        /// <param name="field"></param>
        public MemoryPlayground(UniformGrid field)
        {
            // zum zaehlen fuer die Bilder
            int count = 0;

            // das Array fuer die Karten erstellen, insgesamt 42 Stueck
            cards = new MemoryCard[42];

            // fuer das Paar
            pair = new MemoryCard[2];

            // fuer das Gedaechtnis (speichert fuer jede Karte paarewise die Psotion im Spielfeld)
            rememberCards = new int[2, 21];

            // keiner hat zu beginn einen Punkt
            playerPoints = 0;
            computerPoints = 0;

            // es ist keine KArte umgedreht
            tournedCards = 0;

            // der Mensch faengt an
            player = 0;

            // das Spielfeld setzen
            this.field = field;

            // es gibt keine gemerkte Karten
            for (int outside = 0; outside < 2; outside++)
            {
                for (int inside = 0; inside < 21; inside++)
                {
                    rememberCards[outside, inside] = -1;
                }
            }

            // das eigentliche Spielfeld erstellen
            for (int i = 0; i <= 41; i++)
            {
                // eine neue Karte erzeugen
                cards[i] = new MemoryCard(pics[count], count);
                // die Position der Karte setzen
                cards[i].SetPicPos(i);

                // die Karte hinzufuegen
                field.Children.Add(cards[i]);
                // bei jeder zweiten Karte kommt auch ein neues Bild hinzu
                if ((i + 1) % 2 == 0)
                {
                    count++;
                }
            }

            // die Labels fuer die Punkte
            Label human = new Label();
            human.Content = "Spieler";
            field.Children.Add(human);
            playerPointsLabel = new Label();
            playerPointsLabel.Content = 0;
            field.Children.Add(playerPointsLabel);

            Label computer = new Label();
            computer.Content = "Computer";
            field.Children.Add(computer);
            computerPointsLabel = new Label();
            computerPointsLabel.Content = 0;
            field.Children.Add(computerPointsLabel);
        }
    }
}
