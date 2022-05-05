using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocaDatabaseTutorial2
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();

        }
        //every time the user interface shown to the user, this method will be executed!
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPeopleAsync();
            await DisplayAlert("Alert", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "people.db3"), "OK");
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(nameEntry.Text) && !string.IsNullOrEmpty(ageEntry.Text))
            {
                Person person = new Person{ Name = nameEntry.Text,Age =  Convert.ToInt32( ageEntry.Text) };
                await App.Database.SavePersonAsync(person);
                nameEntry.Text = ageEntry.Text = String.Empty;
                await DisplayAlert("Alert", "A record has been saved!", "OK");
                //Refresh the list again
                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
            }
        }

        private void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person selectedItem = e.CurrentSelection[0] as Person;
            DisplayAlert("Alert", $"You have selected {selectedItem.Name}", "Ok");
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            //Get the Person user has selected
            Person selectedItem =collectionView.SelectedItem as Person;
            //Delete this object from the databaase
            await App.Database.DeletePersonAsync(selectedItem);
            //Refresh the list again
            collectionView.ItemsSource = await App.Database.GetPeopleAsync();

        }
    }
}
