using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chow.Presentation.Controllers
{
    public class OrdersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public OrdersController(IServiceManager service)
        {
            _service = service;
        }

        
    }
}
