using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace TreeViewCalendar
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private int year = 2018;
		private int month = 0;
		public MainWindow()
		{
			InitializeComponent();
			LoadTreeView();
		}

		private void LoadTreeView()
		{
			int month = 12;
			foreach(TreeViewItem item in treeView.Items)
			{
				for (int i = 0; i < 3; i++)
				{
					month = month > 12 ? 1 : month;
					TreeViewItem it = new TreeViewItem() { Header = new DateTime(year, month, 1).ToString("MMM", CultureInfo.InvariantCulture) };
					it.Tag = month++;
					it.Selected += Item_Selected;
					item.Items.Add(it);					
				}
			}
			for (int i = 0; i < 7; i++)
			{
				dayOfWeek.Children.Add(new TextBlock() { Text = new DateTime(2018, 05, 21 + i).ToString("ddd", CultureInfo.InvariantCulture)});
			}
		}

		private void Item_Selected(object sender, RoutedEventArgs e)
		{
			TreeViewItem it = sender as TreeViewItem;
			month = (int)(it.Tag);
			tblMonth.Text = new DateTime(year, month, 1).ToString("MMMM", CultureInfo.InvariantCulture);
			LoadListView();
		}

		private void LoadListView()
		{
			if(month == 0)
			{
				return;
			}
			listView.Items.Clear();
			int day = (int)new DateTime(year, month, 1).DayOfWeek - 1;
			day = day < 0 ? 6 : day;
			day = 1 - day;
			int dayInMonth = DateTime.DaysInMonth(year, month);
			while (day<=dayInMonth)
			{
				string d = day.ToString();
				if (day < 1)
				{
					d = " ";
				}
				TextBlock tb = new TextBlock()
				{
					Text = d,					
				};
				listView.Items.Add(tb);
				day++;
			}
		}

		private void LeftButton_Click(object sender, RoutedEventArgs e)
		{
			year--;
			tblYear.Text = year.ToString();
			LoadListView();
		}
		private void RightButton_Click(object sender, RoutedEventArgs e)
		{
			year++;
			tblYear.Text = year.ToString();
			LoadListView();
		}
	}
}
