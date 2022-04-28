 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record BuyerManipulationDto
    {
        
        [Required(ErrorMessage = "Buyer Email is required")]
        public string? Email { get; init; }
        [Required(ErrorMessage = "Buyer Phone is required")]
        public string? Phone { get; init; }
    }
}
