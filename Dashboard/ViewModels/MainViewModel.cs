using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Dashboard.Models;
using Dashboard.Models.Interfaces;
using Dashboard.Services;
using Dashboard.Views;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;

namespace Dashboard.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool _isDarkTheme;
        private Period _selectedPeriod;
        private Report _selectedReport;
        private readonly IAppSettings _appSettings;
        private readonly IDataService _dataService;

        private readonly DelegateCommand _openReportCommand;
        private readonly DelegateCommand<Report> _deleteReportCommand;
        public ICommand OpenReportCommand => _openReportCommand;
        public ICommand DeleteReportCommand => _deleteReportCommand;

        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }

        public Period SelectedPeriod { get => _selectedPeriod; set => SetProperty(ref _selectedPeriod, value); }

        public Report SelectedReport 
        { 
            get => _selectedReport;
            set 
            { 
                if (SetProperty(ref _selectedReport, value))
                    SelectedPeriod = _selectedReport?.Periods?.FirstOrDefault(); 
            }
        }

        public ObservableCollection<Report> Reports { get => _appSettings.Reports; set => _appSettings.Reports = value; }


        public MainViewModel(IAppSettings appSettings, IDataService dataService, PaletteHelper paletteHelper)
        {
            _appSettings = appSettings;
            _dataService = dataService;
            _selectedReport = Reports.FirstOrDefault();

            var theme = paletteHelper.GetTheme();
            IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark;
            if (paletteHelper.GetThemeManager() is { } themeManager)
            {
                themeManager.ThemeChanged += (_, e) =>
                {
                    IsDarkTheme = e.NewTheme?.GetBaseTheme() == BaseTheme.Dark;
                };
            }
            _openReportCommand = new DelegateCommand(ExecuteOpenReportCommand);
            _deleteReportCommand = new DelegateCommand<Report>(ExecuteDeleteReportCommand);
        }

        private void ExecuteDeleteReportCommand(Report report)
        {
            if (report is null || !Reports.Contains(report))
                return;
            
            Reports.Remove(report);
            _appSettings.Save();
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
        
        private async void ExecuteOpenReportCommand()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var data = _dataService.GetData(openFileDialog.FileName);
                    Reports.Add(data);
                    SelectedReport = data;
                    _appSettings.Save();
                }
                catch (Exception ex)
                {
                    var sampleMessageDialog = new MessageDialog
                    {
                        Message = { Text = ex.Message }
                    };

                    await DialogHost.Show(sampleMessageDialog, "RootDialog");
                    // System.Windows.Forms.MessageBox.Show(ex.Message);
                }
               
            }
        }
    }
}