using System;

namespace TuanVu.Services.Extensions {

    public static class GuidExtension {

        public static string ToStringN(this Guid guid) {
            return guid.ToString("N");
        }
        #nullable disable
        public static string ToStringN(this Guid? guid) {
            return guid?.ToString("N");
        }
    }
}