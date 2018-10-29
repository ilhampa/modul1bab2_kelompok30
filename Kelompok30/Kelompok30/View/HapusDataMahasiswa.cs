using KelompokXX.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kelompok30.View
{
    public class HapusDataMahasiswa : ContentPage
    {
        private ListView _listview;
        private Button _tombol;

        DataMahasiswa _datamahasiswa = new DataMahasiswa();

        String _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db4");

        public HapusDataMahasiswa()
        {
            this.Title = "Edit Data";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listview = new ListView();
            _listview.ItemsSource = db.Table<DataMahasiswa>().OrderBy(x => x.Nama).ToList();
            _listview.ItemSelected += _listview_ItemSelected;
            stackLayout.Children.Add(_listview);

            _tombol = new Button();
            _tombol.Text = "Hapus Data";
            _tombol.Clicked += _tombol_Clicked;
            stackLayout.Children.Add(_tombol);

            Content = stackLayout;
        }
         private void _listview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _datamahasiswa = (DataMahasiswa)e.SelectedItem;
        }

        private async void _tombol_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.Table<DataMahasiswa>().Delete(X => X.Id == _datamahasiswa.Id);
            await Navigation.PopAsync();
        }
    }
}