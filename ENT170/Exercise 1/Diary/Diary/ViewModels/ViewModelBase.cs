using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;

namespace Diary.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected AccountManager AccountManager { get; }
        private string _title;

        public ViewModelBase() : this(null, null, null)
        {

        }
        public ViewModelBase(AccountManager accountManager, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            if (NavigationService == null && navigationService != null)
            {
                NavigationService = navigationService;
            }

            if (accountManager != null)
            {
                AccountManager = accountManager;
            }

            if (EventAggregator == null && eventAggregator != null)
            {
                EventAggregator = eventAggregator;
            }
        }

        protected static INavigationService NavigationService { get; set; }
        public IEventAggregator EventAggregator { get; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public virtual void Destroy()
        {
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}