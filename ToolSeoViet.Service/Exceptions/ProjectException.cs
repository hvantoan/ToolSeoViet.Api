using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Service.Exceptions
{
    [Serializable]
    public class ProjectException : Exception
    {
        public ProjectException()
        {
        }

        public ProjectException(string message) : base(message)
        {
        }

        public ProjectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
