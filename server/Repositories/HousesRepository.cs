



namespace csharp_gregslist_api.Repositories;
public class HousesRepository
{
    private readonly IDbConnection _db;
    public HousesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal House CreateHouse(House houseData)
    {
        string sql = @"
        INSERT INTO
    houses(
        sqft,
        bedrooms,
        bathrooms,
        imgUrl,
        description,
        price,
        creatorId
    )
VALUES(
        @Sqft,
        @Bedrooms,
        @Bathrooms,
        @ImgUrl,
        @Description,
        @price,
        @creatorId
    );
        SELECT * FROM houses WHERE id = LAST_INSERT_ID();";

        House house = _db.Query<House>(sql, houseData).FirstOrDefault();
        return house;
    }

    internal List<House> GetAllHouses()
    {
        string sql = @"SELECT houses.*, accounts.* FROM houses
        JOIN accounts ON accounts.id = houses.creatorId ;";

        List<House> houses = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }
        ).ToList();
        return houses;
    }

    internal House GetHouseById(int houseId)
    {
        string sql = @"SELECT houses.*, accounts.*
        FROM houses
        JOIN accounts ON accounts.id = houses.creatorId 
        WHERE houses.id = @houseId;";

        House house = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }, new { houseId }).FirstOrDefault();
        return house;
    }

    internal House UpdateHouse(House houseToUpdate)
    {
        string sql = @"
        UPDATE houses SET
        sqft = @sqft,
        bedrooms = @bedrooms,
        bathrooms = @bathrooms,
        imgUrl = @imgUrl,
        description = @description,
        price = @price
        WHERE id = @id;

        SELECT houses.*, accounts.*
        FROM houses
        JOIN accounts ON accounts.id = houses.creatorId 
        WHERE houses.id = @id;";
        House updatedHouse = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }, houseToUpdate).FirstOrDefault();
        return updatedHouse;
    }
}