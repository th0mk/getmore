using System;
using System.Collections.Generic;
using MediatR;

namespace GetMore.Application.Queries
{
    public class GetUnitsOfWorkForWeekQuery : IRequest<IEnumerable<UnitsOfWorkForWeekDto>>
    {
        public GetUnitsOfWorkForWeekQuery(DateTime startWorkDate, DateTime endWorkDate)
        {
            this.StartWorkDate = startWorkDate;
            this.EndWorkDate = endWorkDate;
        }

        public DateTime StartWorkDate { get; }

        public DateTime EndWorkDate { get; }
    }
}