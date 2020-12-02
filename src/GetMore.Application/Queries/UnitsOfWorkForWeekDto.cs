using System;

namespace GetMore.Application.Queries
{
    public class UnitsOfWorkForWeekDto
    {
        public int UnitOfWorkId { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public TimeSpan TimeAmount { get; set; }

        public DateTime WorkDate { get; set; }
    }
}