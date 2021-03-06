﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WeatherAppDemo.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static WeatherAppDemo.Model.ApiManager;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherAppDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var postion = await LocationData.GetPosition();
                var lat = postion.Coordinate.Latitude;
                var lon = postion.Coordinate.Longitude;

                RootObject myWeather = await ApiManager.GetWeather(lat, lon);
                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                TemTextBlock.Text = ((double)myWeather.main.temp).ToString();
                DescriptionTextBlock.Text = myWeather.weather[0].description;
                LocationTextBlock.Text = myWeather.name;

            }
            catch (Exception ex)
            {
                LocationTextBlock.Text = "Khong lay duoc toa do";
            }
        }

        private async void Click_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
