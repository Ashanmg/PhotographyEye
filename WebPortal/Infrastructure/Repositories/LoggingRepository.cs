using PhotographyEye.Entities;
using PhotographyEye.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotographyEye.Infrastructure.Repositories
{
    public class LoggingRepository :EntityBaseRepository<Error>, ILoggingRepository
    {
        public LoggingRepository(PhotographyEyeContext context) :base(context)
        {
        }

        public override void Commit()
        {
            try
            {
                base.Commit();
            }
            catch { }
        }
    }
}
