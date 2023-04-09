namespace Common.Base.Exceptions
{
    public abstract record BaseExceptionModel
    {
        public required List<string> Errors { get; init; }
    }
}
