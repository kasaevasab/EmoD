using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;

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

        Button regButton = new Button
        {
            Text = "Зарегистрироваться",
            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
            BackgroundColor = Color.White,
            BorderColor = Color.FromRgb(114, 161, 255),
            TextColor = Color.FromRgb(114, 161, 255),
            CornerRadius = 70,
            BorderWidth = 5,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };

        Frame loginAndRegistrationFrame = new Frame { BorderColor = Color.Blue, BackgroundColor = Color.White, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.CenterAndExpand, WidthRequest = 220, HeightRequest = 200 };

        //Визуальные элементы, которые будут содержимым страницы при регистрации пользователя. Поля для ввода пароля, логина и кода, описание функции кода гостя и кнопка "Войти".
        Entry nameCreate = new Entry() { Placeholder = "Введите имя", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)), BackgroundColor = Color.White, TextColor = Color.FromRgb(114, 161, 255) };
        Entry surnameCreate = new Entry() { Placeholder = "Введите фамилию", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)), BackgroundColor = Color.White, TextColor = Color.FromRgb(114, 161, 255) };
        Entry mailCreate = new Entry() { Placeholder = "Введите почту", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)), BackgroundColor = Color.White, TextColor = Color.FromRgb(114, 161, 255) };
        Entry passwordCreate = new Entry() { Placeholder = "Введите пароль", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)), BackgroundColor = Color.White, TextColor = Color.FromRgb(114, 161, 255), IsPassword = true };
        Entry codeCreate = new Entry() { Placeholder = "Введите код гостя", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)), BackgroundColor = Color.White, TextColor = Color.FromRgb(114, 161, 255), IsPassword = true };
        Label hintCode = new Label { Text = "Гости - пользователи, которые могут читать дневник, но не могут его изменить. Заходить в ваш дневник гости могут с помощью кода.", FontSize = 13, TextColor = Color.FromRgb(114, 161, 255) };
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
            loginAndRegistrationFrame.HeightRequest = 400;
            loginAndRegistrationFrame.Content = new StackLayout { Children = { nameCreate, surnameCreate, mailCreate, passwordCreate, codeCreate, hintCode, signInButton } };

            regButton.Clicked += registration;

            StackLayout stackLayout = new StackLayout() { Children = { appTitle, appIcon, loginAndRegistrationFrame, regButton }, BackgroundColor = Color.White };

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
            var obj1 = new
            {
                query = $@"SELECT id FROM Accounts WHERE userMail='{mailEntry.Text}' AND userPassword = '{passwordEntry.Text}';"
            };

            string URL = "https://searchuserfunc.azurewebsites.net/api/Function1?code=ZhPtLGHcnujFpmS4mvOufST8C63u9DtGCqk8UCui/NzspWMqyK1GGw==";
            using (var client = new HttpClient())
            {
                var payload = JsonConvert.SerializeObject(obj1);
                var content = new StringContent(payload, Encoding.UTF8, @"application/json");

                var response = await client.PostAsync(URL, content);
                Globals.curUserId = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode && Globals.curUserId != "")
                {
                    await Navigation.PushAsync(new DiariesPage());
                }
                else if (Globals.curUserId == "")
                {
                    await DisplayAlert("Упс...", "Учетная запись не найдена.", "Закрыть");
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await DisplayAlert("Упс...", "Что-то пошло не так.", "Закрыть");
                    await Navigation.PushAsync(new MainPage());
                }
            }
        }

        async void registration(object Sender, EventArgs e)
        {
            var obj2 = new
            {
                query = $@"SELECT id FROM Accounts WHERE userMail = '{mailCreate.Text}'; "
            };
            string URL1 = "https://searchuserfunc.azurewebsites.net/api/Function1?code=ZhPtLGHcnujFpmS4mvOufST8C63u9DtGCqk8UCui/NzspWMqyK1GGw==";
            using (var client = new HttpClient())
            {
                var payload = JsonConvert.SerializeObject(obj2);
                var content = new StringContent(payload, Encoding.UTF8, @"application/json");

                var response = await client.PostAsync(URL1, content);
                Globals.curUserId = await response.Content.ReadAsStringAsync();
                if (Globals.curUserId == "")
                {
                    string URL2 = "https://addtoaccdb.azurewebsites.net/api/AddToAccounts";

                    var obj = new
                    {
                        name = nameCreate.Text,
                        surname = surnameCreate.Text,
                        code = codeCreate.Text,
                        mail = mailCreate.Text,
                        password = passwordCreate.Text,
                        birthday = ""
                    };

                    using (var client1 = new HttpClient())
                    {
                        try
                        {
                            var payload1 = JsonConvert.SerializeObject(obj);
                            var content1 = new StringContent(payload1, Encoding.UTF8, @"application/json");

                            var response1 = await client1.PostAsync(URL2, content1);
                            if (response1.IsSuccessStatusCode)
                            {
                                await DisplayAlert("Ваша учетная запись создана", $"", "OK");
                                await Navigation.PushAsync(new MainPage());
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                        }
                        catch
                        {
                            await DisplayAlert("Упс...", "Что-то пошло не так, Ваша учетная запись не создана.", "Закрыть");
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Упс...", "К данному почтовому адресу уже привязана учетная запись.", "Закрыть");
                }
            }
        }
    }
}