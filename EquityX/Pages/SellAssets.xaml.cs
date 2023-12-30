namespace EquityX.Pages;

public partial class SellAssets : ContentPage
{
    string runningNumberInput = "";
	public SellAssets()
	{
		InitializeComponent();

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
        // Get the current selected stocks value
        // Slider value needs to be multiplied by current stock price
        // Display multiplied value in the currency section
        double stockValue = 4.67;
		double value = Math.Round(args.NewValue, MidpointRounding.ToEven);

        runningNumberInput = value.ToString();
		ShareStockAmount.Text = value.ToString();
        ShareSellAmount.Text = "$" + (value * stockValue).ToString();
	}

    private void ClearButtonClicked(object sender, EventArgs e)
    {
        SellSlider.Value = 0;
        runningNumberInput = "";
    }

    private void DelButtonClicked(object sender, EventArgs e)
    {
        if (SellSlider.Value >= 10)
        {
            int currentSliderValue = (int)SellSlider.Value;
            int newValue = currentSliderValue / 10; // Remove the last digit
            runningNumberInput = newValue.ToString();
            SellSlider.Value = newValue;
        }
        else
        {
            SellSlider.Value = 0;
        }
    }

    private void NumberButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            runningNumberInput += button.Text; // Append the button's number to the current input
            UpdateSliderValue();
        }
    }

    private void UpdateSliderValue()
    {
        if (double.TryParse(runningNumberInput, out double value))
        {
            value = Math.Min(value, SellSlider.Maximum); // Ensure value does not exceed maximum
            value = Math.Max(value, SellSlider.Minimum); // Ensure value is not below minimum
            SellSlider.Value = value;
        }
    }
}