using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp6.Entities;
using MauiApp6.Services;

namespace LabMaui;

public partial class SpaService : ContentPage
{
    private readonly IDbService _dbService;

    private List<ProcedureType> _types;
    private List<Procedure> _procedures;
    public SpaService(IDbService dbService)
    {
        try 
        {
            InitializeComponent();
            _dbService = dbService;
            _dbService.Init();
            LoadProcedureTypes();
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"Не удалось загрузить данные: {ex.Message}", "OK");
        }
    }

    private void LoadProcedureTypes()
    {
        _types = _dbService.GetProcedureTypes().ToList();
        procedureTypePicker.ItemsSource = _types;
    }


    private void ProcedureTypePicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        var id = _types[procedureTypePicker.SelectedIndex].Id;
        ShowProcedures(id);
    }

    private void ShowProcedures(int id)
    {
        try
        {
            _procedures = _dbService.GetProcedures(id).ToList();
            ProcedureCollectionView.ItemsSource = _procedures;
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"Не удалось загрузить данные: {ex.Message}", "OK");
        }
        
    }
}