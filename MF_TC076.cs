/* **********************************************************
 * Description : MF_TC076.cs test case for 
 *     Verify if user is able to create an account from favourites page
 * Date :  22-Mar-2016 
 * **********************************************************
 */
using Automation.Mercury;
using NationalVision.Automation.Pages;
namespace NationalVision.Automation.Tests.Cases.MyFavourite
{
    /// <summary>
    /// Description : MF_TC076.cs test case for 
    ///               Verify if user is able to create an account from favourites page
    /// </summary>
    class MF_TC076 : BaseCase
    {
        protected override void ExecuteTestCase()
        {
            Reporter.Chapter.Title = "Verify if user is able to create an account from favourites page";
            Step = "Launch America Best Web Application";
            CommonPage.NavigateTo(Driver, Reporter, Util.EnvironmentSettings["Server"]);
            Step = "Hover Over eyeglasses Tab";
            CommonPage.MouseOverHomePageTabs(Driver, Reporter, TestData["tabName"]);
            Step = "Verifying sub tabs under eyeglasses tab";
            string[] sections = { "shop by type", "shop by price", "learn more" };
            CommonPage.AssertSubSections(Driver, Reporter, sections);
            Step = "Selecting 'All Glasses' under 'Shop By Type'";
            CommonPage.ClickSubMenuLink(Driver, Reporter, TestData["SUB_LINK"]);
            Step = "Selecting any section of glasses then verifying the resutls sortby, pagination, results per page";
            EyeGlassesShelfPage.VerifySortBy(Driver, Reporter);
            EyeGlassesShelfPage.VerifyAddToFavorites(Driver, Reporter);
            EyeGlassesShelfPage.VerifyPagination(Driver, Reporter);
            EyeGlassesShelfPage.VerifyResultsPerPage(Driver, Reporter);

            Step = "Verifying tryon, clicking on the product and verifying all the features in the product display page";
            string _product = TestData["PRODUCT"];
            EyeGlassesShelfPage.ClickMyFavoritesIcon(Driver, Reporter, _product);
            EyeGlassesShelfPage.ClickMyFavoriteLink(Driver, Reporter);

            Step = "Verify Each product should display the below mentioned items 'Try - On' 'View Product Page Link' 'Remove link' 'Print to take to store' ";
            EyeGlassWizardPage.VerifyPrintToStore(Driver, Reporter);
            EyeGlassesShelfPage.VerifyViewButtonFavorite(Driver, Reporter);
            EyeGlassesShelfPage.VerifyRemoveButtonFavorite(Driver, Reporter);

            Step = "In favorites page, click 'Try on' button of the product";
            Selenide.Click(Driver, Util.GetLocator("tryon_img"));

            Step = "Verify the details in try-on popup window";
            EyeGlassesShelfPage.VerifyTryOnPopupDetails(Driver, Reporter);

            Step = "Click on 'Login to access your existing Ditto' link";
            EyeGlassesShelfPage.ClickOnDittoLink(Driver, Reporter);

            Step = " Verify the contents in 'Login to access your existing Ditto' link";
            EyeGlassesShelfPage.ClickCreateProfileButton(Driver, Reporter);
            
            Step = "Fill the Form and Click create profile button";
            EyeGlassesShelfPage.FillCreateProfileForm(Driver, Reporter,TestData["firstName"],TestData["lastName"],TestData["Password"],
                TestData["ReEnterPassword"],TestData["Email"]);
            EyeGlassesShelfPage.CheckTermsAndConditionChkBox(Driver,Reporter);
            EyeGlassesShelfPage.DittoClickCreateProfileButton(Driver, Reporter);

            Step = "Click Manage Buton after profile creation";
            EyeGlassesShelfPage.ClickManageTryOnBtn(Driver, Reporter);

            Step = "Click Manage Buton after profile creation";
            EyeGlassesShelfPage.ClickManageTryOnBtn(Driver, Reporter);
            Step = "Verify Create Try On, Delete Try On, Cancel Elements";
            EyeGlassesShelfPage.AssertManageTryOnSection(Driver, Reporter,TestData["firstName"]);


        }
    }
}