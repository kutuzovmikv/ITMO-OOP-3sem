﻿using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Models.Employees;
using Reports.Server.Services;

namespace Reports.Server.Controllers;

[ApiController]
[Route("/supervisors")]
public class SupervisorController : ControllerBase
{
    private readonly SupervisorService _service;

    public SupervisorController(SupervisorService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<EmployeeModel> Create([FromQuery] string name)
    {
        return await _service.Create(name);
    }

    [HttpGet("name")]
    public IActionResult FindByName([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return BadRequest();
        EmployeeModel result = _service.FindByName(name);
        if (result != null)
        {
            return Ok(result);
        }

        return NotFound();
    }

    [HttpGet("id")]
    public IActionResult FindById([FromQuery] Guid id)
    {
        if (id == Guid.Empty) return BadRequest();
        EmployeeModel result = _service.FindById(id);
        if (result != null)
        {
            return Ok(result);
        }

        return NotFound();
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpDelete]
    public void Delete([FromQuery] Guid id)
    {
        _service.Delete(id);
    }

    [HttpPut]
    public EmployeeModel Update([FromQuery] EmployeeModel entity)
    {
        return _service.Update(entity);
    }

    [HttpGet("subordinatesTasks")]
    public IActionResult GetAll(Guid id)
    {
        return Ok(_service.GetSubordinatesTasks(id));
    }

    [HttpPut("subordinate")]
    public EmployeeModel AddSubordinate([FromQuery] Guid supervisorId, [FromQuery] Guid employeeId)
    {
        return _service.AddSubordinate(supervisorId, employeeId);
    }
}