using System.Web.Mvc;
using AlloyTechEpi10.Models.Pages;
using AlloyTechEpi10.Models.ViewModels;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

namespace AlloyTechEpi10.Infrastructure
{
    public class CustomWebViewPage : WebViewPage
    {
        public override void Execute() { }
    }

    public class CustomWebViewPage<TModel> : WebViewPage<TModel>
    {
        private Injected<LayoutModel> LayoutModel { get; set; }

        public LayoutModel PageLayout => LayoutModel.Service;

        public SitePageData CurrentPage => (SitePageData) ServiceLocator.Current.GetInstance<IPageRouteHelper>().Page;

        public override void Execute() { }
    }
}
