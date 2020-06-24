﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApplication.Data;
using DatingApplication.Dtos;
using DatingApplication.Helper;
using DatingApplication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApplication.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public MessagesController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var messageFromRepo = await _repo.GetMessage(id);
            if (messageFromRepo == null)
                return NotFound();
            return Ok(messageFromRepo);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreation messageForCreation)
        {
            var sender = await _repo.GetUser(userId);
            if (sender.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            messageForCreation.SenderId = userId;
            var recipient = await _repo.GetUser(messageForCreation.RecipientId);
            if (recipient == null)
                return BadRequest("Could not find user");

            var message = _mapper.Map<Message>(messageForCreation);
            _repo.Add(message);
            if(await _repo.SaveAll())
            {
                var messageToReturn = _mapper.Map<MessageToReturn>(message);
                return CreatedAtRoute("GetMessage", new { userId, id = message.Id }, messageToReturn);
            }
            throw new Exception("Craeting the message failed on save");
                
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userId, [FromQuery]MessageParams messageParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            messageParams.UserId = userId;
            var messagesFromRepo = await _repo.GetMessagesForUser(messageParams);

            var messages = _mapper.Map<IEnumerable<MessageToReturn>> (messagesFromRepo);
            Response.AddPagination(messagesFromRepo.CurrentPage, messagesFromRepo.PageSize, 
                                messagesFromRepo.TotalCount, messagesFromRepo.TotalPages);
            return Ok(messages);
        }
        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var messageFromRepo = await _repo.GetMessageThread(userId, recipientId);
            var messageThread = _mapper.Map<IEnumerable<MessageToReturn>>(messageFromRepo);
            return Ok(messageThread);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteMessage(int id, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var messageFromRepo = await _repo.GetMessage(id);

            if (messageFromRepo.SenderId == userId)
                messageFromRepo.SenderDeleted = true; ;
            if (messageFromRepo.RecipientId == userId)
                messageFromRepo.RecipientDeleted = true;
            if (messageFromRepo.SenderDeleted && messageFromRepo.RecipientDeleted)
                _repo.Delete(messageFromRepo);
            if (await _repo.SaveAll())
                return NoContent();
            throw new Exception("Error deleting the message");
        }
        [HttpPost("{id}/read")]
        public async Task<IActionResult> MarkMessageAsRead (int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var message = await _repo.GetMessage(id);
            if (message.RecipientId != userId)
                return Unauthorized();
            message.IsRead = true;
            message.DateRead = DateTime.Now;
            await _repo.SaveAll();
            return NoContent();
        }

    }
}