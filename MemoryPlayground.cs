using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

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

        // Timer erzeugen (Instanz)
        DispatcherTimer timer = new DispatcherTimer();

        // Schwierigkeitsgrad des Computers
        int difficulty = 10;

        /// <summary>
        /// Der Kosntruktor fuer das Spielfeld.
        /// </summary>
        /// <param name="field"></param>
        public MemoryPlayground(UniformGrid field)
        {
            // Zufallszahl generieren
            Random rndNum = new Random();

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
                cards[i] = new MemoryCard(pics[count], count, this);
                // bei jeder zweiten Karte kommt auch ein neues Bild hinzu
                if ((i + 1) % 2 == 0)
                {
                    count++;
                }
            }

            // die Karten mischen
            for (int i = 0; i <= 41; i++)
            {
                int temp1;
                MemoryCard temp2;
                // eine Zufaellige Zahl im Bereich 0 bis 41 erzeugen
                temp1 = rndNum.Next(42);
                // den alten Wert in Sciherheit bringen
                temp2 = cards[temp1];
                //die Werte tauschen
                cards[temp1] = cards[i];
                cards[i] = temp2;
            }

            // die Karten ins Spielfeld bringen
            for (int i = 0; i <= 41; i++)
            {
                // die Position der Karte setzen
                cards[i].SetPicPos(i);

                // die Karte hinzufuegen
                field.Children.Add(cards[i]);
            }

            // das Intervall setzen
            timer.Interval = TimeSpan.FromMilliseconds(2000);
            // die Methode fuer das Ereignis zuwesien
            timer.Tick += new EventHandler(TimerTick);

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


        /// <summary>
        /// In dieser Methode wird die wesentliche Steuerung uebernommen
        /// </summary>
        /// <param name="card">Bekommt die Karte aus MemoryCard Klasse</param>
        public void OpenCard(MemoryCard card)
        {
            // zum zwischenspeichern der ID und der Position
            int cardId, cardPos;

            // die Karte zwischenspeichern
            pair[tournedCards] = card;
            // die ID und die Position beschaffen
            cardId = card.GetPicID();
            cardPos = card.GetPicPos();

            // die Karten in das "Gedaechtnis" des Compiuters eintragen, aber nur dann,
            // we es noch keinen Eintrag gibt
            if (rememberCards[0, cardId] == -1)
            {
                rememberCards[0, cardId] = cardPos;
            }
            else
            {
                // wenn es schon einen Eintrag gibt und der nicht mit der aktuellen Position übereinstimmt, 
                // dann haben wir die zweite Karte gefunden
                // Sie wird in die zweite Dimension eingetragen
                if (rememberCards[0, cardId] != cardPos)
                {
                    rememberCards[1, cardId] = cardPos;
                }
            }
            // ummgedrehte Karten erhoehen
            tournedCards++;

            // sind 2 Karten umgedereht worden?
            if (tournedCards == 2)
            {
                // dann pruefen wir ob es ein Paar ist
                ProofPair(cardId);
                //den Timer starten
                timer.Start();
                // die Karten wieder umdrehen
            }

            // haben wir zusammen 21 Paare, dann ist das Spiel beendent
            if (computerPoints + playerPoints == 21)
            {
                EndofGame();
            }
        }


        // die methode prueft, ob ein Paar gefunden wurde
        private void ProofPair(int cardId)
        {
            //bool isPair = false;

            if (pair[0].GetPicID() == pair[1].GetPicID())
            {
                //die Punkte setzen
                PairFound();
                // die Karten aus dem Gedaechtnis loeschen
                rememberCards[0, cardId] = -2;
                rememberCards[1, cardId] = -2;
            }
        }

        // die Methode setzt die Punkte wenn ein Paar gefunden wurde
        private void PairFound()
        {
            // spielt gerade der Mensch?
            if (player == 0)
            {
                playerPoints++;
                playerPointsLabel.Content = playerPoints.ToString();
            }
            else
            {
                computerPoints++;
                computerPointsLabel.Content = computerPoints.ToString();
            }
        }

        // die Methode dreht die Karten wieder auf die Rueckseite, bzw. nimmt sie aus dem Spiel
        private void CloseCards()
        {
            bool goOut = false;

            if (pair[0].GetPicID() == pair[1].GetPicID())
            {
                goOut = true;
            }

            pair[0].ShowBackside(goOut);
            pair[1].ShowBackside(goOut);

            tournedCards = 0;

            if (!goOut)
            {
                // dann wird der Spieler gewechelt
                ChangePlayer();
            }
            else
            {
                // hat der Computer ein Paar gefunden? Dann ist er nocheinmal an der Reihe
                if (player == 1)
                {
                    ComputerGame();
                }
            }
        }


        // die Methode fuer den Timer
        private void TimerTick(object sender, EventArgs e)
        {
            // den Timer anhalten
            timer.Stop();
            // die Karte zurueckdrehen
            CloseCards();
        }

        // die Methode wehselt den Spieler
        private void ChangePlayer()
        {
            // wenn der Mensch an der Reihe war, kommt jetzt der Computer
            if (player == 0)
            {
                player = 1;
                ComputerGame();
            }
            else
            {
                player = 0;
            }
        }


        // die Methode setzt die Computerzuege um
        private void ComputerGame()
        {
            int cardCounter = 0;
            int rnd = 0;
            bool strike = false;
            Random rndNum = new Random();

            // erst einmal nach einem Paar suchen, dazu durchsuchen wir das Array rememberdCards, bis wir
            // in beiden Dimensionen einen Wert fuer eine Karte finden
            if (rndNum.Next(difficulty) == 0)
            {
                while ((cardCounter < 21) && (!strike))
                {
                    // gibt es in beiden Dimensonen einen Wert >= 0?
                    if ((rememberCards[0, cardCounter] >= 0) && (rememberCards[1, cardCounter] >= 0))
                    {
                        // Paar gefunden
                        strike = true;
                        // die erste Karte umdrehen durch simulierten klick
                        cards[rememberCards[0, cardCounter]].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        cards[rememberCards[0, cardCounter]].ShowFrontSide();
                        OpenCard(cards[rememberCards[0, cardCounter]]);
                        // zweite Karte auch
                        cards[rememberCards[1, cardCounter]].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        cards[rememberCards[1, cardCounter]].ShowFrontSide();
                        OpenCard(cards[rememberCards[1, cardCounter]]);
                    }
                    cardCounter++;
                }
            }

            // wenn wir kein Paar gefunden haben, drehen wir zufaellig 2 Karten um
            if (!strike)
            {
                // so lange eine Zufallszahl generieren bis eine Karte gefunden inGame == true
                do
                {
                    rnd = rndNum.Next(42);
                } while (!cards[rnd].GetInGame());
                // die erste Karte umdrehen
                cards[rnd].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                cards[rnd].ShowFrontSide();
                OpenCard(cards[rnd]);

                // fuer die zweite Karte muessen wir ausserdem pruefen, ob sie nicht gerade gezeigt wird
                do
                {
                    rnd = rndNum.Next(42);
                } while ((cards[rnd].GetIsTourned()) || (!cards[rnd].GetInGame()));
                // und die zweite Karte umdrehen
                cards[rnd].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                cards[rnd].ShowFrontSide();
                OpenCard(cards[rnd]);
            }

        }


        /// <summary>
        /// Die Methode leifert, ob Zuege des Menschen erlaubt sind.
        /// </summary>
        /// <returns>False wenn gerade der Computer zieht oder wenn schon zwei Karten aufgedeckt sind.</returns>
        public bool PickAllowed()
        {
            bool allowed = true;
            // zieht der Computer?
            if (player == 1)
            {
                allowed = false;
            }
            // sind schon 2 Karten umgedreht?
            if (tournedCards == 2)
            {
                allowed = false;
            }
            return allowed;
        }

        // das Spiel ist beendet
        private void EndofGame()
        {
            MessageBox.Show("Das Spiel ist vorbei.");
            timer.Stop();
            Application.Current.Shutdown();
        }
    }
}
