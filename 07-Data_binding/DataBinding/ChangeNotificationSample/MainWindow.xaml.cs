using System.Collections.ObjectModel;
using System.Windows;

namespace ChangeNotificationSample
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<User> _users = new ObservableCollection<User>();

        public MainWindow()
        {
            this.InitializeComponent();
            this._users.Add(new User {Name = "John Doe"});
            this._users.Add(new User {Name = "Jane Doe"});
            this.lbUsers.ItemsSource = this._users;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            this._users.Add(new User
            {
                Name = "New user"
            });
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (this.lbUsers.SelectedItem != null)
                ((User) this.lbUsers.SelectedItem).Name = "Random Name";
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (this.lbUsers.SelectedItem != null)
                this._users.Remove(this.lbUsers.SelectedItem as User);
        }
    }
}