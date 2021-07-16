using NewProject.AuthenticationServer.Models.Dtos;
using System;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Repositories
{
    public interface IServicePermissions
    {
        Task<bool> CheckExistInAvailiableServices(string serviceId);
        void CreateAvailiableService(AvailiableServiceCreateDto serviceCreateDto);
        Task<bool> DeleteAvailiableService(string serviceId);
        Task<bool> DeleteServicePermissions(Guid accountId);
        Task<System.Collections.Generic.IEnumerable<AvailiableServiceDto>> GetAvailiableServices();
        Task<AccountServicePermissionsDto> GetServicePermissions(Guid accountId);
        Task<AccountServicePermissionsDto> SaveServicePermissions(AccountServicePermissionsCreateDto permissionsCreateDto);
    }
}