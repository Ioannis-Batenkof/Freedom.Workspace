namespace Freedom.Workspace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            user.Name = "Test";
            user.Password = "password";

            (User UserInfo, FailedToCreateUserReasons status) result = (user, Validate.User(user));

            if (result.status == FailedToCreateUserReasons.Success)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Smth went wrong");
            }
        }
    }
}
