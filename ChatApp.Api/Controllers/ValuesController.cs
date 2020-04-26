using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.Api.Hubs;
using ChatApp.Bl.Services.Models;
using ChatApp.Core.IoC;
using ChatApp.Messages.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NServiceBus;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private  IHubContext<ChatAppHub> _hubContext;
        public ValuesController(IMapper mapper, IHubContext<ChatAppHub> hubContext)
        {
            _mapper = mapper;
            //_bus = bus;
            _hubContext = hubContext;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {

            //await _bus.Send(new RequestStockCSV { Id = Guid.NewGuid().ToString(), Stock = "asd" });
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var messageHub = new ChatRoomMessageResponseHubDto();
           // _hubContext = (IHubContext<ChatAppHub>)Dependency.ServiceProvider.GetService(typeof(IHubContext<ChatAppHub>));

            messageHub.ChatRoomId = "asd";
            messageHub.Sender = "asd";
            messageHub.Message = "asd";
            messageHub.MessageDate = DateTime.Now;
            await _hubContext.Clients.Group("5bcefe2b-23f8-458f-a06c-efbdb6f63f56").SendAsync(HubConstants.ON_MSG_RECVD, messageHub);
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
