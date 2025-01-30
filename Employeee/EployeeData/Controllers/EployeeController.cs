using EployeeData.Models; // Reference to the Models namespace where the Employee class is defined.

namespace EployeeData.Controllers
{
    /// <summary>
    /// API Controller to manage Employee data.
    /// Provides endpoints for CRUD operations on employee records.
    /// </summary>
    public class EployeeController : ApiController
    {
        /// <summary>
        /// Static list to store employee data in memory.
        /// </summary>
        static List<Employee> employees = new List<Employee>
        {
            new Employee(1, "Alice Johnson", "Software Engineer"),
            new Employee(2, "Bob Smith", "Project Manager"),
            new Employee(3, "Charlie Brown", "QA Engineer")
        };

        /// <summary>
        /// Retrieves the list of all employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return employees;
        }

        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The employee with the specified ID, or an error if not found.</returns>
        [HttpGet]
        public HttpResponseMessage GetEmplpyeeByID(int id)
        {
            Employee employee = employees.Find(e => (id == e.Id));

            if (employee != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with ID {id} not found.");
            }
        }

        /// <summary>
        /// Retrieves an employee by their name.
        /// </summary>
        /// <param name="name">The name of the employee to retrieve.</param>
        /// <returns>The employee with the specified name, or an error if not found.</returns>
        public HttpResponseMessage GetEmployeeByName(string name)
        {
            Employee employee = employees.Find(e => (name == e.Name));

            if (employee != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with name {name} not found.");
            }
        }

        /// <summary>
        /// Adds a new employee to the list.
        /// </summary>
        /// <param name="employee">The employee object to add.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPost]
        public HttpResponseMessage AddEmployee([FromBody] Employee employee)
        {
            try
            {
                employees.Add(employee);
                Console.WriteLine(employees.Count);
                return Request.CreateResponse(HttpStatusCode.Created, employee);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        /// <summary>
        /// Updates an existing employee's details.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="rqstEmployee">The updated employee details.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPut]
        public HttpResponseMessage UpdateEmployee(int id, [FromBody] Employee rqstEmployee)
        {
            try
            {
                Employee employee = employees.Find(e => e.Id == id);

                if (employee == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with ID {id} not found.");
                }
                else
                {
                    employee.Id = rqstEmployee.Id;
                    employee.Name = rqstEmployee.Name;
                    employee.Designation = rqstEmployee.Designation;
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        /// <summary>
        /// Deletes an employee from the list.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpDelete]
        public HttpResponseMessage DeleteEmployee(int id)
        {
            try
            {
                Employee employee = employees.Find(e => e.Id == id);

                if (employee == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with ID {id} not found.");
                }
                else
                {
                    employees.Remove(employee);
                    return Request.CreateResponse(HttpStatusCode.OK, $"Employee with ID {id} deleted successfully.");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
