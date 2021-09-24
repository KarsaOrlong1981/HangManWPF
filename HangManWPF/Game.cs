using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HangManWPF
{
    public class Game
    {
        Hangman hangman;
        Ellipse head;
        Line legLeft;
        Line legRigth;
        Line armLeft;
        Line armRigth;
        Line body;
        Line rope;
        Line barTop;
        Line barHold;
        Line barLeft;
        Line barBottom;
         
        Label gameKoordinater;
        Random word;
        TextBox tbInstance;
        Canvas remove;
        UniformGrid gridUniform;
        int counter;
        int charCounter;
        string modus;
        bool fail;
        bool normalMode;
        string[] words = {    "hallo", "schneekugel",
                              "zahn", "hilfe",
                              "buchstabe", "papier",
                              "maus","katze",
                              "igel","kaenguruh",
                              "sessel","marvin",
                              "lio","mama","papa",
                              "kabelklemme","tabakpfeife",
                              "programm","stuhl","bein","arm","dino","lego","auto","hase"};
        string mysteryWord;
        string saveChar;

        System.Windows.Threading.DispatcherTimer timer;
        
      
        public Game(Canvas canvas,string mode)
        {
            modus = mode;
            normalMode = true;
            gridUniform = new UniformGrid();
            hangman = new Hangman();
            counter = 0;
            mysteryWord = RandomWord();
            remove = canvas;
            legLeft = new Line();
            legLeft = hangman.LegLeft();
            legRigth = new Line();
            legRigth = hangman.LegRigth();
            armLeft = new Line();
            armLeft = hangman.ArmLeft();
            armRigth = new Line();
            armRigth = hangman.ArmRigth();
            body = new Line();
            body = hangman.Body();
            rope = new Line();
            rope = hangman.Rope();
            barHold = new Line();
            barHold = hangman.BarHold();
            barTop = new Line();
            barTop = hangman.BarTop();
            barLeft = new Line();
            barLeft = hangman.BarLeft();
            barBottom = new Line();
            barBottom = hangman.BarBottom();
            tbInstance = new TextBox();
            hangman = new Hangman();
            head = new Ellipse();
            head = hangman.Head();
            Canvas.SetLeft(head, 200);
            Canvas.SetTop(head, 200);
            gameKoordinater = new Label { Width = 150, FontSize = 20.0, Height = 35 ,Background = Brushes.Black};
            Canvas.SetLeft(gameKoordinater, 100);
            Canvas.SetTop(gameKoordinater, 100);
            canvas.Children.Add(gameKoordinater);
            canvas.Children.Add(head);
            canvas.Children.Add(body);
            canvas.Children.Add(armLeft);
            canvas.Children.Add(armRigth);
            canvas.Children.Add(legRigth);
            canvas.Children.Add(legLeft);
            canvas.Children.Add(rope);
            canvas.Children.Add(barTop);
            canvas.Children.Add(barLeft);
            canvas.Children.Add(barBottom);
            canvas.Children.Add(barHold);
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(Timer_Tick);
        }

        public string RandomWord()
        {
            string randomWord = string.Empty;
            int rndNumber = 0;
            word = new Random();
            rndNumber = word.Next(25);
            randomWord = words[rndNumber];
            
            return randomWord;
        }

        public void SetWord(UniformGrid uniformGrid,Grid grid)
        {
            gridUniform = uniformGrid;
            for (int i = 0; i < mysteryWord.Length; i++)
            {

                TextBox tb = new TextBox { FontSize = 20.0,
                    Background = Brushes.Black,
                    Foreground = Brushes.White,
                    Name = "let" + (i + 1),
                    MaxLength = 1,
                    Tag = i,
                    TextAlignment = System.Windows.TextAlignment.Justify,
                     CharacterCasing = CharacterCasing.Lower };
                    
                tbInstance = tb;
                if(modus != "Normal")
                {
                    tb.TextChanged += new TextChangedEventHandler(TextChangedHardMode);
                    normalMode = false;
                }
                else
                {
                    tb.IsReadOnly = true;
                }
                
                 uniformGrid.Children.Add(tb);
            }
            if(normalMode == true)
            {
                //Normal Modus Aktivieren.........
                Label searchChar = new Label
                {
                    Width = 100,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(20,150,20,0),
                    Content = "Search Char:",
                    Foreground = Brushes.White
                };
                grid.Children.Add(searchChar);
                TextBox tbNewChar = new TextBox
                {
                    Width = 20,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(20, 150, 150, 0),
                    FontSize = 20.0,
                    Focusable = true,
                    MaxLength = 1,
                    CharacterCasing = CharacterCasing.Lower
                    
                };
                tbNewChar.TextChanged += new TextChangedEventHandler(TextChangedNormalMode);
                tbInstance = tbNewChar;
                grid.Children.Add(tbNewChar);
                tbNewChar.Focus();

                //Erst das komplette Wort in die einzelnen TBs eintragen und über den Foreground unsichtbar machen
                int count = 0;
                foreach (Control control in gridUniform.Children)
                {
                    var tbBottom = control as TextBox;
                    tbBottom.IsReadOnly = false;
                    tbBottom.Foreground = Brushes.Black;
                    tbBottom.Text = Convert.ToString(mysteryWord[count]);
                    tbBottom.IsReadOnly = true;
                    count++;
                }
            }

        }

        private void TextChangedNormalMode(object sender,TextChangedEventArgs e)
        {
            saveChar = (sender as TextBox).Text;
            bool wrongChar = true;
            //Dann durchsuchen ob der eingetragene Buchstabe in dem Wort vorhanden ist, wenn ja sichtbar machen
            
            foreach(Control control in gridUniform.Children)
            {
                var tbBottom = control as TextBox;
                if (tbBottom.Text == saveChar)
                {
                    tbBottom.Foreground = Brushes.White;
                    tbInstance.TextChanged -= this.TextChangedNormalMode;
                    tbInstance.Text = string.Empty;
                    tbInstance.TextChanged += this.TextChangedNormalMode;
                    wrongChar = false;
                    Hit();
                    timer.Start();
                }
               
                   


            }
            if(wrongChar == true)
            {
                timer.Start();
                DeleteHangman();
                tbInstance.TextChanged -= this.TextChangedNormalMode;
                tbInstance.Text = string.Empty;
                tbInstance.TextChanged += this.TextChangedNormalMode;
            }

            //Prüfung ob alle Buchstaben weiss sind, wenn ja hat der Spieler das Wort eraten und gewonnen
            int count = 0;
            foreach(Control controls in gridUniform.Children)
            {
                var tbBottom = controls as TextBox;
                if(tbBottom.Foreground == Brushes.White)
                {
                    count++;
                }

            }
            if(count == mysteryWord.Length)
            {
                gameKoordinater.Content = "You Win";
                gameKoordinater.Background = Brushes.Green;
                gameKoordinater.Foreground = Brushes.White;
                MessageBox.Show("Sie haben gewonnen.");
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
                
            }
            if (counter >= 11)
            {
                gameKoordinater.Content = "Game Over";
                gameKoordinater.Background = Brushes.Red;
                gameKoordinater.Foreground = Brushes.White;


                tbInstance.TextChanged -= this.TextChangedHardMode;
                MessageBox.Show("Leider nicht geschafft!!!\nDas gesuchte Wort war: " + mysteryWord + ".");
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();

            }
        }
        private void TextChangedHardMode(object sender, TextChangedEventArgs e)
        {
            saveChar = (sender as TextBox).Text;
            if (mysteryWord.IndexOf((sender as TextBox).Text) == -1)
            {
                DeleteHangman();
                tbInstance = (sender as TextBox);

               
                timer.Start();
                tbInstance.TextChanged -= this.TextChangedHardMode;
                tbInstance.Text = string.Empty;
                tbInstance.TextChanged += this.TextChangedHardMode;
            }
            else
            {
                if ((sender as TextBox).Text != string.Empty)
                {
                    fail = false;
                  
                    // Schneekugel
                    if (mysteryWord.Contains(saveChar))
                    {
                        foreach (Control C in gridUniform.Children)
                        {
                            var tb = C as TextBox;
                            int index = mysteryWord.IndexOf(saveChar);
                            int charIndex = Convert.ToInt32((sender as TextBox).Tag);
                           
                           if(mysteryWord[charIndex] == Convert.ToChar(saveChar))
                           {
                                Hit();
                                fail = false;
                                //Wenn charCounter genau so gross ist wie die länge
                                //des gesuchten wortes, hat der Spieler gewonnen
                                charCounter++;
                                break;
                           }
                            else
                            {
                                
                                DeleteHangman();
                                fail = true;
                                break;
                            }

                        }
                        if (fail == true)
                        {
                            tbInstance = (sender as TextBox);
                            tbInstance.TextChanged -= this.TextChangedHardMode;
                            tbInstance.Text = string.Empty;
                            tbInstance.TextChanged += this.TextChangedHardMode;

                            timer.Start();
                        }
                    }

                    
                }
                if (fail == false)
                {
                   
                    timer.Start();
                }



            }
           
               
                
            if(charCounter == mysteryWord.Length)
            {
                gameKoordinater.Content = "You Win";
                gameKoordinater.Background = Brushes.Green;
                gameKoordinater.Foreground = Brushes.White;
                MessageBox.Show("Sie haben gewonnen.");
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
                
            
            if (counter >= 11)
            {
                gameKoordinater.Content = "Game Over";
                gameKoordinater.Background = Brushes.Red;
                gameKoordinater.Foreground = Brushes.White;
                
                
                tbInstance.TextChanged -= this.TextChangedHardMode;
                MessageBox.Show("Leider nicht geschafft!!!\nDas gesuchte Wort war: " + mysteryWord + ".");
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();

            }
        }

        private void Fail()
        {
            
            gameKoordinater.Background = Brushes.Red;
            gameKoordinater.Content = "Fail";
            gameKoordinater.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            gameKoordinater.Foreground = Brushes.White;
        }
        private void Hit()
        {
           
            gameKoordinater.Background = Brushes.Green;
            gameKoordinater.Content = "Hit";
            gameKoordinater.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            gameKoordinater.Foreground = Brushes.White;
        }

        private void DeleteHangman()
        {
            
                switch (counter)
                {
                    case 0:
                        remove.Children.Remove(legLeft);
                        Fail();
                        counter++;
                        break;
                    case 1:
                        remove.Children.Remove(legRigth);
                        Fail();
                        counter++;
                        break;
                    case 2:
                        remove.Children.Remove(armLeft);
                        Fail();
                        counter++;
                        break;
                    case 3:
                        remove.Children.Remove(armRigth);
                        Fail();
                        counter++;
                        break;
                    case 4:
                        remove.Children.Remove(body);
                        Fail();
                        counter++;
                        break;
                    case 5:
                        remove.Children.Remove(head);
                        Fail();
                        counter++;
                        break;
                    case 6:
                        remove.Children.Remove(rope);
                        Fail();
                        counter++;
                        break;
                    case 7:
                        remove.Children.Remove(barHold);
                        Fail();
                        counter++;
                        break;
                    case 8:
                        remove.Children.Remove(barTop);
                        Fail();
                        counter++;
                        break;
                    case 9:
                        remove.Children.Remove(barLeft);
                        Fail();
                        counter++;
                        break;
                    case 10:
                        remove.Children.Remove(barBottom);
                        Fail();
                        counter++;
                        break;
                }
            
        }
        private void Timer_Tick(object sender,EventArgs e)
        {
            timer.Stop();
            gameKoordinater.Background = Brushes.Black;

            gameKoordinater.Foreground = Brushes.Black;
            
           
           
        }

       

      
    }
}
