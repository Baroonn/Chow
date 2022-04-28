using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record OrderReadDto(Guid id, string Description, string Status, string BuyerPhone, string BuyerLocation, Guid StoreId, Guid BuyerId);
}
