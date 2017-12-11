using System.ComponentModel;

namespace ChangeNotificationSample
{
    public class User : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get => this._name;
            set
            {
                if (this._name == value) return;
                this._name = value;
                this.NotifyPropertyChanged(nameof(this.Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}