using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameFun.ViewsModels
{
    public class CreateGameFormViewModel
    {
       
        [Required]
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;









        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();


        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem> Devices { get; set; } = new List<SelectListItem>();

        [Required]
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public IFormFile Cover { get; set; } = default!;
    }
}
