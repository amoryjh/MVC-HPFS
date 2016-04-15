using HPSMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HPSMVC.DAL
{
  public class HPSMVCEntities : DbContext
  {
    public DbSet<Event> Events { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<Index> Indices { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
      modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
    }

    public System.Data.Entity.DbSet<HPSMVC.Models.Contact> Contacts { get; set; }
  }
}