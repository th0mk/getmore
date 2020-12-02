using System;
using MediatR;

namespace GetMore.Application.Commands
{
    public class AddUnitOfWorkCommand : IRequest
    {
        public int ProjectId { get; set; }

        public int EmployeeId { get; set; }

        public TimeSpan TimeAmount { get; set; }

        public DateTime WorkDate { get; set; }
    }
}