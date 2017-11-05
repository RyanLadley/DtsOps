using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dtso.api.Models.Forms;
using dtso.core.Managers;
using dtso.api.Models.Responses;

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

        [HttpGet("vendor/{vendorId}")]
        public IActionResult GetTicketsForVendor(int vendorId, bool onlyPending = false)
        {
            var tickets = _ticketManager.GetTicketsForVendor(vendorId, onlyPending);

            var response = new List<TicketBasic>();
            foreach(var ticket in tickets)
            {
                response.Add(TicketBasic.MapFromObject(ticket));
            }

            return Ok(response);
        }

    }
}
