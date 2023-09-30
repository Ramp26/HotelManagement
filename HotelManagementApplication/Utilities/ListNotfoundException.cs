namespace SuppliersApp.Utilities
{
    public class ListNotfoundException:Exception
    {
        string message = "";
        public ListNotfoundException()
        {
            message = "no data or list found as of now please add";
        }
        public override string Message => message;
    }
}
