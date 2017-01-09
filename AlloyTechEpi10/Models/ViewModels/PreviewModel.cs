using System.Collections.Generic;
using EPiServer.Core;

namespace AlloyTechEpi10.Models.ViewModels
{
    public class PreviewModel
    {
        public PreviewModel(IContent previewContent)
        {
            PreviewContent = previewContent;
            Areas = new List<PreviewArea>();
        }

        public IContent PreviewContent { get; set; }
        public List<PreviewArea> Areas { get; set; } 

        public class PreviewArea
        {
            public bool Supported { get; set; }
            public string AreaName { get; set; }
            public string AreaTag { get; set; }
            public ContentArea ContentArea { get; set; }
        }
    }
}
