using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.ResponseHandlers.Dtos
{
    public abstract class ResponseHandlerDto
    {
        public ResponseHandlerDto()
        {

        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public HttpStatusCode? Status { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<BadRequestDto> Errors { get; set; }
    }
}
