using MGTConcerts.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MGTConcerts.ViewModels
{
    public class ConcertVm
    {
        public Concert Concert {  get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Artists { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MusicVenues { get; set; }


    }
}
