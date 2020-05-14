using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Extensions;

namespace VideoClub.WPF.Views
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : MetroWindow
    {
        private readonly DateTime _todayDateTime;
        private ClientService _clientService;
        private IList<ClientDto> _clients;
        private readonly IDictionary<AccreditationEnum, string> _itemsAccreditationType;
        private ClientDto _clientSelected;
        private readonly string _prefixSpain = "34";
        public ClientWindow()
        {
            InitializeComponent();
            _todayDateTime = DateTime.Today;
            _itemsAccreditationType = new Dictionary<AccreditationEnum, string>
            {
                {AccreditationEnum.Dni,AccreditationEnum.Dni.GetDescription()},
                {AccreditationEnum.Nie,AccreditationEnum.Nie.GetDescription()}
            };
        }

        private void LoadingData()
        {
            _clientService = new ClientService();
            _clients = _clientService.All();
        }

        private async Task LoadingDataTask()
        {
            await Task.Run(LoadingData);
        }

        private async void ClientDataGrid_OnLoadedDataGrid(object sender, RoutedEventArgs e)
        {
            await LoadDataGrid();
        }

        private async Task LoadDataGrid()
        {
            LoadingPanel.Visibility = Visibility.Visible;
            await LoadingDataTask();
            ClientDataGrid.ItemsSource = _clients;
            LoadingPanel.Visibility = Visibility.Hidden;
        }

        public async Task<MessageDialogResult> PromptAsync(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative, MetroDialogSettings settings = null)
        {
            return await this.ShowMessageAsync(title, message, style, settings);
        }

        private void AccreditationDropDown_OnLoaded(object sender, RoutedEventArgs e)
        {
            var items = new List<string> {string.Empty};
            items.AddRange(_itemsAccreditationType.Values);
            AccreditationDropDown.ItemsSource = items;
        }

        private void DateNowTextBlock_OnLoaded(object sender, RoutedEventArgs e)
        {
            DateNowTextBlock.Text = _todayDateTime.ToShortDateString();
        }

        private bool AddClient()
        {
            var client = new ClientDto();
            FillDataFromFields(client);
            return _clientService.Add(client);
        }

        private void FillDataFromFields(ClientDto client)
        {
            var accreditationType = _itemsAccreditationType.FirstOrDefault(x => x.Value.Equals(AccreditationDropDown.SelectedValue)).Key;
            client.Accreditation = AccreditationText.Text;
            client.AccreditationType = accreditationType;
            client.Address = AddressText.Text.RemoveMultipleSpace().ToUpperAllFirstLetter();
            client.Email = EmailText.Text.RemoveMultipleSpace();
            client.Name = NameText.Text.RemoveMultipleSpace().ToUpperAllFirstLetter();
            client.LastName = LastNameText.Text.RemoveMultipleSpace().ToUpperAllFirstLetter();
            client.PhoneContact = _prefixSpain + PhoneContactText?.Text;
            client.PhoneAux = string.IsNullOrEmpty(PhoneAuxText?.Text) ? string.Empty : _prefixSpain + PhoneAuxText?.Text;
            client.SubscriptionDate = _todayDateTime;
        }

        private bool UpdateClient()
        {
            FillDataFromFields(_clientSelected);
            return _clientService.Update(_clientSelected);
        }

        private void FillDataFromDataGrid()
        {
            if (_clientSelected == null) return;
            AccreditationText.Text = _clientSelected.Accreditation;
            AccreditationDropDown.SelectedItem = _itemsAccreditationType[_clientSelected.AccreditationType];
            AddressText.Text = _clientSelected.Address;
            EmailText.Text = _clientSelected.Email;
            NameText.Text = _clientSelected.Name;
            LastNameText.Text = _clientSelected.LastName;
            PhoneContactText.Text = _clientSelected.PhoneContact.Replace(_prefixSpain, string.Empty);
            PhoneAuxText.Text = string.IsNullOrEmpty(_clientSelected.PhoneAux)
                ? string.Empty
                : _clientSelected.PhoneAux.Replace(_prefixSpain, string.Empty);
            VipCheckBox.IsChecked = _clientSelected.IsVip;
        }

        private bool RemoveClient(ClientDto client)
        {
            return _clientService.Remove(client.Id);
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddClient())
            {
                await LoadDataGrid();
            }
        }

        private void ClientDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeEnabledToButtons(true);
            FillFields(sender);
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateClient())
            {
                await LoadDataGrid();
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RemoveClient(_clientSelected))
            {
                await LoadDataGrid();
            }
        }

        private void UpdateButtonDataGrid_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeEnabledToButtons(true);
            FillFields(sender);
        }

        private void ChangeEnabledToButtons(bool isEnabled)
        {
            UpdateButton.IsEnabled = isEnabled;
            DeleteButton.IsEnabled = isEnabled;
            AddButton.IsEnabled = !isEnabled;
            AccreditationText.IsEnabled = !isEnabled;
            AccreditationDropDown.IsEnabled = !isEnabled;
        }

        private void FillFields(object sender)
        {
            ClientSelected(sender);
            FillDataFromDataGrid();
            MainPanel.IsEnabled = true;
        }

        private void ClientSelected(object sender)
        {
            switch (sender)
            {
                case Button rowButton:
                    _clientSelected = (ClientDto)rowButton?.DataContext;
                    break;
                case DataGrid rowDataGrid:
                    _clientSelected = (ClientDto)rowDataGrid?.SelectedValue;
                    break;
            }
        }

        private async void DeleteButtonDataGrid_OnClick(object sender, RoutedEventArgs e)
        {
            ClientSelected(sender);
            if (RemoveClient(_clientSelected))
            {
                await LoadDataGrid();
            }
        }

        private void NewButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeEnabledToButtons(false);
            HelperWindow.ClearFields(MainPanel);
            MainPanel.IsEnabled = true;
        }


        
    }
}
