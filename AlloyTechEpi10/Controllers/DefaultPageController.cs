using System.Web.Mvc;
using AlloyTechEpi10.Models.Pages;
using EPiServer;
using EPiServer.Framework.DataAnnotations;

namespace AlloyTechEpi10.Controllers
{
    /// <summary>
    ///     Concrete controller that handles all page types that don't have their own specific controllers.
    /// </summary>
    /// <remarks>
    ///     Note that as the view file name is hard coded it won't work with DisplayModes (ie Index.mobile.cshtml).
    ///     For page types requiring such views add specific controllers for them. Alterntively the Index action
    ///     could be modified to set ControllerContext.RouteData.Values["controller"] to type name of the currentPage
    ///     argument. That may however have side effects.
    /// </remarks>
    [TemplateDescriptor(Inherited = true)]
    public class DefaultPageController : PageControllerBase<SitePageData>
    {
        public ViewResult Index(SitePageData currentPage)
        {
            return View($"~/Views/{currentPage.GetOriginalType().Name}/Index.cshtml", currentPage);
        }
    }
}
