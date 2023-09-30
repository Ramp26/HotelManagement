namespace SuppliersApp.Utilities
{
    public class ExistingEmailexcsption:Exception
    {
        string message = "";
        public ExistingEmailexcsption()
        {
            message = "this Email is already someone using please provide different one";
        }
        public override string Message => message;
    }
}
