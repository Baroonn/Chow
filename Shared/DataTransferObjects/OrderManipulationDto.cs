using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record OrderManipulationDto
    {
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; init; }
        [Required(ErrorMessage = "Buyer Phone is required")]
        public string? BuyerPhone { get; init; }    
    }
}
