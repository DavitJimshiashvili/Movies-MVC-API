using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class ObjectNotFoundException:Exception
    {
        public string Code = "ObjectNotFound";
        public ObjectNotFoundException(string errorMessage) : base(errorMessage) { }
    }
}
