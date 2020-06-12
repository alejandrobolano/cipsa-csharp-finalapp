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
using VideoClub.Common.Model.Extensions;
using VideoClub.WPF.Utils;

namespace VideoClub.WPF.Views
{
    /// <summary>
    /// Interaction logic for VideoGameWindow.xaml
    /// </summary>
    public partial class VideoGameWindow : MetroWindow
    {
        private DateTime _todayDateTime;
        private VideoGameService _videoGameService;
        private IList<VideoGameDto> _videosGame;
        private VideoGameDto _videoGameSelected;
        private IDictionary<GamePlatformEnum, string> _itemsPlatform;
        private readonly ResourceManager _resourceManager = Properties.Resources.ResourceManager;
        private IDictionary<StateProductEnum, string> _itemsStateProduct;
        private Dictionary<StateProductEnum, string> _changeItemsStateProduct;
        private StateProductEnum _stateProduct;
        public VideoGameWindow(StateProductEnum stateProduct)
        {
            InitializeComponent();
            InitializeVariables();
            _stateProduct = stateProduct;

        }
        private void InitializeVariables()
        {
            _todayDateTime = DateTime.Today;
            _videoGameService = VideoGameService.Instance;

            _itemsStateProduct = new Dictionary<StateProductEnum, string>
            {
                {StateProductEnum.All, _resourceManager.GetResourceValue("ALL_VIDEOGAMES")}
            };
            _itemsStateProduct.Append(GetAllStateProductEnum());
            _changeItemsStateProduct = GetAllStateProductEnum();
            FillSourcePlatformComboBox();
        }

        private Dictionary<StateProductEnum, string> GetAllStateProductEnum()
        {
            return new Dictionary<StateProductEnum, string>
            {
                {StateProductEnum.Available, StateProductEnum.Available.GetDescription()},
                {StateProductEnum.BadState, StateProductEnum.BadState.GetDescription()},
                {StateProductEnum.Lost, StateProductEnum.Lost.GetDescription()},
                {StateProductEnum.NonAvailable, StateProductEnum.NonAvailable.GetDescription()}
            };
        }

        private void FillSourcePlatformComboBox()
        {
            _itemsPlatform = new Dictionary<GamePlatformEnum, string>
            {
                {GamePlatformEnum.Pc, GamePlatformEnum.Pc.GetDescription()},
                {GamePlatformEnum.Ps2, GamePlatformEnum.Ps2.GetDescription()},
                {GamePlatformEnum.Ps3, GamePlatformEnum.Ps3.GetDescription()},
                {GamePlatformEnum.Wii, GamePlatformEnum.Wii.GetDescription()},
                {GamePlatformEnum.XBox, GamePlatformEnum.XBox.GetDescription()}
            };
        }

        private void DateNowTextBlock_OnLoaded(object sender, RoutedEventArgs e)
        {
            DateNowTextBlock.Text = _todayDateTime.ToShortDateString();
        }

        private async void VideoGameDataGrid_OnLoadedDataGrid(object sender, RoutedEventArgs e)
        {
            await LoadDataGrid();
        }

        private void VideoGameDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateData(sender);
        }

        private void UpdateData(object sender)
        {
            ChangeEnabledToButtons(true);
            FillFields(sender);
        }

        private void UpdateButtonDataGrid_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateData(sender);
        }

        private async Task LoadDataGrid()
        {
            LoadingPanel.Visibility = Visibility.Visible;
            StateProductPanel.Visibility = Visibility.Collapsed;
            await LoadingDataTask();
            VideoGameDataGrid.ItemsSource = _videosGame;
            LoadingPanel.Visibility = Visibility.Hidden;
            StateProductPanel.Visibility = Visibility.Visible;
        }

        private void LoadingData()
        {
            _videosGame = _stateProduct.Equals(StateProductEnum.All)
                ? _videoGameService.All()
                : _videoGameService.GetVideoGamesByState(_stateProduct);
        }
        private async Task LoadingDataTask()
        {
            await Task.Run(LoadingData);
        }

        private void ChangeEnabledToButtons(bool isEnabled)
        {
            UpdateButton.IsEnabled = isEnabled;
            DeleteButton.IsEnabled = isEnabled;
            AddButton.IsEnabled = !isEnabled;
            TitleText.IsEnabled = !isEnabled;
        }

        private void FillFields(object sender)
        {
            VideoGameSelected(sender);
            FillDataFromDataGrid();
            MainPanel.IsEnabled = true;
        }

        private void VideoGameSelected(object sender)
        {
            switch (sender)
            {
                case Button rowButton:
                    _videoGameSelected = (VideoGameDto)rowButton?.DataContext;
                    break;
                case DataGrid rowDataGrid:
                    _videoGameSelected = (VideoGameDto)rowDataGrid?.SelectedValue;
                    break;
            }
        }

        private void FillDataFromDataGrid()
        {
            if (_videoGameSelected == null) return;
            TitleText.Text = _videoGameSelected.Title;
            PriceNumeric.Value = Convert.ToDouble(_videoGameSelected.Price, CultureInfo.CurrentCulture);
            QuantityNumeric.Value = _videoGameSelected.QuantityDisc;
            PlatformDropDown.SelectedItem = _itemsPlatform[_videoGameSelected.Platform];
            ChangeProductStateComboBox.SelectedItem = _changeItemsStateProduct[_videoGameSelected.State];
        }

        private bool AddVideoGame()
        {
            var videoGame = new VideoGameDto();
            FillDataFromFields(videoGame);
            return _videoGameService.Add(videoGame, out _);
        }

        private void FillDataFromFields(VideoGameDto videoGame)
        {
            var platform = _itemsPlatform.FirstOrDefault(x => x.Value.Equals(PlatformDropDown.SelectedValue)).Key;
            videoGame.Title = TitleText.Text.RemoveMultipleSpace().ToUpperAllFirstLetter();
            videoGame.Price = Convert.ToDecimal(PriceNumeric.Value);
            videoGame.QuantityDisc = Convert.ToInt32(QuantityNumeric.Value);
            videoGame.Platform = platform;
            var state = _changeItemsStateProduct.FirstOrDefault(valuePair => valuePair.Value.Equals(ChangeProductStateComboBox.SelectedValue)).Key;
            videoGame.State = state;
        }

        private bool RemoveVideoGame(VideoGameDto videoGame)
        {
            return _videoGameService.Remove(videoGame.Id);
        }
        private bool UpdateVideoGame()
        {
            FillDataFromFields(_videoGameSelected);
            return _videoGameService.Update(_videoGameSelected);
        }
        private void NewButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeEnabledToButtons(false);
            HelperWindow.ClearFields(MainPanel);
            MainPanel.IsEnabled = true;
        }
        private bool CheckFields()
        {
            if (HelperWindow.HasAnyEmptyFields(MainPanel))
            {
                this.ShowGenericErrorDataMessage(_resourceManager);
                HelperWindow.HandleLogError(string.Empty);
                return true;
            }

            return false;
        }

        private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckFields()) return;
            if (AddVideoGame())
            {
                await LoadDataGrid();
            }
            else
            {
                this.ShowGenericErrorMessage(_resourceManager);
                HelperWindow.HandleLogError(string.Empty);
            }
        }

        private async void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (UpdateVideoGame())
            {
                await LoadDataGrid();
            }
        }

        private async void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            VideoGameSelected(sender);
            if (RemoveVideoGame(_videoGameSelected))
            {
                await LoadDataGrid();
            }
            HelperWindow.ClearFields(MainPanel);
        }

        private void PlatformDropDown_OnLoadedDropDown_OnLoaded(object sender, RoutedEventArgs e)
        {
            var items = new List<string> { string.Empty };
            items.AddRange(_itemsPlatform.Values);
            PlatformDropDown.ItemsSource = items;
        }

        private void VideoGameStateComboBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            var combo = (ComboBox)sender;
            combo.ItemsSource = _itemsStateProduct.Values;
            combo.SelectedItem = _itemsStateProduct[_stateProduct];
        }

        private async void VideoGameStateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var stateEnumSelected = _itemsStateProduct.FirstOrDefault(x => x.Value.Equals(((ComboBox)sender).SelectedValue)).Key;
            if (stateEnumSelected.Equals(_stateProduct)) return;
            _stateProduct = stateEnumSelected;
            await LoadDataGrid();
        }

        private void ChangeVideoGameStateComboBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            var combo = (ComboBox)sender;
            combo.ItemsSource = _changeItemsStateProduct.Values;
        }

    }
}
