using EPiServer.Core;

namespace AlloyTechEpi10.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
