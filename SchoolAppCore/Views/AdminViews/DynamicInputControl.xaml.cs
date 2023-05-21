using SchoolAppCore.Models;
using SchoolAppCore.ViewModels.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

		private object EntityInstance;
		public List<PropertyItem> Properties { get; private set; }

		public Type EntityType
		{
			get { return (Type)GetValue(EntityTypeProperty); }
			set { SetValue(EntityTypeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for EntityType.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty EntityTypeProperty =
			DependencyProperty.Register("EntityType", typeof(Type), typeof(DynamicInputControl), new PropertyMetadata(null, OnEntityTypeChanged));

		private static void OnEntityTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = d as DynamicInputControl;
			control.LoadProperties();
		}

		private void LoadProperties()
		{
			if (EntityType != null)
			{
				Properties = new List<PropertyItem>(EntityType.GetProperties().Select(p => new PropertyItem() { Name = p.Name }));
			}
		}


		//public static readonly DependencyProperty PropertiesProperty =
		//	DependencyProperty.Register("Properties", typeof(IEnumerable<PropertyItem>), typeof(DynamicInputControl));

		public static readonly DependencyProperty OKCommandProperty =
			DependencyProperty.Register("OKCommand", typeof(ICommand), typeof(DynamicInputControl));

		public static readonly DependencyProperty CancelCommandProperty =
			DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(DynamicInputControl));

		//public IEnumerable<PropertyItem> Properties
		//{
		//	get { return (IEnumerable<PropertyItem>)GetValue(PropertiesProperty); }
		//	set { SetValue(PropertiesProperty, value); }
		//}

		public ICommand OKCommand
		{
			get { return (ICommand)GetValue(OKCommandProperty); }
			set { SetValue(OKCommandProperty, value); }
		}

		public ICommand CancelCommand
		{
			get { return (ICommand)GetValue(CancelCommandProperty); }
			set { SetValue(CancelCommandProperty, value); }
		}

		public DynamicInputControl()
		{
			InitializeComponent();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Cancel button clicked!");
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			// Create an instance of the EntityType
			var entity = Activator.CreateInstance(EntityType);

			// Set the property values based on the user input
			foreach (var property in Properties)
			{
				PropertyInfo propertyInfo = EntityType.GetProperty(property.Name);
				if (propertyInfo != null)
				{
					propertyInfo.SetValue(entity, property.Value);
				}
			}

			// Store the entity instance
			EntityInstance = entity;

			if (OKCommand?.CanExecute(EntityInstance) == true)
			{
				OKCommand.Execute(EntityInstance);
			}
		}
	}

	public class PropertyItem : DependencyObject
	{
		public static readonly DependencyProperty NameProperty =
			DependencyProperty.Register("Name", typeof(string), typeof(PropertyItem));

		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(object), typeof(PropertyItem));

		public string Name
		{
			get { return (string)GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}

		public object Value
		{
			get { return GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}

}
