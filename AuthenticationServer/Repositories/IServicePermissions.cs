using NewProject.AuthenticationServer.Models.Dtos;
using NewProject.AuthenticationServer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Repositories
{
    public interface IServicePermissions
    {
        Task<bool> CheckExistInAvailiableServices(string serviceId);
        void CreateAvailiableService(AvailiableServiceCreateDto serviceCreateDto);
        Task<bool> DeleteAvailiableService(string serviceId);
        Task<bool> DeleteServicePermissions(Guid accountId);
        Task<IEnumerable<AvailiableServiceDto>> GetAvailiableServices();
        Task<AccountServicePermissions> GetServicePermissions(Guid accountId);
        void SaveServicePermissions(AccountServicePermissions permissionsCreateDto);
    }
}