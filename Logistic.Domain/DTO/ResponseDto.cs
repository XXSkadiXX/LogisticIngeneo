using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.Domain.DTO
{
    [ExcludeFromCodeCoverage]
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public object? Result { get; set; }
    }
}
