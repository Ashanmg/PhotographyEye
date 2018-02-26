using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotographyEye.Infrastructure.Services.Abstract
{
    public interface IEncryptionService
    {
        /// <summary>
        /// Creates a random salt key
        /// </summary>
        /// <returns></returns>
        string CreateSalt();

        /// <summary>
        /// The password encryption method
        /// </summary>
        /// <param name="password">the password</param>
        /// <param name="salt">the salt key</param>
        /// <returns>returns encrypted password</returns>
        string EncryptPassword(string password, string salt);
    }
}
