namespace DroneNews.CommandHandlers
{
    public interface ICommand
    {
        Guid CommandId { get; }
    }
}