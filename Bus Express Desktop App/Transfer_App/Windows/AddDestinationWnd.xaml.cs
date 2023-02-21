namespace Transfer_App.Windows
{
    using System;
    using System.Windows;
    using Transfer_App.Models;
    using Transfer_App.Models.EF;

    public partial class AddDestinationWnd : Window
    {
        string mode;
        Destination ds;
        readonly DataContext db;

        public AddDestinationWnd(string mode, DataContext db)
        {
            InitializeComponent();
            this.db = db;
            this.mode = mode;
            InitializeComponent();
            this.Title = "Add Form";
            this.Add_Btn.Content = "Добавити";
            this.group.Header = "Добавити нове замовлення";
        }

        public AddDestinationWnd(string mode, Destination ds, DataContext db)
        {
            this.db = db;
            this.mode = mode;
            InitializeComponent();
            Title = "Update Form";
            Add_Btn.Content = "Оновити";
            this.ds = ds;
            group.Header = "Оновити вибране замовлення";
            try
            {
                FillTxtFilds(ds);
            }
            catch (Exception ex) { Title = ex.Message; }
        }

        private void Add_Btn_Click(object sender, RoutedEventArgs e)
        {
            var exec = Action();
            MessageBox.Show(exec[0], exec[1], MessageBoxButton.OK, MessageBoxImage.Exclamation);
            if (mode == "Add") ClearTxt();
            else
                try
                {
                    (this.Owner as MainWindow).RefreshGrid(true);
                }
                catch { }
                finally { this.Close(); }
        }

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.Owner as MainWindow).RefreshGrid(true);
            }
            catch { }
            finally { this.Close(); }
        }

        private string[] Action()
        {
            var retType = new string[2];
            if (from_txt.Text == null && to_txt.Text == null)
            {
                retType[0] = "Ви залишили всі поля пустими!";
                retType[1] = "Всі поля пусті..";
                return retType;
            }
            else if (from_txt.Text == null || to_txt.Text == null)
            {
                retType[0] = "Не всі поля заповнені!";
                retType[1] = "Деякі поля пусті..";
                return retType;
            }
            else
            {
                ds = FillObject();
                switch (mode)
                {
                    case "Add":
                        retType[0] = new Models.ADO.ServiceDestinations().Create(ds);
                        retType[1] = "Add Result";
                        return retType;
                    case "Edit":
                        retType[0] = new Models.ADO.ServiceDestinations().Update(ds);
                        retType[1] = "Update Result";
                        return retType;
                    default:
                        return new string[] { "[NoN Actions...] => default statement." };
                }
            }
        }

        private void FillTxtFilds(Destination oi)
        {
            var slt = oi.Name.Split(new char[] { '-' });
            from_txt.Text = slt[0];
            to_txt.Text = slt[1];
        }

        private Destination FillObject()
        {
            ds = new Destination();
            ds.Name = from_txt.Text + " - " + to_txt.Text;
            return ds;
        }

        void ClearTxt()
        {
            from_txt.Text = to_txt.Text = "";
            (this.Owner as MainWindow).RefreshGrid(true);
        }
    }
}
