namespace Client.Core.Entities.Viewer.Models
{
    public record ViewerModel
    {
        public required int Id { get; init; }
        public required string Email { get; init; }
        public required string UserName { get; init; }
        public DateTime? LastDbUpdateDate { get; init; }
    }
}
