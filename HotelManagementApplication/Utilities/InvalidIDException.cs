namespace HotelManagementApplication.Utilities
{
    public class InvalidIDException:Exception
    {
        string message = "";
        public InvalidIDException()
        {
            message = "Invalid Id PLease check";
        }
        public override string Message => message;
    }
}
