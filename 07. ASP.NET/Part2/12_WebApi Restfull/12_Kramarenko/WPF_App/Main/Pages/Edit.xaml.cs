using Main.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Main.Pages
{
	/// <summary>
	/// Логика взаимодействия для Edit.xaml
	/// </summary>
	public partial class Edit : UserControl
	{
		#region Delegates

		public delegate void EditTitleDelegate(Edit sender, TitleFormModel formModel);
		public delegate void SimpleDelegate();

		#endregion

		#region Events

		public event EditTitleDelegate OnSubmitClick;
		public event SimpleDelegate OnCancelClick;

		#endregion

		public TitleFormModel EditTitle
		{
			get { return (TitleFormModel)GetValue(EditTitleProperty); }
			set { SetValue(EditTitleProperty, value); }
		}

		// Using a DependencyProperty as the backing store for EditTitle.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty EditTitleProperty =
			DependencyProperty.Register("EditTitle", typeof(TitleFormModel), typeof(Edit), new PropertyMetadata(new TitleFormModel()));

		ObservableCollection<ServerPublisher> Publishers = new ObservableCollection<ServerPublisher>();

		public Edit()
		{
			InitializeComponent();
			PubIdCmbBox.DataContext = Publishers;
			EditTitle = new TitleFormModel();
		}

		public Edit(TitleFormModel title)
		{
			InitializeComponent();
			PubIdCmbBox.DataContext = Publishers;
			EditTitle = title;
		}

		public void UpdatePublishers(List<ServerPublisher> pubs)
		{
			Publishers.Clear();
			foreach (var pub in pubs) Publishers.Add(pub);
		}

		private void AddBtn_Click(object sender, RoutedEventArgs e)
		{
			OnSubmitClick?.BeginInvoke(this, EditTitle, null, null);
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			OnCancelClick?.BeginInvoke(null, null);
		}

		public void UpdateErrors(IEnumerable<KeyValuePair<string, object>> errors)
		{
			Dispatcher.Invoke(() =>
			{
				Style TextInputStyle = FindResource("TextInput") as Style;
				Style ComboBoxInputStyle = FindResource("ComboBoxInput") as Style;
				Style DatePickerInputStyle = FindResource("DatePickerInput") as Style;

				foreach (StackPanel group in TitleFormStackPanel.Children)
				{
					if (group == null) continue;

					foreach (FrameworkElement control in group.Children)
					{
						if (control is TextBox)
						{
							control.Style = TextInputStyle;
						}
						else if (control is ComboBox)
						{
							control.Style = ComboBoxInputStyle;
						}
						else if (control is DatePicker)
						{
							control.Style = DatePickerInputStyle;
						}
						else if (control is TextBlock && (control.Tag?.ToString() ?? "") != "Header")
						{
							(control as TextBlock).Text = "";
						}
					}
				}

				if (errors != null && errors.Count() > 0)
				{
					Style ErrorTextInputStyle = FindResource("ErrorTextInput") as Style;
					Style ErrorComboBoxInputStyle = FindResource("ErrorComboBoxInput") as Style;
					Style ErrorDatePickerInputStyle = FindResource("ErrorDatePickerInput") as Style;

					// перебрать все ошибки валидации
					foreach (KeyValuePair<string, object> currentValidationError in errors)
					{
						// перебрать все элементы на панели
						foreach (StackPanel group in TitleFormStackPanel.Children)
						{
							if (group == null) continue;
							if ((group.Tag?.ToString() ?? "") == currentValidationError.Key)
							{
								foreach (FrameworkElement control in group.Children)
								{
									if (control is TextBox)
									{
										control.Style = ErrorTextInputStyle;
									}
									else if (control is ComboBox)
									{
										control.Style = ErrorComboBoxInputStyle;
									}
									else if (control is DatePicker)
									{
										control.Style = ErrorDatePickerInputStyle;
									}
									else if (control is TextBlock && (control.Tag?.ToString() ?? "") != "Header")
									{
										System.Collections.IEnumerable errs = (System.Collections.IEnumerable)currentValidationError.Value;
										foreach (string currentError in errs)
										{
											(control as TextBlock).Text = currentError;
											break;
										}
									}
								}
								break;
							}
						}
					}
				}
			});
		}
	}
}
