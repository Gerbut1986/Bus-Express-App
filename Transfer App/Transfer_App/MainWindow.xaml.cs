namespace Transfer_App
{
    using Models;
    using System;
    using Models.EF;
    using System.Linq;
    using System.Windows;
    using Microsoft.Win32;
    using Transfer_App.Windows;
    using System.Windows.Media;
    using System.Windows.Controls;
    using Transfer_App.Models.ADO;
    using System.Collections.Generic;

    public partial class MainWindow : Window
    {
        #region Fields:
        PassInfo selPassInfo;
        OrderInfo selOrderInfo;
        readonly DataContext db;
        static bool notSelected = true;
        const string _Title = "BUSS EXPRESS";
        public static string selectedPlace = null;
        readonly string pathPassangers, pathOrders;
        List<string> SchemaPlaceProps { get; set; }
        public static string SelectDest { get; set; }
        public static DateTime SelectDate { get; set; }
        static IEnumerable<PassInfo> PassInfos { get; set; }
        static IEnumerable<SchemaPlace> SchemaPlaces { get; set; }
        public static IEnumerable<OrderInfo> OrderInfos { get; set; }
        #endregion

        public MainWindow()
        {
            var startWnd = new StartSelectWnd();
            startWnd.ShowDialog();
            InitializeComponent(); 
            db = new DataContext();
            // SchemaPlaceProps = GetSchemaPlacesProperties();
            PassengersGrid.Background = OrdersGrid.Background = Background 
                      = new SolidColorBrush(Color.FromArgb(50, 100, 150, 100));
            FillPathes(out pathPassangers, out pathOrders);
            try
            {
                RefreshGrid(false);
            }
            catch (Exception ex) { Title = ex.Message; MsgBoxBLog("An Error occured...", ex.Message); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshGrid(false);
            }
            catch (Exception ex) { Title = ex.Message; MsgBoxBLog("An Error occured...", ex.Message); }
            BusName_lbl.Content = GetSchemaNames()[0];
        }

        #region Schedule Places Button's event:
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Button).Content;
            var add = new AddEditOrderWnd("Add", int.Parse(select.ToString()), db);
            if (add.Title == "Exists")
            {
                Title = $"{select} місце зайняте!";
                return;
            }
            selectedPlace = select.ToString();
            add.Owner = this;
            add.ShowDialog();
        }
        #endregion

        #region Selected records from DataGrids [both events]:
        private void PassengersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Title = _Title;
            selPassInfo = PassengersGrid.SelectedItem as PassInfo;
            if (selPassInfo == null)
                notSelected = true;
            else notSelected = false;
        }

        private void OrdersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Title = _Title;
            selOrderInfo = OrdersGrid.SelectedItem as OrderInfo;
            if (selOrderInfo == null)
                notSelected = true;
            else notSelected = false;
        }
        #endregion       

        #region Passengers Buttons:
        private void AddPass_Btn_Click(object sender, RoutedEventArgs e)
        {
            var add = new AddEditWnd("Add", db);
            add.Owner = this;
            add.ShowDialog();
        }

        private void DeletePass_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (notSelected)
            {
                Title = "Спочатку виберіть запис зі списку!";
                MessageBox.Show(Title, "Not selected...", MessageBoxButton.OK,
                               MessageBoxImage.Warning);
                return;
            }
            else
            {
                Title = new ServicePassInfos().Delete(selPassInfo.Id);
                RefreshGrid(true);
            }
        }

        private void EditPass_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (notSelected)
            {
                Title = "Спочатку виберіть запис зі списку!";
                MessageBox.Show(Title, "Not selected...", MessageBoxButton.OK,
                      MessageBoxImage.Warning);

                return;
            }
            else
            {
                var editForm = new AddEditWnd("Edit", db, selPassInfo);
                editForm.Owner = this;
                editForm.Show();
            }
        }

        private void ExportPass_Btn_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                var dataToExport = PassInfos.ToList();
                ExcelExporter<PassInfo>.ExportDataToExcel(dataToExport, pathPassangers);
            }
        }
        #endregion

        #region Orders Buttons:
        private void AddOrder_Btn_Click(object sender, RoutedEventArgs e)
        {
            var add = new AddEditOrderWnd("Add", db);
            add.Owner = this;
            add.ShowDialog();
        }

        private void DeleteOrder_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (notSelected)
            {
                Title = "Спочатку виберіть запис зі списку!";
                MessageBox.Show(Title, "Not selected...", MessageBoxButton.OK,
                         MessageBoxImage.Warning);
                return;
            }
            else
            {
                Title = new ServiceOrderInfos().Delete(selOrderInfo.Id);
                new ServiceSchemaPlaces().UpdateOnePlc(selOrderInfo.PlaceNumber, true);
                RefreshGrid(true);
            }
        }

        private void EditOrder_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (notSelected)
            {
                Title = "Спочатку виберіть запис зі списку!";
                MessageBox.Show(Title, "Not selected...", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            else
            {
                var editForm = new AddEditOrderWnd("Edit", db, selOrderInfo);
                editForm.Owner = this;
                editForm.Show();
            }
        }

        private void ExportOrder_Btn_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                var path = sfd.FileName + ".xlsx";
                var dataToExport = OrderInfos.ToList();
                ExcelExporter<OrderInfo>.ExportDataToExcel(dataToExport, path);
            }
        }
        #endregion

        #region Shown the number of the Bus:
        string[] GetSchemaNames()
        {
            var i = 0;
            var arr = new string[SchemaPlaces.Count()];
            foreach (var id in SchemaPlaces)
                arr[i++] = "Схема мест № " + id.Id.ToString();
            return arr;
        }
        #endregion

        #region Checking IsReserved or free:
        private void UpdatePlaces()
        {
            foreach (var order in OrderInfos)
            {
                FreeOrReserved(order.PlaceNumber, null, true);
            }
            SchemaPlaces = new DataContext().SchemaPlaces.ToList();
        }
        #endregion

        #region Initializing buttons as a free or reserved:
        private void FreeOrReserved(int plcNumber, SchemaPlace places, bool isReserved = false)
        {
            switch (plcNumber)
            {
                case 1:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else if (!places.Is1Place)
                    {
                        _1Btn.Background = Brushes.White;
                        _1Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _1Btn.Background = Brushes.Yellow;
                        _1Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 2:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else if (!places.Is2Place)
                    {
                        _2Btn.Background = Brushes.White;
                        _2Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _2Btn.Background = Brushes.Yellow;
                        _2Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 3:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is3Place)
                    {
                        _3Btn.Background = Brushes.White;
                        _3Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _3Btn.Background = Brushes.Yellow;
                        _3Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 4:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is4Place)
                    {
                        _4Btn.Background = Brushes.White;
                        _4Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _4Btn.Background = Brushes.Yellow;
                        _4Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 5:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is5Place)
                    {
                        _5Btn.Background = Brushes.White;
                        _5Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _5Btn.Background = Brushes.Yellow;
                        _5Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 6:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is6Place)
                    {
                        _6Btn.Background = Brushes.White;
                        _6Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _6Btn.Background = Brushes.Yellow;
                        _6Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 7:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is7Place)
                    {
                        _7Btn.Background = Brushes.White;
                        _7Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _7Btn.Background = Brushes.Yellow;
                        _7Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 8:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is8Place)
                    {
                        _8Btn.Background = Brushes.White;
                        _8Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _8Btn.Background = Brushes.Yellow;
                        _8Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 9:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is9Place)
                    {
                        _9Btn.Background = Brushes.White;
                        _9Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _9Btn.Background = Brushes.Yellow;
                        _9Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 10:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is10Place)
                    {
                        _10Btn.Background = Brushes.White;
                        _10Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _10Btn.Background = Brushes.Yellow;
                        _10Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 11:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is11Place)
                    {
                        _11Btn.Background = Brushes.White;
                        _11Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _11Btn.Background = Brushes.Yellow;
                        _11Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 12:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is12Place)
                    {
                        _12Btn.Background = Brushes.White;
                        _12Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _12Btn.Background = Brushes.Yellow;
                        _12Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 13:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is13Place)
                    {
                        _13Btn.Background = Brushes.White;
                        _13Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _13Btn.Background = Brushes.Yellow;
                        _13Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 14:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is14Place)
                    {
                        _14Btn.Background = Brushes.White;
                        _14Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _14Btn.Background = Brushes.Yellow;
                        _14Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 15:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is15Place)
                    {
                        _15Btn.Background = Brushes.White;
                        _15Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _15Btn.Background = Brushes.Yellow;
                        _15Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 16:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is16Place)
                    {
                        _16Btn.Background = Brushes.White;
                        _16Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _16Btn.Background = Brushes.Yellow;
                        _16Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 17:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is17Place)
                    {
                        _17Btn.Background = Brushes.White;
                        _17Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _17Btn.Background = Brushes.Yellow;
                        _17Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 18:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is18Place)
                    {
                        _18Btn.Background = Brushes.White;
                        _18Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _18Btn.Background = Brushes.Yellow;
                        _18Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 19:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is19Place)
                    {
                        _19Btn.Background = Brushes.White;
                        _19Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _19Btn.Background = Brushes.Yellow;
                        _19Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 20:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is20Place)
                    {
                        _20Btn.Background = Brushes.White;
                        _20Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _20Btn.Background = Brushes.Yellow;
                        _20Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 21:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is21Place)
                    {
                        _21Btn.Background = Brushes.White;
                        _21Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _21Btn.Background = Brushes.Yellow;
                        _21Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 22:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is22Place)
                    {
                        _22Btn.Background = Brushes.White;
                        _22Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _22Btn.Background = Brushes.Yellow;
                        _22Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 23:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is23Place)
                    {
                        _23Btn.Background = Brushes.White;
                        _23Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _23Btn.Background = Brushes.Yellow;
                        _23Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 24:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is24Place)
                    {
                        _24Btn.Background = Brushes.White;
                        _24Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _24Btn.Background = Brushes.Yellow;
                        _24Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 25:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is25Place)
                    {
                        _25Btn.Background = Brushes.White;
                        _25Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _25Btn.Background = Brushes.Yellow;
                        _25Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 26:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is26Place)
                    {
                        _26Btn.Background = Brushes.White;
                        _26Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _26Btn.Background = Brushes.Yellow;
                        _26Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 27:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is27Place)
                    {
                        _27Btn.Background = Brushes.White;
                        _27Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _27Btn.Background = Brushes.Yellow;
                        _27Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 28:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is28Place)
                    {
                        _28Btn.Background = Brushes.White;
                        _28Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _28Btn.Background = Brushes.Yellow;
                        _28Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 29:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is29Place)
                    {
                        _29Btn.Background = Brushes.White;
                        _29Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _29Btn.Background = Brushes.Yellow;
                        _29Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 30:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is30Place)
                    {
                        _30Btn.Background = Brushes.White;
                        _30Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _30Btn.Background = Brushes.Yellow;
                        _30Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 31:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is31Place)
                    {
                        _31Btn.Background = Brushes.White;
                        _31Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _31Btn.Background = Brushes.Yellow;
                        _31Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 32:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is32Place)
                    {
                        _32Btn.Background = Brushes.White;
                        _32Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _32Btn.Background = Brushes.Yellow;
                        _32Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 33:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is33Place)
                    {
                        _33Btn.Background = Brushes.White;
                        _33Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _33Btn.Background = Brushes.Yellow;
                        _33Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 34:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is34Place)
                    {
                        _34Btn.Background = Brushes.White;
                        _34Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _34Btn.Background = Brushes.Yellow;
                        _34Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 35:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is35Place)
                    {
                        _35Btn.Background = Brushes.White;
                        _35Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _35Btn.Background = Brushes.Yellow;
                        _35Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 36:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is36Place)
                    {
                        _36Btn.Background = Brushes.White;
                        _36Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _36Btn.Background = Brushes.Yellow;
                        _36Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 37:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is37Place)
                    {
                        _37Btn.Background = Brushes.White;
                        _37Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _37Btn.Background = Brushes.Yellow;
                        _37Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 38:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is38Place)
                    {
                        _38Btn.Background = Brushes.White;
                        _38Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _38Btn.Background = Brushes.Yellow;
                        _38Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 39:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is39Place)
                    {
                        _39Btn.Background = Brushes.White;
                        _39Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _39Btn.Background = Brushes.Yellow;
                        _39Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 40:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is40Place)
                    {
                        _40Btn.Background = Brushes.White;
                        _40Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _40Btn.Background = Brushes.Yellow;
                        _40Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 41:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is41Place)
                    {
                        _41Btn.Background = Brushes.White;
                        _41Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _41Btn.Background = Brushes.Yellow;
                        _41Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 42:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is42Place)
                    {
                        _42Btn.Background = Brushes.White;
                        _42Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _42Btn.Background = Brushes.Yellow;
                        _42Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 43:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is43Place)
                    {
                        _43Btn.Background = Brushes.White;
                        _43Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _43Btn.Background = Brushes.Yellow;
                        _43Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 44:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is44Place)
                    {
                        _44Btn.Background = Brushes.White;
                        _44Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _44Btn.Background = Brushes.Yellow;
                        _44Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 45:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is45Place)
                    {
                        _45Btn.Background = Brushes.White;
                        _45Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _45Btn.Background = Brushes.Yellow;
                        _45Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 46:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is46Place)
                    {
                        _46Btn.Background = Brushes.White;
                        _46Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _46Btn.Background = Brushes.Yellow;
                        _46Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 47:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is47Place)
                    {
                        _47Btn.Background = Brushes.White;
                        _47Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _47Btn.Background = Brushes.Yellow;
                        _47Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 48:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is48Place)
                    {
                        _48Btn.Background = Brushes.White;
                        _48Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _48Btn.Background = Brushes.Yellow;
                        _48Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 49:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is49Place)
                    {
                        _49Btn.Background = Brushes.White;
                        _49Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _49Btn.Background = Brushes.Yellow;
                        _49Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 50:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is50Place)
                    {
                        _50Btn.Background = Brushes.White;
                        _50Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _50Btn.Background = Brushes.Yellow;
                        _50Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 51:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is51Place)
                    {
                        _51Btn.Background = Brushes.White;
                        _51Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _51Btn.Background = Brushes.Yellow;
                        _51Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 52:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is52Place)
                    {
                        _52Btn.Background = Brushes.White;
                        _52Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _52Btn.Background = Brushes.Yellow;
                        _52Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 53:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is53Place)
                    {
                        _53Btn.Background = Brushes.White;
                        _53Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _53Btn.Background = Brushes.Yellow;
                        _53Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 54:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is54Place)
                    {
                        _54Btn.Background = Brushes.White;
                        _54Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _54Btn.Background = Brushes.Yellow;
                        _54Btn.Foreground = Brushes.Red;
                    }
                    break;
                case 55:
                    if (isReserved)
                    {
                        new ServiceSchemaPlaces().UpdateOnePlc(plcNumber);
                    }
                    else
                    if (!places.Is55Place)
                    {
                        _55Btn.Background = Brushes.White;
                        _55Btn.Foreground = Brushes.Green;
                    }
                    else
                    {
                        _55Btn.Background = Brushes.Yellow;
                        _55Btn.Foreground = Brushes.Red;
                    }
                    break;
                default: break;
            }
        }

        private void Refresh_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshGrid(false);
            }
            catch (Exception ex) { Title = ex.Message; MsgBoxBLog(ex.Message, "Error occured..."); }
        }
        #endregion

        #region Refresh (call to database) to Update each DataGrid table:
        public void RefreshGrid(bool isAction)
        {
            OrdersGrid.Items.Refresh();
            PassengersGrid.Items.Refresh();
            PassengersGrid.ItemsSource = isAction ? new DataContext().PassInfos.ToList() : db.PassInfos.ToList();
            OrdersGrid.ItemsSource = isAction ? new DataContext().OrderInfos.ToList() : db.OrderInfos.ToList();
            PassInfos = db.PassInfos.ToList();
            PassengersGrid.ItemsSource = PassInfos = SelectDate != DateTime.MinValue && SelectDest != null ?
                PassInfos.Where(dd => dd.Booking_Date == SelectDate)
                .Where(d => d.Booking_Route == SelectDest).ToList() : PassInfos;
            OrderInfos = db.OrderInfos.ToList();
            SchemaPlaces = isAction ? new DataContext().SchemaPlaces.ToList() : db.SchemaPlaces.ToList();
            UpdatePlaces();
            for (var i = 1; i <= 55; i++)
            {
                FreeOrReserved(i, SchemaPlaces.ToArray()[0]);
            }
        }
        #endregion

        #region Auxiliary method:
        private void MsgBoxBLog(string msg, string titleMsg = null)
        {
            MessageBox.Show(titleMsg, msg, MessageBoxButton.OK, MessageBoxImage.Warning);// == MessageBoxResult.OK ? Close() : MsgBoxBLog("","");
        }

        /// <summary>
        /// Method to get all attributes name SchedulePlace:  [Not usage still..]
        /// </summary>
        /// <returns> List of names </returns>
        private List<string> GetSchemaPlacesProperties()
        {
            var lstProps = new SchemaPlace().GetType().GetProperties().Select(n => n.Name).ToList();
            for (var i = 0; i < 3; i++) // Delete first 3 elements(attributes)
                SchemaPlaceProps.RemoveAt(0);
            return lstProps;
        }

        private void FillPathes(out string path1, out string path2)
        {
            path2 = AppDomain.CurrentDomain.BaseDirectory + "Orders Info.xlsx";
            path1 = AppDomain.CurrentDomain.BaseDirectory + "Passengers Info.xlsx";
        }

        private void FilterDestDate()
        {
            if(SelectDate != DateTime.MinValue && 
               SelectDest != null)
            {

            }
        }
        #endregion
    }
}
