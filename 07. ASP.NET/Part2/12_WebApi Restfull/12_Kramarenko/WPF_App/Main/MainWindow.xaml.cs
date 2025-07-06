using Main.Models;
using Main.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;
using System.Web.Script.Serialization;
using System.Net;
using System.Security.Authentication;

namespace Main
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<ServerPublisher> publishers = new List<ServerPublisher>();
		HttpClient client = null;

		public MainWindow()
		{
			InitializeComponent();
			client = new HttpClient();
			client.BaseAddress = new Uri(Properties.Resources.TitlesBaseAddress);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

			Task<HttpResponseMessage> PublishersResponse = client.GetAsync("api/TitlesApi/publishers");
			PublishersResponse.Wait();

			if (PublishersResponse.Result.IsSuccessStatusCode)
			{
				Task<string> content = PublishersResponse.Result.Content.ReadAsStringAsync();
				content.Wait();

				publishers = new JavaScriptSerializer().Deserialize<List<ServerPublisher>>(content.Result);
			}
			else
			{
				MessageBox.Show("Ошибка подключения!\nПрограмма будет закрыта!");
				this.Close();
			}

			Index page = CreateIndexPage();
			MainBorder.Child = page;
		}


		#region IndexPage

		private Index CreateIndexPage()
		{
			Index page = new Index();
			ServerOnPublisherTitles titlesPagging = null;

			Task<HttpResponseMessage> TitlesPaggingResponse = client.GetAsync("api/TitlesApi/pub/0");
			TitlesPaggingResponse.Wait();

			if (TitlesPaggingResponse.Result.IsSuccessStatusCode)
			{
				Task<string> titlesPaggingStr = TitlesPaggingResponse.Result.Content.ReadAsStringAsync();
				titlesPaggingStr.Wait();
				titlesPagging = new JavaScriptSerializer().Deserialize<ServerOnPublisherTitles>(titlesPaggingStr.Result);
			}
			else
			{
				MessageBox.Show("Не удалось получить данные с сервера!\nПриложение будет закрыто!");
				Dispatcher.Invoke(() => this.Close());
			}

			page.UpdatePublishers(publishers);

			List<TitleItem> titleLst = new List<TitleItem>();
			foreach (var item in titlesPagging.titles)
			{
				double? price = null;
				if (double.TryParse(item.price, out double _price)) price = _price;

				titleLst.Add(new TitleItem()
				{
					ID = item.id,
					Title = item.title,
					Price = price,
					PubName = item.pubName
				});
			}
			page.UpdateTitles(titleLst);
			int paggingCnt = titlesPagging.paggingCnt.HasValue ? titlesPagging.paggingCnt.Value : 1;
			page.UpdatePagging(paggingCnt);

			page.OnPublisherChanged += IndexPage_OnPublisherChanged;
			page.OnPaggingChanged += IndexPage_OnPaggingChanged;
			page.OnAddClicked += IndexPage_OnAddClicked;
			page.OnEditClick += IndexPage_OnEditClick;
			page.OnDeleteClick += IndexPage_OnDeleteClick;

			return page;
		}

		private bool IndexPage_OnDeleteClick(string id)
		{
			Task<HttpResponseMessage> resultResponse = client.DeleteAsync($"api/TitlesApi/{id}");
			resultResponse.Wait();

			if (resultResponse.Result.IsSuccessStatusCode)
			{
				try
				{
					Task<string> content = resultResponse.Result.Content.ReadAsStringAsync();
					content.Wait();

					string resID = new JavaScriptSerializer().Deserialize<string>(content.Result);
					if (id == resID) return true;
				}
				catch { }
			}
			return false;
		}

		private void IndexPage_OnEditClick(string id)
		{
			Task<HttpResponseMessage> resultResponse = client.GetAsync($"api/TitlesApi/{id}");
			resultResponse.Wait();

			if (resultResponse.Result.IsSuccessStatusCode)
			{
				Task<string> content = resultResponse.Result.Content.ReadAsStringAsync();
				content.Wait();

				ServerTitle serverTitle = new JavaScriptSerializer().Deserialize<ServerTitle>(content.Result);

				TitleFormModel model = new TitleFormModel()
				{
					Title = serverTitle.Title1,
					Type = serverTitle.Type,
					PubId = serverTitle.PubId,
					Price = serverTitle.Price,
					Royalty = serverTitle.Royalty,
					YtdSales = serverTitle.YtdSales,
					Advance = serverTitle.Advance,
					ID = serverTitle.TitleId,
					Notes = serverTitle.Notes,
					Pubdate = serverTitle.Pubdate
				};

				Dispatcher.Invoke(() =>
				{
					Edit page = CreateEditPage(model);
					MainBorder.Child = page;
				});
			}
		}

		private void IndexPage_OnAddClicked()
		{
			Dispatcher.Invoke(() =>
			{
				Create page = CreateCreatePage();
				MainBorder.Child = page;
			});
		}

		private void IndexPage_OnPaggingChanged(Index sender, string pubId, string page)
		{
			ServerOnPublisherTitles titles = null;
			Task<HttpResponseMessage> TitlesResponse = client.GetAsync($"api/TitlesApi/{pubId}/{page}");
			TitlesResponse.Wait();

			if (TitlesResponse.Result.IsSuccessStatusCode)
			{
				Task<string> titlesStr = TitlesResponse.Result.Content.ReadAsStringAsync();
				titlesStr.Wait();
				titles = new JavaScriptSerializer().Deserialize<ServerOnPublisherTitles>(titlesStr.Result);
			}
			else
			{
				MessageBox.Show("Не удалось получить данные с сервера!\nПриложение будет закрыто!");
				Dispatcher.Invoke(() => this.Close());
			}

			List<TitleItem> titleLst = new List<TitleItem>();
			foreach (var item in titles.titles)
			{
				double? price = null;
				if (double.TryParse(item.price, out double _price)) price = _price;

				titleLst.Add(new TitleItem()
				{
					ID = item.id,
					Title = item.title,
					Price = price,
					PubName = item.pubName
				});
			}
			if(titles.paggingCnt.HasValue) sender.UpdatePagging(titles.paggingCnt.Value, null);
			sender.UpdateTitles(titleLst);
		}

		private void IndexPage_OnPublisherChanged(Index sender, string pubId)
		{
			ServerOnPublisherTitles titlesPagging = null;
			Task<HttpResponseMessage> TitlesResponse = client.GetAsync($"api/TitlesApi/pub/{pubId}");
			TitlesResponse.Wait();

			if (TitlesResponse.Result.IsSuccessStatusCode)
			{
				Task<string> titlesStr = TitlesResponse.Result.Content.ReadAsStringAsync();
				titlesStr.Wait();
				titlesPagging = new JavaScriptSerializer().Deserialize<ServerOnPublisherTitles>(titlesStr.Result);
			}
			else
			{
				MessageBox.Show("Не удалось получить данные с сервера!\nПриложение будет закрыто!");
				Dispatcher.Invoke(() => this.Close());
			}

			List<TitleItem> titleLst = new List<TitleItem>();
			foreach (var item in titlesPagging.titles)
			{
				double? price = null;
				if (double.TryParse(item.price, out double _price)) price = _price;

				titleLst.Add(new TitleItem()
				{
					ID = item.id,
					Title = item.title,
					Price = price,
					PubName = item.pubName
				});
			}
			sender.UpdateTitles(titleLst);
			int paggingCnt = titlesPagging.paggingCnt.HasValue ? titlesPagging.paggingCnt.Value : 1;
			sender.UpdatePagging(paggingCnt);
		}

		#endregion

		#region Create page

		private Create CreateCreatePage()
		{
			Create page = new Create();
			page.UpdatePublishers(publishers);
			page.OnSubmitClick += CreatePage_OnSubmitClick;
			page.OnCancelClick += CreatePage_OnCancelClick;
			return page;
		}

		private void CreatePage_OnCancelClick()
		{
			if (Dispatcher.CheckAccess())
			{
				Index page = CreateIndexPage();
				MainBorder.Child = page;
			}
			else
			{
				Dispatcher.Invoke(() => CreatePage_OnCancelClick());
			}
		}

		private void CreatePage_OnSubmitClick(Create sender, TitleFormModel formModel)
		{
			List<KeyValuePair<string, object>> errors = new List<KeyValuePair<string, object>>();

			ServerTitle title = formModel.GetTitle();

			var json = new JavaScriptSerializer().Serialize(title);
			StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			Task<HttpResponseMessage> AddTitleResponse = client.PostAsync("api/TitlesApi", httpContent);
			AddTitleResponse.Wait();

			if (AddTitleResponse.Result.IsSuccessStatusCode)
			{
				if (Dispatcher.CheckAccess())
				{
					Index page = CreateIndexPage();
					MainBorder.Child = page;
				}
				else
				{
					Dispatcher.Invoke(() =>
					{
						Index page = CreateIndexPage();
						MainBorder.Child = page;
					});
				}
				return;
			}
			else
			{
				Task<string> content = AddTitleResponse.Result.Content.ReadAsStringAsync();
				content.Wait();

				IEnumerable<KeyValuePair<string, object>> errorObj = new JavaScriptSerializer().Deserialize<IEnumerable<KeyValuePair<string, object>>>(content.Result);
				foreach (KeyValuePair<string, object> currentRecord in errorObj)
				{
					// осли обнаружен список ошибок валидации
					if (currentRecord.Key == "errors")
					{
						sender.UpdateErrors(currentRecord.Value as IEnumerable<KeyValuePair<string, object>>);
						return;
					}
				}
			}
		}

		#endregion

		#region Edit page

		private Edit CreateEditPage(TitleFormModel model = null)
		{
			Edit page = null;
			if(model == null) page = new Edit();
			else page = new Edit(model);

			page.UpdatePublishers(publishers);
			page.OnSubmitClick += EditPage_OnSubmitClick;
			page.OnCancelClick += EditPage_OnCancelClick;

			return page;
		}

		private void EditPage_OnCancelClick()
		{
			if (Dispatcher.CheckAccess())
			{
				Index page = CreateIndexPage();
				MainBorder.Child = page;
			}
			else
			{
				Dispatcher.Invoke(() => CreatePage_OnCancelClick());
			}
		}

		private void EditPage_OnSubmitClick(Edit sender, TitleFormModel formModel)
		{
			List<KeyValuePair<string, object>> errors = new List<KeyValuePair<string, object>>();

			ServerTitle title = formModel.GetTitle();

			var json = new JavaScriptSerializer().Serialize(title);
			StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			Task<HttpResponseMessage> AddTitleResponse = client.PutAsync($"api/TitlesApi/{formModel.ID}", httpContent);
			AddTitleResponse.Wait();

			if (AddTitleResponse.Result.IsSuccessStatusCode)
			{
				if (Dispatcher.CheckAccess())
				{
					Index page = CreateIndexPage();
					MainBorder.Child = page;
				}
				else
				{
					Dispatcher.Invoke(() =>
					{
						Index page = CreateIndexPage();
						MainBorder.Child = page;
					});
				}
				return;
			}
			else
			{
				Task<string> content = AddTitleResponse.Result.Content.ReadAsStringAsync();
				content.Wait();

				IEnumerable<KeyValuePair<string, object>> errorObj = new JavaScriptSerializer().Deserialize<IEnumerable<KeyValuePair<string, object>>>(content.Result);
				foreach (KeyValuePair<string, object> currentRecord in errorObj)
				{
					// осли обнаружен список ошибок валидации
					if (currentRecord.Key == "errors")
					{
						sender.UpdateErrors(currentRecord.Value as IEnumerable<KeyValuePair<string, object>>);
						return;
					}
				}
			}
		}

		#endregion


	}
}
