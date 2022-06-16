namespace ToolSeoViet.Services.Models.Auth {

    public class LoginRequest {
        public string UserCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse {
        public string Token { get; set; }
        public string UserCode { get; set; }
        public string Username { get; set; }
        public long ExpiredTime { get; set; }
    }
}