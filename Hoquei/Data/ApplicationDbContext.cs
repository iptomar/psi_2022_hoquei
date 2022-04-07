using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoquei.Data
{
    public class HoqueiDB : IdentityDbContext
    {
        public class ApplicationUser : IdentityUser
        {

            /// <summary>
            /// recolhe a data de registo de um utilizador
            /// </summary>
            public DateTime DataRegisto { get; set; }

            ////[ForeignKey(nameof(cliente))]
            ////public int UtilizadorFK { get; set; }
            //[ForeignKey("userId")]
            //public virtual Utilizador user { get; set; }
        }
        public HoqueiDB(DbContextOptions<HoqueiDB> options)
            : base(options)
        {

        }
    }
}
