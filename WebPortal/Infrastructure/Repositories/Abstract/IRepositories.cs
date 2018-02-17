using PhotographyEye.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotographyEye.Infrastructure.Repositories.Abstract
{
    public interface IAlbumRepository : IEntityBaseRepository<Album> { }
    public interface ILoggingRepository : IEntityBaseRepository<Error> { }
    public interface IPhotoRepository : IEntityBaseRepository<Photo> { }
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        User GetSingleByUsername(string username);
        IEnumerable<Role> GetUserRoles(string username);
    }
    public interface IRoleRepository : IEntityBaseRepository<Role> { }
    public interface IUserRoleRepository : IEntityBaseRepository<UserRole> { }

}
