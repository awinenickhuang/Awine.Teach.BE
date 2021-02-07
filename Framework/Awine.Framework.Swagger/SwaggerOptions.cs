using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.Swagger
{
    public class SwaggerOptions : IOptions<SwaggerOptions>
    {
        public string Version { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string XMLDocument { get; set; }

        public SwaggerOptions Value => throw new NotImplementedException();
    }
}
