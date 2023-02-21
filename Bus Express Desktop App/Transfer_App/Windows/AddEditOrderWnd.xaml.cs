namespace Transfer_App.Windows
{
    using System;
    using System.Linq;
    using System.Windows;
    using Transfer_App.Models;
    using Transfer_App.Models.EF;
    using System.Collections.Generic;
    using DocumentFormat.OpenXml.Office2010.Word;

    public partial class AddEditOrderWnd : Window
    {
        string mode;
        int removedPlace;
        OrderInfo oi, upd;
        readonly DataContext db;

        public AddEditOrderWnd(string mode, int plcNum, DataContext db) // .. From SchemaPlaces tab:
        {
            this.db = db;
            this.mode = mode;
            InitializeComponent();
            Title = "Fill the Free Place";
            group.Header = $"Додати {MainWindow.selectedPlace} місце";
            this.Add_Btn.Content = "Добавити";
            plcenum_cmbo.Text = plcNum.ToString();
            plcenum_cmbo.Items.Add(plcNum.ToString());
            var freePls = GetFreePlaces(db.OrderInfos.ToList());
            if (!freePls.Contains(plcNum)) { Title = "Exists"; Close(); }
        }

        public AddEditOrderWnd(string mode, DataContext db)
        {
            this.db = db;
            this.mode = mode;
            InitializeComponent();
            this.Title = "Add Form";
            this.Add_Btn.Content = "Добавити";
            this.group.Header = "Добавити нове замовлення";
            plcenum_cmbo.ItemsSource = GetFreePlaces(db.OrderInfos.ToList());
        }
     
        public AddEditOrderWnd(string mode, DataContext db, OrderInfo oi)
        {
            this.db = db;
            this.mode = mode;
            InitializeComponent();
            Title = "Update Form";
            Add_Btn.Content = "Оновити";
            this.oi = upd = oi; 
            group.Header = "Оновити вибране замовлення";
            plcenum_cmbo.ItemsSource = GetFreePlaces(db.OrderInfos.ToList());
            try
            {
                FillTxtFilds(oi);
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
            if (from_txt.Text  == null && to_txt.Text       == null &&
                pib_txt.Text   == null && plcenum_cmbo.Text == null &&
                phone_txt.Text == null && ordnum_txt.Text   == null &&
                pay_txt.Text   == null)
            {
                retType[0] = "Ви залишили всі поля пустими!";
                retType[1] = "Всі поля пусті..";
                return retType;
            }
            else if (from_txt.Text  == null  || to_txt.Text       == null ||
                     pib_txt.Text   == null  || plcenum_cmbo.Text == null ||
                     phone_txt.Text == null  || ordnum_txt.Text   == null ||
                     pay_txt.Text   == null)
            {
                retType[0] = "Не всі поля заповнені!";
                retType[1] = "Деякі поля пусті..";
                return retType;
            }
            else
            {
                oi = FillObject();
                switch (mode)
                {
                    case "Add":
                        retType[0] = new Models.ADO.ServiceOrderInfos().Create(oi);
                        retType[1] = "Add Result";
                        return retType;
                    case "Edit":
                        oi.Id = upd.Id; 
                        // Remove old place:
                        new Models.ADO.ServiceSchemaPlaces().UpdateOnePlc(removedPlace, true);
                        // Update on new:
                        new Models.ADO.ServiceSchemaPlaces().UpdateOnePlc(int.Parse(plcenum_cmbo.Text));
                        retType[0] = new Models.ADO.ServiceOrderInfos().Update(oi);
                        retType[1] = "Update Result";
                        return retType;
                    default:
                        return new string[] { "[NoN Actions...] => default statement." };
                }
            }
        }

        private List<int> GetFreePlaces(List<OrderInfo> orders)
        {
            bool isAlready = false;
            var list = new List<int>();
            for (var i = 1; i <= 55; i++)
            {
                for (var l = 0; l < orders.Count; l++)
                    if (orders[l].PlaceNumber == i)
                        isAlready = true;
                if (!isAlready) list.Add(i);
                else isAlready = false;
            }
            return list;
        }

        private void FillTxtFilds(OrderInfo oi)
        {
            from_txt.Text = oi.From;
            to_txt.Text = oi.To;
            pib_txt.Text = oi.LName_FName;
            plcenum_cmbo.Text = (removedPlace = oi.PlaceNumber).ToString();
            phone_txt.Text = oi.Phone;
            ordnum_txt.Text = oi.OrderNumber;
            pay_txt.Text = oi.MoneyAmount;
            isOrdered_txt.IsChecked = oi.IsOrdered ? true : false;
        }

        private OrderInfo FillObject()
        {
            oi = new OrderInfo();
            oi.From = from_txt.Text;
            oi.To = to_txt.Text;
            oi.LName_FName = pib_txt.Text;
            oi.PlaceNumber = int.TryParse(plcenum_cmbo.Text, out int pn) ? pn : 0;
            oi.Phone = phone_txt.Text;
            oi.OrderNumber = ordnum_txt.Text;
            oi.MoneyAmount = pay_txt.Text;
            oi.IsOrdered = (bool)isOrdered_txt.IsChecked ? true : false;
            return oi;
        }

        void ClearTxt()
        {
            from_txt.Text =
            to_txt.Text =
            pib_txt.Text =
            plcenum_cmbo.Text =
            phone_txt.Text =
            ordnum_txt.Text =
            pay_txt.Text = "";
            isOrdered_txt.IsChecked = false;
            (this.Owner as MainWindow).RefreshGrid(true);
        }
    }
}
