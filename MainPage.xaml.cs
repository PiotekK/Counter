using Counter.Models;

namespace Counter
{
    public partial class MainPage : ContentPage
    {
        private CounterDatabase database;
        private List<Models.Counter> counters;

        public MainPage()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadPickerItems();
            LoadCounters();
        }

        private void LoadPickerItems()
        {
            ColorPicker.ItemsSource = new List<string> { "Lightblue", "Lightgreen", "Yellow", "Cyan" };
            ColorPicker.SelectedItem = ColorPicker.ItemsSource[0];
        }

        private async void InitializeDatabase()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "counter.db");
            database = new CounterDatabase(path);
        }

        private async Task SaveCounter(Models.Counter counter)
        {
            if (counter != null)
            {
                await database.SaveCountersAsync(counter);
                LoadCounters();
            }
        }

        private async void LoadCounters()
        {
            counters = await database.GetCountersAsync();
            CollectionViewCounters.ItemsSource = counters;
        }
        private async void OnPlusCounterClicked(object sender, EventArgs e)
        {
            var counter = (Models.Counter)((Button)sender).CommandParameter;
            counter.Value++;
            await SaveCounter(counter);
        }

        private async void OnMinusCounterClicked(object sender, EventArgs e)
        {
            var counter = (Models.Counter)((Button)sender).CommandParameter;
            counter.Value--;
            await SaveCounter(counter);
        }

        private async void OnNewCounterClicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(CounterNameEntry.Text) &&
                int.TryParse(CounterStartValueEntry.Text, out int initialValue))
            {
                var selectedColor = ColorPicker.SelectedItem as string;
                if (selectedColor == null)
                {
                    await DisplayAlert("Error", "Please chose a color of your counter", "OK");
                    return;
                }

                var newCounter = new Models.Counter
                {
                    Name = CounterNameEntry.Text,
                    Value = initialValue,
                    Color = selectedColor,
                    startValue = initialValue
                };

                CounterNameEntry.Text = "";
                CounterStartValueEntry.Text = "";
                LoadPickerItems();

                await SaveCounter(newCounter);
            }
        }

        
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var counter = (Models.Counter)((Button)sender).CommandParameter;
            await database.DeleteCounterAsync(counter);
            LoadCounters();
        }

        private async void OnResetButtonClicked(object sender, EventArgs e)
        {
            var counter = (Models.Counter)((Button)sender).CommandParameter;
            counter.Value = counter.startValue;
            await SaveCounter(counter);
        }

    }

}
