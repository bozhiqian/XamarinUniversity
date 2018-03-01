using Diary.Entities;
using Prism.Commands;
using Diary.Security;
using Prism.Navigation;
using Xamarin.Auth;

namespace Diary.ViewModels
{
    public class EditorPageViewModel : ViewModelBase
    {
        Account _account;
        private DiaryEntry _entry;
        private string _editorEntry;

        public EditorPageViewModel()
        {
            SaveCommand = new DelegateCommand(Save, () => EditorEntry != null);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            NavigationService.GoBackAsync();
        }

        private void Save()
        {
            if (EditorEntry != null)
            {
                //encrypt
                _entry.CipherText = GetCipherText(EditorEntry);

                if (_entry.CipherText != null)
                    App.Store.SaveEntryAsync(_entry);
            }

            var parameter = new NavigationParameters { { "Account", _account }};
            NavigationService.NavigateAsync("DiaryEntriesPage", parameter);
        }

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public string EditorEntry
        {
            get => _editorEntry;
            set
            {
                SetProperty(ref _editorEntry, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            _account = parameters.GetValue<Account>("Account");
            _entry = parameters.GetValue<DiaryEntry>("Entry");

            if (this._entry.CipherText != null)
            {
                EditorEntry = GetDiaryText(this._entry.CipherText);
            }

            base.OnNavigatedTo(parameters);
        }

        string GetDiaryText(byte[] cipherText)
        {
            //TODO
            return CryptoUtilities.ByteArrayToString(cipherText);
        }

        byte[] GetCipherText(string diaryText)
        {
            //TODO
            return CryptoUtilities.StringToByteArray(diaryText);
        }
    }
}
