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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Memory
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Fields


        public MainWindow()
        {
            InitializeComponent();

            // Instanz zum Spielfeld mit uebergabe des UniformGrids mit dem Bezeichner "playground"
            new MemoryPlayground(playground);
        }

        // Wenn der User entschiedet noch einmal zu spielen, wird diese Methode ausgefuehrt.
        public void NewGame()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MemoryPlayground.SaveSettings();
        }
    }
}
