using GuildCars.Models.Tables;
using GuildCars.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.AjaxRequest;

namespace GuildCars.Data.Interface_and_Factory
{
    public interface IRepository
    {
        HomeIndexVM GetHomeIndexVM();
        IEnumerable<Special> GetSpecials();
        IEnumerable<CarDetailVM> NewSearch(SearchRequest searchRequest);
        IEnumerable<CarDetailVM> UsedSearch(SearchRequest searchRequest);
        CarDetailVM GetDetailsVM(int id);
        void PostContactRequest(ContactRequest contactRequest);
        IEnumerable<CarDetailVM> SalesSearch(SearchRequest searchRequest);
        List<State> GetStates();
        List<PurchaseType> GetPurchaseTypes();
        List<Make> GetMakes();
        List<Model> GetModels();
        List<BodyStyle> GetBodyStyles();
        List<Transmission> GetTransmissions();
        List<InteriorColor> GetInteriorColors();
        List<ExteriorColor> GetExteriorColors();
        int SaveBuyer(Buyer buyer);
        Car GetCarById(int id);
        void SaveCar(Car car);
        List<Model> GetFilteredModels(int id);
        string GetBodystyle(int id);
        int AddCar(Car car);
        void CreateCarExteriorBridgeEnties(int newCarId, string[] exteriorColorIds);
        void CreateCarInteriorBridgeEntries(int newCarId, string[] interiorColorIds);
        int GetMakeByModel(int modelId);
        void DeleteCarExteriorBridgeEntries(int carId);
        void DeleteCarInteriorBridgeEntries(int carId);
        void EditCar(Car car);
        void DeleteCar(int carId);
        string GetImageFileName(int carId);
        IEnumerable<User> GetUsers();
        IEnumerable<Role> GetRoles();
        User GetUserById(string id);
        void SaveUser(User user);
        void CreateUser(User user);
        void SaveNewMake(Make make);
        void SaveNewModel(Model model);
        int AddSpecial(Special special);
        void SaveSpecialImageFileName(int SpecialId, string imageFileName);
        void SaveCarImageFileName(int carId, string imageFileName);
        void DeleteSpecial(int SpecialId);
        List<SalesQuery> GetSalesReport(string userId, DateTime startDate, DateTime endDate);
        List<InvSummary> InventoryQuery(bool isNew);
    }
}
