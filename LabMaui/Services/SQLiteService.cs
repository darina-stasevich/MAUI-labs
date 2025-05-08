using MauiApp6.Entities;
using SQLite;

namespace MauiApp6.Services;

public class SQLiteService : IDbService
{
    private readonly SQLiteConnection _db;
    private const string DatabaseName = "spa_service.db";

    private const SQLiteOpenFlags Flags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;

    public SQLiteService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
        _db = new SQLiteConnection(dbPath, Flags);

    }

    public void Init()
    {
        var random = new Random();

        _db.DropTable<ProcedureType>();
        _db.DropTable<Procedure>();
        _db.CreateTable<ProcedureType>();
        _db.CreateTable<Procedure>();


        var procedureTypes = new List<ProcedureType>
        {
            new() { Name = "Массаж" },
            new() { Name = "Уход за лицом" },
            new() { Name = "Уход за телом" },
            new() { Name = "Маникюр и педикюр" },
            new() { Name = "Процедуры для волос" },
            new() { Name = "Ароматерапия" },
            new() { Name = "Бальнеотерапия" },
            new() { Name = "Косметические процедуры" },
            new() { Name = "Спа-ритуалы" },
         };
        _db.InsertAll(procedureTypes);

        var procedureList = new List<Procedure>
        {
            new()
            {
                ProcedureName = "Швеский массаж",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[0].Id,
                Information = "расслабляющий массаж с использованием длинных движений"
            },
            new()
            {
                ProcedureName = "Глубокий массаж тканей",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[0].Id,
                Information = "проработка глубоких слоев мышц"
            },
            new()
            {
                ProcedureName = "Ароматерапевтический массаж",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[0].Id,
                Information = "массаж с эфирными маслами для расслабления"
            },
            new()
            {
                ProcedureName = "Тайский массаж",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[0].Id,
                Information = "сочетание растяжек и давления на точки"
            },
            new()
            {
                ProcedureName = "Шиацу",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[0].Id,
                Information = "японский массаж с акцентом на точки давления"
            },
            new()
            {
                ProcedureName = "Массаж горячими камнями",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[0].Id,
                Information = "использование нагретых камней для глубокого расслабления"
            },
            new()
            {
                ProcedureName = "Глубокое очищение кожи",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[1].Id,
                Information = "эксфолиация, паровая баня и маски"
            },
            new()
            {
                ProcedureName = "Антивозрастной уход",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[1].Id,
                Information = "процедуры с сыворотками для уменьшения морщин"
            },
            new()
            {
                ProcedureName = "Увлажняющий уход",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[1].Id,
                Information = "питательные маски и кремы для сухой кожи"
            },
            new()
            {
                ProcedureName = "Чистка лица",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[1].Id,
                Information = "механическая или ультразвуковая чистка"
            },
            new()
            {
                ProcedureName = "Питательная маска",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[1].Id,
                Information = "маски с коллагеном и витаминами"
            },
            new()
            {
                ProcedureName = "Лифтинг-маска",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[1].Id,
                Information = "маски для подтяжки и упругости кожи"
            },
            new()
            {
                ProcedureName = "Скрабы для тела",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[2].Id,
                Information = "отшелушивающие процедуры с солью или сахаром"
            },
            new()
            {
                ProcedureName = "Обёртывания",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[2].Id,
                Information = "процедуры с грязями или водорослями для детоксикации"
            },
            new()
            {
                ProcedureName = "Сауна или паровая баня",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[2].Id,
                Information = "для расслабления и очищения кожи"
            },
            new()
            {
                ProcedureName = "Солярий",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[2].Id,
                Information = "процедуры для получения загара"
            },
            new()
            {
                ProcedureName = "Маска для тела",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[2].Id,
                Information = "специальные маски с глиной или водорослями"
            },
            new()
            {
                ProcedureName = "Скрабирование",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[2].Id,
                Information = "процедуры с использованием кофе, соли или сахара"
            },
            new()
            {
                ProcedureName = "Классический маникюр",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[3].Id,
                Information = "обрезка и уход за ногтями и кутикулой"
            },
            new()
            {
                ProcedureName = "Долговременное покрытие",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[3].Id,
                Information = "нанесение гель-лака"
            },
            new()
            {
                ProcedureName = "Ароматический педикюр",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[3].Id,
                Information = "с эфирными маслами для расслабления"
            },
            new()
            {
                ProcedureName = "Французский маникюр",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[3].Id,
                Information = "классический стиль с белыми кончиками"
            },
            new()
            {
                ProcedureName = "Наращивание ногтей",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[3].Id,
                Information = "создание длины с использованием геля или акрила"
            },
            new()
            {
                ProcedureName = "Классический педикюр",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[3].Id,
                Information = "уход за ногами с обрезкой и полировкой ногтей"
            },
            new()
            {
                ProcedureName = "Кератиновое выпрямление",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[4].Id,
                Information = "восстановление и выпрямление волос"
            },
            new()
            {
                ProcedureName = "Маски для волос",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[4].Id,
                Information = "питательные процедуры для восстановления"
            },
            new()
            {
                ProcedureName = "Стрижка и укладка",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[4].Id,
                Information = "профессиональный уход и оформление прически"
            },
            new()
            {
                ProcedureName = "Окрашивание волос",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[4].Id,
                Information = "разные техники, включая мелирование и омбре"
            },
            new()
            {
                ProcedureName = "Стрижка горячими ножницами",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[4].Id,
                Information = "предотвращение сечения кончиков"
            },
            new()
            {
                ProcedureName = "Уход с помощью масла",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[4].Id,
                Information = "масляные процедуры для питания волос"
            },
            new()
            {
                ProcedureName = "Ароматические ингаляции",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[5].Id,
                Information = "использование эфирных масел для расслабления"
            },
            new()
            {
                ProcedureName = "Массаж с ароматерапией",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[5].Id,
                Information = "массаж с добавлением масел"
            },
            new()
            {
                ProcedureName = "Ароматические ванны",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[5].Id,
                Information = "ванные процедуры с эфирными маслами"
            },
            new()
            {
                ProcedureName = "Ароматерапия в сауне",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[5].Id,
                Information = "использование масел для улучшения атмосферы"
            },
            new()
            {
                ProcedureName = "Ароматические свечи",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[5].Id,
                Information = "создание уюта с эфирными маслами"
            },
            new()
            {
                ProcedureName = "Арома-диффузоры",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[5].Id,
                Information = "устройства для ароматизации воздуха"
            },
            new()
            {
                ProcedureName = "Минеральные ванны",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[6].Id,
                Information = "купания в воде с минералами"
            },
            new()
            {
                ProcedureName = "Грязевые обёртывания",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[6].Id,
                Information = "использование лечебных грязей"
            },
            new()
            {
                ProcedureName = "Ванны с морской солью",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[6].Id,
                Information = "расслабляющие процедуры с морской солью"
            },
            new()
            {
                ProcedureName = "Фито-ванны",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[6].Id,
                Information = "ванны с добавлением трав и растений"
            },
            new()
            {
                ProcedureName = "Целебные обёртывания",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[6].Id,
                Information = "с лечебными растительными экстрактами"
            },
            new()
            {
                ProcedureName = "Соляные ванны",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[6].Id,
                Information = "расслабляющие процедуры с морской солью"
            },
            new()
            {
                ProcedureName = "Пилинг",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[7].Id,
                Information = "химический или механический пилинг для обновления кожи"
            },
            new()
            {
                ProcedureName = "Мезотерапия",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[7].Id,
                Information = "инъекции витаминов для улучшения состояния кожи"
            },
            new()
            {
                ProcedureName = "Лифтинг-процедуры",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[7].Id,
                Information = "подтяжка и омоложение кожи"
            },
            new()
            {
                ProcedureName = "Биоревитализация",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[7].Id,
                Information = "инъекции гиалуроновой кислоты для увлажнения"
            },
            new()
            {
                ProcedureName = "Лазерный пилинг",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[7].Id,
                Information = "удаление верхнего слоя кожи с помощью лазера"
            },
            new()
            {
                ProcedureName = "Ультразвуковая чистка",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[7].Id,
                Information = "очищение кожи с помощью ультразвуковых волн"
            },
            new()
            {
                ProcedureName = "Спа-день для двоих",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[8].Id,
                Information = "комплекс процедур для пар"
            },
            new()
            {
                ProcedureName = "Традиционный восточный ритуал",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[8].Id,
                Information = "массаж, обёртывание и травяные чаи"
            },
            new()
            {
                ProcedureName = "Спа-пакеты",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[8].Id,
                Information = "комбинированные предложения с несколькими процедурами"
            },
            new()
            {
                ProcedureName = "Спа-ритуал \"Тропики\"",
                Price = random.Next(1, 10) * 5,
                StartDate = DateTime.Now.AddDays(random.Next(0, 10)),
                ProcedureTypeId = procedureTypes[8].Id,
                Information = "с использованием кокоса и ананаса"
            }
        };
        _db.InsertAll(procedureList);
    }

    public IEnumerable<ProcedureType> GetProcedureTypes()
    {
        return _db.Table<ProcedureType>();
    }

    public IEnumerable<Procedure> GetProcedures(int id)
    {
        return _db.Table<Procedure>().Where(p => p.ProcedureTypeId == id);
    }
}