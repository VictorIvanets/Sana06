using Microsoft.AspNetCore.Mvc.Rendering;

namespace MF.Models.ViewModels
{
    public class FishesVM
    {
        public Fishes Fishes { get; set; }
        public IEnumerable<SelectListItem> TypesFishSelectList { get; set; }
    }
}
