﻿using System.Threading.Tasks;
using System.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace ZTMDSBT.Purchase
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void button_Click(object sender, RoutedEventArgs e)
    {

      FirefoxProfile fp = new FirefoxProfile();
      fp.SetPreference("webdriver.load.strategy", "fast");
      fp.SetPreference("browser.startup.homepage", "about:blank");
      fp.SetPreference("startup.homepage_welcome_url", "about:blank");
      fp.SetPreference("startup.homepage_welcome_url.additional", "about:blank");
      fp.SetPreference("browser.cache.disk.enable", "true");
      fp.SetPreference("browser.cache.memory.enable", "true");
      fp.SetPreference("browser.cache.offline.enable", "true");
      fp.SetPreference("network.http.use-cache", "true");
      IWebDriver driver = new FirefoxDriver(fp) { Url = "http://www.nike.com/cn/zh_cn/" };
      var steps = new SeleniumSteps(driver);
      var product = PurchaseConfiguration.GetProduct();
      var user = PurchaseConfiguration.GetUser();
      Task.Run(() => steps.Login(user))
        .ContinueWith(s => steps.GotoProductPage(product.URL))
        .ContinueWith(s => steps.SelectSizeAndQuantity(product))
        .ContinueWith(s => steps.AddedToCart());
      //      setps.AddedToCart();
      //      setps.CreateOrder();
      //      setps.FillPaymentGetwayAndOtherInfo();
    }
  }
}
