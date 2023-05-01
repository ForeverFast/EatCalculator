using Client.Core.Shared.Api.LocalDatabase.Context;
using Client.Core.Shared.Api.LocalDatabase.DalQc;
using MediatR;

namespace Client.EntryPoints.Maui.Implementations
{
    internal class ChangeDbFileDataRequestHandler : IRequestHandler<ChangeDbFileDataRequest>
    {
        private readonly IClientEatCalculatorDbContextFileProvider _clientEatCalculatorDbContextFileProvider;

        public ChangeDbFileDataRequestHandler(IClientEatCalculatorDbContextFileProvider clientEatCalculatorDbContextFileProvider)
        {
            _clientEatCalculatorDbContextFileProvider = clientEatCalculatorDbContextFileProvider;
        }

        public async Task Handle(ChangeDbFileDataRequest request, CancellationToken cancellationToken)
        {
            var tmpFilePath = Path.Combine(FileSystem.AppDataDirectory, "tmp-eat-calc.db");
            await File.WriteAllBytesAsync(tmpFilePath, request.FileData);

            var provider = (MauiClientEatCalculatorDbContextFileProvider)_clientEatCalculatorDbContextFileProvider;
            await provider.Backup(tmpFilePath, request.TargetFilePath!);
        }
    }
}
