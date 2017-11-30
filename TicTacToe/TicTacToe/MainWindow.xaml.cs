
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

        }
    }
}
