﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Models.Tasks;
using Reports.Server.Services;

namespace Reports.Server.Controllers;

[ApiController]
[Route("/tasks")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;

    public TaskController(ITaskService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<TaskModel> Create([FromQuery] string name)
    {
        return await _service.Create(name);
    }

    [HttpGet]
    [Route("id")]
    public IActionResult FindById([FromQuery] Guid id)
    {
        if (id != Guid.Empty)
        {
            TaskModel result = _service.FindById(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        return StatusCode((int) HttpStatusCode.BadRequest);
    }

    [HttpGet]
    [Route("all")]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet]
    [Route("creationTime")]
    public IActionResult FindByCreationTime([FromQuery] DateTime creationTime)
    {
        TaskModel result = _service.FindByCreationTime(creationTime);
        if (result != null)
        {
            return Ok(result);
        }

        return NotFound();
    }

    [HttpGet]
    [Route("modificationTime")]
    public IActionResult FindByModificationTime([FromQuery] DateTime modificationTime)
    {
        TaskModel result = _service.FindByCreationTime(modificationTime);
        if (result != null)
        {
            return Ok(result);
        }

        return NotFound();
    }

    [HttpDelete]
    public void Delete([FromQuery] Guid id)
    {
        _service.Delete(id);
    }

    [HttpPut]
    [Route("condition")]
    public TaskModel UpdateCondition([FromQuery] Guid id, [FromQuery] TaskCondition newCondition)
    {
        return _service.UpdateCondition(id, newCondition);
    }

    [HttpPut]
    [Route("comment")]
    public TaskModel AddComment([FromQuery] Guid id, [FromQuery] string comment)
    {
        return _service.AddComment(id, comment);
    }
}