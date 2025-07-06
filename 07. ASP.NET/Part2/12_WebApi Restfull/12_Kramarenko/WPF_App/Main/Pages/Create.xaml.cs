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
	/// Логика взаимодействия для Create.xaml
	/// </summary>
	public partial class Create : UserControl
	{
		#region Delegates

		public delegate void AddTitleDelegate(Create sender, TitleFormModel formModel);
		public delegate void SimpleDelegate();

		#endregion

		#region Events

		public event AddTitleDelegate OnSubmitClick;
		public event SimpleDelegate OnCancelClick;

		#endregion


		public TitleFormModel CreatedTitle
		{
			get { return (TitleFormModel)GetValue(CreatedTitleProperty); }
			set { SetValue(CreatedTitleProperty, value); }
		}

		// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CreatedTitleProperty =
			DependencyProperty.Register("CreatedTitle", typeof(TitleFormModel), typeof(Create), new PropertyMetadata(new TitleFormModel()));

		ObservableCollection<ServerPublisher> Publishers = new ObservableCollection<ServerPublisher>();
		public Create()
		{
			InitializeComponent();
			SetValue(CreatedTitleProperty, new TitleFormModel());

			PubIdCmbBox.DataContext = Publishers;
		}

		public void UpdatePublishers(List<ServerPublisher> pubs)
		{
			Publishers.Clear();
			foreach (var pub in pubs) Publishers.Add(pub);
		}

		private void AddBtn_Click(object sender, RoutedEventArgs e)
		{
			OnSubmitClick?.BeginInvoke(this, CreatedTitle, null, null);
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
                        if(control is TextBox)
						{
                            control.Style = TextInputStyle;
						}
                        else if(control is ComboBox)
						{
                            control.Style = ComboBoxInputStyle;
                        }
                        else if(control is DatePicker)
						{
                            control.Style = DatePickerInputStyle;
                        }
                        else if(control is TextBlock && (control.Tag?.ToString() ?? "") != "Header")
                        {
                            (control as TextBlock).Text = "";
						}
					}
                }

				if(errors != null && errors.Count() > 0)
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
