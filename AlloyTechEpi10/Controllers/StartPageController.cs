using System.Web.Mvc;
using AlloyTechEpi10.Models.Pages;
using EPiServer.Web;
using EPiServer.Web.Mvc;

namespace AlloyTechEpi10.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {
        public ActionResult Index(StartPage currentPage)
        {
            // Check if it is the StartPage or just a page of the StartPage type.
            if(SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink))
            {
                // Connect the view models logotype property to the start page's to make it editable
                var editHints = ViewData.GetEditHints<StartPage, StartPage>();
                editHints.AddConnection(m => PageLayout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => PageLayout.ProductPages, p => p.ProductPageLinks);
                editHints.AddConnection(m => PageLayout.CompanyInformationPages, p => p.CompanyInformationPageLinks);
                editHints.AddConnection(m => PageLayout.NewsPages, p => p.NewsPageLinks);
                editHints.AddConnection(m => PageLayout.CustomerZonePages, p => p.CustomerZonePageLinks);
            }

            return View(currentPage);
        }
    }
}
