using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Components
{
    public class EditorForModelViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(object model) => View(model);
    }
}
