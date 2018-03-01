using Prism.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Diary.Entities;
using Prism.Navigation;
using Xamarin.Auth;

namespace Diary.ViewModels
{
    public class DiaryEntriesPageViewModel : ViewModelBase
    {
        Account _account;
        private DiaryEntry _selectedDiaryEntry;

        public DiaryEntriesPageViewModel()
        {
            DeleteCommand = new DelegateCommand(Delete, () => SelectedDiaryEntry != null);
            AddCommand = new DelegateCommand(Add);
            ItemTappedCommand = new DelegateCommand<DiaryEntry>(ItemTapped);
            DiaryEntries = new ObservableCollection<DiaryEntry>();
        }

        private async void ItemTapped(DiaryEntry entry)
        {
            var parameter = new NavigationParameters { { "Account", _account }, { "Entry", entry } };
            await NavigationService.NavigateAsync("NavigationPage/EditorPage", parameter);
        }

        private async void Add()
        {
            var entry = SelectedDiaryEntry ?? new DiaryEntry() { AccountName = _account == null ? "" : _account.Username };
            var parameter = new NavigationParameters { { "Account", _account }, { "Entry", entry } };
            await NavigationService.NavigateAsync("NavigationPage/EditorPage", parameter);
        }

        public ObservableCollection<DiaryEntry> DiaryEntries { get; }

        public DiaryEntry SelectedDiaryEntry
        {
            get => _selectedDiaryEntry;
            set => SetProperty(ref _selectedDiaryEntry, value);
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _account = parameters.GetValue<Account>("Account");
            await LoadDiaryEntries();

            
        }

        private async Task LoadDiaryEntries()
        {
            var diaryEntries = await App.Store.GetEntriesAsync(_account.Username);

            DiaryEntries.Clear();
            foreach (var diaryEntry in diaryEntries)
            {
                DiaryEntries.Add(diaryEntry);
            }
        }

        private async void Delete()
        {
            await App.Store.DeleteEntryAsync(SelectedDiaryEntry);
            await LoadDiaryEntries();
        }

        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand<DiaryEntry> ItemTappedCommand { get; }

    }
}
