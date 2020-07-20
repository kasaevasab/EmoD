using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmoDia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiariesPage : ContentPage
    {
        public DiariesPage()
        {
            Title = "Дневники";

            InitializeComponent();
        }

        /*Каждый из этих методов привязывается к одной из кнопок.
        Работают они следующим образом: в каждом методе в качестве параметра в конструкторе следующей страницы определяется ключ, по которому с помощью простой конструкции switch определяется функционал следующей страницы.
        CalendarPage не имеет собственного xaml файла, внешний вид этой страницы определяется целиком на C# в файле CalendarPage.cs.*/
        private async void PushEmotionalDiary(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage("Emotional Diary"));
        }

        private async void PushThankDiary(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage("Thank Diary"));
        }

        private async void PushPersonalDiary(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage("Personal Diary"));
        }
    }
}