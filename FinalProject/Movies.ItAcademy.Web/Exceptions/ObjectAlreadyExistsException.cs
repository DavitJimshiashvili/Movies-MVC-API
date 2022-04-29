using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
   
        public class ObjectAlreadyExistsException : Exception
        {
            public string Code = "ObjectAlreadyExists";
            public ObjectAlreadyExistsException(string errorMessage) : base(errorMessage) { }
        }
}
