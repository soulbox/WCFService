﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceHost.ITS_Service
{

    using Entity;
    using Repository.ITSRepo;
    using DTO.ITSDTO;
    using System.ServiceModel;

    public class İşEmriService : ITS_ServiceBase<İşEmriRepo, İşEmri, İşEmriDTO>, IİşEmriService
    {

    }
    [ServiceContract]
    public interface IİşEmriService : IServiceBase<İşEmriDTO>
    {

    }
}
