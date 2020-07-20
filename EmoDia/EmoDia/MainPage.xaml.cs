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
        public MainPage()
        {
            InitializeComponent();
        }


        //Метод привязывается к кнопке "начать" и позволяет переключиться на страницу функционала. Для этого в App.xaml.cs MainPage определяется как NavigationPage.
        private async void BeginButtonClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new DiariesPage());
        }
    }
}