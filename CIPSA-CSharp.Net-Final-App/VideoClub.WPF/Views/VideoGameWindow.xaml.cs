using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Extensions;

namespace VideoClub.WPF.Views
{
    /// <summary>
    /// Interaction logic for VideoGameWindow.xaml
    /// </summary>
    public partial class VideoGameWindow : MetroWindow
    {
        private readonly DateTime _todayDateTime;
        private VideoGameService _videoGameService;
        private IList<VideoGameDto> _videosGame;
        private VideoGameDto _videoGameSelected;
        private IDictionary<GamePlatformEnum, string> _itemsPlatform;
        public VideoGameWindow()
        {
            InitializeComponent();
            _todayDateTime = DateTime.Today;
            FillSourcePlatformComboBox();
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
            await LoadingDataTask();
            VideoGameDataGrid.ItemsSource = _videosGame;
            LoadingPanel.Visibility = Visibility.Hidden;
        }

        private void LoadingData()
        {
            _videoGameService = new VideoGameService();
            _videosGame = _videoGameService.All();
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
        }

        private bool AddVideoGame()
        {
            var videoGame = new VideoGameDto();
            FillDataFromFields(videoGame);
            return _videoGameService.Add(videoGame);
        }

        private void FillDataFromFields(VideoGameDto videoGame)
        {
            var platform = _itemsPlatform.FirstOrDefault(x => x.Value.Equals(PlatformDropDown.SelectedValue)).Key;
            videoGame.Title = TitleText.Text.RemoveMultipleSpace().ToUpperAllFirstLetter();
            videoGame.Price = Convert.ToDecimal(PriceNumeric.Value);
            videoGame.QuantityDisc = Convert.ToInt32(QuantityNumeric.Value);
            videoGame.Platform = platform;
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

        private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (AddVideoGame())
            {
                await LoadDataGrid();
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
    }
}
