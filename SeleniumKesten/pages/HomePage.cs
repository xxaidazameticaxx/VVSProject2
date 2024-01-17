using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumKesten.pages
{
    public class HomePage
    {
        private IWebDriver driver;

        private By searchLinkLocator = By.CssSelector("#Mod397 .item-3720 > a");
        private By priceFromInputLocator = By.Id("price_from");
        private By priceToInputLocator = By.Id("price_to");
        private By searchButtonLocator = By.CssSelector(".button:nth-child(1)");
        private By detailButtonLocator = By.CssSelector(".owl-item:nth-child(1) .button_detail:nth-child(2)");
        private By addToWishlistButtonLocator = By.CssSelector(".btn-wishlist");
        private By productListLocator = By.XPath("//div[@class='jshop' and descendant::div[@class='jshop_list_product']]");

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenPage(string url, int maxRetries = 3)
        {
            int retryCount = 0;
            while (retryCount < maxRetries)
            {
                try
                {
                    driver.Navigate().GoToUrl(url);
                    return;
                }
                catch (WebDriverException)
                {
                    retryCount++;
                }
            }
        }

        public void ClickSearchLink()
        {
            IWebElement searchLink = driver.FindElement(searchLinkLocator);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", searchLink);
        }

        public void SetPriceRange(string priceFrom, string priceTo)
        {
            IWebElement priceFromInput = driver.FindElement(priceFromInputLocator);
            IWebElement priceToInput = driver.FindElement(priceToInputLocator);

            priceFromInput.Clear();
            priceFromInput.SendKeys(priceFrom);

            priceToInput.Clear();
            priceToInput.SendKeys(priceTo);
        }

        public void ClickSearchButton()
        {
            IWebElement searchButton = driver.FindElement(searchButtonLocator);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", searchButton);
        }

        public void ClickDetailButton()
        {
            IWebElement detailButton = driver.FindElement(detailButtonLocator);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", detailButton);
        }

        public void ClickAddToWishlistButton()
        {
            IWebElement addToWishlistButton = driver.FindElement(addToWishlistButtonLocator);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", addToWishlistButton);
        }

        public bool IsProductListDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(productListLocator));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void ExecuteScript(string script)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(script);
        }
    }
}
