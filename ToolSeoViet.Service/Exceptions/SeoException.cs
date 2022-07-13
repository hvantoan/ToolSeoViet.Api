using System;
using System.Runtime.Serialization;

namespace ToolSeoViet.Service.Exceptions {
    [Serializable]
    public class SeoException : Exception {

        public SeoException() {
        }

        public SeoException(string message) : base(message) {
        }

        public SeoException(string message, Exception innerException) : base(message, innerException) {
        }

        protected SeoException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
