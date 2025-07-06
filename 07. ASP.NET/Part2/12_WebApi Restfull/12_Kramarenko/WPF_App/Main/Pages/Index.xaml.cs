using Main.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Main.Pages
{
	/// <summary>
	/// Логика взаимодействия для Index.xaml
	/// </summary>
	public partial class Index : UserControl
	{
		#region Delegates

		public delegate void SimpleDelegate();
		public delegate void EditDelegate(string pubId);
		public delegate bool DeleteDelegate(string pubId);
		public delegate void PublisherDelegate(Index sender, string pubId);
		public delegate void PaggingDelegate(Index sender, string pubId, string page);

		#endregion

		#region Events

		public event SimpleDelegate OnAddClicked;
		public event PublisherDelegate OnPublisherChanged;
		public event PaggingDelegate OnPaggingChanged;
		public event EditDelegate OnEditClick;
		public event DeleteDelegate OnDeleteClick;

		#endregion


		#region Properties

		public ObservableCollection<TitleItem> Titles
		{
			get { return (ObservableCollection<TitleItem>)GetValue(TitlesProperty); }
			private set { SetValue(TitlesProperty, value); }
		}

		#endregion

		#region Dependency Properties

		// Using a DependencyProperty as the backing store for Titles.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty TitlesProperty =
			DependencyProperty.Register("Titles", typeof(ObservableCollection<TitleItem>),
				typeof(Index), new PropertyMetadata(new ObservableCollection<TitleItem>()));

		#endregion


		public Index()
		{
			InitializeComponent();
			TitlesDataGrid.DataContext = Titles;
		}

		private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
		{
			e.Cancel = true;
		}

		private void TitleEditClick(object sender, RoutedEventArgs e)
		{
			string id = (sender as Button).Tag.ToString();
			OnEditClick?.BeginInvoke(id, null, null);
		}

		private void TitleDeleteClick(object sender, RoutedEventArgs e)
		{
			string id = (sender as Button).Tag.ToString();

			if (OnDeleteClick.Invoke(id))
			{
				TitleItem title = Titles.Where(x => x.ID == id).FirstOrDefault();
				if (title != null) Titles.Remove(title);
			}
			else
			{
				MessageBox.Show("Ошибка!");
			}
		}

		#region Value changed

		private void PaggingButton_Checked(object sender, RoutedEventArgs e)
		{
			RadioButton btn = sender as RadioButton;
			var selected = PublishersComboBox.SelectedItem;
			if(selected != null)
			{
				string pubId = (selected as ComboBoxItem).Tag.ToString();
				string page = btn.Tag.ToString();
				OnPaggingChanged?.BeginInvoke(this, pubId, page, null, null);
			}
			else
			{
				Titles.Clear();
				MessageBox.Show("Издательство не выбрано!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void PublishersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selected = PublishersComboBox.SelectedItem;
			if (selected != null)
			{
				string pubId = (selected as ComboBoxItem).Tag.ToString();
				OnPublisherChanged?.BeginInvoke(this, pubId, null, null);
			}
			else
			{
				MessageBox.Show("Издательство не выбрано!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}

		#endregion

		#region Update data

		public void UpdateTitles(List<TitleItem> items, [CallerMemberName] string callerName = "")
		{
			if (Dispatcher.CheckAccess())
			{
				Titles.Clear();
				foreach (var item in items) Titles.Add(item);
			}
			else
			{
				Dispatcher.Invoke(() => UpdateTitles(items));
			}
		}

		public void UpdatePublishers(List<ServerPublisher> items, string selectedID = "0")
		{
			if (Dispatcher.CheckAccess())
			{
				PublishersComboBox.Items.Clear();

				ComboBoxItem item = new ComboBoxItem()
				{
					Content = "Все",
					Tag = "0",
					IsSelected = "0" == selectedID
				};
				PublishersComboBox.Items.Add(item);
				foreach (var pub in items)
				{
					item = new ComboBoxItem()
					{
						Content = pub.PubName,
						Tag = pub.PubId,
						IsSelected = pub.PubId == selectedID
					};
					PublishersComboBox.Items.Add(item);
				}
			}
			else
			{
				Dispatcher.Invoke(() => UpdatePublishers(items, selectedID));
			}
		}

		public void UpdatePagging(int count, string selectedNum = "1")
		{
			if (Dispatcher.CheckAccess())
			{
				if(selectedNum == null)
				{
					foreach (var item in PaggingStackPanel.Children)
					{
						RadioButton btn = item as RadioButton;
						if (btn != null && btn.IsChecked == true)
						{
							selectedNum = btn.Tag.ToString();
							break;
						}
					}
				}

				if (PaggingStackPanel.Children.Count != count)
				{
					if (PaggingStackPanel.Children.Count > count)
					{
						PaggingStackPanel.Children.RemoveRange(count, PaggingStackPanel.Children.Count - count);
					}
					else
					{
						for (int i = PaggingStackPanel.Children.Count + 1; i <= count; i++)
						{
							RadioButton btn = new RadioButton()
							{
								Content = i.ToString(),
								Tag = i.ToString(),
								Style = FindResource("PaggingButtonStyle") as Style
							};
							btn.Checked += PaggingButton_Checked;
							PaggingStackPanel.Children.Add(btn);
						}
					}
				}

				if (PaggingStackPanel.Children.Count < int.Parse(selectedNum))
					selectedNum = PaggingStackPanel.Children.Count.ToString();


				foreach (var item in PaggingStackPanel.Children)
				{
					RadioButton btn = item as RadioButton;
					if (btn != null && btn.Tag.ToString() == selectedNum)
					{
						btn.IsChecked = true;
						break;
					}
				}
			}
			else
			{
				Dispatcher.Invoke(() => UpdatePagging(count, selectedNum));
			}
		}

		#endregion

		private void AddBtn_Click(object sender, RoutedEventArgs e)
		{
			OnAddClicked?.BeginInvoke(null, null);
		}
	}
}
