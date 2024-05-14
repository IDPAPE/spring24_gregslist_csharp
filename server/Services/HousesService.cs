


namespace csharp_gregslist_api.Services;
public class HousesService
{
    private readonly HousesRepository _repository;

    public HousesService(HousesRepository repository)
    {
        _repository = repository;
    }

    internal House CreateHouse(House houseData)
    {
        House house = _repository.CreateHouse(houseData);
        return house;
    }

    internal List<House> GetAllHouses()
    {
        List<House> houses = _repository.GetAllHouses();
        return houses;
    }

    internal House GetHouseById(int houseId)
    {
        House house = _repository.GetHouseById(houseId);
        if (house == null)
        {
            throw new Exception($"could not find house with Id {houseId}");
        }
        return house;
    }

    internal House UpdateHouse(House houseData, int houseId, string userId)
    {
        House houseToUpdate = GetHouseById(houseId);
        if (houseToUpdate.CreatorId != userId)
        {
            throw new Exception($"Cannot update this house, you are not the owner");
        }

        houseToUpdate.Sqft = houseData.Sqft ?? houseToUpdate.Sqft;
        houseToUpdate.Bathrooms = houseData.Bathrooms ?? houseToUpdate.Bathrooms;
        houseToUpdate.Bedrooms = houseData.Bedrooms ?? houseToUpdate.Bedrooms;
        houseToUpdate.ImgUrl = houseData.ImgUrl ?? houseToUpdate.ImgUrl;
        houseToUpdate.Description = houseData.Description ?? houseToUpdate.Description;
        houseToUpdate.Price = houseData.Price ?? houseToUpdate.Price;

        House updatedHouse = _repository.UpdateHouse(houseToUpdate);
        return updatedHouse;



    }
}