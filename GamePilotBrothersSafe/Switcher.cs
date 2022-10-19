using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GamePilotBrothersSafe
{
    internal class Switcher : INotifyPropertyChanged
    {
        private bool isChecked;
        public Switcher( int verticalPosition, int horizontalPosition)
        {
            VerticalPosition = verticalPosition;
            HorizontalPosition = horizontalPosition;
        }

        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }
        public int VerticalPosition { get; set; }
        public int HorizontalPosition { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
