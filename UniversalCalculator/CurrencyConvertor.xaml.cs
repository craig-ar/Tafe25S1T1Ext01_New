using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
	public sealed partial class CurrencyConvertor
	{
		// Conversion rates for each currency pair
		private Dictionary<string, Dictionary<string, double>> exchangeRates = new Dictionary<string, Dictionary<string, double>>
		{
            // Conversion rates from USD
            { "USD", new Dictionary<string, double>
				{
					{ "EUR", 0.85189982 },
					{ "GBP", 0.72872436 },
					{ "INR", 74.257327 }
				}
			},
            // Conversion rates from EUR
            { "EUR", new Dictionary<string, double>
				{
					{ "USD", 1.1739732 },
					{ "GBP", 0.8556672 },
					{ "INR", 87.00755 }
				}
			},
            // Conversion rates from GBP
            { "GBP", new Dictionary<string, double>
				{
					{ "USD", 1.371907 },
					{ "EUR", 1.1686692 },
					{ "INR", 101.68635 }
				}
			},
            // Conversion rates from INR
            { "INR", new Dictionary<string, double>
				{
					{ "USD", 0.011492628 },
					{ "EUR", 0.013492774 },
					{ "GBP", 0.0098339397 }
				}
			}
		};

		// Dictionary to map currency codes to full currency names
		private Dictionary<string, string> currencyNames = new Dictionary<string, string>
		{
			{ "USD", "US Dollars" },
			{ "EUR", "Euros" },
			{ "GBP", "British Pounds" },
			{ "INR", "Indian Rupees" }
		};

		public CurrencyConvertor()
		{
			this.InitializeComponent();
		}

		// Event handler for Currency Conversion Button
		private void CurrencyConversionButton_Click(object sender, RoutedEventArgs e)
		{
			double amount;
			if (double.TryParse(Amount_TextBox.Text, out amount))
			{

				string fromCurrency = (FromComboBox.SelectedItem as ComboBoxItem).Tag.ToString();
				string toCurrency = (ToComboBox.SelectedItem as ComboBoxItem).Tag.ToString();


				if (exchangeRates.ContainsKey(fromCurrency) && exchangeRates[fromCurrency].ContainsKey(toCurrency))
				{
					// Get the conversion rate from the dictionary
					double fromRate = exchangeRates[fromCurrency][toCurrency]; // e.g., USD to EUR
					double toRate = exchangeRates[toCurrency][fromCurrency]; // e.g., EUR to USD

					// Calculate the converted amount
					double convertedAmount = (amount / fromRate) * toRate;

					// Display Amount and Currency
					AmountCurrencyTextBlock.Text = $"{amount:F2} {currencyNames[fromCurrency]} =";

					// Display Conversion Result
					ConversionTextBlock.Text = $"{convertedAmount:F7} {currencyNames[toCurrency]}";


					// Show To-from info (1 {fromCurrency} = x {toCurrency})
					ToFromTextBlock.Text = $"1 {fromCurrency} = {fromRate / toRate:F7} {toCurrency}";

					// Show From-to info (1 {toCurrency} = x {fromCurrency})
					FromToTextBlock.Text = $"1 {toCurrency} = {toRate / fromRate:F2} {fromCurrency}";
				}
				else
				{
					// If the currencies are not found in the exchange rates dictionary
					AmountCurrencyTextBlock.Text = "Invalid currency pair!";
					ConversionTextBlock.Text = "";
					FromToTextBlock.Text = "";
					ToFromTextBlock.Text = "";
				}
			}
			else
			{
				// If the entered amount is invalid, show an error message
				AmountCurrencyTextBlock.Text = "Invalid amount!";
				ConversionTextBlock.Text = "";
				FromToTextBlock.Text = "";
				ToFromTextBlock.Text = "";
			}
		}



		// Event handler for Exit Button
		private void ExitButton_Click_1(object sender, RoutedEventArgs e)
		{

			this.Frame.Navigate(typeof(MainMenu));
		}

	}
}