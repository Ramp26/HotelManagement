namespace HotelManagementApplication.Utilities
{
    public class UnAvailableRoomException:Exception
    
         {
        string message = "";
        public UnAvailableRoomException(DateTime maxdate,DateTime mindate)
        {
            message = "Room is not Available ----After This Date u will get room "+ maxdate+"  Or You will get room before this date== "+mindate;
        }
        public override string Message => message;
    }
}
