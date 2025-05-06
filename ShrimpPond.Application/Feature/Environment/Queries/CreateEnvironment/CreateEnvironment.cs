using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Environment.Queries.CreateEnvironment
{
    public class CreateEnvironment: IRequest<string>
    {
        public string PondId {  get; set; } = string.Empty;
        public string Name {  get; set; } = string.Empty;
        public string Value {  get; set; } = string.Empty;
        public DateTime Timestamp {  get; set; } 
    }
}
