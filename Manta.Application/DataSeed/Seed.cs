using Manta.Application.Factories;
using Manta.Application.Services;
using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Infrastructure.Persistence;
using Manta.Infrastructure.Repositories;

namespace Manta.Application.DataSeed;

public class Seed : ISeed
{
    private IParcelRepository _parcelRepository;
    private IDeliveryPointRepository _deliveryPointRepository;
    private IDeliveryVehicleRepository _deliveryVehicleRepository;
    private IUserRepository _userRepository;

    public Seed(
        IParcelRepository parcelRepository,
        IDeliveryPointRepository deliveryPointRepository,
        IDeliveryVehicleRepository deliveryVehicleRepository,
        IUserRepository userRepository)
    {
        _parcelRepository = parcelRepository;
        _deliveryPointRepository = deliveryPointRepository;
        _deliveryVehicleRepository = deliveryVehicleRepository;
        _userRepository = userRepository;
    }
    public async Task SeedAsync()
    {
        // Створення тестових даних
        await DeliveryVehicleFactory.Create(new DeliveryVehicleCreationOptions(
            LicensePlate: "AI0000KA",
            CarModel: ("Mercedes", "Sprinter"),
            Capacity: 5000), _deliveryVehicleRepository);
        await DeliveryVehicleFactory.Create(new DeliveryVehicleCreationOptions(
            LicensePlate: "BO0000OM",
            CarModel: ("Renault", "Traffic"),
            Capacity: 3000), _deliveryVehicleRepository);
        await DeliveryVehicleFactory.Create(new DeliveryVehicleCreationOptions(
            LicensePlate: "PO0000MA",
            CarModel: ("Volkswagen", "Transporter"),
            Capacity: 3500), _deliveryVehicleRepository);

        await DeliveryPointFactory.Create(new DeliveryPointCreationOptions(Address: "Kyiv"), _deliveryPointRepository);
        await DeliveryPointFactory.Create(new DeliveryPointCreationOptions(Address: "Lviv"), _deliveryPointRepository);
        await DeliveryPointFactory.Create(new DeliveryPointCreationOptions(Address: "Odessa"), _deliveryPointRepository);
        await DeliveryPointFactory.Create(new DeliveryPointCreationOptions(Address: "Donetsk"), _deliveryPointRepository);

        var admin1 = await UserFactory.Create<Admin>(new UserCreationOptions(
            Name: "Карпета Кирило Андрійович", Email: "kka@manta.com"), _userRepository);
        var admin2 = await UserFactory.Create<Admin>(new UserCreationOptions(
            Name: "Жирнова Марія Олегівна", Email: "zmo@manta.com"), _userRepository);
        var driver1 = await UserFactory.Create<Driver>(new UserCreationOptions(
            Name: "Кривовух Микола Потапович", Email: "krivovuh@gmail.com", VehicleId: "AI0000KA"), _userRepository);
        var driver2 = await UserFactory.Create<Driver>(new UserCreationOptions(
            Name: "Потужний Микола Олександрович", Email: "potuzmy@gmail.com", VehicleId: "BO0000OM"), _userRepository);
        var driver3 = await UserFactory.Create<Driver>(new UserCreationOptions(
            Name: "Зубенко Михайло Петрович", Email: "zubenko@gmail.com", VehicleId: "PO0000MA"), _userRepository);
        var cashier1 = await UserFactory.Create<Cashier>(new UserCreationOptions(
            Name: "Петрик Микола Андрійович", Email: "petryk@gmail.com", DeliveryPointId: 1), _userRepository);
        var cashier2 = await UserFactory.Create<Cashier>(new UserCreationOptions(
            Name: "Іваненко Олександр Петрович", Email: "alexander.ivanenko@gmail.com", DeliveryPointId: 2), _userRepository);
        var cashier3 = await UserFactory.Create<Cashier>(new UserCreationOptions(
            Name: "Коваленко Марія Дмитрівна", Email: "maria.kovalenko@yahoo.com", DeliveryPointId: 3), _userRepository);
        var cashier4 = await UserFactory.Create<Cashier>(new UserCreationOptions(
            Name: "Шевчук Олег Васильович", Email: "oleg.shevchuk@ukr.net", DeliveryPointId: 4), _userRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 52m, Weight: 4,
            RecipientName: "Гаврилюк Олександр Сергійович",
            RecipientPhoneNumber: "+380931234567",
            RecipientEmail: "oleksandr.gavryliuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 5,
            RecipientName: "Бондар Вікторія Ігорівна",
            RecipientPhoneNumber: "+380961234568",
            RecipientEmail: "victoria.bondar@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 75m, Weight: 3,
            RecipientName: "Лисенко Валерій Павлович",
            RecipientPhoneNumber: "+380671987654",
            RecipientEmail: "valeriy.lysenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 12m, Weight: 10,
            RecipientName: "Остапчук Олена Василівна",
            RecipientPhoneNumber: "+380981122233",
            RecipientEmail: "olena.ostapchuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 100m, Weight: 6,
            RecipientName: "Мельник Іван Михайлович",
            RecipientPhoneNumber: "+380731234559",
            RecipientEmail: "ivan.melnyk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 80m, Weight: 9,
            RecipientName: "Петренко Анна Олегівна",
            RecipientPhoneNumber: "+380992233445",
            RecipientEmail: "anna.petrenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 3,
            RecipientName: "Шевчук Андрій Васильович",
            RecipientPhoneNumber: "+380961119988",
            RecipientEmail: "andriy.shevchuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 56m, Weight: 7,
            RecipientName: "Зубко Юлія Іванівна",
            RecipientPhoneNumber: "+380631234578",
            RecipientEmail: "yulia.zubko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 10m, Weight: 8,
            RecipientName: "Сидоренко Богдан Петрович",
            RecipientPhoneNumber: "+380991772233",
            RecipientEmail: "bogdan.sydorenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 130m, Weight: 12,
            RecipientName: "Романюк Марія Вікторівна",
            RecipientPhoneNumber: "+380671928374",
            RecipientEmail: "maria.romanyuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 4,
            RecipientName: "Коваль Денис Павлович",
            RecipientPhoneNumber: "+380961210123",
            RecipientEmail: "denys.koval@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 4,
            RecipientName: "Коваль Денис Павлович",
            RecipientPhoneNumber: "+380961210123",
            RecipientEmail: "denys.koval@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 67m, Weight: 8,
            RecipientName: "Гринюк Валентина Сергіївна",
            RecipientPhoneNumber: "+380931234589",
            RecipientEmail: "valentyna.grynyuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 49m, Weight: 11,
            RecipientName: "Гончарук Олексій Романович",
            RecipientPhoneNumber: "+380732112233",
            RecipientEmail: "oleksiy.goncharuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 98m, Weight: 7,
            RecipientName: "Бойко Ольга Василівна",
            RecipientPhoneNumber: "+380672334455",
            RecipientEmail: "olga.boyko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 120m, Weight: 6,
            RecipientName: "Ткаченко Лілія Миколаївна",
            RecipientPhoneNumber: "+380631234599",
            RecipientEmail: "liliya.tkachenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 32m, Weight: 5,
            RecipientName: "Корнієнко Євген Вікторович",
            RecipientPhoneNumber: "+380961234577",
            RecipientEmail: "yevhen.korniienko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 58m, Weight: 10,
            RecipientName: "Кравченко Артем Ігорович",
            RecipientPhoneNumber: "+380991234588",
            RecipientEmail: "artem.kravchenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 0m, Weight: 9,
            RecipientName: "Демчук Ірина Костянтинівна",
            RecipientPhoneNumber: "+380671234589",
            RecipientEmail: "iryna.demchuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 47m, Weight: 7,
            RecipientName: "Степанюк Аркадій Віталійович",
            RecipientPhoneNumber: "+380631234123",
            RecipientEmail: "arkadiy.stepanyuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);
        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 120m, Weight: 6,
            RecipientName: "Іваненко Олександр Петрович",
            RecipientPhoneNumber: "+380671234567",
            RecipientEmail: "ivan.ivanenko@example.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 0m, Weight: 4,
            RecipientName: "Іваненко Олександр Петрович",
            RecipientPhoneNumber: "+380671234567",
            RecipientEmail: "ivan.ivanenko@example.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 90m, Weight: 7,
            RecipientName: "Коваленко Марія Дмитрівна",
            RecipientPhoneNumber: "+380631112233",
            RecipientEmail: "maria.kovalenko@example.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 30m, Weight: 3,
            RecipientName: "Коваленко Марія Дмитрівна",
            RecipientPhoneNumber: "+380631112233",
            RecipientEmail: "maria.kovalenko@example.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 0m, Weight: 10,
            RecipientName: "Шевчук Олег Васильович",
            RecipientPhoneNumber: "+380501234567",
            RecipientEmail: "oleg.shevchuk@ukr.net",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 50m, Weight: 5,
            RecipientName: "Петренко Наталія Іванівна",
            RecipientPhoneNumber: "+380979876543",
            RecipientEmail: "nataliya.petrenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 4, AmountDue: 58m, Weight: 4,
            RecipientName: "Грицай Богдан Ігорович",
            RecipientPhoneNumber: "+380672334455",
            RecipientEmail: "bogdan.grysay@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 20m, Weight: 6,
            RecipientName: "Демчук Інна Миколаївна",
            RecipientPhoneNumber: "+380931112244",
            RecipientEmail: "inna.demchuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 0m, Weight: 7,
            RecipientName: "Демчук Інна Миколаївна",
            RecipientPhoneNumber: "+380931112244",
            RecipientEmail: "inna.demchuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 15m, Weight: 2,
            RecipientName: "Ткаченко Юрій Олександрович",
            RecipientPhoneNumber: "+380679876543",
            RecipientEmail: "yuriy.tkachenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 70m, Weight: 10,
            RecipientName: "Кравченко Наталія Миколаївна",
            RecipientPhoneNumber: "+380671234599",
            RecipientEmail: "nataliya.kravchenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 10m, Weight: 5,
            RecipientName: "Мельник Олексій Петрович",
            RecipientPhoneNumber: "+380981234567",
            RecipientEmail: "oleksiy.melnyk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 0m, Weight: 3,
            RecipientName: "Мельник Олексій Петрович",
            RecipientPhoneNumber: "+380981234567",
            RecipientEmail: "oleksiy.melnyk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 4, AmountDue: 30m, Weight: 6,
            RecipientName: "Совгиря Ігор Васильович",
            RecipientPhoneNumber: "+380671723456",
            RecipientEmail: "igor.sovyrya@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 4, AmountDue: 0m, Weight: 12,
            RecipientName: "Литвиненко Петро Олексійович",
            RecipientPhoneNumber: "+380501234890",
            RecipientEmail: "petro.lytvynenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 5m, Weight: 2,
            RecipientName: "Сидоренко Анастасія Григорівна",
            RecipientPhoneNumber: "+380963456789",
            RecipientEmail: "anastasia.sydorenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 15m, Weight: 5,
            RecipientName: "Сидоренко Анастасія Григорівна",
            RecipientPhoneNumber: "+380963456789",
            RecipientEmail: "anastasia.sydorenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 70m, Weight: 9,
            RecipientName: "Панасюк Олег Іванович",
            RecipientPhoneNumber: "+380639876543",
            RecipientEmail: "oleg.panasyuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 4,
            RecipientName: "Панасюк Олег Іванович",
            RecipientPhoneNumber: "+380639876543",
            RecipientEmail: "oleg.panasyuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 25m, Weight: 6,
            RecipientName: "Гончарук Катерина Ігорівна",
            RecipientPhoneNumber: "+380507654321",
            RecipientEmail: "kateryna.honcharuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 0m, Weight: 8,
            RecipientName: "Гончарук Катерина Ігорівна",
            RecipientPhoneNumber: "+380507654321",
            RecipientEmail: "kateryna.honcharuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 4, AmountDue: 60m, Weight: 7,
            RecipientName: "Довженко Максим Анатолійович",
            RecipientPhoneNumber: "+380631234567",
            RecipientEmail: "maksym.dovzhenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 4, AmountDue: 0m, Weight: 5,
            RecipientName: "Довженко Максим Анатолійович",
            RecipientPhoneNumber: "+380631234567",
            RecipientEmail: "maksym.dovzhenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 12m, Weight: 5,
            RecipientName: "Яценко Ірина Олегівна",
            RecipientPhoneNumber: "+380678912345",
            RecipientEmail: "iryna.yatsenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);
        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 50m, Weight: 10,
            RecipientName: "Гнатюк Богдан Ігорович",
            RecipientPhoneNumber: "+380671234111",
            RecipientEmail: "bogdan.gnatyuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 0m, Weight: 7,
            RecipientName: "Гнатюк Богдан Ігорович",
            RecipientPhoneNumber: "+380671234111",
            RecipientEmail: "bogdan.gnatyuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 60m, Weight: 8,
            RecipientName: "Панасюк Марина Іванівна",
            RecipientPhoneNumber: "+380631234222",
            RecipientEmail: "marina.panasyuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 3,
            RecipientName: "Панасюк Марина Іванівна",
            RecipientPhoneNumber: "+380631234222",
            RecipientEmail: "marina.panasyuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 33m, Weight: 4,
            RecipientName: "Козак Ірина Вікторівна",
            RecipientPhoneNumber: "+380501112233",
            RecipientEmail: "iryna.kozak@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 76m, Weight: 11,
            RecipientName: "Даниленко Олег Сергійович",
            RecipientPhoneNumber: "+380971234444",
            RecipientEmail: "oleg.danylenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 4, AmountDue: 22m, Weight: 7,
            RecipientName: "Мельник Юлія Миколаївна",
            RecipientPhoneNumber: "+380931234555",
            RecipientEmail: "yulia.melnyk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 4, AmountDue: 0m, Weight: 6,
            RecipientName: "Мельник Юлія Миколаївна",
            RecipientPhoneNumber: "+380931234555",
            RecipientEmail: "yulia.melnyk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 91m, Weight: 9,
            RecipientName: "Кравченко Андрій Васильович",
            RecipientPhoneNumber: "+380671244111",
            RecipientEmail: "andriy.kravchenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 0m, Weight: 5,
            RecipientName: "Кравченко Андрій Васильович",
            RecipientPhoneNumber: "+380671244111",
            RecipientEmail: "andriy.kravchenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 45m, Weight: 8,
            RecipientName: "Сидоренко Олена Михайлівна",
            RecipientPhoneNumber: "+380631112334",
            RecipientEmail: "olena.sydorenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 4,
            RecipientName: "Сидоренко Олена Михайлівна",
            RecipientPhoneNumber: "+380631112334",
            RecipientEmail: "olena.sydorenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 75m, Weight: 7,
            RecipientName: "Козаченко Дмитро Андрійович",
            RecipientPhoneNumber: "+380507773344",
            RecipientEmail: "dmytro.kozachenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 0m, Weight: 3,
            RecipientName: "Козаченко Дмитро Андрійович",
            RecipientPhoneNumber: "+380507773344",
            RecipientEmail: "dmytro.kozachenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 4, AmountDue: 65m, Weight: 8,
            RecipientName: "Пономаренко Наталія Миколаївна",
            RecipientPhoneNumber: "+380971255533",
            RecipientEmail: "nataliya.ponomarenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 4, AmountDue: 0m, Weight: 6,
            RecipientName: "Пономаренко Наталія Миколаївна",
            RecipientPhoneNumber: "+380971255533",
            RecipientEmail: "nataliya.ponomarenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 20m, Weight: 4,
            RecipientName: "Дмитренко Сергій Володимирович",
            RecipientPhoneNumber: "+380671123456",
            RecipientEmail: "sergiy.dmytrenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 0m, Weight: 3,
            RecipientName: "Дмитренко Сергій Володимирович",
            RecipientPhoneNumber: "+380671123456",
            RecipientEmail: "sergiy.dmytrenko@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 40m, Weight: 8,
            RecipientName: "Горбач Олена Віталіївна",
            RecipientPhoneNumber: "+380633221144",
            RecipientEmail: "olena.gorbach@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 5,
            RecipientName: "Горбач Олена Віталіївна",
            RecipientPhoneNumber: "+380633221144",
            RecipientEmail: "olena.gorbach@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 75m, Weight: 9,
            RecipientName: "Романчук Тарас Андрійович",
            RecipientPhoneNumber: "+380975512233",
            RecipientEmail: "taras.romanchuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 3, AmountDue: 0m, Weight: 7,
            RecipientName: "Романчук Тарас Андрійович",
            RecipientPhoneNumber: "+380975512233",
            RecipientEmail: "taras.romanchuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 4, AmountDue: 25m, Weight: 8,
            RecipientName: "Верес Ольга Миколаївна",
            RecipientPhoneNumber: "+380501556677",
            RecipientEmail: "olga.veres@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 4, AmountDue: 0m, Weight: 6,
            RecipientName: "Верес Ольга Миколаївна",
            RecipientPhoneNumber: "+380501556677",
            RecipientEmail: "olga.veres@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 100m, Weight: 10,
            RecipientName: "Кравчук Віктор Іванович",
            RecipientPhoneNumber: "+380671555234",
            RecipientEmail: "viktor.kravchuk@gmail.com",
            CreatedBy: SystemUser.Instance), _parcelRepository);
        await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 1, AmountDue: 100m, Weight: 8,
    RecipientName: "Ковальчук Іван Олександрович",
    RecipientPhoneNumber: "+380671234567",
    RecipientEmail: "ivan.kovalchuk@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 2, AmountDue: 0m, Weight: 3,
    RecipientName: "Ковальчук Іван Олександрович",
    RecipientPhoneNumber: "+380671234567",
    RecipientEmail: "ivan.kovalchuk@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 3, AmountDue: 55m, Weight: 10,
    RecipientName: "Петренко Ольга Вікторівна",
    RecipientPhoneNumber: "+380631112233",
    RecipientEmail: "olga.petrenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 4, AmountDue: 20m, Weight: 7,
    RecipientName: "Петренко Ольга Вікторівна",
    RecipientPhoneNumber: "+380631112233",
    RecipientEmail: "olga.petrenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 1, AmountDue: 0m, Weight: 6,
    RecipientName: "Шевченко Сергій Васильович",
    RecipientPhoneNumber: "+380501234567",
    RecipientEmail: "sergiy.shevchenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 2, AmountDue: 77m, Weight: 9,
    RecipientName: "Мельник Олена Миколаївна",
    RecipientPhoneNumber: "+380971234567",
    RecipientEmail: "olena.melnyk@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 3, AmountDue: 35m, Weight: 4,
    RecipientName: "Даниленко Андрій Петрович",
    RecipientPhoneNumber: "+380979876543",
    RecipientEmail: "andriy.danylenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 4, AmountDue: 15m, Weight: 5,
    RecipientName: "Кравець Оксана Іванівна",
    RecipientPhoneNumber: "+380681234568",
    RecipientEmail: "oksana.kravets@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 1, AmountDue: 120m, Weight: 11,
    RecipientName: "Бондаренко Марина Олегівна",
    RecipientPhoneNumber: "+380671987654",
    RecipientEmail: "marina.bondarenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 2, AmountDue: 0m, Weight: 8,
    RecipientName: "Бондаренко Марина Олегівна",
    RecipientPhoneNumber: "+380671987654",
    RecipientEmail: "marina.bondarenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 3, AmountDue: 45m, Weight: 7,
    RecipientName: "Ткаченко Світлана Вікторівна",
    RecipientPhoneNumber: "+380633211234",
    RecipientEmail: "svitlana.tkachenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 4, AmountDue: 0m, Weight: 6,
    RecipientName: "Ткаченко Світлана Вікторівна",
    RecipientPhoneNumber: "+380633211234",
    RecipientEmail: "svitlana.tkachenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 1, AmountDue: 60m, Weight: 9,
    RecipientName: "Гнатюк Володимир Степанович",
    RecipientPhoneNumber: "+380967654321",
    RecipientEmail: "volodymyr.gnatyuk@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 2, AmountDue: 85m, Weight: 8,
    RecipientName: "Гнатюк Володимир Степанович",
    RecipientPhoneNumber: "+380967654321",
    RecipientEmail: "volodymyr.gnatyuk@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 3, AmountDue: 0m, Weight: 4,
    RecipientName: "Кравченко Наталія Володимирівна",
    RecipientPhoneNumber: "+380971234678",
    RecipientEmail: "nataliya.kravchenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 4, AmountDue: 0m, Weight: 10,
    RecipientName: "Кравченко Наталія Володимирівна",
    RecipientPhoneNumber: "+380971234678",
    RecipientEmail: "nataliya.kravchenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 1, AmountDue: 50m, Weight: 5,
    RecipientName: "Романюк Олег Ігорович",
    RecipientPhoneNumber: "+380681112233",
    RecipientEmail: "oleh.romanyuk@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 2, AmountDue: 0m, Weight: 7,
    RecipientName: "Романюк Олег Ігорович",
    RecipientPhoneNumber: "+380681112233",
    RecipientEmail: "oleh.romanyuk@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 3, AmountDue: 30m, Weight: 6,
    RecipientName: "Левченко Катерина Олександрівна",
    RecipientPhoneNumber: "+380503344556",
    RecipientEmail: "kateryna.levchenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 4, AmountDue: 0m, Weight: 3,
    RecipientName: "Левченко Катерина Олександрівна",
    RecipientPhoneNumber: "+380503344556",
    RecipientEmail: "kateryna.levchenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 1, AmountDue: 75m, Weight: 11,
    RecipientName: "Демчук Максим Петрович",
    RecipientPhoneNumber: "+380991234567",
    RecipientEmail: "maksym.demchuk@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 2, AmountDue: 0m, Weight: 5,
    RecipientName: "Демчук Максим Петрович",
    RecipientPhoneNumber: "+380991234567",
    RecipientEmail: "maksym.demchuk@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 3, AmountDue: 20m, Weight: 6,
    RecipientName: "Яценко Ірина Миколаївна",
    RecipientPhoneNumber: "+380671122334",
    RecipientEmail: "iryna.yatsenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);

await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 4, AmountDue: 0m, Weight: 4,
    RecipientName: "Яценко Ірина Миколаївна",
    RecipientPhoneNumber: "+380671122334",
    RecipientEmail: "iryna.yatsenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);
await ParcelFactory.Create(new ParcelCreationOptions(
    DeliveryPointId: 4, AmountDue: 300m, Weight: 4,
    RecipientName: "Яценко Ірина Миколаївна",
    RecipientPhoneNumber: "+380671122334",
    RecipientEmail: "iryna.yatsenko@gmail.com",
    CreatedBy: SystemUser.Instance), _parcelRepository);
    }
}