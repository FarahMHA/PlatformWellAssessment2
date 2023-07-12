using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PlatformWellAssessment2.Data;
using PlatformWellAssessment2.Models;
using static PlatformWellAssessment2.DtoModels.PlatformWellModelDto;

namespace PlatformWellAssessment2.DataAccess
{
    public class PlatformWellDal
    {
        private readonly PlatformWelldbContext _context;

        public PlatformWellDal(PlatformWelldbContext context)
        {
            _context = context;
        }

    }
}
