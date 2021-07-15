using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewProject.AuthenticationServer;
using NewProject.AuthenticationServer.Models.Dtos;
using NewProject.AuthenticationServer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Repositories
{
    public class ServicePermissionsInSqlRepository : IServicePermissions
    {
        private readonly AuthorizationDbContext _db;
        private readonly ILogger<ServicePermissionsInSqlRepository> _logger;

        public ServicePermissionsInSqlRepository(AuthorizationDbContext db,
            ILogger<ServicePermissionsInSqlRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<AvailiableServiceDto>> GetAvailiableServices()
        {
            var services = await _db.AvailiableService.ToListAsync();

            List<AvailiableServiceDto> servicesDto = new List<AvailiableServiceDto>();

            foreach (var service in services)
            {
                servicesDto.Add(new AvailiableServiceDto(service));
            }

            return servicesDto;
        }

        public async void CreateAvailiableService(AvailiableServiceCreateDto serviceCreateDto)
        {
            // var methodInfo = $"{nameof(CreateAvailiableService)}(name = {service.Id}, uri = {service.Uri})";
            // _logger.Info(methodInfo);

            var newService = serviceCreateDto.ToEntity();

            await _db.AvailiableService.AddAsync(newService);
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();
        }
        public async Task<bool> CheckExistInAvailiableServices(string serviceId)
        {
            var service = await _db.AvailiableService.FirstOrDefaultAsync(c => c.Id == serviceId);
            return service != null;
        }

        public async Task<bool> DeleteAvailiableService(string serviceId)
        {
            //var methodInfo = $"{nameof(DeleteAvailiableService)}(name = {serviceId})";
            //Log.Info(methodInfo);

            var service = await _db.AvailiableService.FirstOrDefaultAsync(c => c.Id == serviceId);

            if (service == null) return false;

            _db.Remove(service);
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return true;
        }

        public async Task<AccountServicePermissionsDto> GetServicePermissions(Guid accountId)
        {
            var permissions = await _db.AccountServicePermissions.FirstOrDefaultAsync(c => c.Id == accountId);
            return new AccountServicePermissionsDto(permissions);
        }

        public async Task<AccountServicePermissionsDto> SaveServicePermissions(AccountServicePermissionsCreateDto permissionsCreateDto)
        {
            //var methodInfo = $"{nameof(SaveServicePermissions)}(id = {permissions.Id})";
            //Log.Info(methodInfo);
            //permissions.Timestamp = GetDtFunc.Invoke();

            var newPermission = permissionsCreateDto.ToEntity();

            //var col = Db.GetCollection<AccountServicePermissions>();
            //col.Upsert(permissions);

            await Task.CompletedTask;
            return new AccountServicePermissionsDto(newPermission);
        } //TODO

        public async Task<bool> DeleteServicePermissions(Guid accountId)
        {
            var account = await _db.AccountServicePermissions.FirstOrDefaultAsync(c => c.Id == accountId);
            //var methodInfo = $"{nameof(DeleteServicePermissions)}(id = {accountId})";
            //Log.Info(methodInfo);

            return true;
        }   //TODO
    }
}
