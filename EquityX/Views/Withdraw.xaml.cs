using EquityX.ViewModel;

namespace EquityX.Pages;

public partial class Withdraw : ContentPage
{
    // The logic behind having to variables to store input amount is so that one can continue to be appended using the calculator buttons
    // While the other will have formatting done to it for presentation
    private string displayInputAmountFormatted = "$0.00";
    private string runningNumberInput = "";
    private double amount;

    private UserDataViewModel viewModel;
    public Withdraw()
	{
		InitializeComponent();
        viewModel = new UserDataViewModel();
        this.BindingContext = viewModel;

        // Create a Grid
        var grid = new Grid
        {
            RowSpacing = 5,
            ColumnSpacing = 5
        };

        // Define rows
        for (int i = 0; i < 4; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        // Define columns
        for (int i = 0; i < 3; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 70 });
        }

        // Add buttons
        AddButton(grid, "7", 0, 0);
        AddButton(grid, "8", 0, 1);
        AddButton(grid, "9", 0, 2);
        AddButton(grid, "4", 1, 0);
        AddButton(grid, "5", 1, 1);
        AddButton(grid, "6", 1, 2);
        AddButton(grid, "1", 2, 0);
        AddButton(grid, "2", 2, 1);
        AddButton(grid, "3", 2, 2);
        AddButton(grid, "Clear", 3, 0);
        AddButton(grid, "0", 3, 1);
        AddButton(grid, "Del", 3, 2);

        // Add the Grid to the ContentPage placeholder section
        calculatorPlaceholder.Content = grid;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        amount = await viewModel.Balance();
        TotalMoney.Text = Math.Round(amount, 2).ToString("C");
        WithdrawSlider.Maximum = amount;
    }

    private void AddButton(Grid grid, string text, int row, int column)
    {
        var button = new Button
        {
            Text = text,
            WidthRequest = 70,
            HeightRequest = 40,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#3A5066"),
            TextColor = Colors.White
        };

        // Need to check to see if buttons contain specific text values
        if (text == "Clear")
        {
            button.Clicked += ClearButtonClicked;
        }
        else if (text == "Del")
        {
            button.Clicked += DelButtonClicked;
        }
        else
        {
            button.Clicked += NumberButtonClicked;
        }

        Grid.SetRow(button, row);
        Grid.SetColumn(button, column);

        grid.Children.Add(button);
    }

    void OnSliderChange(object sender, ValueChangedEventArgs args)
    {
        double value = Math.Round(args.NewValue, 2);

        runningNumberInput = value.ToString();
        displayInputAmountFormatted = runningNumberInput;

        if(double.TryParse(runningNumberInput, out double amount))
            WithdrawAmount.Text = amount.ToString("C");
        else
            WithdrawAmount.Text = displayInputAmountFormatted;
    }

    private void ClearButtonClicked(object sender, EventArgs e)
    {
        WithdrawSlider.Value = 0;
        runningNumberInput = "";
        displayInputAmountFormatted = "$0.00";
    }

    private void DelButtonClicked(object sender, EventArgs e)
    {
        try
        {
            double.TryParse(runningNumberInput, out double currentSliderValue);

            if (WithdrawSlider.Value >= 10)
            {
                double newValue = Math.Round((currentSliderValue / 10), 2); // Remove the last digit
                runningNumberInput = newValue.ToString();
                WithdrawSlider.Value = newValue;
            }
            else
            {
                WithdrawSlider.Value = 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void NumberButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null && amount > 0)
        {
            runningNumberInput += button.Text; // Append the button's number to the current input
            UpdateSliderValue();
        }
    }

    private void UpdateSliderValue()
    {
        if (double.TryParse(runningNumberInput, out double value))
        {
            value = Math.Min(value, WithdrawSlider.Maximum); // Ensure value does not exceed maximum
            value = Math.Max(value, WithdrawSlider.Minimum); // Ensure value is not below minimum
            WithdrawSlider.Value = value;
        }
    }

    private async void AdjustSliderSettings()
    {
        try
        {
            double balance = await viewModel.Balance();
            WithdrawSlider.Maximum = balance;

            WithdrawSlider.Value = 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void ConfirmButtonClicked(object sender, EventArgs e)
    {
        if (amount > 0)
        {
            try
            {
                double.TryParse(runningNumberInput, out double withdrawAmount);
                await viewModel.WithdrawBalance(withdrawAmount);

                double balance = await viewModel.Balance();

                TotalMoney.Text = balance.ToString("C");
                WithdrawAmount.Text = "$0.00";

                AdjustSliderSettings();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            // Now retrieve and use the updated balance
            double updatedBalance = await viewModel.Balance();
        }
    }
}