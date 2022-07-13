namespace ToolSeoViet.Services.Resources {

    public static class Messages {

      

        public static class Auth {

            public static class Login {
                public const string Merchant_NotFound = "Cửa hàng không tồn tại.";
                public const string Merchant_Inactive = "Cửa hàng không hoạt động.";
                public const string Merchant_Expired = "Cửa hàng đã hết hạn.";

                public const string User_NotFound = "Người dùng không tồn tại.";
                public const string User_Inactive = "Người dùng không hoạt động.";
                public const string User_IncorrectPassword = "Sai mật khẩu.";
            }
        }

        public static class User {

            public static class CreateOrUpdate {
                public const string Role_NotFound = "Phân quyền không tồn tại.";

                public const string User_Existed = "Người dùng đã tồn tại.";
                public const string User_NotFound = "Người dùng không tồn tại.";
                public const string User_NotInactive = "Không thể dừng hoạt động với người quản trị.";
            }

            public static class ChangePassword {
                public const string User_NotFound = "Người dùng không tồn tại.";
                public const string User_IncorrentOldPassword = "Sai mật khẩu.";
            }

            public static class ResetPassword {
                public const string User_NotFound = "Người dùng không tồn tại.";
            }
        }
        public static class Project {

            public static class CreateOrUpdate {
                public const string Role_NotFound = "Phân quyền không tồn tại.";

                public const string User_Existed = "Người dùng đã tồn tại.";
                public const string User_NotFound = "Người dùng không tồn tại.";
                public const string User_NotInactive = "Không thể dừng hoạt động với người quản trị.";
            }

            public static class CheckProject {
                public const string Project_NotFound = "Không tìm thấy dự án";
                
            }

            public static class ResetPassword {
                public const string User_NotFound = "Người dùng không tồn tại.";
            }
        }

        public static class Seo {
            public static class GetContent {
            }
        }

    }
}