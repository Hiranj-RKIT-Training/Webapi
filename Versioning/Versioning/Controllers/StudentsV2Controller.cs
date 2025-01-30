using System.Collections.Generic;
using System.Web.Http;
using Versioning.Models;

namespace Versioning.Controllers
{
    /// <summary>
    /// API Controller for managing StudentV2 data.
    /// This is version 2 of the Students API, introducing first and last name fields.
    /// </summary>
    public class StudentsV2Controller : ApiController
    {
        /// <summary>
        /// A static list of students to simulate data storage for version 2 of the API.
        /// </summary>
        private List<StudentV2> lstStudents = new List<StudentV2>()
        {
            new StudentV2 { Id = 1, FirstName = "Alice", LastName = "Johnson" },
            new StudentV2 { Id = 2, FirstName = "Bob", LastName = "Smith" },
            new StudentV2 { Id = 3, FirstName = "Charlie", LastName = "Brown" }
        };

        /// <summary>
        /// Retrieves the list of all students in version 2 of the API.
        /// </summary>
        /// <returns>An HTTP response containing the list of students.</returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(lstStudents);
        }

        /// <summary>
        /// Retrieves a specific student by their ID in version 2 of the API.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>
        /// An HTTP response containing the student with the specified ID if found;
        /// otherwise, a Bad Request response.
        /// </returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            StudentV2 student = lstStudents.Find(s => s.Id == id);

            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return BadRequest($"Student with ID {id} not found.");
            }
        }
    }
}
