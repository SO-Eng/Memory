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
    /// Interaktionslogik für SettingsMem.xaml
    /// </summary>
    public partial class SettingsMem : Window
    {
        // Fields
        int difficulty;
        int hard = 20;
        int middle = 40;
        int soft = 100;


        public SettingsMem()
        {
            InitializeComponent();

            // Slider der aktuellen Schwiereigkeitseinstellung anpassen
            if (MemoryPlayground.Difficulty == 0)
            {
                sliderDifficulty.Value = 1;
            }
            else
            {
                difficulty = MemoryPlayground.Difficulty;
                if (difficulty == soft)
                {
                    sliderDifficulty.Value = 1;
                }
                else if (difficulty == middle)
                {
                    sliderDifficulty.Value = 2;
                }
                else
                {
                    sliderDifficulty.Value = 3;
                }
            }

            // Schummeloption der aktuellen Einstellung anpassen
            if (MemoryPlayground.CheatButton)
            {
                rbCheatOn.IsChecked = true;
            }
            else
            {
                rbCheatOff.IsChecked = true;
            }

            // Soundoptionen den aktuellen Einstellungen anpassen
            if (MemoryPlayground.SoundSettings)
            {
                rbSoundOn.IsChecked = true;
            }
            else
            {
                rbSoundOff.IsChecked = true;
            }
        }


        // Methode fuer das schliessen des Fensters
        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            SliderValue();
            Close();
        }


        // Methode fuer das Schwieriegkeitsgrad setzen
        private void SliderValue()
        {
            if (sliderDifficulty.Value == 1)
            {
                MemoryPlayground.Difficulty = soft;
            }
            else if (sliderDifficulty.Value == 2)
            {
                MemoryPlayground.Difficulty = middle;
            }
            else
            {
                MemoryPlayground.Difficulty = hard;
            }
        }


        // Buttins fuer die Kartenrueckenauswahl
        private void ButtonCc1_Click(object sender, RoutedEventArgs e)
        {
            MemoryCard.SetCoverCard(0);
            MemoryPlayground.TurnCardsBack();
        }

        private void ButtonCc2_Click(object sender, RoutedEventArgs e)
        {
            MemoryCard.SetCoverCard(1);
            MemoryPlayground.TurnCardsBack();
        }

        private void ButtonCc3_Click(object sender, RoutedEventArgs e)
        {
            MemoryCard.SetCoverCard(2);
            MemoryPlayground.TurnCardsBack();
        }

        private void ButtonCc4_Click(object sender, RoutedEventArgs e)
        {
            MemoryCard.SetCoverCard(3);
            MemoryPlayground.TurnCardsBack();
        }

        private void ButtonCc5_Click(object sender, RoutedEventArgs e)
        {
            MemoryCard.SetCoverCard(4);
            MemoryPlayground.TurnCardsBack();
        }

        private void ButtonCc6_Click(object sender, RoutedEventArgs e)
        {
            MemoryCard.SetCoverCard(5);
            MemoryPlayground.TurnCardsBack();
        }


        // RadioButtons fuer die Schummeleinstellung
        private void RbCheatOff_Click(object sender, RoutedEventArgs e)
        {
            MemoryPlayground.CheatButton = false;
            MemoryPlayground.SetCheatButton();
        }

        private void RbCheatOn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Du konntest wohl als Kind schon nicht verlieren?", "Schummeln!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            MemoryPlayground.CheatButton = true;
            MemoryPlayground.SetCheatButton();
        }


        // RadioButtons fuer die Soundeinstellung
        private void RbSoundOn_Click(object sender, RoutedEventArgs e)
        {
            MemoryPlayground.SoundSettings = true;
        }

        private void RbSoundOff_Click(object sender, RoutedEventArgs e)
        {
            MemoryPlayground.SoundSettings = false;
        }
    }
}
