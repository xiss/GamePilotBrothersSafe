using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;


namespace GamePilotBrothersSafe
{
    public partial class MainWindow : Window
    {
        private Game _game;
        private int _sizeOfSafe;
        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            // Create game
            _sizeOfSafe = (int)SizeOfSafeSlider.Value;
            _game = new Game(_sizeOfSafe);
            _game.IsVictory += SetVictoryStatus;
            _game.PrepareGame();

            // Clear field
            GameField.Children.Clear();
            GameField.RowDefinitions.Clear();
            GameField.ColumnDefinitions.Clear();
            SetNewGameStatus();

            // Create new field
            for (int i = 0; i < _sizeOfSafe; i++)
            {
                GameField.RowDefinitions.Add(new RowDefinition());
                GameField.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Fill field with switchers
            for (int row = 0; row < _sizeOfSafe; row++)
            {
                for (int col = 0; col < _sizeOfSafe; col++)
                {
                    Binding binding = new()
                    {
                        Source = _game[row, col],
                        Path = new PropertyPath(nameof(Switcher.IsChecked)),
                        Mode = BindingMode.OneWay
                    };

                    ToggleButton button = new ToggleButton();
                    button.Style = (Style)FindResource("OnOffToggleButtonStyle");
                    button.Click += Button_Click;
                    button.SetBinding(ToggleButton.IsCheckedProperty, binding);
                    button.SetValue(Grid.RowProperty, row);
                    button.SetValue(Grid.ColumnProperty, col);

                    GameField.Children.Add(button);
                }
            }
        }

        private void StartNewGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);
            _game.Update(row, col);
        }

        private void SetVictoryStatus()
        {
            GameField.IsEnabled = false;
            VictoryStatusLabel.Visibility = Visibility.Visible;
        }
        private void SetNewGameStatus()
        {
            GameField.IsEnabled = true;
            VictoryStatusLabel.Visibility = Visibility.Hidden;
        }
    }
}
