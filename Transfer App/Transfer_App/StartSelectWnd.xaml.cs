namespace Transfer_App
{
    using System;
    using System.Linq;
    using System.Windows;
    using Transfer_App.Models.EF;

    public partial class StartSelectWnd : Window
    {
        readonly DataContext db;

        public StartSelectWnd()
        {
            InitializeComponent();
            db = new DataContext();
            destination.ItemsSource = db.Destinations.Select(n => n.Name).ToList();
        }

        private void ok_btn_Click(object sender, RoutedEventArgs e)
        {
            if (destination.Text != "" && DateTime.TryParse(date.Text, out DateTime d))
            {
                MainWindow.SelectDest = destination.Text;
                MainWindow.SelectDate = d;
                this.Close();
            }
            else
            {
                MessageBox.Show("Потрібно вибрати пункт призначення, та дату рейсу...", "..Not Selected...",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
