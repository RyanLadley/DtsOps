using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dtso.api.Models.Forms;
using dtso.core.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dtso.api.Controllers
{
    [Route("api/ticket")]
    public class TicketController : Controller
    {
        private TicketManager _ticketManager;

        public TicketController(TicketManager ticketManager)
        {
            _ticketManager = ticketManager;
        }

        [HttpPost]
        public IActionResult AddTicket([FromBody] List<TicketForm> form)
        {
            foreach(var ticket in form)
            {
                if(ticket.IsValid())
                    _ticketManager.AddTicket(ticket.MapToCore());
            }

            return Ok();
        }
        
    }
}
