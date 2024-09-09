using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public interface IEmployeeRepository //Note: This is a custom service.
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetAllEmployee(); //Note: Here we have declared another method.
        Employee Add(Employee employee);

        Employee Update(Employee employeechanges);

        Employee Delete(int id);
    }
}
