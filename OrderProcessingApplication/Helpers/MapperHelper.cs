using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AutoMapper;

namespace OrderProcessingApplication.Helpers
{
    public static class MapperHelper
    {
        public static List<TDest> MapList<TSrc, TDest>(this List<TSrc> src) where TDest : class
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSrc, TDest>());

            return config.CreateMapper().Map<List<TSrc>, List<TDest>>(src);
        }

        public static TDest MapSingle<TSrc, TDest>(this TSrc src) where TDest : class
        {
            var config = new MapperConfiguration(cfg =>
                  cfg.CreateMap<TSrc, TDest>()
              );

            return config.CreateMapper().Map<TSrc, TDest>(src);
        }


    }

}