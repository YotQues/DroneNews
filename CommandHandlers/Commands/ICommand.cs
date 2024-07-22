namespace CommandHandlers.Commands;

internal interface ICommand
{
    Guid CommandId { get; }
}
