using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    
    public record BuyerCreateDto : BuyerManipulationDto
    {
        [Required(ErrorMessage = "Buyer Name is required")]
        public string? Name { get; init; }
    }
}
