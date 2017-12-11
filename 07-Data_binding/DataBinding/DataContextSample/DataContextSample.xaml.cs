using System.Windows;
using System.Windows.Controls;

namespace DataBindingSample
{
    /// <summary>
    ///     Interaction logic for DataContextSample.xaml
    /// </summary>
    public partial class DataContextSample : Window
    {
        public DataContextSample()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        private void btnUpdateSource_Click(object sender, RoutedEventArgs e)
        {
            var binding = this.txtWindowTitle.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }
    }
}