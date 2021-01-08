﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QueueApi.DTO;
using QueueApi.Models;
using QueueApi.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QueueApi.Controllers
{
    [Route("api/[controller]")]
    public class QueueController : Controller
    {
        private readonly IQueueRepository _queueRepository;

        public QueueController(IQueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }


        [HttpPost("add")]
        public async Task<ActionResult> Add(MessageDto message)
        {
            Message m = new Message()
            {
                Id = Guid.NewGuid(),
                Type = message.Type,
                Handled = false,
                JsonContent = message.JsonContent,
                AddedAt = DateTime.Now
            };

            if (await _queueRepository.AddMessage(m))
            {
                return Ok("A new message was added.");
            }

            return BadRequest("Error.");
        }


        [HttpGet("handled/{messageId}")]
        public async Task<ActionResult> SetHandled(Guid messageId)
        {
            if (await _queueRepository.SetHandled(messageId))
            {
                return Ok("Message Handled.");
            }

            return BadRequest("Error.");
        }


        [HttpGet("retrieve/email")]
        public async Task<ActionResult> GetEmail()
        {
            var messages = await _queueRepository.GetEmailMessage();
            return Ok(messages);
        }


        [HttpGet("retrieve/log")]
        public async Task<ActionResult> GetLog()
        {
            var messages = await _queueRepository.GetLoggingMessage();
            return Ok(messages);
        }
    }
}
