using AllupTemplate.Models;
using AllupTemplate.Views.BaketViewModels;

namespace AllupTemplate.ViewModels.ComponentViewModels.HeaderViewComponents
{
    public class HeaderVM
    {
        public IDictionary<string, string> Settings { get; set; }

        public List<BasketVM> BasketVMs { get; set; }

        public IEnumerable<Category> Categories { get; set; }


    }
}
