namespace EployeeData.Models
{
    /// <summary>
    /// Represents an employee with their details.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the designation of the employee.
        /// </summary>
        public string Designation { get; set; }
    }
}
