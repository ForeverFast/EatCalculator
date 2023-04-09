namespace Client.Core.Shared.Lib.Calculator.Models
{
    public record PortionWithProductInfo
    {
        public required Portion Portion { get; init; }
        public required Product Product { get; init; }
    }
}
