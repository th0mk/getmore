using System;

namespace GetMore.Domain.Models
{
    public class UnitOfWork
    {
        public int UnitOfWorkId { get; set; }

        public int ProjectId { get; set; }

        public int EmployeeId { get; set; }

        public TimeSpan TimeAmount  { get; set; }

        public DateTime WorkDate { get; set; }
    }
}