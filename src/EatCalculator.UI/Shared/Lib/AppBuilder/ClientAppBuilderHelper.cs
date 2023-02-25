namespace EatCalculator.UI.Shared.Lib.AppBuilder
{
    public static class ClientAppBuilderHelper
    {
        public static bool IsDevelopment(this ClientAppBuilder appBuilder)
            => appBuilder.Environment is ClientAppEnvironment.Development;

        public static bool IsStaging(this ClientAppBuilder appBuilder)
            => appBuilder.Environment is ClientAppEnvironment.Staging;

        public static bool IsProduction(this ClientAppBuilder appBuilder)
            => appBuilder.Environment is ClientAppEnvironment.Production;
    }
}
