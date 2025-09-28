using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;

namespace Manta.Application.Factories;

public static class ParcelFactory
{
    public static Parcel Create(ParcelCreationOptions options) =>
        Parcel.Create(options);
}