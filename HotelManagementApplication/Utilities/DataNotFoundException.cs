namespace SuppliersApp.Utilities
{
    public class DataNotFoundException:Exception
    {
        string message = "";
        public DataNotFoundException()
        {
            message = "data not found Please check your entry once";
        }
        public override string Message => message;
    }
}
