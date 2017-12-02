
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private members
        private MarkType[] results;
        private bool player1Turn;
        private bool gameEnded;
        #endregion
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }


        #endregion
        private void NewGame()
        {
            results = new MarkType[9];

            for (int i = 0; i < results.Length; i++)
            {
                results[i] = MarkType.Free;
            }

            player1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            gameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            // Find button index
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + row * 3;

            // Can't overwrite taken cells
            if (results[index] != MarkType.Free)
            {
                return;
            }

            // Update button content
            results[index] = player1Turn ? MarkType.Cross : MarkType.Nought;
            button.Content = player1Turn ? "X" : "O";

            // Change turns on click
            if (player1Turn)
            {
                player1Turn = false;
            }
            else
            {
                player1Turn = true;
            }

            if (player1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            // Row 0
            if (results[0] != MarkType.Free && (results[0] & results[1] & results[2]) == results[0])
            {
                gameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
                return;
            }
            // Row 1
            if (results[3] != MarkType.Free && (results[3] & results[4] & results[5]) == results[3])
            {
                gameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
                return;
            }
            // Row 2
            if (results[6] != MarkType.Free && (results[6] & results[7] & results[8]) == results[6])
            {
                gameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
                return;
            }

            // Column 0
            if (results[0] != MarkType.Free && (results[0] & results[3] & results[6]) == results[0])
            {
                gameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
                return;
            }
            // Column 1
            if (results[1] != MarkType.Free && (results[1] & results[4] & results[7]) == results[1])
            {
                gameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
                return;
            }
            // Column 2
            if (results[2] != MarkType.Free && (results[2] & results[5] & results[8]) == results[2])
            {
                gameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
                return;
            }
            // Diagonal LR
            if (results[0] != MarkType.Free && (results[0] & results[4] & results[8]) == results[0])
            {
                gameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
                return;
            }
            // Diagonal RL
            if (results[2] != MarkType.Free && (results[2] & results[4] & results[6]) == results[2])
            {
                gameEnded = true;
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
                return;
            }

            //Check for full board and no winner
            if (!results.Any(cell => cell == MarkType.Free))
            {
                gameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
        }
    }
}
