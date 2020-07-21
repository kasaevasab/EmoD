using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmoDia
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //Визуальные элементы, которые будут содержимым страницы при любом режиме. Фрейм для ввода данных пользователя, заголовок и кнопка "Начать".
        Label appTitle = new Label { Text = "EmoDia", Margin = new Thickness(15, 25, 15, 5), TextColor = Color.FromRgb(114, 161, 255), FontSize = 70, HorizontalOptions = LayoutOptions.Center };
        Button beginButton = new Button
        {
            Text = "Начать",
            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
            BackgroundColor = Color.White,
            BorderColor = Color.FromRgb(114, 161, 255),
            TextColor = Color.FromRgb(114, 161, 255),
            CornerRadius = 70,
            BorderWidth = 5,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };
        Frame loginAndRegistrationFrame = new Frame { BorderColor = Color.Blue, BackgroundColor = Color.White, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.CenterAndExpand, WidthRequest = 220, HeightRequest = 145 };

        //Визуальные элементы, которые будут содержимым страницы при регистрации пользователя. Поля для ввода пароля, логина и кода, описание функции кода гостя и кнопка "Войти".
        Entry loginCreate = new Entry() { Placeholder = "Введите логин", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)), BackgroundColor = Color.White, TextColor = Color.FromRgb(114, 161, 255) };
        Entry passwordCreate = new Entry() { Placeholder = "Введите пароль", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)), BackgroundColor = Color.White, TextColor = Color.FromRgb(114, 161, 255), IsPassword = true };
        Entry codeCreate = new Entry() { Placeholder = "Введите код гостя", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)), BackgroundColor = Color.White, TextColor = Color.FromRgb(114, 161, 255), IsPassword = true };
        Label hintCode = new Label { Text = "Гости - пользователи, которые могут читать дневник, но не могут его изменить. Заходить в ваш дневник гости могут с помощью кода.", FontSize = 15, TextColor = Color.FromRgb(114, 161, 255) };
        Button signInButton = new Button { Text = "Войти", FontSize = 9, BackgroundColor = Color.White, TextColor = Color.FromRgb(114, 161, 255), HorizontalOptions = LayoutOptions.Start };

        public MainPage()
        {
            Title = "Вход";

            beginButton.Clicked += BeginButtonClicked;

            signInButton.Clicked += SignInButtonClicked;

            InitializeComponent();
        }

        private void RegistrationButtonClicked(object sender, EventArgs e)
        {
            loginAndRegistrationFrame.HeightRequest = 310;
            loginAndRegistrationFrame.Content = new StackLayout { Children = { loginCreate, passwordCreate, codeCreate, hintCode, signInButton } };

            StackLayout stackLayout = new StackLayout() { Children = {appTitle, appIcon, loginAndRegistrationFrame, beginButton } };

            ScrollView scrollView = new ScrollView();

            scrollView.Content = stackLayout;

            Content = scrollView;
        }

        private async void SignInButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        //Метод привязывается к кнопке "начать" и позволяет переключиться на страницу функционала. Для этого в App.xaml.cs MainPage определяется как NavigationPage.
        private async void BeginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DiariesPage());
        }
    }
}