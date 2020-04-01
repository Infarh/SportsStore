using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Components
{
    public class IncludeAjaxUnobtrusiveViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
