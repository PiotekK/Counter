using System;

namespace Counter;

public partial class CreateNewCounter : ContentPage
{

    public event EventHandler<string> CreateNewCounterAction;
	public CreateNewCounter()
	{
		InitializeComponent();
	}



    private async void OnNewCounterButtonClicked(object sender, EventArgs e)
    {

        if (!string.IsNullOrWhiteSpace(CounterNameEntry.Text) &&
                int.TryParse(CounterStartValueEntry.Text, out int initialValue))
        {
            var selectedColor = CounterColorEntry.SelectedItem as string;
            if (selectedColor == null)
            {
                await DisplayAlert("B??d", "Prosz? wybra? kolor.", "OK");
                return;
            }

        }


    }
}