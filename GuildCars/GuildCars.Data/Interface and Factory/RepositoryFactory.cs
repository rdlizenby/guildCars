using GuildCars.Data.ADO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interface_and_Factory
{
    public class RepositoryFactory
    {
        //static IRepository _mock = new GuildCarsMockRepository();
        public static IRepository GetRepository()
        {
            string repositoryType = ConfigurationManager.AppSettings["RepositoryType"].ToString();
            switch (repositoryType)
            {
                //case "QA":
                //    return _mock;
                case "ADO":
                    return new ADORepository();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
