/* **********************************************************
 * Description: MF_TC077.cs test case for 
 *     Verify if user is able to create an account from favourites page
 *Preconditions: 1. User is not logged in
                2. Choose a product for which Try-On feature is availablePreconditions
 * Date :  04-April-2016 
+- * **********************************************************
 */
using Automation.Mercury;
using NationalVision.Automation.Pages;

namespace NationalVision.Automation.Tests.Cases.MyFavourite
{
    /// <summary>
    /// Description : MF_TC077.cs test case for 
    ///               Verify if user is able to create an account from favourites page
    /// </summary>
    class MF_TC077 : BaseCase
    {
        protected override void ExecuteTestCase()
        {
            Reporter.Chapter.Title = "Verify if user is able to first create a ditto and then a new account to see if the created ditto gets saved";
            Step = "Launch America Best Web Application";
            CommonPage.NavigateTo(Driver, Reporter, Util.EnvironmentSettings["Server"]);

            Step = "Hover Over Eyeglasses Tab";
            CommonPage.MouseOverHomePageTabs(Driver, Reporter, TestData["TABNAME"]);

            Step = "Verify Sub tabs under eyeglasses tab";
            string[] sections = { "shop by type", "shop by price", "learn more" };
            CommonPage.AssertSubSections(Driver, Reporter, sections);

            Step = "Select 'All Glasses' under 'Shop By Type'";
            CommonPage.ClickSubMenuLink(Driver, Reporter, TestData["SUB_LINK"]);

            Step = "Select any section of glasses then verifying the resutls sortby, pagination, results per page";
            EyeGlassesShelfPage.VerifySortBy(Driver, Reporter);
            EyeGlassesShelfPage.VerifyAddToFavorites(Driver, Reporter);
            EyeGlassesShelfPage.VerifyPagination(Driver, Reporter);
            EyeGlassesShelfPage.VerifyResultsPerPage(Driver, Reporter);

            Step = "Verify tryon, click on the product and verify all the features of the product";
            string _product = TestData["PRODUCT"];
            EyeGlassesShelfPage.ClickMyFavoritesIcon(Driver, Reporter, _product);
            EyeGlassesShelfPage.ClickTopMenuLink(Driver, Reporter, "my favorites");

            Step = "Verify 'TryOn', 'View Product Page Link', 'Remove link', 'Print to take to store' for each product";
            FavoritePage.VerifyPrintToStore(Driver, Reporter);
            FavoritePage.VerifyViewButtonFavorite(Driver, Reporter);
            FavoritePage.VerifyRemoveButtonFavorite(Driver, Reporter);

            Step = "Click 'Try on' button of the product";
            FavoritePage.ClickTryOnButton(Driver, Reporter);
            FavoritePage.VerifyVirtualTryOnWrapperDetails(Driver, Reporter);

            Step = "Click on 'Login to access your existing Ditto' link";
            FavoritePage.ClickOnDittoLink(Driver, Reporter);
            FavoritePage.ClickCreateProfileButton(Driver, Reporter);

            Step = "Fill the Form and click create profile button";
            FavoritePage.FillCreateProfileForm(Driver, Reporter,
                newUserFName,
                newUserNName,
                getDynamicEmail,
                standardPassword,
                standardPassword);
            FavoritePage.CheckTermsAndConditionChkBox(Driver, Reporter);
            FavoritePage.DittoClickCreateProfileButton(Driver, Reporter);

            Step = "Click Manage Buton after profile creation";
            FavoritePage.ClickManageTryOnBtn(Driver, Reporter);
            FavoritePage.AssertManageTryOnSection(Driver, Reporter, newUserFName + " " + newUserNName);

            Step = "Click 'My account / Register' link";
            FavoritePage.ClickTopMenuLink(Driver, Reporter, "my account / register");
            
        }
    }
}
