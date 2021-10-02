using MVC.Database;
using MVT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVT.Services
{
    public class ConfigurationsService
    {
        #region Singleton
        public static ConfigurationsService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationsService();

                return instance;
            }
        }
        private static ConfigurationsService instance { get; set; }
        private ConfigurationsService()
        {
        }
        #endregion
        public Config GetConfig(string key)
        {
            using (var context = new MVTDbContext())
            {
                return context.Configurations.Find(key);
            }
        }
        public int PageSize()
        {
            using (var context = new MVTDbContext())
            {
                var pageSizeConfig = context.Configurations.Find("PageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 5;
            }
        }
        public int ShopPageSize()
        {
            using (var context = new MVTDbContext())
            {
                var pageSizeConfig = context.Configurations.Find("ShopPageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }
        }
    }
}
