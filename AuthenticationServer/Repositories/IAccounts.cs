
using AuthenticationServer.Models.Dtos;
using NewProject.AuthenticationServer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Repositories
{
    public interface IAccounts
    {
        /// <summary>
        /// Создать аккаунт
        /// </summary>
        /// <param name="accountCreateDto"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<AccountReturnDto> CreateAccount(AccountCreateDto accountCreateDto, Role role);

        /// <summary>
        /// Удаление аккаунта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAccount(Guid id); //+

        /// <summary>
        /// Получить аккаунт
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AccountReturnDto> GetAccount(Guid id); //+

        /// <summary>
        /// Получить все удаленные аккаунты
        /// </summary>
        /// <returns></returns>
        Task<List<AccountReturnDto>> GetAllDeletedAccounts(); 

        /// <summary>
        /// Получить все аккаунты
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AccountReturnDto>> GetAllAccounts(); //+

        /// <summary>
        /// Ргистрация нового слушателя
        /// </summary>
        /// <param name="accountCreateDto"></param>
        /// <returns></returns>
        Task<AccountReturnDto> RegisterListenerAccount(AccountCreateDto accountCreateDto);

        /// <summary>
        /// Регистрация нового исполнителя
        /// </summary>
        /// <param name="accountCreateDto"></param>
        /// <returns></returns>
        Task<AccountReturnDto> RegisterPerformerAccount(AccountCreateDto accountCreateDto);

        /// <summary>
        /// Восстановление аккаунта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RestoreAccount(Guid id);

        /// <summary>
        /// Обновить аккаунт
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountCreateDto"></param>
        /// <returns></returns>
        Task<bool> UpdateAccount(Guid id, AccountCreateDto accountCreateDto);

       
    }
}