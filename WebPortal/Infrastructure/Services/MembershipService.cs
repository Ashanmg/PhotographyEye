using PhotographyEye.Infrastructure.Repositories.Abstract;
using PhotographyEye.Infrastructure.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotographyEye.Infrastructure.Services
{
    public class MembershipService // : IMembershipService
    {
        #region fields
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IEncryptionService _encryptionService;
        #endregion

        public MembershipService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository,
            IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _encryptionService = encryptionService;
        }

    }
}
