namespace Server.Core.Base.Requests
{
    public abstract record BaseRequest
    {
        [FromHeader] public int? AuthorizedUserId { get; init; }
    }
}
