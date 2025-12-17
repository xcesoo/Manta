using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Domain.Interfaces;

namespace Manta.Application.Factories;

public static class ParcelFactory
{
    public static async Task<int> Create(ParcelCreationOptions options, IParcelRepository context)
    {
        var newOptions = options with { Id = await context.GetNextIdAsync() };
        var parcel = Parcel.Create(newOptions);
        await context.AddAsync(parcel);
        await context.SaveChangesAsync();
        return parcel.Id;
    }
}