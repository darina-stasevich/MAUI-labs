using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMaui;

public partial class IntegralAsync : ContentPage
{
    private CancellationTokenSource? _cancellationTokenSource;
    
    public IntegralAsync()
    {
        
        InitializeComponent();
        var _calculator = (CalculateLib.Calculate)this.Resources["MyCalculator"];
        _calculator.OnProgressEvent += progress =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ProgressLabel.Text = progress.ToString() + "%";
                ProgressBar.Progress = progress / 100f;
            });
        };
    }

    private async void StartButton_OnClicked(object? sender, EventArgs e)
    {
        _cancellationTokenSource = new CancellationTokenSource();
        
        StartButton.IsEnabled = false;
        CancelButton.IsEnabled = true;
        
        StatusLabel.Text = "Calculating...";
        try
        {
            var _calculator = (CalculateLib.Calculate)this.Resources["MyCalculator"];
            var result = await Task.Run(()=>_calculator
                                                .CalculateIntAsync(_cancellationTokenSource.Token));
            StatusLabel.Text = "result: " + result.ToString();
        }
        catch (OperationCanceledException)
        {
            ProgressBar.Progress = 0;
            ProgressLabel.Text = "0%";
            StatusLabel.Text = "Canceled";
        }
        finally
        {
            StartButton.IsEnabled = true;
            CancelButton.IsEnabled = true;
        }
    }

    private void CancelButton_OnClicked(object? sender, EventArgs e)
    {
        _cancellationTokenSource?.Cancel();
    }
}