namespace WebApi.Application.ViewModel;

public class EmployeeViewModel
{
    public string name { get; set; }
    public int age { get; set; }
    public int id { get; set; }
    public IFormFile Photo { get; set; }
}