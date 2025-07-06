using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Restful_Client_Auth
{
    /// <summary>
    /// Interaction logic for AuthorForm.xaml
    /// </summary>
    public partial class AuthorForm : Window
    {
        public delegate Task<IEnumerable> AuthorValidateDelegate(Author author);

        public event AuthorValidateDelegate ValidateAuthor;

        public AuthorForm()
        {
            InitializeComponent();

            au_idTextBox.IsReadOnly = false;
            titleLabel.Content = "Add author";
        }

        public AuthorForm(Author author)
        {
            InitializeComponent();

            au_idTextBox.Text = author.Au_id;
            au_fnameTextBox.Text = author.au_fname;
            au_lnameTextBox.Text = author.au_lname;
            phoneTextBox.Text = author.Phone;
            cityTextBox.Text = author.City;
            addressTextBox.Text = author.Address;
            stateTextBox.Text = author.State;
            zipTextBox.Text = author.Zip;

            au_idTextBox.IsReadOnly = true;
            titleLabel.Content = "Edit author";
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            // создание объекта класса Author для сохранения на сервере
            Author author = new Author
            {
                Au_id = au_idTextBox.Text,
                au_fname = au_fnameTextBox.Text,
                au_lname = au_lnameTextBox.Text,
                Phone = phoneTextBox.Text,
                City = cityTextBox.Text,
                Address = addressTextBox.Text,
                State = stateTextBox.Text,
                Zip = zipTextBox.Text,
                Contract = true
            };

            // список ошибок валидации
            IEnumerable errorList = null;

            // если есть обработчик для кнопки Save, то вызвать его
            if (ValidateAuthor != null)
                errorList = await ValidateAuthor(author);

            // если нет ошибок валидации 
            if (errorList == null)
            {
                // закрыть окно
                this.DialogResult = true;
                this.Close();
            }
            else    // есть ошибки валидации
            {
                // очистить поля с описанием ошибок
                foreach (UIElement control in mainGrid.Children)
                {
                    if (control is TextBox)
                    {
                        TextBox currentTb = (TextBox)control;
                        currentTb.Style = null;
                    }

                    if (control is Label)
                    {
                        Label currentLabel = (Label)control;
                        string tag = (string)(currentLabel).Tag;
                        if (tag != null)
                            currentLabel.Content = "";
                    }
                }

                // перебрать все ошибки валидации
                foreach (KeyValuePair<string, object> currentField in errorList)
                {
                    Console.WriteLine(@"Field: {0}", currentField.Key);

                    // избавление от названия таблицы в названии полей для валидации
                    //string[] names = currentField.Key.Split(new char[] { '.'});

                    // перебрать все элементы на панели
                    foreach (UIElement control in mainGrid.Children)
                    {
                        if (control is TextBox)
                        {
                            TextBox currentTb = (TextBox)control;
                            string tag = (string)(currentTb).Tag;

                            if (tag == currentField.Key.ToLower())
                            {
                                Style nonValidStyle = FindResource("NonValidTextBox") as Style;
                                currentTb.Style = nonValidStyle;
                            }
                        }

                        if (control is Label)
                        {
                            Label currentLabel = (Label)control;
                            string tag = (string)(currentLabel).Tag;

                            // если найдена нужная метка, соответсвующая полю для валидации
                            if (tag == currentField.Key.ToLower())
                            {
                                // цикл по ошибкам для конкретного поля
                                foreach (string currentError in (IEnumerable)currentField.Value)
                                {
                                    currentLabel.Content = currentError;
                                    break;
                                }
                            }
                        }
                    }

                    foreach (string currentError in (IEnumerable)currentField.Value)
                    {
                        Console.WriteLine(currentError);
                    }
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
