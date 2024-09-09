using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeesList;

        public MockEmployeeRepository()
        {
            _employeesList = new List<Employee>
            {
                new Employee(){Id = 1, Name = "Mary", Department = Dept.HR, Email = "mary@pragimtech.com" }, 
                new Employee(){Id = 2, Name = "John", Department = Dept.IT, Email = "john@pragimtech.com"},
                new Employee(){Id = 3, Name = "Sam", Department = Dept.IT, Email = "sam@pragimtech.com"}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeesList.Max(e => e.Id) + 1;
            _employeesList.Add(employee);
            return employee;
        }
        public Employee GetEmployee(int Id)
        {
            return _employeesList.FirstOrDefault(emp => emp.Id == Id);
        }

        public IEnumerable<Employee> GetAllEmployee() //Here we have defined the method.
        {
            return _employeesList;
        }

        public Employee Update(Employee employeechanges)
        {
            Employee employee = _employeesList.FirstOrDefault(e => e.Id == employeechanges.Id);
            if (employee != null)
            {
                employee.Name = employeechanges.Name;
                employee.Email = employeechanges.Email;
                employee.Department = employeechanges.Department;
            }
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeesList.FirstOrDefault(e => e.Id == id);
            if(employee != null)
            {
                _employeesList.Remove(employee);
            }
            return employee;
        }
    }
}

/*_ViewStart.cshtml in asp net core MVC and uses.*/
/*Imp Note: We have specified Layout property in each of the view pages. What if we have 100's of action method? then we have specify the Layout
file path in each of the view page.*/
/*Dry principle: don't repeat yourself priniciple.*/
/*Specifing the Layout path in each view results in code duplication.*/
/*_ViewStart is a special file in asp net core whose code gets executed before the code in each of the view gets executed.*/
/*Note: We typically place _Viewstart file in View folder of the project.*/