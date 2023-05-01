namespace Client.Core.Shared.Api.LocalDatabase.DalQc
{
    public record ChangeDbFileDataRequest : IRequest
    {
        public required byte[] FileData { get; init; }
        public string? TargetFilePath { get; init; }     
    }
}
