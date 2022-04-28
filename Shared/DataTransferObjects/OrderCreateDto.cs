using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record OrderCreateDto : OrderManipulationDto
    {
        [Required(ErrorMessage = "Buyer Location is required")]
        public string? BuyerLocation { get; init; }
        public Guid StoreId { get; init; }
    }


}
