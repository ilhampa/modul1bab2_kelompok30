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
	public class EditDataMahasiswa : ContentPage
	{
        private ListView _listview;
        private Entry _id;
        private Entry _nama;
        private Entry _jurusan;
        private Button _tombol;

        DataMahasiswa _datamahasiswa = new DataMahasiswa();
        
        String _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db4");

        public EditDataMahasiswa ()
		{
            this.Title = "Edit Data";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listview = new ListView();
            _listview.ItemsSource = db.Table<DataMahasiswa>().OrderBy(x => x.Nama).ToList();
            _listview.ItemSelected += _listview_ItemSelected;
            stackLayout.Children.Add(_listview);

            _id = new Entry();
            _id.Placeholder = "ID";
            _id.IsVisible = false;
            stackLayout.Children.Add(_id);

            _nama = new Entry();
            _nama.Keyboard = Keyboard.Text;
            _nama.Placeholder = "Nama";
            stackLayout.Children.Add(_nama);

            _jurusan = new Entry();
            _jurusan.Keyboard = Keyboard.Text;
            _jurusan.Placeholder = "Jurusan";
            stackLayout.Children.Add(_jurusan);

            _tombol = new Button(); 
            _tombol.Text = "Update";
            _tombol.Clicked += _tombol_Clicked;
            stackLayout.Children.Add(_tombol);
            
            Content = stackLayout;

        }

        private async void _tombol_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            DataMahasiswa datamahasiswa = new DataMahasiswa()
            {
                Id = Convert.ToInt32(_id.Text),
                Nama = _nama.Text,
                Jurusan = _jurusan.Text
            };
            db.Update(datamahasiswa);
            await Navigation.PopAsync();
        }

        private void _listview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _datamahasiswa = (DataMahasiswa)e.SelectedItem;
            _id.Text = _datamahasiswa.Id.ToString();
            _nama.Text = _datamahasiswa.Nama;
            _jurusan.Text = _datamahasiswa.Jurusan;
        }
    }
}