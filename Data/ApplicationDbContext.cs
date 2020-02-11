using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GerenciamentoEvento.Models;

namespace GerenciamentoEvento.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GerenciamentoEvento.Models.Local> Local { get; set; }

         public DbSet<GerenciamentoEvento.Models.Evento> Evento { get; set; }
    }
}
