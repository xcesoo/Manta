using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Domain.Interfaces;

namespace Manta.Application.Factories;

public static class ParcelFactory
{
    public static Parcel Create(ParcelCreationOptions options)
    {
        var parcel = Parcel.Create(options);
        return parcel;
    }
}