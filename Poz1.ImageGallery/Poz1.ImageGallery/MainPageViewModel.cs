using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Poz1.ImageGallery
{
    class MainPageViewModel : INotifyPropertyChanged
    {

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }
     
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> PhotoList { get; set; }

        private int _currentPhoto;
        public int CurrentPhoto
        {
            get { return _currentPhoto; }
            set
            {
                if (value != _currentPhoto)
                {
                    _currentPhoto = value;
                    OnPropertyChanged();
                }
            }
        }
    
        public ICommand CurrentPhotoChangedCommand { get { return new Command<int>(i => CurrentPhoto = i + 1); }}
        public MainPageViewModel()
        {
            PhotoList = new ObservableCollection<string>();

            Title = "KLOOF ROAD HOUSE";
            Text = "Located at the foot of a nature reserve in Bedfordview, Johannesburg, Kloof Road House is the latest project by Nico van der Meulen Architects. " +
                   "The client's brief called for a family orientated home suitable for indoor/outdoor entertainment that maximizes the views to the north. " +
                   "The result is a 1100m² sculptural piece of architecture that is an extreme transformation from the previously modest single story. " +
                   "With every room in the house opening outdoors, linking the home with the landscaped garden, indoor/ outdoor living is guaranteed. " +
                   "Werner van der Meulen used morphed steels forms that wrap around and frame the structure by the use of parasitic architecture. " +
                   "From the street, the boldly designed off-shutter boundary wall with black steel shapes creeping over predicts that this is no ordinary piece of architecture.";

            GetPhotos();
        }

        private async Task GetPhotos()
        {
            using (var client = new HttpClient())
            {
                for (int i = 1; i <= 45; i++)
                {
                    var image = string.Empty;

                    if (i < 10)
                        image = "https://image.architonic.com/imgArc/project-1/4/5205178/kloof-rd-by-nico-van-der-meulen-architects-0" + i + ".jpg";
                    else
                        image = "https://image.architonic.com/imgArc/project-1/4/5205178/kloof-rd-by-nico-van-der-meulen-architects-" + i + ".jpg";

                    //Check if the image exist
                    var exist = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, image));
                    if (exist.IsSuccessStatusCode)
                        PhotoList.Add(image);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
