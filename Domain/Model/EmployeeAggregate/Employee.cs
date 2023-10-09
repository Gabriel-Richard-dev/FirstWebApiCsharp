using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Domain.Model.EmployeeAggregate;
[Table("employee")]
public class Employee
{
    [Key]
    public int id { get; private set; }

    public string name { get; private set; }

    public int age { get; private set; }

    public string? photo { get; private set; }
    public Employee() { }
    public Employee(int id, string name, int age, string photo)
    {
        this.id = id;
        this.name = name;
        this.age = age;
        this.photo = photo;
    }

  
}