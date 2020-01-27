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
        int picCoverCard;

        int difficulty;
        int hard = 20;
        int middle = 40;
        int soft = 100;


        public SettingsMem()
        {
            InitializeComponent();

            if (MemoryPlayground.GetDefficulty() == 0)
            {
                sliderDifficulty.Value = 1;
            }
            else
            {
                difficulty = MemoryPlayground.GetDefficulty();
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
        }


        private void SliderValue()
        {
            if (sliderDifficulty.Value == 1)
            {
                MemoryPlayground.SetDifficulty(soft);
            }
            else if (sliderDifficulty.Value == 2)
            {
                MemoryPlayground.SetDifficulty(middle);
            }
            else
            {
                MemoryPlayground.SetDifficulty(hard);
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            SliderValue();
            Close();
        }

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
    }
}
