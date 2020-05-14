using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Enums;

namespace VideoClub.WPF.Views
{
    /// <summary>
    /// Interaction logic for RentalsWindow.xaml
    /// </summary>
    public partial class RentalsWindow : MetroWindow
    {
        private readonly DateTime _todayDateTime;
        private RentalService _rentalService;
        private ClientService _clientService;
        private MovieService _movieService;
        private VideoGameService _videoGameService;
        private IList<RentalDto> _rentals;
        private RentalDto _rentalSelected;
        private readonly IList<string> _itemsProductType;
        public RentalsWindow()
        {
            InitializeComponent();
            _todayDateTime = DateTime.Today;
            _itemsProductType = new List<string>
            {
                {ProductTypeEnum.VideoGame.ToString()},
                {ProductTypeEnum.Movie.ToString()}
            };

            _clientService = new ClientService();
            _movieService = new MovieService();
            _videoGameService = new VideoGameService();
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
            await LoadingDataTask();
            RentalsDataGrid.ItemsSource = _rentals;
            LoadingPanel.Visibility = Visibility.Hidden;
        }

        private void LoadingData()
        {
            _rentalService = new RentalService();
            _rentals = _rentalService.All();
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

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var client = GetClientOfParam();
            var product = GetProductOfParam();

            var quantity = Convert.ToDouble(QuantityNumeric.Value);
            var finishRental = _todayDateTime.AddDays(quantity);

            var rental = new RentalDto
            {
                ClientAccreditation = client.Accreditation,
                ClientId =  client.Id,
                ProductId = product.Id,
                ProductTitle = product.Title,
                StartRental = _todayDateTime,
                FinishRental = finishRental
            };
            var isStartRental = _rentalService.StartRentalProduct(rental,StateProductEnum.NonAvailable);

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

            ProductDto product;
            if (productType.Equals(ProductTypeEnum.Movie.ToString()))
            {
                product = _movieService.Get(productParam);
            }
            else
            {
                product = _videoGameService.Get(productParam);
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

        private void FinishButton_OnClick(object sender, RoutedEventArgs e)
        {

            var clientParam = GetClientParam();
            var productParam = GetProductParam();
            var differencePrice = 0M;
            var isFinishRental = _rentalService.FinishRentalProduct(clientParam, productParam, out differencePrice);
        }

        private void ProductComboBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            var items = new List<string> { string.Empty };
            items.AddRange(_itemsProductType.ToList());
            ProductTypeComboBox.ItemsSource = items;
        }
    }
}
