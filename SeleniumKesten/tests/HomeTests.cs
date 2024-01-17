using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumKesten.pages;

namespace SeleniumKesten.tests
{
    public class HomeTests
    {
        private IWebDriver driver;
        private HomePage homePage;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            homePage = new HomePage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }


        [Test]
        [TestCase("200", "200")]
        [TestCase("1000", "100")]
        public void TestValidanRangeCijena(string maxPrice, string minPrice)
        {
            homePage.OpenPage("https://kesten.ba/index.php/bih/");
            homePage.ClickSearchLink();
            homePage.SetPriceRange(minPrice, maxPrice);
            homePage.ClickSearchButton();

            Assert.IsTrue(homePage.IsProductListDisplayed(), "Lista produkata treba da bude prikazana.");

        }


        [Test]
        [TestCase("0", "0")] // BUG - prikazuje sve artikle web shopa uprkos nultim vrijednostima minimalne i maksimalne cijene
        [TestCase("-100", "-50")] 
        [TestCase("-50", "-100")]
        [TestCase("50", "100")]
        [TestCase("-50", "-50")]
        [TestCase("100", "-100")] // BUG - prikazuje proizvode cije su cijene u rangu 0 - 100KM, uprkos negativnoj minimalnoj cijeni
        [TestCase("-100", "100")]
        public void TestNevalidanRangeCijena(string maxPrice, string minPrice)
        {
            homePage.OpenPage("https://kesten.ba/index.php/bih/");
            homePage.ClickSearchLink();
            homePage.SetPriceRange(minPrice, maxPrice);
            homePage.ClickSearchButton();

            Assert.IsFalse(homePage.IsProductListDisplayed(), "Lista produkata ne treba biti prikazana, ocekuje se not found result.");

        }


        // BUG - dodavanje proizvoda na wishlistu otvara cart pogled
        [Test]
        public void DodavanjeProizvoljnogProizvodaUWishlistu()
        {
            homePage.OpenPage("https://kesten.ba/index.php/bih/");
            homePage.ClickDetailButton();
            homePage.ClickAddToWishlistButton();

            // Ovaj assert je napravljen kako bi test pao, kad zelimo da assignamo bug nekome, simulira AzureDevOps workflow    
            Assert.DoesNotThrow(() =>
            {
                homePage.ExecuteScript("if (window.location.href.includes('https://kesten.ba/index.php/bih/shopping-korpa/cart/add')) { throw new Error('URL match error'); }");
            }, "URL match error");


            /*

            // Koristimo ovaj assert kad zelimo da test prodje
            var exception = Assert.Throws<JavaScriptException>(() =>
            {
                homePage.ExecuteScript("if (window.location.href.includes('https://kesten.ba/index.php/bih/shopping-korpa/cart/add')) { throw new Error('URL match error'); }");
            });

            */


        }


    }
}