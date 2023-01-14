namespace Transfer_App.Windows
{
    using System;
    using Models;
    using Models.EF;
    using System.Windows;

    public partial class AddEditWnd : Window
    {
        readonly DataContext db;
        string mode;
        PassInfo pi, upd;
        MainWindow owner;

        public AddEditWnd(string mode, DataContext db)
        {
            InitializeComponent();
            Title = "Add Form";
            gb.Header = "Добавити нового пасажира";
            Add_Btn.Content = "Добавити";
            this.mode = mode;
            this.db = db;
        }

        public AddEditWnd(string mode, DataContext db, PassInfo pi)
        {
            InitializeComponent();
            Title = "Update Form";
            gb.Header = "Оновити нового пасажира";
            Add_Btn.Content = "Оновити";
            this.mode = mode;
            this.pi = upd = pi;
            this.db = db;
            try
            {
                bDate_txt.Text = pi.Booking_Date.ToString();
                bRoute_txt.Text = pi.Booking_Route;
                qty_txt.Text = pi.Qty.ToString();
                tax_txt.Text = pi.Tax;
                total_txt.Text = pi.Total;
                pay_txt.Text = pi.Payment_Method;
                fName_txt.Text = pi.C_FName;
                lName_txt.Text = pi.C_LName;
                phone_txt.Text = pi.C_Phone;
                email_txt.Text = pi.C_Email;
                notes_txt.Text = pi.C_Notes;
            }
            catch(Exception ex) { Title = ex.Message; }
        }

        private void Add_Btn_Click(object sender, RoutedEventArgs e)
        {
            var exec = Action();
            MessageBox.Show(exec[0], exec[1], MessageBoxButton.OK, MessageBoxImage.Exclamation);
            if (mode == "Add") ClearTxt();
            else
            {
                (this.Owner as MainWindow).RefreshGrid(true);
                this.Close();
            }
        }

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            (this.Owner as MainWindow).RefreshGrid(true);
            this.Close();
        }

        private string[] Action()
        {
            var retType = new string[2];
            if (bDate_txt.Text == null && bRoute_txt.Text == null &&
                qty_txt.Text   == null && tax_txt.Text    == null &&
                total_txt.Text == null && pay_txt.Text    == null &&
                fName_txt.Text == null && lName_txt.Text  == null &&
                phone_txt.Text == null && email_txt.Text  == null &&
                notes_txt.Text == null)
            {
                retType[0] = "Ви залишили всі поля пустими!";
                retType[1] = "Всі поля пусті..";
                return retType;
            }
            else if (bDate_txt.Text == null || bRoute_txt.Text == null ||
                     qty_txt.Text   == null || tax_txt.Text    == null ||
                     total_txt.Text == null || pay_txt.Text    == null ||
                     fName_txt.Text == null || lName_txt.Text  == null ||
                     phone_txt.Text == null || email_txt.Text  == null ||
                     notes_txt.Text == null)
            {
                retType[0] = "Не всі поля заповнені!";
                retType[1] = "Деякі поля пусті.."; 
                return retType;
            }
            else
            {
                pi = new PassInfo();
                pi.Booking_Date = DateTime.Parse(bDate_txt.Text);
                pi.Booking_Route = bRoute_txt.Text;
                pi.Qty = int.Parse(qty_txt.Text);
                pi.Tax = tax_txt.Text;
                pi.Total = total_txt.Text;
                pi.Payment_Method = pay_txt.Text;
                pi.C_FName = fName_txt.Text;
                pi.C_LName = lName_txt.Text;
                pi.C_Phone = phone_txt.Text;
                pi.C_Email = email_txt.Text;
                pi.C_Notes = notes_txt.Text;
                switch (mode)
                {
                    case "Add":
                        retType[0] = new Models.ADO.ServicePassInfos().Create(pi);
                        retType[1] = "Add Result";
                        return retType;
                    case "Edit":
                        pi.Id = upd.Id;
                        retType[0] = new Models.ADO.ServicePassInfos().Update(pi);
                        retType[1] = "Update Result";
                        return retType;
                    default:
                        return new string[] { "default statement." };
                }
            }           
        }

        void ClearTxt()
        {
            bDate_txt.Text =
            bRoute_txt.Text =
            qty_txt.Text =
            tax_txt.Text =
            total_txt.Text =
            pay_txt.Text =
            fName_txt.Text =
            lName_txt.Text =
            phone_txt.Text =
            email_txt.Text =
            notes_txt.Text = "";
            (this.Owner as MainWindow).RefreshGrid(true);
        }
    }
}
