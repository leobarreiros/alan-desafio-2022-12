using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ResponseHandlers.Dtos
{
    public class BadRequestDto
    {
        public BadRequestDto(string tag, string description)
        {
            Tag = tag;
            Description = description;
        }

        public string Tag { get; set; }
        public string Description { get; set; }
    }
}
