/* **********************************************************
 * Description : EyeGlassesShelfPage.cs contains the methods related to 
 *                 verifying if Sort By, Pagination, Results per page displayed and etc.
 *              
 * Date :  09-Feb-2015
 * 
 * **********************************************************
 */

using Automation.Mercury;
using Automation.Mercury.Report;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace NationalVision.Automation.Pages
{
    public class EyeGlassesShelfPage : CommonPage
    {
        // *** PageTitle varible store the Title of this page,
        // *** If user call AssertPageTitle pageTitle value will be passed.
        protected static string pageTitle = "Eyeglasses";
        /// <summary>
        /// AssertPageTitle method assert page title
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void AssertPageTitle(RemoteWebDriver driver, Iteration reporter)
        {
            AssertPageTitle(driver, reporter, pageTitle);
        }

        /// <summary>
        /// VerifySortBy method verify if Sort by is present on the page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void VerifySortBy(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verifying 'Sort-by' option available in EyeglassesShelf page"));
            Selenide.VerifyVisible(driver, Util.GetLocator("sortby_dd"));
        }

        /// <summary>
        /// SelectSortBy method select the sort by value;
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="sortBy">default values: name; popular, lowest, highest</param>
        public static void SelectSortBy(RemoteWebDriver driver,
            Iteration reporter,
            string sortBy = "name")
        {
            string sortvalue = null;
            switch (sortBy.ToLower())
            {
                case "name":
                    sortvalue = "Product Name";
                    break;
                case "popular":
                    sortvalue = "Most Popular";
                    break;
                case "lowest":
                    sortvalue = "Price: Lowest First";
                    break;
                case "highest":
                    sortvalue = "Price: Highest First";
                    break;
                default:
                    sortvalue = "Product Name";
                    break;
            }
            reporter.Add(new Act(string.Format("EyeglassesShelf page results sorted-by: '{0}'", sortvalue)));
            Selenide.SetText(driver, Util.GetLocator("sortby_dd"), Selenide.ControlType.Select, sortvalue);
            WaitLoadingComplete(driver);
        }

        /// <summary>
        /// SelectResutlsperPage method select results per page dropdown
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="resultperPage">default=12;other possible values: 1,6,12,24,All</param>
        public static void SelectResutlsperPage(RemoteWebDriver driver,
            Iteration reporter,
            string resultperPage = "12")
        {
            reporter.Add(new Act(string.Format("In EyeglassesShelf page 'results per page' value set to: {0} ", resultperPage)));
            Selenide.SetText(driver, Util.GetLocator("results_dd"), Selenide.ControlType.Select, resultperPage);
            WaitLoadingComplete(driver);
        }

        /// <summary>
        /// VerifyPagination method verify if Sort by is present on the page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void VerifyPagination(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verifying Pagination available on EyeglassesShelf page"));
            string totalText = Selenide.GetText(driver, Locator.Get(LocatorType.XPath, "//div[@class='category-paging']/b"), Selenide.ControlType.Label);
            string rangeCount = Selenide.GetText(driver, Locator.Get(LocatorType.XPath, "//div[@class='category-paging']/descendant::span[@class='accent-color-red']"), Selenide.ControlType.Label);

            string[] totalImageCount = totalText.Split(new string[] { "of" }, System.StringSplitOptions.RemoveEmptyEntries);
            string[] currentPageCount = rangeCount.Split('-');

            // *** Below condition verify the Is pagenation available on EyeGlasses shlef page 
            if (int.Parse(currentPageCount[1]) < int.Parse(totalImageCount[1]))
            {
                Selenide.VerifyVisible(driver, Locator.Get(LocatorType.XPath,
                    "//div[@class='category-paging']/descendant::div[@class='page-numbers']"));
            }

        }

        /// <summary>
        /// NavigationPaginationandVerify method click on pagination and verify next page is navigated.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void NavigateNextPageandVerify(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Clicking on pagination arrow-button to see the next pages"));
            // *** currentPage1 variable store the current page number;
            Selenide.WaitForElementVisible(driver, Locator.Get(LocatorType.XPath,
                "//div[@class='page-numbers']/a[@class='paging-link current-page']"));
            string currentPage1 = Selenide.GetText(driver, Locator.Get(LocatorType.XPath,
                "//div[@class='page-numbers']/a[@class='paging-link current-page']"), Selenide.ControlType.Label);

            // *** Now click on Pagination Icon to navigate next page..
            Selenide.Click(driver, Locator.Get(LocatorType.XPath, "//div[@class='page-numbers']/a[span[@class='icon-circle-right']]"));

            WaitLoadingComplete(driver);

            // *** currentSelected2 variable store the page number after click on pagination 
            string currentPage2 = Selenide.GetText(driver, Locator.Get(LocatorType.XPath,
                "//div[@class='page-numbers']/a[@class='paging-link current-page']"), Selenide.ControlType.Label);

            if (!(int.Parse(currentPage2) > int.Parse(currentPage1)))
            {
                throw new Exception("Pagination not working properly");
            }
        }

        /// <summary>
        /// VerifyResultsPerPage method verify if Sort by is present on the page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void VerifyResultsPerPage(RemoteWebDriver driver,
            Iteration reporter,
            string expResultsPage = "12")
        {
            string resultPerPage = Selenide.GetText(driver, Util.GetLocator("results_dd"), Selenide.ControlType.Select);

            reporter.Add(new Act(string.Format(@"EyeglassesShelf page currently displaying {0} results  per page", resultPerPage)));

            if (expResultsPage.Equals("All"))
            {
                if (!expResultsPage.Equals(resultPerPage.Trim()))
                {
                    throw new Exception(string.Format(@"Page results not match : <b><font color=red> Expected Pageper Results: ""{0}""  </font></b> <br> Actual Pageper results: ""{1}""",
                        expResultsPage, resultPerPage));
                }
            }
            else
            {
                if (Convert.ToInt16(expResultsPage) != Convert.ToInt16(resultPerPage))
                {
                    throw new Exception(string.Format(@"Page results not match : <b><font color=red> Expected Pageper Results: ""{0}""  </font></b> <br> Actual Pageper results: ""{1}""",
                        expResultsPage, resultPerPage));
                }
            }
        }

        /// <summary>
        /// Assert try on if available for the product with virtual try on in eyeglasses product display page 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="productname">productname which has the tryon button</param>
        public static void AssertTryOn(RemoteWebDriver driver, Iteration reporter, string productname)
        {
            Selenide.Wait(driver, 3, true);
            // *** Verifing 'Try On' feature avilable for a specified Product.
            if (Selenide.IsElementExists(driver, Locator.Get(LocatorType.XPath,
                 string.Format(@"//span[@class='product-name'][contains(.,'{0}')]/parent::a/parent::div/descendant::div/span[text()='Try On']", productname))))
            {
                reporter.Add(new Act("Click product image, to navigate to Productdetails Page"));
                ClickEyeGlassProduct(driver, reporter, productname); // **** It will redirect to Product Details Page

                Selenide.WaitForElementVisible(driver, Util.GetLocator("eyeglassproductimage_img"));

                reporter.Add(new Act("Verifying 'Virtual Try On' Available on Product details page"));
                Selenide.VerifyVisible(driver, Util.GetLocator("virtualtryon_img"));
            }
            else
            {
                reporter.Add(new Act("'Virtual Try On' not possible for this product"));
                ClickEyeGlassProduct(driver, reporter, productname); // **** It will redirect to Product Details Page
            }
        }

        /// <summary>
        /// AssertLeftPaneLinks method is to verify left menu links are visible in eye glass shelf page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void AssertLeftPaneLinks(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Assert left menu links are presence in EyeGlassShelf page"));
            int menucount = Selenide.GetElementCount(driver, Locator.Get(LocatorType.XPath, "//div[@class='left-nav']/descendant::li/a"));
            if (menucount == 0)
            {
                reporter.Add(new Act("In eyeglass shelf Page Left menu list not visible"));
                throw new Exception("Custom Exception Message : In eye glass shelf Page Left menu list not visible");
            }
        }

        /// <summary>
        /// VerifyProductsInEyeGlass method is to verify various attributes displayed for each product in eye glass shelf page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void VerifyProductsInEyeGlass(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verify attributes for each product in EyeGlassShelf page"));
            int rowsCount = Selenide.GetElementCount(driver, Locator.Get(LocatorType.XPath, "//div[@id='products']/div/div"));

            for (int i = 1; i <= rowsCount; i++)
            {
                int productsCount = Selenide.GetElementCount(driver, Locator.Get(LocatorType.XPath, "//div[@id='products']/div/div[" + i + "]"));

                for (int j = 1; j <= productsCount; j++)
                {
                    Selenide.Query.isElementVisible(driver, Locator.Get(LocatorType.XPath,
                        "//div[@id='products']/div/div[" + i + "]/div[" + j + "]/div/a[@class='product-link']/span[@class='product-name']"));
                    Selenide.Query.isElementVisible(driver, Locator.Get(LocatorType.XPath,
                        "//div[@id='products']/div/div[" + i + "]/div[" + j + "]/div/a[@class='product-image']/img"));
                    Selenide.Query.isElementVisible(driver, Locator.Get(LocatorType.XPath,
                        "//div[@id='products']/div/div[" + i + "]/div[" + j + "]/div/a[@class='product-link']/span[@data-price-tier='frame-price-tier-1']"));
                }
            }
        }

        /// <summary>
        /// RemoveFilters method is to click on x symbol to remove filters in eye glass shelf page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        ///  <param name="filterName">Filter Name wish to delete, displaying on EyeGlasses shelf page.</param>
        public static void RemoveAppliedFilter(RemoteWebDriver driver, Iteration reporter,
            string filterName)
        {
            reporter.Add(new Act("removing applied filters on Eyeglass shelf page"));
            Selenide.Click(driver, Locator.Get(LocatorType.XPath, string.Format(@"//a[@href='#{0}']", filterName)));
        }

        /// <summary>
        /// ClickRemoveAll method is to click on remove all link to remove all filters in eye glass shelf page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void RemoveAllAppliedFilters(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click 'remove all' link to remove all applied filter on Eyeglassshelf page"));
            Selenide.Click(driver, Util.GetLocator("removeall_lnk"));
        }

        /// <summary>
        /// ClickBackArrow method is to click on back arrow icon in current page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void ClickPaginationBackArrow(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Clicking 'Back arrow' icon on the right side of the current page"));
            Selenide.Click(driver, Util.GetLocator("backarrow_icn"));
        }

        /// <summary>
        /// ClickMyFavoritesIcon method is to click on + icon to add product to favorites
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="productName">Product Name</param>
        public static void  ClickMyFavoritesIcon(RemoteWebDriver driver, Iteration reporter,
            string productName)
        {
            reporter.Add(new Act("Clicking favorites icon link to add the product to favorites"));
            Selenide.Click(driver, Locator.Get(LocatorType.XPath,
                string.Format(@"//a[span[@class='product-name' and contains(text(),'{0}')]]/parent::div/parent::div[contains(@class,'show-product')]/descendant::div[@class='favorite-star']", productName)));
        }

        /// <summary>
        /// ClickMyFavoritesIcon method is to click on + icon to add product to favorites
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="productName">Product Name</param>
        public static void RemoveFromFavorites(RemoteWebDriver driver, Iteration reporter,
            string productName)
        {
            reporter.Add(new Act("remove favorites icon link to add the product to favorites"));
            Selenide.WaitForAjax(driver);
            Selenide.Click(driver, Locator.Get(LocatorType.XPath,
                string.Format(@"//a[span[@class='product-name' and contains(text(),'{0}')]]/parent::div/parent::div[contains(@class,'show-product')]/descendant::div[@class='is-favorite']", productName)));
        }

        /// <summary>
        /// ClickViewFavorites method is to click on view favorites in eye glass shelf page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void ClickViewFavorites(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Clicking 'view favorites' link on the right side of the Eye Glass Shelf page"));
            ClickTopMenuLink(driver, reporter, "my favorites");
        }
        /// <summary>
        /// VerifyAddToFavorites method is to verify whether AddToFavorites is availble for all the eye glasses products displayed 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void VerifyAddToFavorites(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verifying AddToFavorites link for each product displayed"));
            //Selenide.WaitForElementVisible(driver, Util.GetLocator("addtofavorites_icon"));
            WaitLoadingComplete(driver);
            int productsDisplayed = Selenide.GetElementCount(driver, Util.GetLocator("addtofavorites_icon"));
            for (int i = 1; i <= productsDisplayed; i++)
            {
                Selenide.VerifyVisible(driver, Locator.Get(LocatorType.XPath,
                    String.Format("//div[@class='product-desktop-row']/descendant::span[@class='favorite-star-wrap'][{0}]", i)));
            }
        }

        /// <summary>
        /// Verifyviewbutton method is to verify View button available in Favorites page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void VerifyViewButtonFavorite(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verifying view button is present on Favorite List page"));
            Selenide.VerifyVisible(driver, Util.GetLocator("view_btn"));
        }

        /// <summary>
        /// VerifyRemovebutton method is to verify Remove button available in Favorites page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void VerifyRemoveButtonFavorite(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verifying remove button is present on Favorite List page"));
            Selenide.VerifyVisible(driver, Util.GetLocator("remove_btn"));
        }

        /// <summary>
        /// IsRemoveAllLinkExist verify Remove all filters exist on the page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <returns>return true: if element exist; false: if element is not exist</returns>
        public static bool IsRemoveAllLinkExist(RemoteWebDriver driver, Iteration reporter)
        {
            return Selenide.IsElementExists(driver, Util.GetLocator("removeall_lnk"));
        }

        /// <summary>
        /// IsShowAllLinkExist verify Show all plus(+) exist on the page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <returns>return true: if element exist; false: if element is not exist</returns>
        public static bool IsShowAllLinkExist(RemoteWebDriver driver, Iteration reporter, string filterOn = "brand")
        {
            return Selenide.IsElementExists(driver, Locator.Get(LocatorType.XPath, string.Format(@"//div[@class='left-nav']/descendant::ul[@class='{0}']/descendant::a[@class='show-more-btn' and starts-with(text(),'Show All')]", filterOn)));
        }





        /// <summary>
        /// Clickviewbutton method is to click on  View button in Favorites page of women's eye glasses
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void ClickViewButton(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click on view button in Favorite List page"));
            Selenide.Click(driver, Util.GetLocator("view_btn"));
        }

        /// <summary>
        /// AssertProductNames assert Product name on EyeGlassesshlef page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="productName"></param>
        /// <param name="position"></param>
        public static void AssertProductNames(RemoteWebDriver driver,
            Iteration reporter,
            string productName,
            int position = 1)
        {

            string ActualName = Selenide.GetText(driver, Locator.Get(LocatorType.XPath,
                    string.Format(@"//div[@id='products']/descendant::span[@class='product-name'][{0}]", position)), Selenide.ControlType.Label);

            reporter.Add(new Act(string.Format("Verify Product Name on : '{0}' ", ActualName)));

            if (!ActualName.Contains(productName))
            {
                throw new Exception(string.Format(@"Product Name not matched: Expected ProductName: ""{0}"" <br> Actual ProductName: ""{1}""", productName, ActualName));
            }

            // This mehtod here Initiated because "ActualName" 
            IsProductHavePrice(driver, reporter, ActualName, position);
            IsTryOnAvailable(driver, reporter, position, ActualName);
            IsProductHaveColorOptions(driver, reporter, ActualName, position);
            IsProductHaveRating(driver, reporter, ActualName, position);
            IsProductHavePromotions(driver, reporter, ActualName, position);
        }

        /// <summary>
        /// AssertProductPrice assert each product have price details
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="productName"></param>
        /// <param name="position"></param>
        public static void IsProductHavePrice(RemoteWebDriver driver,
            Iteration reporter,
            string productName,
            int position = 1)
        {
            if (Selenide.IsElementExists(driver, Locator.Get(LocatorType.XPath,
                    string.Format(@"//div[@id='products']/descendant::a[@class='product-link'][{0}]/span[starts-with(@class, 'product-price')]", position))))
                reporter.Add(new Act(string.Format("'{0}'  : has PriceTag", productName)));
            else
                reporter.Add(new Act(string.Format("<b><font color=red>'{0}' </font></b>: doesn't has PriceTag", productName)));

            //*** This mehtod here Initiated because "ActualName" 
        }

        public static void IsProductHaveColorOptions(RemoteWebDriver driver,
            Iteration reporter,
            string productName,
            int position = 1)
        {
            if (Selenide.IsElementExists(driver, Locator.Get(LocatorType.XPath,
                string.Format(@"//div[@id='products']/descendant::div[@class='col-md-12 product-text'][{0}]/descendant::ul[@class='swatches']", position))))
                reporter.Add(new Act(string.Format("{0} : have Color options", productName)));
            else
                reporter.Add(new Act(string.Format("<b><font color=red>'{0}' </font></b>: doesn't have Color options", productName)));

            //*** This mehtod here Initiated because "ActualName" 
        }

        /// <summary>
        /// IsProductHaveRating verify is product having rating option
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="productName"></param>
        /// <param name="position"></param>
        public static void IsProductHaveRating(RemoteWebDriver driver,
            Iteration reporter,
            string productName,
            int position = 1)
        {
            if (Selenide.IsElementExists(driver, Locator.Get(LocatorType.XPath,
                string.Format(@"//div[@id='products']/descendant::div[@class='col-md-12 product-text'][{0}]/descendant::div[@class='icon-star-container']", position))))
                reporter.Add(new Act(string.Format("{0} : have rating options", productName)));
            else
                reporter.Add(new Act(string.Format("<b><font color=red>'{0}' </font></b>: doesn't have rating options", productName)));

            //*** This mehtod here Initiated because "ActualName" 
        }

        public static void IsProductHavePromotions(RemoteWebDriver driver,
            Iteration reporter,
            string productName,
            int position = 1)
        {
            if (Selenide.IsElementExists(driver, Locator.Get(LocatorType.XPath,
                string.Format(@"//div[@id='products']/descendant::div[@class='col-md-12 product-text'][{0}]/descendant::div[@class='promotional-text']", position))))
                reporter.Add(new Act(string.Format("{0} : have promotion options", productName)));
            else
                reporter.Add(new Act(string.Format("<b><font color=red>'{0}' </font></b>: doesn't have promotion options", productName)));

            //*** This mehtod here Initiated because "ActualName" 
        }

        /// <summary>
        /// IsTryOnAvailable verify TryOn button available for specific product
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="position">int position</param>
        /// <param name="productname">Product name </param>
        public static void IsTryOnAvailable(RemoteWebDriver driver, Iteration reporter, int position = 1, string productname = null)
        {
            Selenide.Wait(driver, 3, true);
            // *** Verifing 'Try On' feature avilable for a specified Product.
            if (Selenide.IsElementExists(driver, Locator.Get(LocatorType.XPath,
                 string.Format(@"//div[@id='products']/descendant::div[@class='col-md-12 product-text'][{0}]/descendant::span[text()='Try On']", position))))
                reporter.Add(new Act(string.Format("{0} : has Try On feature", productname)));
            else
                reporter.Add(new Act(string.Format("<b><font color=red>'{0}' </font></b>: has Not Try On feature", productname)));
        }

        /// <summary>
        /// GetProductCount return  product count of EyeGlassesShlef page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public static int GetProductCount(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verify Product name "));
            return Selenide.GetElementCount(driver, Locator.Get(LocatorType.XPath,
                @"//div[@id='products']/descendant::div[starts-with(@class,'show-product products')]"));
        }

        /// <summary>
        /// Page should display ''Try on Introduction'' section with the below features Get Started Now Button,
        /// Login to access your existing Ditto" Lin, Close button
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public static void VerifyTryOnPopupDetails(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verify 'login to access your existing Ditto™' link "));
            Selenide.VerifyVisible(driver, Util.GetLocator("DittoLoginLink"));
            reporter.Add(new Act("Verify 'Get Started Now™' button "));
            Selenide.VerifyVisible(driver, Util.GetLocator("GetStartedNow"));
            reporter.Add(new Act("Verify close/cancel icon"));
            Selenide.VerifyVisible(driver, Util.GetLocator("DittoPopupCloseBbtn"));
        }

        /// <summary>
        /// Click on "Login to access your existing Ditto" Link
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public static void ClickOnDittoLink(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click on 'login to access your existing Ditto™' link "));
            Selenide.Click(driver, Util.GetLocator("DittoLoginLink"));
        }

        /// <summary>
        /// Verify 'Login to access your existing Ditto' popup contents
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public static void VerifyLoginDittoPopup(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verify 'Log In or Create Your Profile' text"));
            Selenide.VerifyVisible(driver, Util.GetLocator("DittoPopupHeading"));
            reporter.Add(new Act("Verify 'Log In or Create Your Profile' text"));
            Selenide.VerifyVisible(driver, Util.GetLocator("DittoCreateProfileBtn"));
            reporter.Add(new Act("Verify 'Log In or Create Your Profile' text"));
            Selenide.VerifyVisible(driver, Util.GetLocator("DittoSigninBtn"));
         }

        /// <summary>
        /// Click on Create Profile Button in 'Login to access your existing Ditto' popup
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public static void ClickCreateProfileButton(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click Create Profile Button"));
            Selenide.Click(driver, Util.GetLocator("DittoCreateProfileBtn"));
        }

        /// <summary>
        /// Fill the Form and Click create profile button
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="Password"></param>
        /// <param name="ReEnterPassword"></param>
        /// <param name="Email"></param>
       
        public static void FillCreateProfileForm(RemoteWebDriver driver, Iteration reporter,
            string firstName,
            string lastName,
            string Password,
            string ReEnterPassword,
            string Email)
        {
            TypeUserFName(driver, reporter, firstName);
            TypeUserLName(driver, reporter, lastName);
            TypePassword(driver, reporter, Password);
            TypeReEnterPassword(driver, reporter, ReEnterPassword);
            TypeEmail(driver, reporter, Email);
        }
        /// <summary>
        /// TypeUserFName method enter First name in address book
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="firstName">First name</param>
        public static void TypeUserFName(RemoteWebDriver driver, Iteration reporter, string firstName)
        {
            
            reporter.Add(new Act("Enter first name"));
            Selenide.SetText(driver, Util.GetLocator("DittoFormFirstName"), Selenide.ControlType.Textbox, firstName);
        }
        /// <summary>
        /// TypeUserLName method enter last name in address book
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="lastName">Lastname</param>
        public static void TypeUserLName(RemoteWebDriver driver, Iteration reporter,
            string lastName)
        {
            reporter.Add(new Act("Enter last name"));
            Selenide.SetText(driver, Util.GetLocator("DittoFormLastName"), Selenide.ControlType.Textbox, lastName);
        }
        /// <summary>
        /// TypePassword method enters password
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="Password">Password</param>
        public static void TypePassword(RemoteWebDriver driver, Iteration reporter,
           string Password)
        {
            reporter.Add(new Act("Enter password"));
            Selenide.SetText(driver, Util.GetLocator("DittoFormPwd"), Selenide.ControlType.Textbox, Password);
        }
        /// <summary>
        /// TypeReEnterPassword method reenters password for confirmation
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="ReEnterPassword">ReEnterPassword</param>
        public static void TypeReEnterPassword(RemoteWebDriver driver, Iteration reporter,
            string ReEnterPassword)
        {
            reporter.Add(new Act("Renter password"));
            Selenide.SetText(driver, Util.GetLocator("DittoFormRenterPwd"), Selenide.ControlType.Textbox, ReEnterPassword);
        }




        /// <summary>
        /// TypeEmail method enters valid email address
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="TypeEmail">Email</param>
        public static void TypeEmail(RemoteWebDriver driver, Iteration reporter,
            string Email)
        {
            reporter.Add(new Act("Enter Email Address"));
            Selenide.SetText(driver, Util.GetLocator("DittoFormEmail"), Selenide.ControlType.Textbox, Email);
        }

        /// <summary>
        /// Select Terms And Condition Check Box
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="TypeEmail">CheckTermsAndConditionChkBox</param>
        public static void CheckTermsAndConditionChkBox(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Select Terms And Condition Check Box"));
            Selenide.Click(driver, Util.GetLocator("TermsAndConditionChkBox"));
        }

        /// <summary>
        /// Select Terms And Condition Check Box
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="TypeEmail">CheckTermsAndConditionChkBox</param>
        public static void DittoClickCreateProfileButton(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Select Terms And Condition Check Box"));
            Selenide.Click(driver, Util.GetLocator("ClickCreateProfileButton"));
        }


        /// <summary>
        /// Select Terms And Condition Check Box
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="TypeEmail">CheckTermsAndConditionChkBox</param>
        public static void ClickManageTryOnBtn(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click on Manage Try On button"));
            Selenide.WaitForElementVisible(driver, Util.GetLocator("ManageTryOnBtn"));
            Selenide.Click(driver, Util.GetLocator("ManageTryOnBtn"));
        }

        /// <summary>
        /// Select Terms And Condition Check Box
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="TypeEmail">CheckTermsAndConditionChkBox</param>
        public static void AssertManageTryOnSection(RemoteWebDriver driver, Iteration reporter, string firstName)
        {
            reporter.Add(new Act("Verify Create Try on Button is visible"));
            Selenide.WaitForElementVisible(driver, Util.GetLocator("CreateTryOnBtn"));
            Selenide.Query.isElementVisible(driver, Util.GetLocator("CreateTryOnBtn"));
            reporter.Add(new Act("Verify Close button is visible"));
            Selenide.Query.isElementVisible(driver, Util.GetLocator("DittoPopupCloseBbtn"));
            reporter.Add(new Act("Verify patient name is visible"));
            Selenide.Query.isElementVisible(driver, Util.GetLocator("DittoPopupCloseBbtn"));
            string patientname=Selenide.GetText(driver, Util.GetLocator("FirstTypeName"), Selenide.ControlType.Label);
            if (Selenide.Equals(patientname, firstName))
            {
                reporter.Add(new Act("Click product image, to navigate to Productdetails Page"));
                Selenide.WaitForElementVisible(driver, Util.GetLocator("eyeglassproductimage_img"));

                reporter.Add(new Act("Verifying 'Virtual Try On' Available on Product details page"));
                Selenide.VerifyVisible(driver, Util.GetLocator("virtualtryon_img"));
            }

        }



        /// <summary>
        /// ClickMyFavoriteLink
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="TypeEmail"></param>
        public static void ClickMyFavoriteLink(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click My Favorite Link"));
            Selenide.WaitForElementVisible(driver, Util.GetLocator("MyFavoriteLink"));
            Selenide.Click(driver, Util.GetLocator("MyFavoriteLink"));
        }
        



    }

}

