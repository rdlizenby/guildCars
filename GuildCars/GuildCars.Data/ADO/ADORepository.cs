using GuildCars.Data.Interface_and_Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.AjaxRequest;
using GuildCars.Models.Tables;
using GuildCars.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;

namespace GuildCars.Data.ADO
{
    public class ADORepository : IRepository
    {
        public int AddCar(Car car)
        {
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddCar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter Param = new SqlParameter("@CarId", SqlDbType.Int);
                Param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Param);
                cmd.Parameters.AddWithValue("@ModelId", car.ModelId);
                cmd.Parameters.AddWithValue("@IsNew", car.IsNew);
                cmd.Parameters.AddWithValue("@TransmissionId", car.TransmissionId);
                cmd.Parameters.AddWithValue("@Year", car.Year);
                cmd.Parameters.AddWithValue("@Milage", car.Milage);
                cmd.Parameters.AddWithValue("@Vin", car.Vin);
                cmd.Parameters.AddWithValue("@Msrp", car.Msrp);
                cmd.Parameters.AddWithValue("@SalePrice", car.SalePrice);
                cmd.Parameters.AddWithValue("@Description", car.Description);
                cmd.Parameters.AddWithValue("@IsFeatured", car.IsFeatured);
                cmd.Parameters.AddWithValue("@IsSold", car.IsSold);

                cn.Open();
                cmd.ExecuteNonQuery();

                car.CarId = (int)Param.Value;

            }
            return car.CarId;

        }

        public void CreateCarExteriorBridgeEnties(int newCarId, string[] exteriorColorIds)
        {
            foreach (string c in exteriorColorIds)
            {
                CarExteriorColorBridge newBridge = new CarExteriorColorBridge();
                newBridge.CarId = newCarId;
                newBridge.ExteriorColorId = int.Parse(c);
                using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("CreateCarExteriorBridgeEntry", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CarId", newCarId);
                    cmd.Parameters.AddWithValue("@ExteriorColorId", newBridge.ExteriorColorId);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CreateCarInteriorBridgeEntries(int newCarId, string[] interiorColorIds)
        {
            foreach (string c in interiorColorIds)
            {
                CarInteriorColorBridge newBridge = new CarInteriorColorBridge();
                newBridge.CarId = newCarId;
                newBridge.InteriorColorId = int.Parse(c);
                using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("CreateCarInteriorBridgeEntry", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CarId", newCarId);
                    cmd.Parameters.AddWithValue("@InteriorColorId", newBridge.InteriorColorId);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCar(int carId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DeleteCar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CarId", carId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void EditCar(Car car)
        {
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("EditCar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter Param = new SqlParameter("@CarId", SqlDbType.Int);

                cmd.Parameters.AddWithValue("@CarId", car.CarId);
                cmd.Parameters.AddWithValue("@ModelId", car.ModelId);
                cmd.Parameters.AddWithValue("@IsNew", car.IsNew);
                cmd.Parameters.AddWithValue("@TransmissionId", car.TransmissionId);
                cmd.Parameters.AddWithValue("@Year", car.Year);
                cmd.Parameters.AddWithValue("@Milage", car.Milage);
                cmd.Parameters.AddWithValue("@Vin", car.Vin);
                cmd.Parameters.AddWithValue("@Msrp", car.Msrp);
                cmd.Parameters.AddWithValue("@SalePrice", car.SalePrice);
                cmd.Parameters.AddWithValue("@Description", car.Description);
                cmd.Parameters.AddWithValue("@IsFeatured", car.IsFeatured);
                cmd.Parameters.AddWithValue("@IsSold", car.IsSold);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public string GetBodystyle(int id)
        {
            var bodyStyles = GetBodyStyles();
            var models = GetModels();
            var filteredBodystyle = (from b in bodyStyles
                                     join m in models on b.BodyStyleId equals m.BodyStyleId
                                     where m.ModelId == id
                                     select b.BodyStyleName).FirstOrDefault();
            return filteredBodystyle;
        }

        public List<BodyStyle> GetBodyStyles()
        {
            List<BodyStyle> bodyStyles = new List<BodyStyle>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetBodyStyles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BodyStyle currentRow = new BodyStyle();
                        currentRow.BodyStyleId = (int)dr["BodyStyleId"];
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();

                        bodyStyles.Add(currentRow);
                    }
                }
                return bodyStyles;
            }
        }

        public Car GetCarById(int id)
        {
            Car car = new Car();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetCarById", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CarId", id);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        car.CarId = (int)dr["CarId"];
                        car.ModelId = (int)dr["ModelId"];
                        car.IsNew = (bool)dr["IsNew"];
                        car.TransmissionId = (int)dr["TransmissionId"];
                        car.Year = (int)dr["Year"];
                        car.Milage = (int)dr["Milage"];
                        car.Vin = dr["Vin"].ToString();
                        car.Msrp = (int)dr["Msrp"];
                        car.SalePrice = (int)dr["SalePrice"];
                        car.Description = dr["Description"].ToString();
                        car.ImageFileName = dr["ImageFileName"].ToString();
                        car.IsFeatured = (bool)dr["IsFeatured"];
                        car.IsSold = (bool)dr["IsSold"];

                        if (dr["BuyerId"] != DBNull.Value)
                        {
                            car.BuyerId = (int)dr["BuyerId"];
                        }
                        if (dr["PurchasePrice"] != DBNull.Value)
                        {
                            car.PurchasePrice = (int)dr["PurchasePrice"];
                        }
                        if (dr["SoldBy"] != DBNull.Value)
                        {
                            car.SoldBy = dr["SoldBy"].ToString();
                        }

                        if (dr["SaleDate"] != DBNull.Value)
                        {
                            car.SaleDate = DateTime.Parse(dr["SaleDate"].ToString());
                        }

                            car.AddedDate = DateTime.Parse(dr["AddedDate"].ToString());                   
                            car.AddedBy = dr["AddedBy"].ToString();
                        
                        if (dr["PurchaseTypeId"] != DBNull.Value)
                        {
                            car.PurchaseTypeId = (int)dr["PurchaseTypeId"];
                        }
                    }
                }
                return car;
            }
        }

        public CarDetailVM GetDetailsVM(int id)
        {
            List<Car> cars = new List<Car>();
            Car car = GetCarById(id);
            cars.Add(car);
            CarDetailVM carDetailVM = new CarDetailVM();
            List<Transmission> transmissions = GetTransmissions();
            List<ExteriorColor> exteriorColors = GetExteriorColors();
            List<InteriorColor> interiorColors = GetInteriorColors();
            List<Make> makes = GetMakes();
            List<Model> models = GetModels();
            List<BodyStyle> bodyStyles = GetBodyStyles();
            List<CarExteriorColorBridge> carExteriorColorBridge = GetCarExteriorColorBridges();
            List<CarInteriorColorBridge> carInteriorColorBridge = GetCarInteriorColorBridges();
            CarDetailVM carDetails = (from c in cars
                                      join mod in models on c.ModelId equals mod.ModelId
                                      join mk in makes on mod.MakeId equals mk.MakeId
                                      join t in transmissions on c.TransmissionId equals t.TransmissionId
                                      join b in bodyStyles on mod.BodyStyleId equals b.BodyStyleId
                                      where c.CarId == id
                                      select new CarDetailVM
                                      {
                                          CarId = c.CarId,
                                          Year = c.Year,
                                          Make = mk.MakeName,
                                          Model = mod.ModelName,
                                          BodyStyle = b.BodyStyleName,
                                          TransmitionType = t.TransmissionType,
                                          Milage = c.Milage,
                                          Vin = c.Vin,
                                          SalePrice = c.SalePrice,
                                          Msrp = c.Msrp,
                                          Description = c.Description,
                                          ImageFileName = c.ImageFileName,
                                          IsSold = c.IsSold
                                      }).FirstOrDefault();


            carDetails.InteriorColor = (from c in cars
                                        join ib in carInteriorColorBridge on c.CarId equals ib.CarId
                                        join ic in interiorColors on ib.InteriorColorId equals ic.InteriorColorId
                                        where c.CarId == carDetails.CarId
                                        select ic.InteriorColorName).ToList();

            carDetails.ExteriorColor = (from c in cars
                                        join e in carExteriorColorBridge on c.CarId equals e.CarId
                                        join eb in exteriorColors on e.ExteriorColorId equals eb.ExteriorColorId
                                        where c.CarId == carDetails.CarId
                                        select eb.ExteriorColorName).ToList();

            return carDetails;
        }

        public List<ExteriorColor> GetExteriorColors()
        {
            List<ExteriorColor> exteriorColors = new List<ExteriorColor>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetExteriorColors", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ExteriorColor currentRow = new ExteriorColor();
                        currentRow.ExteriorColorId = (int)dr["ExteriorColorId"];
                        currentRow.ExteriorColorName = dr["ExteriorColorName"].ToString();

                        exteriorColors.Add(currentRow);
                    }
                }
                return exteriorColors;
            }
        }

        public List<Model> GetFilteredModels(int id)
        {
            var models = GetModels();
            var filteredModels = from m in models
                                 where m.MakeId == id
                                 select m;
            return filteredModels.ToList();
        }

        public HomeIndexVM GetHomeIndexVM()
        {
            HomeIndexVM homeIndexVM = new HomeIndexVM();

            List<CarShortList> carshorts = new List<CarShortList>();
            homeIndexVM.Specials = GetSpecials();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetFeaturedShorts", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarShortList currentRow = new CarShortList();
                        currentRow.CarId = (int)dr["CarId"];
                        currentRow.Year = (int)dr["Year"];
                        currentRow.Make = dr["MakeName"].ToString();
                        currentRow.Model = dr["ModelName"].ToString();
                        currentRow.SalePrice = (int)dr["SalePrice"];
                        currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        carshorts.Add(currentRow);
                    }
                }
                homeIndexVM.CarShorts = carshorts;
            }
            return homeIndexVM;
        }

        public string GetImageFileName(int carId)
        {
            return GetCarById(carId).ImageFileName;
        }

        public List<InteriorColor> GetInteriorColors()
        {
            List<InteriorColor> interiorColors = new List<InteriorColor>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetinteriorColors", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InteriorColor currentRow = new InteriorColor();
                        currentRow.InteriorColorId = (int)dr["interiorColorId"];
                        currentRow.InteriorColorName = dr["interiorColorName"].ToString();

                        interiorColors.Add(currentRow);
                    }
                }
                return interiorColors;
            }
        }

        public int GetMakeByModel(int modelId)
        {
            var makes = GetMakes();
            var models = GetModels();
            var makeId = (from m in makes
                          join mod in models on m.MakeId equals mod.MakeId
                          where mod.ModelId == modelId
                          select m.MakeId).FirstOrDefault();

            return makeId;
        }

        public List<Make> GetMakes()
        {
            List<Make> makes = new List<Make>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetMakes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Make currentRow = new Make();
                        currentRow.MakeId = (int)dr["MakeId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.AdminId = dr["AddedBy"].ToString();
                        currentRow.DateAdded = DateTime.Parse(dr["DateAdded"].ToString());

                        makes.Add(currentRow);
                    }
                }
                return makes;
            }
        }

        public List<Model> GetModels()
        {
            List<Model> models = new List<Model>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetModels", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Model currentRow = new Model();
                        currentRow.ModelId = (int)dr["ModelId"];
                        currentRow.MakeId = (int)dr["MakeId"];
                        currentRow.BodyStyleId = (int)dr["BodyStyleId"];
                        currentRow.ModelName = dr["ModelName"].ToString();
                        currentRow.AddedBy = dr["AddedBy"].ToString();
                        currentRow.DateAdded = DateTime.Parse(dr["DateAdded"].ToString());

                        models.Add(currentRow);
                    }
                }
                return models;
            }
        }

        public List<PurchaseType> GetPurchaseTypes()
        {
            List<PurchaseType> purchaseTypes = new List<PurchaseType>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetPurchaseTypes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PurchaseType currentRow = new PurchaseType();
                        currentRow.PurchaseTypeId= (int)dr["PurchaseTypeId"];
                        currentRow.PurchaseTypeName = dr["PurchaseTypeName"].ToString();
                   
                        purchaseTypes.Add(currentRow);
                    }
                }
                return purchaseTypes;
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            int roleIdCount = 0;
            List<Role> roles = new List<Role>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetRoles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Role currentRow = new Role();
                        currentRow.RoleId = roleIdCount;
                        roleIdCount++;
                        currentRow.DbId = dr["Id"].ToString();
                        currentRow.RoleName = dr["Name"].ToString();

                        roles.Add(currentRow);
                    }
                }
                return roles;
            }
        }

        public IEnumerable<Special> GetSpecials()
        {
            List<Special> specials = new List<Special>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetSpecials", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Special currentRow = new Special();
                        currentRow.SpecialId = (int)dr["SpecialId"];
                        currentRow.SpecialName = dr["SpecialName"].ToString();
                        currentRow.SpecialDescription = dr["SpecialDescription"].ToString();
                        currentRow.ImageFileName = dr["ImageFileName"].ToString();
                        currentRow.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                        currentRow.EndDate = DateTime.Parse(dr["EndDate"].ToString());

                        specials.Add(currentRow);
                    }
                }
                return specials;
            }
        }

        public List<State> GetStates()
        {
            List<State> states = new List<State>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetStates", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        State currentRow = new State();
                        currentRow.StateId = (int)dr["StateId"];
                        currentRow.StateAbbreviation =dr["StateAbbreviation"].ToString();
                        currentRow.StateName = dr["StateName"].ToString();

                        states.Add(currentRow);
                    }
                }
                return states;
            }
        }

        public List<Transmission> GetTransmissions()
        {
            List<Transmission> transmissions = new List<Transmission>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetTransmissions", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Transmission currentRow = new Transmission();
                        currentRow.TransmissionId = (int)dr["TransmissionId"];
                        currentRow.TransmissionType = dr["TransmissionType"].ToString();

                        transmissions.Add(currentRow);
                    }
                }
                return transmissions;
            }
        }

        public User GetUserById(string id)
        {
            var users = GetUsers();
            User user = (from u in users
                         where u.UserId == id
                         select u).FirstOrDefault();
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();
            var roles = GetRoles();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetUsers", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        User currentRow = new User();
                        currentRow.UserId = dr["UserId"].ToString();
                        currentRow.FirstName = dr["FirstName"].ToString();
                        currentRow.LastName = dr["LastName"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.RoleId = dr["RoleId"].ToString();
                        currentRow.RoleName = dr["RoleName"].ToString();

                        users.Add(currentRow);
                    }
                }
            }

            foreach (var u in users)
            {
                u.RoleName = (from m in users
                              join r in roles on u.RoleId equals r.DbId
                              select r.RoleName).FirstOrDefault();
            }
            return users;
        }

        public IEnumerable<CarDetailVM> NewSearch(SearchRequest searchRequest)
        {
            var cars = GetAllCars();
            var makes = GetMakes();
            var models = GetModels();
            var transmissions = GetTransmissions();
            var bodyStyles = GetBodyStyles();
            var interiorColors = GetInteriorColors();
            var exteriorColors = GetExteriorColors();
            var carInteriorColorBridge = GetCarInteriorColorBridges();
            var carExteriorColorBridge = GetCarExteriorColorBridges();


            int minPrice = int.Parse(searchRequest.MinPrice);
            int maxPrice = int.Parse(searchRequest.MaxPrice);
            int minYear = int.Parse(searchRequest.MinYear);
            int maxYear = int.Parse(searchRequest.MaxYear);
            var carResults = (from c in cars
                              join mod in models on c.ModelId equals mod.ModelId
                              join mk in makes on mod.MakeId equals mk.MakeId
                              join t in transmissions on c.TransmissionId equals t.TransmissionId
                              join b in bodyStyles on mod.BodyStyleId equals b.BodyStyleId
                              where c.IsNew == true && c.Year >= minYear && c.Year <= maxYear && c.SalePrice > minPrice && c.SalePrice < maxPrice
                              orderby c.SalePrice descending
                              select new CarDetailVM
                              {
                                  CarId = c.CarId,
                                  Year = c.Year,
                                  Make = mk.MakeName,
                                  Model = mod.ModelName,
                                  BodyStyle = b.BodyStyleName,
                                  TransmitionType = t.TransmissionType,
                                  Milage = c.Milage,
                                  Vin = c.Vin,
                                  SalePrice = c.SalePrice,
                                  Msrp = c.Msrp,
                                  Description = c.Description,
                                  ImageFileName = c.ImageFileName
                              }).Take(20).ToList();

            List<CarDetailVM> copy = new List<CarDetailVM>(carResults);

            foreach (CarDetailVM car in copy)
            {

                car.InteriorColor = (from c in cars
                                     join ib in carInteriorColorBridge on c.CarId equals ib.CarId
                                     join ic in interiorColors on ib.InteriorColorId equals ic.InteriorColorId
                                     where c.CarId == car.CarId
                                     select ic.InteriorColorName).ToList();

                car.ExteriorColor = (from c in cars
                                     join e in carExteriorColorBridge on c.CarId equals e.CarId
                                     join eb in exteriorColors on e.ExteriorColorId equals eb.ExteriorColorId
                                     where c.CarId == car.CarId
                                     select eb.ExteriorColorName).ToList();
            }

            var lastFilter = (from c in carResults
                              where c.Year.ToString() == searchRequest.SearchTerm || c.Make.ToLower().Contains(searchRequest.SearchTerm.ToLower()) || c.Model.ToLower().Contains(searchRequest.SearchTerm.ToLower())
                              select c).Distinct();

            return lastFilter;
        }

        public void PostContactRequest(ContactRequest contactRequest)
        {
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PostContactRequest", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", contactRequest.Name);
                if (contactRequest.Phone==null)
                {
                    cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Phone", contactRequest.Phone);

                }
                if (contactRequest.Email==null)
                {
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                }
                else
                { 
                cmd.Parameters.AddWithValue("@Email", contactRequest.Email);
                }
                cmd.Parameters.AddWithValue("@Message", contactRequest.Message);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<CarDetailVM> SalesSearch(SearchRequest searchRequest)
        {
            int minPrice = int.Parse(searchRequest.MinPrice);
            int maxPrice = int.Parse(searchRequest.MaxPrice);
            int minYear = int.Parse(searchRequest.MinYear);
            int maxYear = int.Parse(searchRequest.MaxYear);

            var cars = GetAllCars();
            var carInteriorColorBridge = GetCarInteriorColorBridges();
            var interiorColors = GetInteriorColors();
            var carExteriorColorBridge = GetCarExteriorColorBridges();
            var exteriorColors = GetExteriorColors();
            var models = GetModels();
            var makes = GetMakes();
            var bodyStyles = GetBodyStyles();
            var transmissions = GetTransmissions();

            var carResults = (from c in cars
                              join mod in models on c.ModelId equals mod.ModelId
                              join mk in makes on mod.MakeId equals mk.MakeId
                              join t in transmissions on c.TransmissionId equals t.TransmissionId
                              join b in bodyStyles on mod.BodyStyleId equals b.BodyStyleId
                              where c.IsSold == false && c.Year >= minYear && c.Year <= maxYear && c.SalePrice > minPrice && c.SalePrice < maxPrice
                              orderby c.SalePrice descending
                              select new CarDetailVM
                              {
                                  CarId = c.CarId,
                                  Year = c.Year,
                                  Make = mk.MakeName,
                                  Model = mod.ModelName,
                                  BodyStyle = b.BodyStyleName,
                                  TransmitionType = t.TransmissionType,
                                  Milage = c.Milage,
                                  Vin = c.Vin,
                                  SalePrice = c.SalePrice,
                                  Msrp = c.Msrp,
                                  Description = c.Description,
                                  ImageFileName = c.ImageFileName
                              }).Take(20).ToList();

            List<CarDetailVM> copy = new List<CarDetailVM>(carResults);

            foreach (CarDetailVM car in copy)
            {
                car.InteriorColor = (from c in cars
                                     join ib in carInteriorColorBridge on c.CarId equals ib.CarId
                                     join ic in interiorColors on ib.InteriorColorId equals ic.InteriorColorId
                                     where c.CarId == car.CarId
                                     select ic.InteriorColorName).ToList();

                car.ExteriorColor = (from c in cars
                                     join e in carExteriorColorBridge on c.CarId equals e.CarId
                                     join eb in exteriorColors on e.ExteriorColorId equals eb.ExteriorColorId
                                     where c.CarId == car.CarId
                                     select eb.ExteriorColorName).ToList();
            }

            var lastFilter = (from c in carResults
                              where c.Year.ToString() == searchRequest.SearchTerm || c.Make.ToLower().Contains(searchRequest.SearchTerm.ToLower()) || c.Model.ToLower().Contains(searchRequest.SearchTerm.ToLower())
                              select c).Distinct();

            return lastFilter;
        }

        public int SaveBuyer(Buyer buyer)
        {
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SaveBuyer", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter Param = new SqlParameter("@BuyerId", SqlDbType.Int);
                Param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Param);
                cmd.Parameters.AddWithValue("@Name", buyer.Name);
                cmd.Parameters.AddWithValue("@Street1", buyer.Street1);
                if (buyer.Street2 == null)
                {
                    cmd.Parameters.AddWithValue("@Street2", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Street2", buyer.Street2);
                }

                cmd.Parameters.AddWithValue("@City", buyer.City);
                if (buyer.Phone == null)
                {
                    cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Phone", buyer.Phone);

                }
                if (buyer.Email == null)
                {
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", buyer.Email);
                }
                cmd.Parameters.AddWithValue("@StateId", buyer.StateId);
                cmd.Parameters.AddWithValue("@ZipCode", buyer.ZipCode);

                cn.Open();
                cmd.ExecuteNonQuery();

                buyer.BuyerId = (int)Param.Value;

                return buyer.BuyerId;
            }
        }

        public void SaveCar(Car car)
        {
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SaveCar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CarId", car.CarId);
                cmd.Parameters.AddWithValue("@BuyerId", car.BuyerId);
                cmd.Parameters.AddWithValue("@IsSold", car.IsSold);
                cmd.Parameters.AddWithValue("@PurchasePrice", car.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseTypeId", car.PurchaseTypeId);
                cmd.Parameters.AddWithValue("@SoldBy", car.SoldBy);
                cmd.Parameters.AddWithValue("@SaleDate", car.SaleDate);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SaveUser(User user)
        {
            var roles = GetRoles();
            user.RoleId = (from r in roles
                           where r.RoleName == user.RoleName
                           select r.DbId).FirstOrDefault();

            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UpdateUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CreateUser(User user)
        {
            var roles = GetRoles();
            user.RoleId = (from r in roles
                           where r.RoleName == user.RoleName
                           select r.DbId).FirstOrDefault();

            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CreateUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCarExteriorBridgeEntries(int carId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DeleteCarExteriorBridgeEntries", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CarId", carId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCarInteriorBridgeEntries(int carId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DeleteCarInteriorBridgeEntries", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CarId", carId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<CarDetailVM> UsedSearch(SearchRequest searchRequest)
        {
            var cars = GetAllCars();
            var makes = GetMakes();
            var models = GetModels();
            var transmissions = GetTransmissions();
            var bodyStyles = GetBodyStyles();
            var interiorColors = GetInteriorColors();
            var exteriorColors = GetExteriorColors();
            var carInteriorColorBridge = GetCarInteriorColorBridges();
            var carExteriorColorBridge = GetCarExteriorColorBridges();

            int minPrice = int.Parse(searchRequest.MinPrice);
            int maxPrice = int.Parse(searchRequest.MaxPrice);
            int minYear = int.Parse(searchRequest.MinYear);
            int maxYear = int.Parse(searchRequest.MaxYear);
            var carResults = (from c in cars
                              join mod in models on c.ModelId equals mod.ModelId
                              join mk in makes on mod.MakeId equals mk.MakeId
                              join t in transmissions on c.TransmissionId equals t.TransmissionId
                              join b in bodyStyles on mod.BodyStyleId equals b.BodyStyleId
                              where c.IsNew == false && c.Year >= minYear && c.Year <= maxYear && c.SalePrice > minPrice && c.SalePrice < maxPrice
                              orderby c.SalePrice descending
                              select new CarDetailVM
                              {
                                  CarId = c.CarId,
                                  Year = c.Year,
                                  Make = mk.MakeName,
                                  Model = mod.ModelName,
                                  BodyStyle = b.BodyStyleName,
                                  TransmitionType = t.TransmissionType,
                                  Milage = c.Milage,
                                  Vin = c.Vin,
                                  SalePrice = c.SalePrice,
                                  Msrp = c.Msrp,
                                  Description = c.Description,
                                  ImageFileName = c.ImageFileName
                              }).Take(20).ToList();

            List<CarDetailVM> copy = new List<CarDetailVM>(carResults);

            foreach (CarDetailVM car in copy)
            {

                car.InteriorColor = (from c in cars
                                     join ib in carInteriorColorBridge on c.CarId equals ib.CarId
                                     join ic in interiorColors on ib.InteriorColorId equals ic.InteriorColorId
                                     where c.CarId == car.CarId
                                     select ic.InteriorColorName).ToList();

                car.ExteriorColor = (from c in cars
                                     join e in carExteriorColorBridge on c.CarId equals e.CarId
                                     join eb in exteriorColors on e.ExteriorColorId equals eb.ExteriorColorId
                                     where c.CarId == car.CarId
                                     select eb.ExteriorColorName).ToList();
            }

            var lastFilter = (from c in carResults
                              where c.Year.ToString() == searchRequest.SearchTerm || c.Make.ToLower().Contains(searchRequest.SearchTerm.ToLower()) || c.Model.ToLower().Contains(searchRequest.SearchTerm.ToLower())
                              select c).Distinct();

            return lastFilter;
        }

        //------------Not Interfaced Methods ----------------------
        public List<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetAllCars", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Car currentRow = new Car();
                        currentRow.CarId = (int)dr["CarId"];
                        currentRow.ModelId = (int)dr["ModelId"];
                        currentRow.IsNew = (bool)dr["IsNew"];
                        currentRow.TransmissionId = (int)dr["TransmissionId"];
                        currentRow.Year = (int)dr["Year"];
                        currentRow.Milage = (int)dr["Milage"];
                        currentRow.Vin = dr["Vin"].ToString();
                        currentRow.Msrp = (int)dr["Msrp"];
                        currentRow.SalePrice = (int)dr["SalePrice"];
                        currentRow.Description = dr["Description"].ToString();
                        currentRow.ImageFileName = dr["ImageFileName"].ToString();
                        currentRow.IsFeatured = (bool)dr["IsFeatured"];
                        currentRow.IsSold = (bool)dr["IsSold"];

                        if (dr["BuyerId"] != DBNull.Value)
                        {
                            currentRow.BuyerId = (int)dr["BuyerId"];
                        }
                        if (dr["PurchasePrice"] != DBNull.Value)
                        {
                            currentRow.PurchasePrice = (int)dr["PurchasePrice"];
                        }
                        if (dr["SoldBy"] != DBNull.Value)
                        {
                            currentRow.SoldBy = dr["SoldBy"].ToString();
                        }

                        if (dr["SaleDate"] != DBNull.Value)
                        {
                            currentRow.SaleDate = DateTime.Parse(dr["SaleDate"].ToString());
                        }

                        currentRow.AddedDate = DateTime.Parse(dr["AddedDate"].ToString());
                        currentRow.AddedBy = dr["AddedBy"].ToString();

                        if (dr["PurchaseTypeId"] != DBNull.Value)
                        {
                            currentRow.PurchaseTypeId = (int)dr["PurchaseTypeId"];
                        }
                        cars.Add(currentRow);
                    }
                }
                return cars;
            }
        }

        public List<CarInteriorColorBridge> GetCarInteriorColorBridges()
        {
            List<CarInteriorColorBridge> bridges = new List<CarInteriorColorBridge>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetCarInteriorColorBridges", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarInteriorColorBridge currentRow = new CarInteriorColorBridge();
                        currentRow.CarId = (int)dr["CarId"];
                        currentRow.InteriorColorId = (int)dr["InteriorColorId"];

                        bridges.Add(currentRow);
                    }
                }
                return bridges;
            }
        }

        public List<CarExteriorColorBridge> GetCarExteriorColorBridges()
        {
            List<CarExteriorColorBridge> bridges = new List<CarExteriorColorBridge>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetCarExteriorColorBridges", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarExteriorColorBridge currentRow = new CarExteriorColorBridge();
                        currentRow.CarId = (int)dr["CarId"];
                        currentRow.ExteriorColorId = (int)dr["ExteriorColorId"];

                        bridges.Add(currentRow);
                    }
                }
                return bridges;
            }
        }

        public void SaveNewMake(Make make)
        {
            var makes = GetMakes();
            foreach (var m in makes)
            {
                if (m.MakeName == make.MakeName)
                    return;
            }
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CreateNewMake", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AddedBy", make.AdminId);
                cmd.Parameters.AddWithValue("@DateAdded", make.DateAdded);
                cmd.Parameters.AddWithValue("@MakeName", make.MakeName);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SaveNewModel(Model model)
        {
            var models = GetModels();

            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CreateNewModel", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AddedBy", model.AddedBy);
                cmd.Parameters.AddWithValue("@DateAdded", model.DateAdded);
                cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
                cmd.Parameters.AddWithValue("@BodyStyleId", model.BodyStyleId);
                cmd.Parameters.AddWithValue("@MakeId", model.MakeId);


                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int AddSpecial(Special special)
        {
            special.StartDate = DateTime.Now;
            special.EndDate = DateTime.Now;
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddSpecial", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter Param = new SqlParameter("@SpecialId", SqlDbType.Int);
                Param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Param);
                cmd.Parameters.AddWithValue("@SpecialName", special.SpecialName);
                cmd.Parameters.AddWithValue("@SpecialDescription", special.SpecialDescription);
                cmd.Parameters.AddWithValue("@StartDate", special.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", special.EndDate);

                cn.Open();
                cmd.ExecuteNonQuery();

                special.SpecialId = (int)Param.Value;

            }
            return special.SpecialId;
        }

        public void SaveSpecialImageFileName(int specialId, string imageFileName)
        {
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SaveSpecialImageFileName", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ImageFileName", imageFileName);
                cmd.Parameters.AddWithValue("@SpecialId", specialId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSpecial(int specialId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DeleteSpecial", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SpecialId", specialId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<SalesQuery> GetSalesReport(string userId, DateTime startDate, DateTime endDate)
        {
            List<SalesQuery> query = new List<SalesQuery>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetSalesReport", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesQuery currentRow = new SalesQuery();
                        currentRow.UserId = dr["UserId"].ToString();
                        currentRow.LastName = dr["LastName"].ToString();
                        currentRow.FirstName = dr["FirstName"].ToString();
                        currentRow.TotalSales = (int)dr["TotalSales"];
                        currentRow.TotalVehicles = (int)dr["TotalVehicles"];

                        query.Add(currentRow);
                    }
                }
            }
            if (userId != "")
            {
                query = (from q in query
                         where q.UserId == userId
                         select q).ToList();
            }

            return query;
        }

        public List<InvSummary> InventoryQuery(bool isNew)
        {
            List<InvSummary> query = new List<InvSummary>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryQuery", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IsNew", isNew);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InvSummary currentRow = new InvSummary();
                        currentRow.Year = (int)dr["Year"];
                        currentRow.Make = dr["MakeName"].ToString();
                        currentRow.Model = dr["ModelName"].ToString();
                        currentRow.Count = (int)dr["Count"];
                        currentRow.StockValue = Convert.ToInt32(dr["StockValue"]);

                        query.Add(currentRow);
                    }
                }
            }

            return query;
        }

        public void SaveCarImageFileName(int carId, string imageFileName)
        {
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SaveCarImageFileName", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ImageFileName", imageFileName);
                cmd.Parameters.AddWithValue("@CarId", carId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
