/* **********************************************************
 * Description : Favorite.cs contains the methods related to 
 *                 verifying if Sort By, Pagination, Results per page displayed and etc.
 *              
 * Date :  21-March-2016
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
    /// <summary>
    /// Favorite.cs contains the methods related to 
   ///  verifying if Sort By, Pagination, Results per page displayed and etc.
    /// </summary>
    public class FavoritePage : CommonPage
    {
        // *** PageTitle varible store the Title of this page,
        // *** If user call AssertPageTitle pageTitle value will be passed.
        protected static string pageTitle = "Your Favorite Products";
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
        /// VerifyViewButtonFavorite method is to verify View button available in Favorites page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void VerifyViewButtonFavorite(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verifying view button is present on Favorite List page"));
            Selenide.VerifyVisible(driver, Util.GetLocator("view_btn"));
        }

        /// <summary>
        /// VerifyPrintToStore method verify the Print to Store Button in Review Your Selections page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void VerifyPrintToStore(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verify Print To Store button in Favorites page"));
            Selenide.VerifyVisible(driver, Util.GetLocator("favoritprinttostore_btn"));
        }

        /// <summary>
        /// ClickViewButton method is to verify View button available in Favorites page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void ClickViewButton(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click view button on Favorite List page"));
            Selenide.Click(driver, Util.GetLocator("view_btn"));
        }
        /// <summary>
        /// VerifyRemoveButtonFavorite method is to verify Remove button available in Favorites page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void VerifyRemoveButtonFavorite(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verify remove button is present on Favorite List page"));
            Selenide.VerifyVisible(driver, Util.GetLocator("remove_btn"));
        }
        /// <summary>
        /// ClickTryOnButton method verify the Print to Store Button in Review Your Selections page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void ClickTryOnButton(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verify Print To Store button in the Review Your Selections page"));
            Selenide.Click(driver, Locator.Get(LocatorType.XPath, @"//div/span[text()='Try On']"));
        }

        /// <summary>
        /// Page should display ''Try on Introduction'' section with the below features Get Started Now Button,
        /// Login to access your existing Ditto" Lin, Close button
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public static void VerifyVirtualTryOnWrapperDetails(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Verify 'login to access your existing Ditto™' link "));
            Selenide.VerifyVisible(driver, Util.GetLocator("dittologin_lnk"));

            reporter.Add(new Act("Verify 'Get Started Now™' button "));
            Selenide.VerifyVisible(driver, Util.GetLocator("getstartednow_btn"));

            reporter.Add(new Act("Verify close/cancel icon"));
            Selenide.VerifyVisible(driver, Util.GetLocator("dittopopupclose_btn"));
        }

        /// <summary>
        /// Click on "Login to access your existing Ditto" Link
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public static void ClickOnDittoLink(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click on 'login to access your existing Ditto' link "));
            Selenide.WaitForElementVisible(driver, Util.GetLocator("dittologin_lnk"));
            Selenide.Click(driver, Util.GetLocator("dittologin_lnk"));
        }

        /// <summary>
        /// ClickCreateProfileButton method click 'Create Profile' Button
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void ClickCreateProfileButton(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click Create Profile Button"));
            Selenide.Click(driver, Util.GetLocator("dittocreateprofile_btn"));
        }

        /// <summary>
        /// FillCreateProfileForm create new profile account for tryon
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        /// <param name="reEnterPassword"></param>
        /// <param name="email"></param>

        public static void FillCreateProfileForm(RemoteWebDriver driver, Iteration reporter,
            string firstName,
            string lastName,
            string email,
            string password,
            string reEnterPassword)
        {
            TypeUserFName(driver, reporter, firstName);
            TypeUserLName(driver, reporter, lastName);
            TypeEmail(driver, reporter, email);
            TypePassword(driver, reporter, password);
            TypeReEnterPassword(driver, reporter, reEnterPassword);
        }

        /// <summary>
        /// TypeUserFName method enters First name in the first name field of create new profile form
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="firstName">First name to create new profile</param>
        public static void TypeUserFName(RemoteWebDriver driver, Iteration reporter, string firstName)
        {
            reporter.Add(new Act("Enter first name"));
            Selenide.WaitForElementVisible(driver, Util.GetLocator("dittoformfirstname_txt"));
            Selenide.SetText(driver, Util.GetLocator("dittoformfirstname_txt"), Selenide.ControlType.Textbox, firstName);
        }

        /// <summary>
        /// TypeUserLName method enters Last name in the Last name field of create new profile form
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="lastName">Lastname</param>
        public static void TypeUserLName(RemoteWebDriver driver, Iteration reporter, string lastName)
        {
            reporter.Add(new Act("Enter last name for new profile creation"));
            Selenide.WaitForElementVisible(driver, Util.GetLocator("dittoformlastname_txt"));
            Selenide.SetText(driver, Util.GetLocator("dittoformlastname_txt"), Selenide.ControlType.Textbox, lastName);
        }

        /// <summary>
        /// TypePassword method enters password
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="Password">Password</param>
        public static void TypePassword(RemoteWebDriver driver, Iteration reporter, string Password)
        {
            reporter.Add(new Act("Enter password"));
            Selenide.WaitForElementVisible(driver, Util.GetLocator("dittoformpwd_txt"));
            Selenide.SetText(driver, Util.GetLocator("dittoformpwd_txt"), Selenide.ControlType.Textbox, Password);
        }

        /// <summary>
        /// TypeReEnterPassword method reenters password for confirmation
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="ReEnterPassword">ReEnterPassword</param>
        public static void TypeReEnterPassword(RemoteWebDriver driver, Iteration reporter, string ReEnterPassword)
        {
            reporter.Add(new Act("Renter password"));
            Selenide.SetText(driver, Util.GetLocator("dittoformrenterpwd_txt"), Selenide.ControlType.Textbox, ReEnterPassword);
        }

        /// <summary>
        /// TypeEmail method enters valid email address
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="email">Email address wish to enter for new profile creation</param>
        public static void TypeEmail(RemoteWebDriver driver, Iteration reporter,
            string email)
        {
            reporter.Add(new Act("Enter Email Address for new profile creation"));
            Selenide.SetText(driver, Util.GetLocator("dittoformemail_txt"), Selenide.ControlType.Textbox, email);
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
            Selenide.Click(driver, Util.GetLocator("termsandcondition_chkbox"));
        }

        /// <summary>
        /// DittoClickCreateProfileButton click on Create button to create new profile for ditto
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="TypeEmail">click createprofile button</param>
        public static void DittoClickCreateProfileButton(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click Ditto Click Create Profile Button"));
            Selenide.Click(driver, Util.GetLocator("clickcreateprofile_btn"));

            WaitUnitlSpinnerDisappears(driver);
        }

        /// <summary>
        /// ClickManageTryOnBtn click on Manage button
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="TypeEmail">CheckTermsAndConditionChkBox</param>
        public static void ClickManageTryOnBtn(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click on Manage Try On button"));
            Selenide.WaitForElementVisible(driver, Util.GetLocator("managetryon_btn"));
            Selenide.Click(driver, Util.GetLocator("managetryon_btn"));
        }

        /// <summary>
        /// AssertManageTryOnSection verify manage tryon button for new created user  
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="patinetName"></param>
        public static void AssertManageTryOnSection(RemoteWebDriver driver, 
            Iteration reporter,
            string patinetName)
        {
            reporter.Add(new Act("Verify patient name On manage tryon"));
            Selenide.VerifyVisible(driver, Locator.Get(LocatorType.XPath,
                string.Format(@"//div[@class='tryOnPatient']/descendant::b[contains(text(),'{0}')]", patinetName)));
        }


        /// <summary>
        /// EnterReturnCustomerEmail method is used to enter the Return Customer Email
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="returnMail">Returning Cm Email</param>
        public static void EnterReturnCustomerEmail(RemoteWebDriver driver, Iteration reporter,
            string returnMail)
        {
            reporter.Add(new Act("Enter Email in the returning customer section"));
            Selenide.SetText(driver, Util.GetLocator("existingemail_txt"), Selenide.ControlType.Textbox, returnMail);
        }


        /// <summary>
        /// EnterReturnCustomerPwd method is used to enter the Return Customer Email
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        /// <param name="returnMail">Returning Cm Pwd</param>
        public static void EnterReturnCustomerPwd(RemoteWebDriver driver, Iteration reporter,
            string returnPwd)
        {
            reporter.Add(new Act("Enter Pwd in the returning customer section"));
            Selenide.SetText(driver, Util.GetLocator("existingpwd_txt"), Selenide.ControlType.Textbox, returnPwd);
        }


        /// <summary>
        /// ClickReturnCustomerLogin method is to click on Login in Returning Cm section
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="reporter"></param>
        public static void ClickReturnCustomerLogin(RemoteWebDriver driver, Iteration reporter)
        {
            reporter.Add(new Act("Click on Login in the returning customer section"));
            Selenide.WaitForElementVisible(driver, Util.GetLocator("signin_btn"));
            Selenide.JS.Click(driver, Util.GetLocator("signin_btn"));
        }



    }

}

