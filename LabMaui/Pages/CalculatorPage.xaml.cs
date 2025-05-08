namespace LabMaui;

public enum ButtonType
{
    None,
    Digit,
    Operation,
    Arithmetic,
    Equals
}

public partial class CalculatorPage : ContentPage
{
    private string _currentHistory = "";
    private decimal _Value;
    private string _currentValue = "0";
    private string _lastOperation = ""; // + - / *
    private bool _isResult;
    private string _addToHistory = "";
    private ButtonType _lastButtonType = ButtonType.None;
    private decimal _memoryValue;
    
    public CalculatorPage()
    {
        InitializeComponent();
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
            switch (button.ClassId)
            {
                case "Digit":
                {
                    if (_isResult)
                    {
                        _addToHistory = "";
                        if (_lastButtonType == ButtonType.Equals) ClearHistory();

                        if (button.Text == ",")
                            _currentValue = "0,";
                        else
                            _currentValue = button.Text;

                        UpdateCurrent();
                        UpdateHistory();
                    }
                    else
                    {
                        // add digit / comma
                        if (button.Text == ",") 
                        {
                            if (!_currentValue.Contains(button.Text))
                            {
                                _currentValue += button.Text;
                                UpdateCurrent();
                            }
                        }
                        else
                        {
                            if (_currentValue == "0")
                                _currentValue = button.Text;
                            else
                                _currentValue += button.Text;
                            UpdateCurrent();
                        }
                    }

                    _isResult = false;
                    _lastButtonType = ButtonType.Digit;
                    break;
                }
                case "Arithmetic":
                {
                    switch (_lastButtonType)
                    {
                        case ButtonType.Digit:
                        {
                            if (_lastOperation == "")
                            {
                                _currentHistory += _currentValue + button.Text;
                                _lastOperation = button.Text;
                                _lastButtonType = ButtonType.Arithmetic;
                                _Value = decimal.Parse(_currentValue);
                                UpdateHistory();
                                _isResult = true;
                            }
                            else
                            {
                                _addToHistory = _currentValue + button.Text;
                                var isSuccess = PerformArithmetic(_lastOperation, _currentValue);
                                if (isSuccess)
                                {
                                    _currentValue = _Value.ToString();
                                    _currentHistory += _addToHistory;
                                    _addToHistory = "";
                                    _addToHistory = "";

                                    UpdateHistory();
                                    UpdateCurrent();
                                    _isResult = true;
                                    _lastButtonType = ButtonType.Arithmetic;
                                    _lastOperation = button.Text;
                                }
                                else
                                {
                                    ClearHistory();
                                    ShowError("Ошибка вычисления");
                                }
                            }

                            break;
                        }
                        case ButtonType.Operation:
                        {
                            if (_lastOperation == "")
                            {
                                _Value = decimal.Parse(_currentValue);
                                _currentHistory += _addToHistory + button.Text;
                                _addToHistory = "";
                                UpdateHistory();
                                UpdateCurrent();
                                _isResult = true;
                                _lastButtonType = ButtonType.Arithmetic;
                                _lastOperation = button.Text;
                            }
                            else
                            {
                                var isSuccess = PerformArithmetic(_lastOperation, _currentValue);
                                if (isSuccess)
                                {
                                    _currentValue = _Value.ToString();
                                    _currentHistory += _addToHistory + button.Text;
                                    _addToHistory = "";

                                    UpdateHistory();
                                    UpdateCurrent();
                                    _isResult = true;
                                    _lastButtonType = ButtonType.Arithmetic;
                                    _lastOperation = button.Text;
                                }
                                else
                                {
                                    ClearHistory();
                                    ShowError("Ошибка вычисления");
                                }
                            }

                            break;
                        }
                        case ButtonType.Arithmetic:
                        case ButtonType.Equals:
                        {
                            if (_currentHistory[^1] == '+' || _currentHistory[^1] == '-' ||
                                _currentHistory[^1] == '*' || _currentHistory[^1] == '/' || _currentHistory[^1] == '=')
                            {
                                _currentHistory = _currentHistory.Remove(_currentHistory.Length - 1, 1);
                                _currentHistory += button.Text;
                            }

                            UpdateHistory();
                            _isResult = true;
                            _lastButtonType = ButtonType.Arithmetic;
                            _lastOperation = button.Text;
                            break;
                        }
                    }

                    break;
                }
                case "Operation":
                {
                    var rem = _currentValue;
                    var isSuccess = PerformOperation(button.Text, _currentValue);
                    if (!isSuccess)
                    {
                        ClearHistory();
                        ShowError("Ошибка вычисления");
                    }
                    else
                    {
                        if (_addToHistory.Length != 0)
                            _addToHistory = GetOperation(button.Text) + "(" + _addToHistory + ")";
                        else
                            _addToHistory = GetOperation(button.Text) + "(" + rem + ")";

                        UpdateHistory();
                        UpdateCurrent();
                        _isResult = true;
                        _lastButtonType = ButtonType.Operation;
                    }

                    break;
                }
                case "Clear":
                {
                    switch (button.Text)
                    {
                        case "C":
                        {
                            ClearHistory();
                            break;
                        }
                        case "CE":
                        {
                            _isResult = false;
                            ClearCurrent();
                            break;
                        }
                        case "Del":
                        {
                            if (!_isResult)
                            {
                                _currentValue = new string(_currentValue.Remove(_currentValue.Length - 1) == ""
                                    ? "0"
                                    : _currentValue.Remove(_currentValue.Length - 1));
                                UpdateHistory();
                                UpdateCurrent();
                            }

                            break;
                        }
                    }

                    break;
                }
                case "Equals":
                {
                    switch (_lastButtonType)
                    {
                        case ButtonType.Digit:
                        {
                            if (_lastOperation == "")
                            {
                                _currentHistory += _currentValue + button.Text;
                                _lastButtonType = ButtonType.Equals;
                                UpdateHistory();
                                _isResult = true;
                            }
                            else
                            {
                                _addToHistory = _currentValue + button.Text;
                                var isSuccess = PerformArithmetic(_lastOperation, _currentValue);
                                if (isSuccess)
                                {
                                    _currentValue = _Value.ToString();
                                    _currentHistory += _addToHistory;
                                    _addToHistory = "";

                                    UpdateHistory();
                                    UpdateCurrent();
                                    _isResult = true;
                                    _lastButtonType = ButtonType.Equals;
                                }
                                else
                                {
                                    ClearHistory();
                                    ShowError("Ошибка вычисления");
                                }
                            }

                            _lastOperation = "";
                            break;
                        }
                        case ButtonType.Arithmetic:
                        {
                            _currentHistory = _currentHistory.Remove(_currentHistory.Length - 1, 1);
                            _currentHistory += button.Text;

                            var isSuccess = PerformArithmetic(_lastOperation, _Value.ToString());
                            if (isSuccess)
                            {
                                _currentValue = _Value.ToString();
                                _addToHistory = "";

                                UpdateHistory();
                                UpdateCurrent();
                                _isResult = true;
                                _lastButtonType = ButtonType.Equals;
                            }
                            else
                            {
                                ClearHistory();
                                ShowError("Ошибка вычисления");
                            }

                            _lastOperation = "";
                            break;
                        }
                        case ButtonType.Operation:
                        {
                            if (_lastOperation != "")
                            {
                                var isSuccess = PerformArithmetic(_lastOperation, _currentValue);
                                if (isSuccess)
                                {
                                    _currentValue = _Value.ToString();
                                    _currentHistory += _addToHistory;
                                    _addToHistory = "";

                                    UpdateHistory();
                                    UpdateCurrent();
                                    _isResult = true;
                                    _lastButtonType = ButtonType.Equals;
                                }
                                else
                                {
                                    ClearHistory();
                                    ShowError("Ошибка вычисления");
                                }
                            }
                            else
                            {
                                _Value = decimal.Parse(_currentValue);
                                _currentHistory += _addToHistory;
                                _addToHistory = "";
                                _currentHistory += "=";
                                UpdateHistory();
                                UpdateCurrent();
                                _isResult = true;
                                _lastButtonType = ButtonType.Equals;
                            }

                            _lastOperation = "";
                            break;
                        }
                        case ButtonType.Equals:
                        {
                            _isResult = false;
                            _lastButtonType = ButtonType.Equals;
                            _lastOperation = "";

                            break;
                        }
                    }

                    break;
                }
                case "Memory":
                {
                    switch (button.Text)
                    {
                        case "MC":
                        {
                            _memoryValue = 0;
                            break;
                        }
                        case "MR":
                        {
                            _currentValue = _memoryValue.ToString();
                            _addToHistory = "";
                            UpdateCurrent();
                            UpdateHistory();
                            _isResult = true;
                            break;
                        }
                        case "M+":
                        {
                            _currentValue = (_memoryValue + decimal.Parse(_currentValue)).ToString();
                            _addToHistory = "";
                            UpdateCurrent();
                            UpdateHistory();
                            _isResult = true;
                            break;
                        }
                        case "M-":
                        {
                            _currentValue = (-_memoryValue + decimal.Parse(_currentValue)).ToString();
                            _addToHistory = "";
                            UpdateCurrent();
                            UpdateHistory();
                            _isResult = true;
                            break;
                        }
                        case "MS":
                        {
                            _memoryValue = decimal.Parse(_currentValue);
                            break;
                        }
                    }

                    break;
                }
            }
        else
            throw new ArgumentException("Sender must be a Button");
    }

    private void ClearCurrent()
    {
        _currentValue = "0";
        UpdateCurrent();
    }

    private void UpdateCurrent()
    {
        LabelCurrent.Text = _currentValue;
    }

    private void ClearHistory()
    {
        _currentHistory = "";
        _isResult = false;
        _addToHistory = "";
        _lastOperation = "";
        _lastButtonType = ButtonType.None;
        _Value = 0;

        UpdateHistory();
        ClearCurrent();
    }

    private void UpdateHistory()
    {
        LabelHistory.Text = _currentHistory + _addToHistory;
    }

    private void ShowError(string s)
    {
        LabelCurrent.Text = s;
    }

    private bool PerformArithmetic(string operation, string value)
    {
        switch (operation)
        {
            case "+":
            {
                try
                {
                    _Value += Convert.ToDecimal(value);
                    return true;
                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                    return false;
                }
            }
            case "-":
            {
                try
                {
                    _Value -= Convert.ToDecimal(value);
                    return true;
                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                    return false;
                }
            }
            case "/":
            {
                try
                {
                    _Value /= Convert.ToDecimal(value);
                    return true;
                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                    return false;
                }
            }
            case "*":
            {
                try
                {
                    _Value *= Convert.ToDecimal(value);
                    return true;
                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                    return false;
                }
            }
        }

        ShowError("Unknown operation");
        return false;
    }

    private bool PerformOperation(string operation, string value)
    {
        switch (operation)
        {
            case "+/-":
            {
                try
                {
                    _currentValue = (-1 * Convert.ToDecimal(value)).ToString();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            case "\u221a":
            {
                try
                {
                    _currentValue = Math.Sqrt((double)Convert.ToDecimal(value)).ToString();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            case "%":
            {
                try
                {
                    _currentValue = (Convert.ToDecimal(value) / 100).ToString();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            case "x^2":
            {
                try
                {
                    _currentValue = (Convert.ToDecimal(value) * Convert.ToDecimal(value)).ToString();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            case "1/x":
            {
                try
                {
                    _currentValue = (1 / Convert.ToDecimal(value)).ToString();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            case "10^x":
            {
                try
                {
                    _currentValue = Math.Pow(10, (double)Convert.ToDecimal(value)).ToString();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        ShowError("Unknown operation");
        return false;
    }

    private string GetOperation(string s)
    {
        return s switch
        {
            "+/-" => "negate",
            "\u221a" => "sqrt",
            "%" => "percent",
            "x^2" => "sqr",
            "1/x" => "inverse",
            "10^x" => "pow_10"
        };
    }
}