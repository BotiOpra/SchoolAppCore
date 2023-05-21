using SchoolAppCore.Models;
using SchoolAppCore.ViewModels.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SchoolAppCore.Views.AdminViews
{
	/// <summary>
	/// Interaction logic for DynamicInputControl.xaml
	/// </summary>
	public partial class DynamicInputControl : UserControl
	{
		public Type ModelType
		{
			get { return (Type)GetValue(ModelTypeProperty); }
			set 
			{
				SetValue(ModelTypeProperty, value); 
			}
		}

		// Using a DependencyProperty as the backing store for ModelType.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ModelTypeProperty =
			DependencyProperty.Register("ModelType", typeof(Type), typeof(DynamicInputControl), new PropertyMetadata(null, OnModelObjectChanged));


		public ICommand AddCommand
		{
			get { return (ICommand)GetValue(AddCommandProperty); }
			set { SetValue(AddCommandProperty, value); }
		}

		// Using a DependencyProperty as the backing store for AddCommand.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty AddCommandProperty =
			DependencyProperty.Register("AddCommand", typeof(ICommand), typeof(DynamicInputControl), new PropertyMetadata(null));


		public DynamicInputControl()
		{
			InitializeComponent();
			GenerateInputFields();
		}

		private static void OnModelObjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = (DynamicInputControl)d;
			control.GenerateInputFields();
		}

		private void GenerateInputFields()
		{
			if (ModelType == null)
				return;

			var properties = ModelType.GetProperties();

			int row = 0;
			foreach (var property in properties)
			{
				//fix
				if (!property.CanWrite)
					continue;

				StackPanel inputStackPanel = new StackPanel() 
				{
					Margin=new Thickness(5)
				};

				var label = new Label
				{
					Content = property.Name
				};

				inputStackPanel.Children.Add(label);

				var textBox = new TextBox();
				//textBox.SetBinding(TextBox.TextProperty, new Binding(property.Name) { Source = ModelObject });

				inputStackPanel.Children.Add(textBox);

				// Add label and input field to the container (e.g., a StackPanel)
				// You can customize the layout and styling based on your requirements.

				// Example:
				inputContainer.RowDefinitions.Add(new RowDefinition());
				Grid.SetRow(inputStackPanel, row++);

				inputContainer.Children.Add(inputStackPanel);
			}

			StackPanel buttons = new StackPanel()
			{ 
				Orientation = Orientation.Horizontal,
				HorizontalAlignment = HorizontalAlignment.Right
			};

			buttons.Children.Add(new Button()
			{
				Content = "Close",
				Margin=new Thickness(10)
			});

			buttons.Children.Add(new Button()
			{
				Content = "Ok",
				Margin = new Thickness(10),
				Command = AddCommand

			});

			inputContainer.RowDefinitions.Add(new RowDefinition());
			Grid.SetRow(buttons, row++);

			inputContainer.Children.Add(buttons);


		}
	}

}
