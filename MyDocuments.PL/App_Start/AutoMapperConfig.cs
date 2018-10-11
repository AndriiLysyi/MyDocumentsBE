using AutoMapper;
using MyDocuments.BLL.DTO;
using MyDocuments.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDocuments.PL.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<Document, DocumentDTO>().ReverseMap();
                config.CreateMap<History, HistoryDTO>().ReverseMap();
            });
        }
    }
}