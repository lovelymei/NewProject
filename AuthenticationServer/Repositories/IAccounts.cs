using NewProject.AuthenticationServer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Repositories
{
    public interface IAccounts
    {
        Task<AccountDto> Authenticate(string email, string password);

        /// <summary>
        /// Удаление аккаунта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAccount(Guid id); //+

        /// <summary>
        /// Получение аккаунта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AccountDto> GetAccount(Guid id); //+

        /// <summary>
        /// Получение всех аккаунтов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AccountDto>> GetAllAccounts(); //+

        /// <summary>
        /// Получение всех удаленных аккаунтов
        /// </summary>
        /// <returns></returns>
        Task<List<AccountDto>> GetAllDeletedAccounts(); //+

        /// <summary>
        /// Регистрация нового слушателя
        /// </summary>
        /// <param name="accountCreateDto"></param>
        /// <returns></returns>
        Task<AccountDto> RegisterListenerAccount(AccountCreateDto accountCreateDto); //+

        /// <summary>
        /// Регистрация нового исполнителя
        /// </summary>
        /// <param name="accountCreateDto"></param>
        /// <returns></returns>
        Task<AccountDto> RegisterPerformerAccount(AccountCreateDto accountCreateDto); //+

        /// <summary>
        /// Восстановление аккаунта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RestoreAccount(Guid id);

        /// <summary>
        /// Обновление аккаунта
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountCreateDto"></param>
        /// <returns></returns>
        Task<bool> UpdateAccount(Guid id, AccountCreateDto accountCreateDto);
    }
}