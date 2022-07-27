using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Service.Exceptions {
    [Serializable]
    public class SearchContentException : Exception {

        public SearchContentException() {
        }

        public SearchContentException(string message) : base(message) {
        }

        public SearchContentException(string message, Exception innerException) : base(message, innerException) {
        }

        protected SearchContentException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
