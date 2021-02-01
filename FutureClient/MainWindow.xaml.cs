using FutureClient.Models;
using FutureClient.ViewModel;
using FutureMQClient.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;


namespace FutureClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel dc;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            dc = new MainViewModel(this);
            
            //FutureMQClient.Models.G.db.DbFirst.Where("UserInfo").CreateClassFile(@"D:\workspace\交易接口\总线\FutureClient\Models", "FutureClient");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dc.Initial();
            this.DataContext = dc;
        }

        private void TradeSideRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dc != null)
                {
                    var greyConverter = new BrushConverter();
                    var grey = (Brush)greyConverter.ConvertFromString("#DCDCDC");
                    this.buyRadio.Background = grey;
                    this.sellRadio.Background = grey;
                    this.buyOpen.Background = grey;
                    this.sellClose.Background = grey;
                    this.sellOpen.Background = grey;
                    this.buyClose.Background = grey;

                    if (this.buyRadio.IsChecked == true)
                    {
                        dc.TraderColor = "#FF4500";
                        dc.Direction = "1";
                        dc.IsAutoOC = true;
                        var converter = new BrushConverter();
                        var brush = (Brush)converter.ConvertFromString("#FF4500");
                        this.buyRadio.Background = brush;
                    }
                    else if (this.sellRadio.IsChecked == true)
                    {
                        dc.TraderColor = "#2E8B57";
                        dc.Direction = "2";
                        dc.IsAutoOC = true;
                        var converter = new BrushConverter();
                        var brush = (Brush)converter.ConvertFromString("#2E8B57");
                        this.sellRadio.Background = brush;
                    }
                    else if (this.buyOpen.IsChecked == true)
                    {
                        string tradeColor = "#FF4500";
                        dc.TraderColor = tradeColor;
                        dc.Direction = "1";
                        dc.Entrust_direction = "1";
                        dc.Futures_direction = "1";
                        dc.IsAutoOC = false;
                        var converter = new BrushConverter();
                        var brush = (Brush)converter.ConvertFromString(tradeColor);
                        this.buyOpen.Background = brush;
                    }
                    else if (this.buyClose.IsChecked == true)
                    {
                        string tradeColor = "#FF4500";
                        dc.TraderColor = tradeColor;
                        dc.Direction = "1";
                        dc.Entrust_direction = "1";
                        dc.Futures_direction = "2";
                        dc.IsAutoOC = false;
                        var converter = new BrushConverter();
                        var brush = (Brush)converter.ConvertFromString(tradeColor);
                        this.buyClose.Background = brush;
                    }
                    else if (this.sellOpen.IsChecked == true)
                    {
                        string tradeColor = "#2E8B57";
                        dc.TraderColor = tradeColor;
                        dc.Direction = "2";
                        dc.Entrust_direction = "2";
                        dc.Futures_direction = "1";
                        dc.IsAutoOC = false;
                        var converter = new BrushConverter();
                        var brush = (Brush)converter.ConvertFromString(tradeColor);
                        this.sellOpen.Background = brush;
                    }
                    else if (this.sellClose.IsChecked == true)
                    {
                        string tradeColor = "#2E8B57";
                        dc.TraderColor = tradeColor;
                        dc.Direction = "2";
                        dc.Entrust_direction = "2";
                        dc.Futures_direction = "2";
                        dc.IsAutoOC = false;
                        var converter = new BrushConverter();
                        var brush = (Brush)converter.ConvertFromString(tradeColor);
                        this.sellClose.Background = brush;
                    }
                }

                ChangePrice();
            }
            catch
            {

            }

            
        }


        private void ChangePrice()
        {

            try
            {
                if (dc != null && dc.SelectedCodeItem != null && dc.ShowHQ != null)
                {
                    if (dc.Direction == "1")
                    {
                        inputPrice.Text = dc.ShowHQ.SelPrice1.ToString();
                    }
                    else
                    {
                        inputPrice.Text = dc.ShowHQ.BuyPrice1.ToString();
                    }
                }
            }
            catch
            {

            }
            
            

        }

        private void codeCoboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (dc != null)
                {
                    if (dc.SelectedCodeItem != null)
                    {
                        if (dc.HqDict.ContainsKey(dc.SelectedCodeItem.Code))
                        {
                            dc.ShowHQ = dc.HqDict[dc.SelectedCodeItem.Code];
                        }
                        else
                        {
                            FutureHQ fh = new FutureHQ();
                            fh.SCode = dc.SelectedCodeItem.Code ;
                            dc.ShowHQ = fh;
                        }

                        ChangePrice();
                    }
                }
            }
            catch
            {

            }

            
        }

        private void CollectionViewSource_FilterAccount(object sender, FilterEventArgs e)
        {
            try
            {
                FuturepositionQry_Output t = e.Item as FuturepositionQry_Output;
                if (t != null)
                // If filter is turned on, filter completed items.
                {
                    if (amountCheck.IsChecked == false && int.Parse(t.out_current_amount) <= 0)
                    {
                        e.Accepted = false;
                    }
                    else if (longCheck.IsChecked == false && t.out_position_flag == "1")
                    {
                        e.Accepted = false;
                    }
                    else if (shortCheck.IsChecked == false && t.out_position_flag == "2")
                    {
                        e.Accepted = false;
                    }
                    else if (bondName.Text != "" && !t.out_stock_code.Contains(bondName.Text.ToUpper()))
                    {
                        e.Accepted = false;
                    }
                    else
                    {
                        e.Accepted = true;
                    }
                }
            }
            catch
            {

            }

        }

        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AccountDataGrid != null)
                {
                    CollectionViewSource.GetDefaultView(AccountDataGrid.ItemsSource).Refresh();
                }
            }
            catch
            {

            }

            
        }

        private void CollectionViewSource_FilterFinish(object sender, FilterEventArgs e)
        {
            try
            {
                RequestClass t = e.Item as RequestClass;
                if (t != null)
                // If filter is turned on, filter completed items.
                {
                    if (FinishedLongCheck.IsChecked == false && t.direction == "1")
                    {
                        e.Accepted = false;
                    }
                    else if (FinishedShortCheck.IsChecked == false && t.direction == "2")
                    {
                        e.Accepted = false;
                    }
                    else if (FinishedDeal.IsChecked == false && t.orderState.Contains("成"))
                    {
                        e.Accepted = false;
                    }
                    else if (FinishedWithdraw.IsChecked == false && t.orderState.Contains("撤"))
                    {
                        e.Accepted = false;
                    }
                    else if (FinishedInvalid.IsChecked == false && t.orderState.Contains("废"))
                    {
                        e.Accepted = false;
                    }
                    else if (FinishedPairs.IsChecked == false &&  t.addInfo1 != null)
                    {
                        e.Accepted = false;
                    }
                    else if (FinishedBondName.Text != "" && t.addInfo1=="")
                    {
                        e.Accepted = false;
                    }
                    else
                    {
                        e.Accepted = true;
                    }

                }
            }
            catch (Exception ex)
            {

            }
            
        }

        private void FinishedFilter_Changed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FinishedDataGrid != null)
                {
                    CollectionViewSource.GetDefaultView(FinishedDataGrid.ItemsSource).Refresh();
                }
            }
            catch
            {

            }
            
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void CollectionViewSource_FilterToDeals(object sender, FilterEventArgs e)
        {

            try
            {
                RequestClass t = e.Item as RequestClass;
                if (t != null)
                // If filter is turned on, filter completed items.
                {
                    if (TodealsLongCheck.IsChecked == false && t.direction == "1")
                    {
                        e.Accepted = false;
                    }
                    else if (TodealsShortCheck.IsChecked == false && t.direction == "2")
                    {
                        e.Accepted = false;
                    }
                    else if (TodealsPairs.IsChecked == false && t.addInfo1 != null)
                    {
                        e.Accepted = false;
                    }
                    else if (TodealsBondName.Text != "" && !t.code.Contains(TodealsBondName.Text))
                    {
                        e.Accepted = false;
                    }
                    else if ((MainViewModel.FinishedStateText.Contains(t.orderState) || t.entrust_no == "0" || t.error_info != "" || t.direction == null))
                    {
                        e.Accepted = false;
                    }
                    else
                    {
                        e.Accepted = true;
                    }

                }
            }
            catch
            {

            }

            
        }

        private void ToDealsFilter_Changed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AliveOrdersDataGrid != null)
                {
                    CollectionViewSource.GetDefaultView(AliveOrdersDataGrid.ItemsSource).Refresh();
                }
            }
            catch
            {

            }
            
        }

        private void MinusChangedEvent(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (dc != null)
                {
                    dc.ChangeMinus(false);
                    dc.ChangeMinus(true);
                }
            }
            catch
            {

            }

            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dc != null)
                {
                    dc.ChangeMinus(false);
                    dc.ChangeMinus(true);
                }
            }
            catch
            {

            }
            
        }

        private void Pairs_Filter(object sender, FilterEventArgs e)
        {
            try
            {
                PairOrders t = e.Item as PairOrders;
                if (t != null)
                // If filter is turned on, filter completed items.
                {
                    //if (PairIsFinish.IsChecked == false && (t.IsFinish || t.IsDelete))
                    if (PairIsFinish.IsChecked == false && t.IsFinish)
                    {
                        e.Accepted = false;
                    }
                    else if (PairIsUNFinish.IsChecked == false && !t.IsFinish)
                    {
                        e.Accepted = false;
                    }
                    else
                    {
                        e.Accepted = true;
                    }

                }
            }
            catch
            {

            }


            
        }

        private void Pairs_Changed(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (PairsDataGrid != null)
                {
                    CollectionViewSource.GetDefaultView(PairsDataGrid.ItemsSource).Refresh();
                }
            }
            catch
            {

            }
        }

        public void PairsChange()
        {
            try
            {
                if (PairsDataGrid != null)
                {
                    CollectionViewSource.GetDefaultView(PairsDataGrid.ItemsSource).Refresh();
                }
            }
            catch
            {

            }
            
        }

        private void AccountDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var dg = sender as DataGrid;

                if (dg == null) return;
                var selectedItem = dg.CurrentItem as FuturepositionQry_Output;
                if (selectedItem != null)
                {
                    if (selectedItem.out_position_flag == "1")
                    {
                        sellRadio.IsChecked = true;
                    }
                    else
                    {
                        buyRadio.IsChecked = true;
                    }
                    TradeSideRadioButtonChecked(sender, e);
                    codeCoboBox.Text = selectedItem.out_stock_code;
                    codeCoboBox.SelectedItem = new FutureCode() { Code = selectedItem.out_stock_code };
                    int amt = int.Parse(selectedItem.out_enable_amount);
                    if (amt > 50)
                    {
                        DirectionAmt.Text = "50";
                    }
                    else
                    {
                        DirectionAmt.Text = selectedItem.out_enable_amount;
                    }

                }
            }
            catch
            {

            }
            

        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dg = sender as DataGrid;

            if (dg == null) return;
            var selectedItem = dg.CurrentItem as FutureHQ;
            if (selectedItem != null)
            {
                if (dg.CurrentColumn.SortMemberPath == "BuyPrice1" || dg.CurrentColumn.SortMemberPath == "BuyVol1")
                {
                    sellRadio.IsChecked = true;
                }
                if (dg.CurrentColumn.SortMemberPath == "SelPrice1" || dg.CurrentColumn.SortMemberPath == "SelVol1")
                {
                    buyRadio.IsChecked = true;
                }
                TradeSideRadioButtonChecked(sender, e);
                char[] charsToTrim = { '.', 'Z', 'J' };
                string code = selectedItem.SCode.TrimEnd(charsToTrim);
                codeCoboBox.Text = code;
                codeCoboBox.SelectedItem = new FutureCode() { Code = code };

            }
        }


        private void inputPrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dc != null && dc.SelectedCodeItem != null && dc.ShowHQ != null)
                {
                    double orginalPrice = double.Parse(inputPrice.Text);
                    if (e.KeyStates == Keyboard.GetKeyStates(Key.Right))
                    {
                        double newPrice = orginalPrice + 0.005d;
                        inputPrice.Text = newPrice.ToString();
                    }
                    if (e.KeyStates == Keyboard.GetKeyStates(Key.Left))
                    {
                        double newPrice = orginalPrice - 0.005d;
                        inputPrice.Text = newPrice.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var tc = sender as TabControl; //The sender is a type of TabControl...

                if (tc != null && dc != null)
                {
                    if (PairItem != null && PairItem.IsSelected)
                    {
                        dc.MainColWidth_0 = "0*";
                        dc.MainColWidth_1 = "24*";
                    }
                    else
                    {
                        dc.MainColWidth_0 = "24*";
                        dc.MainColWidth_1 = "0*";
                    }
                }
            }
            catch
            {

            }
            
        }

        

        //private void CheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dc != null)
        //    {
        //        if (dc.IsFishOk==true)
        //        {
        //            dc.IsFishOk = false;
        //        }
        //        else
        //        {
        //            dc.IsFishOk = true;
        //        }
        //    }
        //}
    }
}
