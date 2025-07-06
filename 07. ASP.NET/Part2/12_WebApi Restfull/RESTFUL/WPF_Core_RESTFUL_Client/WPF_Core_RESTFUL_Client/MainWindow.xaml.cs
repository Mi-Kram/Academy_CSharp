using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Core_RESTFUL_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string BASE_ADDRESS = "https://localhost:44391/";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GetAuthors_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // производит асинхронный запрос к WebAPI при помощи JSON
                HttpClient client = new HttpClient();

                // адрес API, к которому происходит обращение
                client.BaseAddress = new Uri(BASE_ADDRESS);

                // указать JSON в качестве заголовка
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // получить ответ от сервера
                HttpResponseMessage response = await client.GetAsync("api/authors");

                // если статус запроса - Success
                if (response.IsSuccessStatusCode)
                {
                    // получить ответ в формате JSON
                    var json = await response.Content.ReadAsStringAsync();

                    var authors = new JavaScriptSerializer().Deserialize<IEnumerable<Author>>(json);

                    //var result = from t in authors.ToList()
                    //             select t;

                    var result = authors.ToList();

                    ObservableCollection<Author> observableCollection = new ObservableCollection<Author>(result);

                    CollectionViewSource collection = new CollectionViewSource() { Source = observableCollection };
                    collection.GroupDescriptions.Add(new PropertyGroupDescription("State"));

                    dataGrid1.ItemsSource = collection.View;
                }
                else
                {
                    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Network failure: {0}", ex.Message));
            }
        }

        private async Task<IEnumerable> AddAuthorWindow_ValidateAuthor(Author author)
        {
            // объект для связи с сервером
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_ADDRESS);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = new JavaScriptSerializer().Serialize(author);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/authors", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                // получить ответ от сервера
                string content = await response.Content.ReadAsStringAsync();

                // десериализовать контейнер ошибок
                IEnumerable errorObj = (IEnumerable)new JavaScriptSerializer().DeserializeObject(content);

                //return (IEnumerable)errorObj;

                // перебрать записи в контейнере ошибок
                foreach (KeyValuePair<string, object> currentRecord in errorObj)
                {
                    // осли обнаружен список ошибок валидации
                    if (currentRecord.Key == "errors")
                    {
                        // возвратить список ошибок валидации в диалоговое окно
                        return (IEnumerable)currentRecord.Value;
                    }
                }
            }

            return null;
        }

        private void PostAuthor_Click(object sender, RoutedEventArgs e)
        {
            // создание окна добавления автора
            AuthorForm addAuthorWindow = new AuthorForm();
            addAuthorWindow.Owner = this;

            // добавить обработчик для кнопки Save в окне ввода данных, который связывается с сервером (принимает объект Author, возвращает список ошибок)
            addAuthorWindow.ValidateAuthor += AddAuthorWindow_ValidateAuthor;

            // показ диалогового окна
            bool? b = addAuthorWindow.ShowDialog();
            //MessageBox.Show(b.Value.ToString());

            // показать обновлённых авторов в окне
            GetAuthors_Click(null, null);
        }

        private async Task<IEnumerable> EditAuthorWindow_ValidateAuthor(Author author)
        {
            // объект для связи с сервером
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_ADDRESS);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = new JavaScriptSerializer().Serialize(author);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            //var response = await client.PostAsync("api/authors", httpContent);
            var response = await client.PutAsync("api/authors/" + author.Au_id, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                // получить ответ от сервера
                string content = await response.Content.ReadAsStringAsync();

                // десериализовать контейнер ошибок
                IEnumerable errorObj = (IEnumerable)new JavaScriptSerializer().DeserializeObject(content);

                //return (IEnumerable)errorObj;

                if(errorObj != null)
                {
                    // перебрать записи в контейнере ошибок
                    foreach (KeyValuePair<string, object> currentRecord in errorObj)
                    {
                        // осли обнаружен список ошибок валидации
                        if (currentRecord.Key == "errors")
                        {
                            // возвратить список ошибок валидации в диалоговое окно
                            return (IEnumerable)currentRecord.Value;
                        }
                    }
                }
            }

            return null;
        }

        private void PutAuthor_Click(object sender, RoutedEventArgs e)
        {
            Author author = (Author)dataGrid1.SelectedItem;
            author.Contract = true;

            // создание окна редактирования автора
            AuthorForm editAuthorWindow = new AuthorForm(author);
            editAuthorWindow.Owner = this;

            // добавить обработчик для кнопки Save в окне ввода данных, который связывается с сервером (принимает объект Author, возвращает список ошибок)
            editAuthorWindow.ValidateAuthor += EditAuthorWindow_ValidateAuthor;

            // показ диалогового окна
            bool? b = editAuthorWindow.ShowDialog();

            // показать обновлённых авторов в окне
            GetAuthors_Click(null, null);
        }

        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            Author author = (Author)dataGrid1.SelectedItem;

            if (author != null)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BASE_ADDRESS);

                var url = "api/authors/" + author.Au_id;

                HttpResponseMessage response = client.DeleteAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Author deleted");

                    // показать обновлённых авторов в окне
                    GetAuthors_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
        }
    }
}
