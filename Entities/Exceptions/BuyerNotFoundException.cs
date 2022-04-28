using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class BuyerNotFoundException : NotFoundException
    {
        public BuyerNotFoundException(Guid buyerId)
            :base($"The buyer with the id: {buyerId} doesn't exist in the database.")
        {

        }
    }
}
