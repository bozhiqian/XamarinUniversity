using Prism.Commands;
using Prism.Navigation;
using System;
using Diary.Events;
using Prism.Events;
using Xamarin.Auth;

namespace Diary.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _userName;
        private string _password;
        public event Action<Account> LoginSucceeded;

        public MainPageViewModel(AccountManager accountManager, INavigationService navigationService, IEventAggregator eventAggregator)
            : base(accountManager, navigationService, eventAggregator)
        {
            Title = "Course ENT170 - Securing Local Data";

            LoginCommand = new DelegateCommand(Login, () => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password));
            CreateAccountCommand = new DelegateCommand(CreateAccount, () => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password));

            LoginSucceeded = async (account) =>
            {
                var parameter = new NavigationParameters { { "Account", account } };
                await NavigationService.NavigateAsync("DiaryEntriesPage", parameter);
            };
        }


        public string UserName
        {
            get => _userName;
            set
            {
                SetProperty(ref _userName, value);
                LoginCommand.RaiseCanExecuteChanged();
                CreateAccountCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                LoginCommand.RaiseCanExecuteChanged();
                CreateAccountCommand.RaiseCanExecuteChanged();
            }
        }

        private void Login()
        {
            if (AccountManager.LoginToAccount(UserName, Password) == false)
            {
                EventAggregator.GetEvent<AlertEvent>().Publish(
                    new AlertEventArgs("Unable to login, please check your username and password")
                    {
                        Title = "Login failed",
                        Cancel = "OK"
                    });
            }
            else
            {
                LoginSucceeded?.Invoke(AccountManager.GetAccount(UserName));
                Password = string.Empty;
                UserName = string.Empty;
            }
        }

        private void CreateAccount()
        {
            if (AccountManager.CreateAndSaveAccount(UserName, Password) == true)
            {
                LoginSucceeded?.Invoke(AccountManager.GetAccount(UserName));
            }
            else
            {
                EventAggregator.GetEvent<AlertEvent>().Publish(
                    new AlertEventArgs("Unable to create a new account - does this account already exist?")
                    {
                        Title = "Create account Failed",
                        Cancel = "OK"
                    });
            }

        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand CreateAccountCommand { get; }

    }
}
