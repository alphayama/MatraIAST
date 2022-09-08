﻿using PowerIAST.Core;
using PowerIAST.Core.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PowerIAST.UI.SettingsPage;

/// <summary>
/// Logique d'interaction pour SortPage.xaml
/// </summary>
public partial class SortPage : Page, INotifyPropertyChanged
{
    private readonly SettingsService _settingService = new SettingsService();

    public SortPage()
    {
        InitializeComponent();
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        Letters.ItemsSource = Enum.GetValues(typeof(LetterKey)).Cast<LetterKey>();
        CharacterList.DataContext = this;
    }

    private ObservableCollection<char> _characters;

    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(string property)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }

    public ObservableCollection<char> Characters
    {
        get
        {
            if (_characters == null)
                _characters = new ObservableCollection<char>();
            return _characters;
        }
        set
        {
            _characters = value;
            NotifyPropertyChanged("Characters");
        }
    }

    private void Letters_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        LetterKey key = (LetterKey)((ListBox)sender).SelectedItem;
        Characters = new ObservableCollection<char>(_settingService.GetLetterKey(key));
        CharacterList.Visibility = Visibility.Visible;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        LetterKey key = (LetterKey)Letters.SelectedItem;
        _settingService.SetLetterKey(key, Characters.ToArray());
        _settingService.Save();
        (Application.Current.MainWindow as Selector).RefreshSettings();
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        LetterKey key = (LetterKey)Letters.SelectedItem;
        Characters = new ObservableCollection<char>(_settingService.GetDefaultLetterKey(key));
    }
}

class VisibilityNullConverter : IValueConverter
{
    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value == null ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    #endregion
}

class BooleanNullConverter : IValueConverter
{
    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value == null;
    }

    #endregion
}