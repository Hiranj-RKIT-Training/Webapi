using System.Collections.Generic; // Namespace for working with collections like List
using System.Web.Http; // Namespace for Web API classes and attributes
using Versioning.Models; // Namespace for the StudentV1 model class

namespace Versioning.controllers
{
    /// <summary>
    /// Controller for handling API requests related to version 1 of the Student entity.
    /// </summary>
    public class StudentsV1Controller : ApiController
    {
        // Simulated data source: A static list of StudentV1 objects
        List<StudentV1> lstStudents = new List<StudentV1>()
        {
            new StudentV1 { Id = 1, Name = "Alice" },
            new StudentV1 { Id = 2, Name = "Bob" },
            new StudentV1 { Id = 3, Name = "Charlie" }
        };

        /// <summary>
        /// Retrieves the list of all students (version 1).
        /// </summary>
        /// <returns>List of all StudentV1 objects.</returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            // Return the list of students as an HTTP 200 OK response
            return Ok(lstStudents);
        }

        /// <summary>
        /// Retrieves a specific student by ID (version 1).
        /// </summary>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>
        /// HTTP 200 OK with the StudentV1 object if found.
        /// HTTP 400 Bad Request if the student is not found.
        /// </returns>
        public IHttpActionResult Get(int id)
        {
            // Find the student with the specified ID in the list
            StudentV1 student = lstStudents.Find(s => s.Id == id);

            if (student != null)
            {
                // Return the student if found
                return Ok(student);
            }
            else
            {
                // Return HTTP 400 Bad Request if the student is not found
                return BadRequest();
            }
        }
    }
}
