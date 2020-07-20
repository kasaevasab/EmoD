using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace EmoDia
{
    public class CalendarPage : ContentPage
    {
        public CalendarPage(string titleHolder)
        {
            //Заголовок, внешний вид и функционал страницы меняется в зависимости от выбранной кнопки на страницу DiariesPage.
            //Содержимое всех страниц состоит из контейнера ScrollView, предоставляющего пользователю возможность их скроллить.
            switch (titleHolder)
            {
                case "Emotional Diary":
                    Title = "Дневник эмоций";

                    //Весь кейс Emotional Diary идентичен методу layoutWithoutHints. Чтобы просто вызвать метод и не повторяться в коде, пришлось бы создать бесполезный объект класса EventArgs, что проблематично.
                    ScrollView EDScrollView = new ScrollView();

                    //В данный момент календарь нужен для красоты :). Но позже нужно будет создать БД для взаимодействия с датами.
                    DatePicker EDDatePicker = new DatePicker { Format = "D" };

                    Label labelTime = new Label { Text = "Время" };
                    Label labelSituation = new Label { Text = "Ситуация" };
                    Label labelReaction = new Label { Text = "Реакция" };
                    Label labelEmotion = new Label { Text = "Эмоция" };
                    Label labelTrigger = new Label { Text = "Триггеры" };
                    Label labelActualEmotion = new Label { Text = "Нынешние эмоции" };
                    Label labelRethinking = new Label { Text = "Переосмысление" };
                    Label labelConclusion = new Label { Text = "Выводы" };

                    Editor editorSituation = new Editor { AutoSize = EditorAutoSizeOption.TextChanges };
                    Editor editorReaction = new Editor { AutoSize = EditorAutoSizeOption.TextChanges };
                    Editor editorEmotion = new Editor { AutoSize = EditorAutoSizeOption.TextChanges };
                    Editor editorTrigger = new Editor { AutoSize = EditorAutoSizeOption.TextChanges };
                    Editor editorActualEmotion = new Editor { AutoSize = EditorAutoSizeOption.TextChanges };
                    Editor editorRethinking = new Editor { AutoSize = EditorAutoSizeOption.TextChanges };
                    Editor editorConclusion = new Editor { AutoSize = EditorAutoSizeOption.TextChanges };

                    Button showHintsButton = new Button { Text = "?", BackgroundColor = Color.White, TextColor = Color.Blue, HorizontalOptions = LayoutOptions.End };
                    Button hideHintsButton = new Button { Text = "?", BackgroundColor = Color.Blue, TextColor = Color.White, HorizontalOptions = LayoutOptions.End };

                    TimePicker timePicker = new TimePicker() { Time = new TimeSpan() };

                    StackLayout EDStackLayout = new StackLayout
                    {
                        Children = { EDDatePicker,
                        labelTime, timePicker,
                        labelSituation, editorSituation,
                        labelReaction, editorReaction,
                        labelEmotion, editorEmotion,
                        labelTrigger, editorTrigger,
                        labelActualEmotion, editorActualEmotion,
                        labelRethinking, editorRethinking,
                        labelConclusion, editorConclusion,
                        showHintsButton }
                    };

                    showHintsButton.Clicked += delegate (object Sender, EventArgs e)
                    {
                        labelTime.Text = "Время (примерное время, когда вы столкнулись с этой ситуацией)";
                        labelSituation.Text = "Ситуация (описание ситуации)";
                        labelReaction.Text = "Реакция (как вы отреагировали на ситуацию)";
                        labelEmotion.Text = "Эмоция (какую эмоцию испытали от ситуации)";
                        labelTrigger.Text = "Триггеры (что именно вызвало эмоции)";
                        labelActualEmotion.Text = "Нынешние эмоции (что испытываете при написании записи)";
                        labelRethinking.Text = "Переосмысление (как вы сейчас видите эту ситуацию)";
                        labelConclusion.Text = "Выводы (какие уроки вынесли для себя из ситуации)";

                        EDStackLayout.Children.Remove(showHintsButton);
                        EDStackLayout.Children.Add(hideHintsButton);
                    };
                    hideHintsButton.Clicked += delegate (object Sender, EventArgs e)
                    {
                        labelTime.Text = "Время";
                        labelSituation.Text = "Ситуация";
                        labelReaction.Text = "Реакция";
                        labelEmotion.Text = "Эмоция";
                        labelTrigger.Text = "Триггеры";
                        labelActualEmotion.Text = "Нынешние эмоции";
                        labelRethinking.Text = "Переосмысление";
                        labelConclusion.Text = "Выводы";

                        EDStackLayout.Children.Remove(hideHintsButton);
                        EDStackLayout.Children.Add(showHintsButton);
                    };

                    EDScrollView.Content = EDStackLayout;

                    Content = EDScrollView;

                    break;
                case "Thank Diary":
                    Title = "Дневник благодарностей";

                    //Поле нужно для нумерования спика.
                    int counter = 1;
                    //В список будут добавляться все создаваемые пользователем поля для ввода в дневник. При этом он предоставит возможность обращаться к каждому полю в индивидуальном порядке.
                    List<Editor> entryLine = new List<Editor>() { new Editor { AutoSize = EditorAutoSizeOption.TextChanges, Text = $"{counter}. " } };

                    ScrollView TDScrollView = new ScrollView();

                    StackLayout TDStackLayout = new StackLayout();

                    DatePicker TDDatePicker = new DatePicker { Format = "D" };

                    //Кнопка addLine позволяет создать новые строки, чтобы появилась возможность схематично благодарить нескольких персон.
                    Button addLine = new Button { Text = "Добавить строку" };

                    TDStackLayout.Children.Add(TDDatePicker);
                    TDStackLayout.Children.Add(entryLine[counter - 1]);
                    TDStackLayout.Children.Add(addLine);

                    addLine.Clicked += delegate (object sender, EventArgs e)
                    {
                        counter++;
                        entryLine.Add(new Editor { AutoSize = EditorAutoSizeOption.TextChanges, Text = $"{counter}. " });

                        TDStackLayout.Children.Remove(addLine);
                        TDStackLayout.Children.Add(entryLine[counter - 1]);
                        TDStackLayout.Children.Add(addLine);
                    };

                    TDScrollView.Content = TDStackLayout;

                    Content = TDScrollView;

                    break;
                case "Personal Diary":
                    //Практически персональный дневник будет состоять из поля для заголовка, в котором можно охарактеризовать в двух словах прошедший день, а также поля для описания прошедшего дня.
                    Title = "Личный дневник";

                    ScrollView PDScrollView = new ScrollView();

                    StackLayout PDStackLayout = new StackLayout();

                    DatePicker PDDatePicker = new DatePicker { Format = "D" };

                    Entry entryHeader = new Entry { Placeholder = "Введите заголовок" };
                    Editor entryNote = new Editor { AutoSize = EditorAutoSizeOption.TextChanges };

                    PDStackLayout.Children.Add(PDDatePicker);
                    PDStackLayout.Children.Add(entryHeader);
                    PDStackLayout.Children.Add(entryNote);

                    PDScrollView.Content = PDStackLayout;

                    Content = PDScrollView;

                    break;
            }
        }
    }
}