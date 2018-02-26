using PhotographyEye.Entities;
using PhotographyEye.Infrastructure.Core;
using PhotographyEye.Infrastructure.Repositories.Abstract;
using PhotographyEye.Infrastructure.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PhotographyEye.Infrastructure.Services
{
    public class MembershipService : IMembershipService
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

        #region  implementation of the interface method
        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipContext = new MembershipContext();

            var user = _userRepository.GetSingleByUsername(username);
            if (user != null && isUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.Username);
                membershipContext.User = user;

                var identity = new GenericIdentity(user.Username);
                membershipContext.Principal = new GenericPrincipal(
                    identity,
                    userRoles.Select(x => x.Name).ToArray());
            }
            return membershipContext;
        }

        public User CreateUser(string username, string email, string password, int[] roles)
        {
            var isUserExist = _userRepository.GetSingleByUsername(username);

            if (isUserExist != null)
            {
                throw new Exception("This user already Exists");
            }

            var passwordSalt = _encryptionService.CreateSalt();

            var user = new User()
            {
                Username = username,
                Salt = passwordSalt,
                Email = email,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                IsLocked = false,
                DateCreated = DateTime.Now
            };

            _userRepository.Add(user);
            _userRepository.Commit();

            if (roles != null || roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    addUserToRole(user, role);
                }
            }
            _userRepository.Commit();

            return user;
        }
        public List<Role> GetUserRoles(string username)
        {
            List<Role> _result = new List<Role>();

            var existingUser = _userRepository.GetSingleByUsername(username);

            if (existingUser != null)
            {
                foreach (var userRole in existingUser.UserRoles)
                {
                    _result.Add(userRole.Role);
                }
            }
            return _result.Distinct().ToList();
        }

        public User GetUser(int userId)
        {
            return _userRepository.GetSingle(userId);
        }

        #endregion

        #region helper methods

        private void addUserToRole(User user, int roleId)
        {
            var role = _roleRepository.GetSingle(roleId);
            if (role == null)
            {
                throw new Exception("Role does not exist.");
            }

            var userRole = new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            };

            _userRoleRepository.Add(userRole);
            _userRoleRepository.Commit();  // change from base
        }
        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }
            return false;
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }
        #endregion
    }
}
