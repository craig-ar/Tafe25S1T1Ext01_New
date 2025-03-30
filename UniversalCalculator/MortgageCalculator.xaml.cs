using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MortgageCalculator : Page
	{
		public MortgageCalculator()
		{
			this.InitializeComponent();
		}

		private async void CalculateButton_Click(object sender, RoutedEventArgs e)
		{
			double principal = 0;
			double yearlyInterestRate = 0;
			int years = 0;
			int months = 0;


			try
			{
				principal = double.Parse(principalTextBox.Text);
			}

			catch (Exception exception)
			{
				var dialog = new MessageDialog("Must be a valid number");
				await dialog.ShowAsync();
				principalTextBox.Focus(FocusState.Programmatic);
				principalTextBox.SelectAll();
				return;
			}

			try
			{
				yearlyInterestRate = double.Parse(yearlyInterestRateTextBox.Text);
			}

			catch (Exception exception)
			{
				var dialog = new MessageDialog("Must be a valid number");
				await dialog.ShowAsync();
				yearlyInterestRateTextBox.Focus(FocusState.Programmatic);
				yearlyInterestRateTextBox.SelectAll();
				return;
			}

			try
			{
				years = int.Parse(yearsTextBox.Text);
			}

			catch (Exception exception)
			{
				var dialog = new MessageDialog("Must be a valid integer");
				await dialog.ShowAsync();
				yearsTextBox.Focus(FocusState.Programmatic);
				yearsTextBox.SelectAll();
				return;
			}

			try
			{
				months = int.Parse(monthsTextBox.Text);
			}

			catch (Exception exception)
			{
				var dialog = new MessageDialog("Must be a valid integer");
				await dialog.ShowAsync();
				monthsTextBox.Focus(FocusState.Programmatic);
				monthsTextBox.SelectAll();
				return;
			}



			double monthlyInterestRate = (yearlyInterestRate / 100) / 12;
			int totalMonths = (years * 12) + months;

			// M = P [ i(1 + i)^n ] / [ (1 + i)^n – 1]

			double monthlyRepayment = principal * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalMonths)) / (Math.Pow(1 + monthlyInterestRate, totalMonths) - 1);
			monthlyInterestRateTextBox.Text = (monthlyInterestRate * 100).ToString();
			monthlyRepaymentTextBox.Text = monthlyRepayment.ToString("F2");
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}
	}
}
