namespace Transfer_App
{
    using System;
    using System.Linq;
    using System.Windows;
    using Transfer_App.Models.EF;

    public partial class StartSelectWnd : Window
    {
        readonly DataContext _db;
        private bool _isFirstCall;

        public StartSelectWnd(bool isFirstCall = true)
        {
            InitializeComponent(); 
            _db = new DataContext();
            _isFirstCall = isFirstCall;
            destination.ItemsSource = _db.Destinations.Select(n => n.Name).ToList();
        }

        private void ok_btn_Click(object sender, RoutedEventArgs e)
        {
            if (destination.Text != "" && DateTime.TryParse(date.Text, out DateTime d))
            {
                if (_isFirstCall)
                {
                    MainWindow.SelectDest = destination.Text;
                    MainWindow.SelectDate = d;
                    this.Close();
                }
                else
                    try
                    {
                        MainWindow.SelectDate = d;
                        MainWindow.SelectDest = destination.Text;
                        (this.Owner as MainWindow).RefreshGrid(true);
                    }
                    catch { }
                    finally { this.Close(); }
            }
            else
            {
                MessageBox.Show("Потрібно вибрати пункт призначення, та дату рейсу...", "..Not Selected...",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
