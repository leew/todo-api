using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private ISlackService _slackService;

        private readonly IMQTTClientService _mQTTClient;
        private readonly PostContext _context;

        public PostController(PostContext context, IMQTTClientService mQTTClient, ISlackService slackService)
        {
            _context = context;
            _slackService = slackService;
            _mQTTClient = mQTTClient;
        }

        // GET api/post
        [HttpGet]
        [Produces("application/json")]
        public ActionResult<IEnumerable<Message>> Get()
        {
            return _context.Messages.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        
        /// <summary>
        /// Sends a new Post to Slack Channel.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Post
        ///     {
        ///        "text": "Message to be sent"
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created Message</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> Post([FromBody] Message message)
        {
            string response = await _slackService.sendAsyncRequest(message);
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            _mQTTClient.publish("Lee", message.text);
            return CreatedAtAction("Get", new { id = message.Id }, message);
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
