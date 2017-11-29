using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dtso.api.Models.Forms;
using dtso.core.Managers;
using dtso.api.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using dtso.core.Utilties;
using dtso.core.Models;
using System.Collections;
using dtso.core.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dtso.api.Controllers
{
    [Route("api/ticket")]
    [Authorize]
    public class TicketController : Controller
    {
        private TicketManager _ticketManager;

        public TicketController(TicketManager ticketManager)
        {
            _ticketManager = ticketManager;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddTicket([FromBody] List<TicketForm> form)
        {
            var error = new Error();
            List <Ticket> tickets = new List<Ticket>();
            foreach(var ticket in form)
            {
                tickets.Add(ticket.MapToCore());
            }

            _ticketManager.AddTickets(tickets, ref error);
            if (error.ErrorCode != ErrorCode.OKAY)
                return BadRequest(error.Message);

            return Ok();
        }

        [HttpPost("edit")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditTicket([FromBody] TicketForm form)
        {
            var error = new Error();
            var ticket = _ticketManager.EditTicket(form.MapToCore(), ref error);

            if (error.ErrorCode != ErrorCode.OKAY)
                return BadRequest(error.Message);

            var response = TicketBasic.MapFromObject(ticket);
            return Ok(response);
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

        [HttpGet("{ticketId}")]
        public IActionResult GetTicket(int ticketId)
        {
            var ticket = _ticketManager.GetTicket(ticketId);
            
            var response = TicketBasic.MapFromObject(ticket);

            return Ok(response);
        }

    }
}
