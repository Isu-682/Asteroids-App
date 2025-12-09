using Asteroids_App.Models;
using Asteroids_App.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Asteroids_App.ViewModels
{
    public class NeoViewModel : INotifyPropertyChanged
    {
        private readonly NeoService _neoService;
        public ObservableCollection<Neo> Neos { get; set; } = new ObservableCollection<Neo>();

        public NeoViewModel()
        {
            _neoService = new NeoService();
            LoadNeos();
        }

        private async void LoadNeos()
        {
            var list = await _neoService.GetNeoFeedAsync(DateTime.Now.ToString("yyyy-MM-dd"),
                                                        DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            foreach (var neo in list)
            {
                Neos.Add(neo);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
