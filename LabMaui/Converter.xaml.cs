using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MauiApp6.Entities;
using MauiApp6.Services;
namespace LabMaui;

public partial class Converter : ContentPage
{
    private readonly IRateService _rateService;
    private List<Rate> _rates = [];
    private int _idx_byn;
    private bool _is_converting = false;

public Converter(IRateService rateService)
    {
        _rateService = rateService;
        InitializeComponent();
    }

    private async void LoadRatesAsync(DateTime date)
    {
        try
        {
            _rates = (await MainThread.InvokeOnMainThreadAsync(() =>_rateService.GetRates(date))).ToList();
            _idx_byn = _rates.FindIndex(r => r.Cur_Abbreviation == "BYN");
            FromPicker.ItemsSource = _rates;
            ToPicker.ItemsSource = _rates;
            RatesCollectionView.ItemsSource = _rates.Where(r => r.Cur_Abbreviation != "BYN");
            DescribeLabel.Text = $"Курс на {date}";
        }
        catch (Exception e)
        {
            DisplayAlert("Error", e.Message, "OKK");
        }
    }
    

    private void DatePicker_OnDateSelected(object? sender, DateChangedEventArgs e)
    {
        if (sender is DatePicker datePicker)
        {
            LoadRatesAsync(datePicker.Date);
        }
    }

    private bool IsValid(string input)
    {
        var pattern = @"\d+[/./,]?\d*";
        return Regex.IsMatch(input, pattern);
    }

    private decimal ConvertToByn(int fromIndex, decimal value)
    {
        var curOfficialRate = _rates[fromIndex].Cur_OfficialRate;
        var curScale = _rates[fromIndex].Cur_Scale;
        if(curOfficialRate == null || curScale == 0)
            throw new Exception("Sorry, CurOfficialRate is null");
        return Math.Round((decimal)(value * curOfficialRate / curScale), 2);
    }

    private decimal ConvertFromByn(int toIndex, decimal value)
    {
        var curOfficialRate = _rates[toIndex].Cur_OfficialRate;
        var curScale = _rates[toIndex].Cur_Scale;
        if(curOfficialRate == null || curOfficialRate == 0)
            throw new Exception("Sorry, CurOfficialRate is null"); 
        return Math.Round((decimal)(value / curOfficialRate * curScale), 2);  
    }
    private void FromEntry_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            if(_is_converting)
                return;
            if (FromPicker.SelectedIndex == -1 || ToPicker.SelectedIndex == -1)
            {
                entry.Text = "";
                return;
            }
            if (e.NewTextValue.Length == 0)
            {
                ToEntry.Text = "";
                FromEntry.Text = "";
                return;
            }
            bool isValid = IsValid(entry.Text);
            if (!isValid)
            {
                entry.Text = e.OldTextValue;
                return;
            }

            entry.Text = e.NewTextValue;
            bool isParsed = decimal.TryParse(entry.Text, out var amount);
            if (!isParsed)
            {
                entry.Text = e.OldTextValue;
                return;
            }

            try
            {
                _is_converting = true;
                // convert from * to BYN
                var bynAmount = ConvertToByn(FromPicker.SelectedIndex, amount);
                // convert from BYN to ToIndex
                var toAmount = ConvertFromByn(ToPicker.SelectedIndex, bynAmount);
                // update ToEntry
                ToEntry.Text = toAmount.ToString();

            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                _is_converting = false;
            }
        }
    }

    private void FromPicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (sender is Picker picker)
        {
            if(_is_converting)
                return;
            if (FromEntry.Text == null || (FromEntry.Text != null && FromEntry.Text.Length == 0))
                return;
            decimal amount = decimal.Parse(FromEntry.Text);
            try
            {
                _is_converting = true;
                // convert from * to BYN
                var bynAmount = ConvertToByn(FromPicker.SelectedIndex, amount);
                // convert from BYN to ToIndex
                var toAmount = ConvertFromByn(ToPicker.SelectedIndex, bynAmount);
                // update ToEntry
                ToEntry.Text = toAmount.ToString();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                _is_converting = false;
            }
        }
    }

    private void ToEntry_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            if(_is_converting)
                return;
            if (FromPicker.SelectedIndex == -1 || ToPicker.SelectedIndex == -1)
            {
                entry.Text = e.OldTextValue;
                return;
            }
            if (e.NewTextValue.Length == 0)
            {
                ToEntry.Text = "";
                FromEntry.Text = "";
                return;
            }
            bool isValid = IsValid(entry.Text);
            if (!isValid)
            {
                entry.Text = e.OldTextValue;
                return;
            }
            entry.Text = e.NewTextValue;
            bool isParsed = decimal.TryParse(entry.Text, out var amount);
            if (!isParsed)
            {
                entry.Text = e.OldTextValue;
                return;
            }

            try
            {
                _is_converting = true;
                // convert from * to BYN
                var bynAmount = ConvertToByn(ToPicker.SelectedIndex, amount);
                //throw new Exception("loh");
                // convert from BYN to FromIndex
                var toAmount = ConvertFromByn(FromPicker.SelectedIndex, bynAmount);
                // update FromEntry
                FromEntry.Text = toAmount.ToString();

            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                _is_converting = false;
            }
        }
    }
    
    private void ToPicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (sender is Picker picker)
        {
            if(_is_converting)
                return;
            if (ToEntry.Text == null || (ToEntry.Text != null && ToEntry.Text.Length == 0))
                return;
            decimal amount = decimal.Parse(ToEntry.Text);
            try
            {
                _is_converting = true;
                // convert from * to BYN
                var bynAmount = ConvertToByn(ToPicker.SelectedIndex, amount);
                //throw new Exception("loh");
                // convert from BYN to FromIndex
                var toAmount = ConvertFromByn(FromPicker.SelectedIndex, bynAmount);
                // update FromEntry
                FromEntry.Text = toAmount.ToString();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                _is_converting = false;
            }
        }
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadRatesAsync(DateTime.Today);
        DatePicker.MaximumDate = DateTime.Today; // Обновить при каждом появлении страницы
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            // swap picker and entry
            _is_converting = true;
            (FromPicker.SelectedIndex, ToPicker.SelectedIndex) = (ToPicker.SelectedIndex, FromPicker.SelectedIndex);
            (FromEntry.Text, ToEntry.Text) = (ToEntry.Text, FromEntry.Text);
            _is_converting = false;
        }
    }
}