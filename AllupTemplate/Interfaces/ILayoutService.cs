using AllupTemplate.Models;
using AllupTemplate.Views.BaketViewModels;

namespace AllupTemplate.Interfaces
{
    public interface ILayoutService
    {
        Task<IDictionary<string,string>> GetSettings();
        Task<IEnumerable<Category>> GetCategories();

        Task<List<BasketVM>> GetBaskets();
    }
}
