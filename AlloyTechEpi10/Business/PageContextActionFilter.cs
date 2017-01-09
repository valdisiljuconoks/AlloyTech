using System.Web.Mvc;
using AlloyTechEpi10.Models.ViewModels;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

namespace AlloyTechEpi10.Business
{
    /// <summary>
    /// Intercepts actions with view models of type IPageViewModel and populates the view models
    /// Layout and Section properties.
    /// </summary>
    /// <remarks>
    /// This filter frees controllers for pages from having to care about common context needed by layouts
    /// and other page framework components allowing the controllers to focus on the specifics for the page types
    /// and actions that they handle. 
    /// </remarks>
    public class PageContextActionFilter : IResultFilter
    {
        private readonly PageViewContextFactory _contextFactory;
        public PageContextActionFilter(PageViewContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //var viewModel = filterContext.Controller.ViewData.Model;

            //var model = viewModel as IPageViewModel<SitePageData>;

            var currentContentLink = filterContext.RequestContext.GetContentLink();

            if(currentContentLink != null)
            {
                var pageLayout = ServiceLocator.Current.GetInstance<LayoutModel>();
                _contextFactory.UpdateLayoutModel(pageLayout, currentContentLink, filterContext.RequestContext);
                var layoutController = filterContext.Controller as IModifyLayout;
                layoutController?.ModifyLayout(pageLayout);

                pageLayout.Section = _contextFactory.GetSection(currentContentLink);

            }

            //if (model != null)
            //{
            //    var currentContentLink = filterContext.RequestContext.GetContentLink();

            //    var layoutModel = model.Layout ?? _contextFactory.CreateLayoutModel(currentContentLink, filterContext.RequestContext);

            //    var layoutController = filterContext.Controller as IModifyLayout;
            //    if(layoutController != null)
            //    {
            //        layoutController.ModifyLayout(layoutModel);
            //    }

            //    //model.Layout = layoutModel;

            //    //if (model.Section == null)
            //    //{
            //    //    model.Section = _contextFactory.GetSection(currentContentLink);
            //    //}
            //}
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
    }
}
