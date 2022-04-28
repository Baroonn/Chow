using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid orderId)
            : base($"The order with the id: {orderId} doesn't exist in the database.")
        { }
    }
}
