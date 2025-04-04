namespace SignalRServerApi.NotificationService
{
    public class NotificationUser : INotificationUserService
    {
        public List<string> Users;
        public NotificationUser()
        {
            Users = new List<string>();
        }
        public bool AddUser(string name)
        {
            if (Users.Contains(name))
            {
                return false;
            }
            else
            {
                Users.Add(name);
            }
            return true;
        }
    }
    public interface INotificationUserService
    {
        public bool AddUser(string name);
    }
}
