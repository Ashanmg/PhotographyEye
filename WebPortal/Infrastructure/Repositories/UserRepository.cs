using PhotographyEye.Entities;
using PhotographyEye.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotographyEye.Infrastructure.Repositories
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        IRoleRepository _roleRepository;
        public UserRepository(PhotographyEyeContext context, IRoleRepository roleRepository) : base(context)
        {
            this._roleRepository = roleRepository;
        }

        public User GetSingleByUsername(string username)
        {
            return this.GetSingle(x => x.Username == username);
        }

        public IEnumerable<Role> GetUserRoles(string username)
        {
            List<Role> _roles = null;
            User _user = this.GetSingle(u => u.Username == username, u => u.UserRoles);
            if (_user != null)
            {
                _roles = new List<Role>();
                foreach (var _userRole in _user.UserRoles)
                    _roles.Add(_roleRepository.GetSingle(_userRole.RoleId));
            }

            return _roles;
        }
    }
}
