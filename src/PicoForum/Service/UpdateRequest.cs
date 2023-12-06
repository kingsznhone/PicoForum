namespace PicoForum.Service
{
    public interface IUpdateRequest
    {
        event Action RefreshRequested;

        void CallRequestRefresh();
    }

    public class UpdateRequestService : IUpdateRequest
    {
        public event Action RefreshRequested;

        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}