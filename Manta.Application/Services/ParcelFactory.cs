using Manta.Domain.Entities;
using Manta.Domain.Services;

namespace Manta.Application.Services;

public static class ParcelFactory
{
    public static Parcel Create(ParcelCreationOptions options) =>
        Parcel.Create(options);
}