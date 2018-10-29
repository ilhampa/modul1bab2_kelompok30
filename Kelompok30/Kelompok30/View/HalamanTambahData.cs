using Kelompok30.Model;
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
    public class HalamanTambahData : ContentPage
    {
        private Entry _nama;
        private Entry _jurusan;
        private Button _simpan;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db4");

        public HalamanTambahData()
        {
            this.Title = "Tambah Data Mahasiswa";

            StackLayout stackLayout = new StackLayout();

            _nama = new Entry();
            _nama.Keyboard = Keyboard.Text;
            _nama.Placeholder = "Nama Mahasiswa";
            stackLayout.Children.Add(_nama);

            _jurusan = new Entry();
            _jurusan.Keyboard = Keyboard.Text;
            _jurusan.Placeholder = "Jurusan";
            stackLayout.Children.Add(_jurusan);

            _simpan = new Button();
            _simpan.Text = "Tambah";
            _simpan.Clicked += _simpan_Clicked;
            stackLayout.Children.Add(_simpan);


            Content = stackLayout;

        }

        private async void _simpan_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<DataMahasiswa>();

            var maxPk = db.Table<DataMahasiswa>().OrderByDescending(c => c.Id).FirstOrDefault();

            DataMahasiswa dbm = new DataMahasiswa()
            {
                Id = (maxPk == null ? 1 : maxPk.Id + 1),
                Nama = _nama.Text,
                Jurusan = _jurusan.Text
            };

            db.Insert(dbm);
            await DisplayAlert(null, "Data " + dbm.Nama + " Berhasil Disimpan", "Ok");
            await Navigation.PopAsync();
        }
    }
}
