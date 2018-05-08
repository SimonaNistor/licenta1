using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Extensions
{
    public static class HtmlHelperForView
    {
        public static HtmlString ViewHelper(List<LandingPage.Models.CMSViewModels.CMSViewModels> cms, int id, string detailZone)
        {
            var result = cms.Where(item => item.Id == id).First().CMSDetails.Where(detail => detail.Name == detailZone).First().Content;
            return new HtmlString(result);
        }

        public static List<string> ImageViewHelper(List<LandingPage.Models.CMSViewModels.CMSViewModels> cms, int tip,int id, string name)
        {
            var result = cms.Where(item => item.Id == id).First().CMSDetails.Where(item => item.HtmlTypeId == tip).Where(item => item.Name==name).OrderBy(obj => obj.OrderIndex).Select(obj =>obj.Content);
            return result.ToList();
        }

        public static string ImageViewHelper(List<LandingPage.Models.CMSDetailsViewModels.CMSDetailsViewModels> cms, int tip, int ox,string name)
        {
            bool exists = cms.Exists(element => element.Name == name && element.OrderIndex == ox);
            string result = "#";
            if (exists)
            {
                result = cms.Where(x => x.Name == name && x.OrderIndex == ox).Select(x => x.Content).First();
            }
            return result;
        }

    }
}
