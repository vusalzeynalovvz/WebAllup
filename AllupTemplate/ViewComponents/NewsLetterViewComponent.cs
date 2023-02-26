using Microsoft.AspNetCore.Mvc;

namespace AllupTemplate.ViewComponents
{
    public class NewsLetterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(); 
        }
    }
}
