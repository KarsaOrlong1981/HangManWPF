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

namespace HangManWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game;
        
        public MainWindow()
        {
            InitializeComponent();

         
            

        }

        private void btn_Hard_Click(object sender, RoutedEventArgs e)
        {
            string mode = "Hard";
            game = new Game(canvas,mode);

            game.SetWord(secretWord,grid);
            btn_Hard.Visibility = Visibility.Collapsed;
            btn_Normal.Visibility = Visibility.Collapsed;
          
            
        }

        private void btn_Normal_Click(object sender, RoutedEventArgs e)
        {
            string mode = "Normal";
            game = new Game(canvas, mode);

            game.SetWord(secretWord,grid);
            btn_Hard.Visibility = Visibility.Collapsed;
            btn_Normal.Visibility = Visibility.Collapsed;
           
           
        }
    }
}
