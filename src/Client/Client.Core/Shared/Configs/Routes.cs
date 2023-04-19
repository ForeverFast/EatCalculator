namespace Client.Core.Shared.Configs
{
    internal class Routes
    {
        public const string Main = "/";

        public class Days
        {
            public const string BasePath = "days";

            public const string Calendar = "calendar";
        }

        public class Products
        {
            public const string BasePath = "products";
        }

        public class Identity
        {
            public const string BasePath = "account";

            public const string SignIn = "sign-in";
            public const string SignUp = "sign-up";
            public const string UserProfile = "profile";
        }
    }
}
