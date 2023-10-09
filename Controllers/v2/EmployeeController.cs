using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ViewModel;
using WebApi.Domain.DTOs;
using WebApi.Domain.Model.EmployeeAggregate;

namespace WebApi.Controllers.v2;

[ApiController]
[Route("api/v{version:apiVersion}/employee")]
[ApiVersion("2.0")]

public class EmployeeController : ControllerBase
{
    public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger,
        IMapper mapper)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILogger<EmployeeController> _logger;
    private readonly IMapper _mapper;
    
   
    [Authorize]
    [HttpPost]
    public IActionResult Add([FromForm] EmployeeViewModel employeeView)
    {
        var filePath = Path.Combine("Storage", employeeView.Photo.FileName);
        using Stream fileStream = new FileStream(filePath, FileMode.Create);
        employeeView.Photo.CopyTo(fileStream);
        var employee = new Employee(employeeView.id,employeeView.name, employeeView.age, filePath);
        _employeeRepository.add(employee);
        
        return Ok();
    } 
    [Authorize]
    [HttpGet]
    public IActionResult Get(int pageNumber, int pageQuantity)
    {
        var employee = _employeeRepository.Get(pageNumber, pageQuantity);
        return Ok(employee);
    }
    [Authorize]
    [HttpPost]
    [Route("{id}/download")]
    public IActionResult DownloadPhoto(int id)
    {
        var employee = _employeeRepository.Get(id);
        var dataBytes = System.IO.File.ReadAllBytes(employee.photo);
        return File(dataBytes, "image/png");
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Search(int id)
    {
        var employees = _employeeRepository.Get(id);
        var employeesDTOS = _mapper.Map<EmployeeDTO>(employees);
        return Ok(employeesDTOS);
    }
    
}