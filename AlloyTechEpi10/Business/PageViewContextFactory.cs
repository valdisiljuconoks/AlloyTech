using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AlloyTechEpi10.Models.Pages;
using AlloyTechEpi10.Models.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Data;
using EPiServer.Web;
using EPiServer.Web.Routing;

namespace AlloyTechEpi10.Business
{
    public class PageViewContextFactory
    {
        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;
        private readonly IDatabaseMode _databaseMode;

        public PageViewContextFactory(IContentLoader contentLoader, UrlResolver urlResolver, IDatabaseMode databaseMode)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
            _databaseMode = databaseMode;
        }

        private string GetLoginUrl(ContentReference returnToContentLink)
        {
            return string.Format(
                "{0}?ReturnUrl={1}",
                (FormsAuthentication.IsEnabled ? FormsAuthentication.LoginUrl : VirtualPathUtility.ToAbsolute(Global.AppRelativeLoginPath)),
                _urlResolver.GetUrl(returnToContentLink));
        }

        public virtual IContent GetSection(ContentReference contentLink)
        {
            var currentContent = _contentLoader.Get<IContent>(contentLink);
            if (currentContent.ParentLink != null && currentContent.ParentLink.CompareToIgnoreWorkID(SiteDefinition.Current.StartPage))
            {
                return currentContent;
            }

            return _contentLoader.GetAncestors(contentLink)
                .OfType<PageData>()
                .SkipWhile(x => x.ParentLink == null || !x.ParentLink.CompareToIgnoreWorkID(SiteDefinition.Current.StartPage))
                .FirstOrDefault();
        }

        public LayoutModel UpdateLayoutModel(LayoutModel pageLayout, ContentReference currentContentLink, RequestContext requestContext)
        {
            var startPageContentLink = SiteDefinition.Current.StartPage;

            // Use the content link with version information when editing the startpage,
            // otherwise the published version will be used when rendering the props below.
            if (currentContentLink.CompareToIgnoreWorkID(startPageContentLink))
            {
                startPageContentLink = currentContentLink;
            }

            var startPage = _contentLoader.Get<StartPage>(startPageContentLink);

            pageLayout.Logotype = startPage.SiteLogotype;
            pageLayout.LogotypeLinkUrl = new MvcHtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage));
            pageLayout.ProductPages = startPage.ProductPageLinks;
            pageLayout.CompanyInformationPages = startPage.CompanyInformationPageLinks;
            pageLayout.NewsPages = startPage.NewsPageLinks;
            pageLayout.CustomerZonePages = startPage.CustomerZonePageLinks;
            pageLayout.LoggedIn = requestContext.HttpContext.User.Identity.IsAuthenticated;
            pageLayout.LoginUrl = new MvcHtmlString(GetLoginUrl(currentContentLink));
            pageLayout.SearchActionUrl = new MvcHtmlString(UrlResolver.Current.GetUrl(startPage.SearchPageLink));
            pageLayout.IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly;

            return pageLayout;
        }
    }
}
