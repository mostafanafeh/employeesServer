namespace employeesServer.Models
{
    public class Employee
    {
        public Guid id { get; set; }
        public string name { get; set; }

        public string Email { get; set; }
        public long Phone { get; set; }
        public long Salary { get; set; }
        public string Department { get; set; }

    }
}
