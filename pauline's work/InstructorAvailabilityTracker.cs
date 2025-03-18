namespace DrivingLessonsBooking.DataStructures
{
    /// <summary>
    /// Custom data structure for tracking instructor availability
    /// </summary>
    public class InstructorAvailabilityTracker
    {
        private Dictionary<string, List<DateTime>> availability;

        public InstructorAvailabilityTracker()
        {
            availability = new Dictionary<string, List<DateTime>>();
        }

        /// <summary>
        /// Adds availability for an instructor
        /// </summary>
        /// <param name="instructorId">The instructor's ID</param>
        /// <param name="availableDate">The date the instructor is available</param>
        public void AddAvailability(string instructorId, DateTime availableDate)
        {
            if (!availability.ContainsKey(instructorId))
            {
                availability[instructorId] = new List<DateTime>();
            }
            availability[instructorId].Add(availableDate);
        }

        /// <summary>
        /// Checks if an instructor is available on a specific date
        /// </summary>
        /// <param name="instructorId">The instructor's ID</param>
        /// <param name="date">The date to check</param>
        /// <returns>True if available, false otherwise</returns>
        public bool IsAvailable(string instructorId, DateTime date)
        {
            return availability.ContainsKey(instructorId) && availability[instructorId].Contains(date);
        }

        /// <summary>
        /// Removes availability for an instructor on a specific date
        /// </summary>
        /// <param name="instructorId">The instructor's ID</param>
        /// <param name="date">The date to remove</param>
        public void RemoveAvailability(string instructorId, DateTime date)
        {
            if (availability.ContainsKey(instructorId))
            {
                availability[instructorId].Remove(date);
                if (availability[instructorId].Count == 0)
                {
                    availability.Remove(instructorId);
                }
            }
        }
    }
}