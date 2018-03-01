using Diary.Data;
using Diary.DependencyService;
using Diary.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Diary
{
    public partial class App : PrismApplication
    {
        private static DiaryEntryStore _store;

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        public static string DatabaseFolder { get; set; }

        public static DiaryEntryStore Store => _store ?? (_store = new DiaryEntryStore(
                                                                 Xamarin.Forms.DependencyService.Get<IFileHelper>().GetLocalFilePath("DiaryEntries.db3")));
        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<DiaryEntriesPage>();
            containerRegistry.RegisterForNavigation<EditorPage>();

            containerRegistry.Register<AccountManager>();
        }


    }
}