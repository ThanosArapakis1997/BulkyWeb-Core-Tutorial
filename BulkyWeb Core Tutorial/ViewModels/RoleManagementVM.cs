using MGTConcerts.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MGTConcerts.ViewModels
{
    public class RoleManagementVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
