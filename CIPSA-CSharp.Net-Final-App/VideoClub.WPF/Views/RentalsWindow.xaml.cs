using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Utils;
using VideoClub.WPF.Utils;

namespace VideoClub.WPF.Views
{
    /// <summary>
    /// Interaction logic for RentalsWindow.xaml
    /// </summary>
    public partial class RentalsWindow : MetroWindow
    {
        private DateTime _todayDateTime;
        private RentalService _rentalService;
        private ClientService _clientService;
        private MovieService _movieService;
        private VideoGameService _videoGameService;
        private IList<RentalDto> _rentals;
        private RentalDto _rentalSelected;
        private IList<string> _itemsProductType;
        private IDictionary<StateProductEnum, string> _itemsStateProduct;
        private readonly ResourceManager _resourceManager = Properties.Resources.ResourceManager;
        private StateProductEnum _stateProduct;
        public RentalsWindow(StateProductEnum stateProduct)
        {
            InitializeComponent();
            InitializeData();
            InitializeVariables();
            _stateProduct = stateProduct;
        }

        private void InitializeVariables()
        {
            _clientService = new ClientService();
            _movieService = new MovieService();
            _videoGameService = new VideoGameService();
        }
        private void InitializeData()
        {
            _todayDateTime = DateTime.Today;
            _itemsProductType = new List<string>
            {
                {ProductTypeEnum.VideoGame.ToString()},
                {ProductTypeEnum.Movie.ToString()}
            };
            _itemsStateProduct = new Dictionary<StateProductEnum, string>
            {
                {StateProductEnum.Available,_resourceManager.GetResourceValue("ALL_RENTALS") },
                {StateProductEnum.NonAvailable,_resourceManager.GetResourceValue("PENDING_RENTALS") }
            };
        }

        private void DateNowTextBlock_OnLoaded(object sender, RoutedEventArgs e)
        {
            DateNowTextBlock.Text = _todayDateTime.ToShortDateString();
        }

        private async void RentalsDataGrid_OnLoadedDataGrid(object sender, RoutedEventArgs e)
        {
            await LoadDataGrid();
        }


        private async Task LoadDataGrid()
        {
            LoadingPanel.Visibility = Visibility.Visible;
            StateProductPanel.Visibility = Visibility.Collapsed;
            await LoadingDataTask();
            RentalsDataGrid.ItemsSource = _rentals;
            LoadingPanel.Visibility = Visibility.Hidden;
            StateProductPanel.Visibility = Visibility.Visible;
        }

        private void LoadingData()
        {
            _rentalService = new RentalService();

            _rentals = _stateProduct == StateProductEnum.Available 
                ? _rentalService.All() 
                : _rentalService.GetRentalsByState(_stateProduct);
        }
        private async Task LoadingDataTask()
        {
            await Task.Run(LoadingData);
        }


        private void RentalSelected(object sender)
        {
            switch (sender)
            {
                case Button rowButton:
                    _rentalSelected = (RentalDto)rowButton?.DataContext;
                    break;
                case DataGrid rowDataGrid:
                    _rentalSelected = (RentalDto)rowDataGrid?.SelectedValue;
                    break;
            }
        }
        private bool RemoveRental(RentalDto rental)
        {
            return _rentalService.Remove(rental.Id);
        }


        private async void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            RentalSelected(sender);
            if (RemoveRental(_rentalSelected))
            {
                await LoadDataGrid();
            }
        }

        private async void StartRentalButton_OnClick(object sender, RoutedEventArgs e)
        {
            var client = GetClientOfParam();
            var product = GetProductOfParam();

            if (HandlePossibleUnsatisfactoryMessages(client, product)) return;

            var quantity = Convert.ToDouble(QuantityNumeric.Value);
            var finishRental = _todayDateTime.AddDays(quantity);

            var rental = TryAddRentalForStart(client, product, finishRental, out var isStartRental);
            ShowMessageOfStartProcess(isStartRental, rental);
            await LoadDataGrid();
        }

        private bool HandlePossibleUnsatisfactoryMessages(ClientDto client, ProductDto product)
        {
            if (client == null || product == null)
            {
                this.ShowGenericErrorDataMessage(_resourceManager);
                return true;
            }

            switch (product.State)
            {
                case StateProductEnum.NonAvailable:
                    {
                        var message = _resourceManager.GetResourceValue("RENTAL_NON_AVAILABLE")
                            ?.Replace("$", product.Id);
                        this.ShowCustomInformationMessage(_resourceManager, message);
                        return true;
                    }

                case StateProductEnum.BadState:
                    {
                        var message = _resourceManager.GetResourceValue("RENTAL_PRODUCT_BAD_STATE")
                            ?.Replace("$", product.Id);
                        this.ShowCustomInformationMessage(_resourceManager, message);
                        return true;
                    }

                case StateProductEnum.Lost:
                    {
                        var message = _resourceManager.GetResourceValue("RENTAL_PRODUCT_LOST")
                            ?.Replace("$", product.Id);
                        this.ShowCustomInformationMessage(_resourceManager, message);
                        return true;
                    }
                default:
                    return false;
            }
        }

        private void ShowMessageOfStartProcess(bool isStartRental, RentalDto rental)
        {
            if (isStartRental)
            {
                var message = _resourceManager.GetResourceValue("ADDED_RENTAL_SUCCESSFUL")
                    ?.Replace("$", rental.Id);
                this.ShowCustomInformationMessage(_resourceManager, message);

            }
            else
            {
                this.ShowGenericErrorDataMessage(_resourceManager);
            }
        }

        private RentalDto TryAddRentalForStart(ClientDto client, ProductDto product, DateTime finishRental,
            out bool isStartRental)
        {
            var rental = new RentalDto
            {
                ClientAccreditation = client.Accreditation,
                ClientId = client.Id,
                ProductId = product.Id,
                ProductTitle = product.Title,
                StartRental = _todayDateTime,
                FinishRental = finishRental
            };
            isStartRental = _rentalService.StartRentalProduct(rental, StateProductEnum.NonAvailable);
            return rental;
        }


        private ClientDto GetClientOfParam()
        {
            var clientParam = GetClientParam();
            var client = _clientService.Get(clientParam);
            return client;
        }

        private ProductDto GetProductOfParam()
        {
            var productParam = GetProductParam();
            var productType = ProductTypeComboBox.SelectedItem;
            ProductDto product = null;
            try
            {

                if (productType.Equals(ProductTypeEnum.Movie.ToString()))
                {
                    product = _movieService.Get(productParam);
                }
                else
                {
                    product = _videoGameService.Get(productParam);
                }
            }
            catch (Exception exception)
            {
                HelperWindow.HandleLogError($"{exception.InnerException} " +
                                             $"\n {exception.Message} " +
                                             $"\n {exception.Source}");
            }

            return product;
        }

        private string GetProductParam()
        {
            var productId = ProductIdText.Text;
            var productTitle = TitleProductText.Text;
            var productParam = string.IsNullOrEmpty(productId) ? productTitle : productId;
            return productParam;
        }

        private string GetClientParam()
        {
            var clientId = ClientIdText.Text;
            var clientAccreditation = AccreditationClientText.Text;


            var clientParam = string.IsNullOrEmpty(clientId) ? clientAccreditation : clientId;
            return clientParam;
        }

        private void FinishRentalButton_OnClick(object sender, RoutedEventArgs e)
        {

            var clientParam = GetClientParam();
            var product = GetProductOfParam();
            if (product.State.Equals(StateProductEnum.Available))
            {
                this.ShowInformationMessage(_resourceManager, "RENTAL_RETURNED");
                return;
            }
            var isFinishRental = _rentalService.FinishRentalProduct(clientParam, product.Id, out var differencePrice);
            if (isFinishRental)
            {
                if (differencePrice > 0)
                {
                    var message = _resourceManager.GetResourceValue("FINISH_RENTAL_DUE_PAYMENT")
                        ?.Replace("$", product.Id)
                        .Replace("PRICE", Convert.ToString(differencePrice, CultureInfo.InvariantCulture));
                    this.ShowCustomInformationMessage(_resourceManager, message);
                }
                else
                {
                    var message = _resourceManager.GetResourceValue("FINISH_RENTAL_SUCCESSFUL")
                        ?.Replace("$", product.Id);
                    this.ShowCustomInformationMessage(_resourceManager, message);
                }
            }
            else
            {
                this.ShowGenericErrorDataMessage(_resourceManager);
            }
        }

        private void ProductComboBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            var items = new List<string> { string.Empty };
            items.AddRange(_itemsProductType.ToList());
            ((ComboBox)sender).ItemsSource = items;
        }

        private void ClientIdText_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = CommonHelper.Client + CommonHelper.Separator;
        }

        private void ProductTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem;
            if (string.IsNullOrEmpty(selectedItem.ToString()))
            {
                ProductIdText.Text = string.Empty;
                return;
            }

            if (selectedItem.Equals(ProductTypeEnum.Movie.ToString()))
            {
                ProductIdText.Text = CommonHelper.Movie + CommonHelper.Separator;
            }
            else
            {
                ProductIdText.Text = CommonHelper.VideoGame + CommonHelper.Separator;
            }
        }

        private void NewButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeEnabledToButtons(false);
            HelperWindow.ClearFields(MainPanel);
            MainPanel.IsEnabled = true;
        }

        private void ChangeEnabledToButtons(bool isEnabled)
        {
            FinishButton.IsEnabled = isEnabled;
            StartButton.IsEnabled = !isEnabled;
        }

        private void RentalDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateData(sender);
        }

        private void UpdateData(object sender)
        {
            ChangeEnabledToButtons(true);
            FillFields(sender);
            MainPanel.IsEnabled = false;
        }

        private void FillFields(object sender)
        {
            RentalSelected(sender);
            FillDataFromDataGrid();
        }

        private void FillDataFromDataGrid()
        {
            if (_rentalSelected == null) return;
            ProductTypeComboBox.SelectedItem = _rentalSelected.ProductId.Contains(CommonHelper.Movie)
                ? ProductTypeEnum.Movie.ToString()
                : ProductTypeEnum.VideoGame.ToString();

            TitleProductText.Text = _rentalSelected.ProductTitle;
            ProductIdText.Text = _rentalSelected.ProductId;
            ClientIdText.Text = _rentalSelected.ClientId;
            AccreditationClientText.Text = _rentalSelected.ClientAccreditation;
            var quantityRentalDays = (_rentalSelected.FinishRental - _rentalSelected.StartRental).Days;
            QuantityNumeric.Value = quantityRentalDays;


        }

        private void ProductStateComboBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            var combo = (ComboBox) sender;
            combo.ItemsSource = _itemsStateProduct.Values;
            combo.SelectedItem = _itemsStateProduct[_stateProduct];
        }

        private async void ProductStateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var stateEnumSelected = _itemsStateProduct.FirstOrDefault(x => x.Value.Equals(((ComboBox)sender).SelectedValue)).Key;
            if (stateEnumSelected.Equals(_stateProduct)) return;
            _stateProduct = stateEnumSelected;
            await LoadDataGrid();
        }
    }
}
