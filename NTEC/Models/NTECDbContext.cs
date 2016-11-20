using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using NTEC.Models.Entities;


namespace NTEC.Models
{
    public class NTECDbContext : DbContext
    {
        public NTECDbContext()
            : base("name=NTECConnectionString")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //con
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            modelBuilder.Entity<ReferenceByUserOrDeprt>().ToTable("ReferenceByUserOrDeprt");
            modelBuilder.Entity<LogginHistoryView>().ToTable("LogginHistoryView");
            modelBuilder.Entity<FullAuditView>().ToTable("FullAuditView");
            modelBuilder.Entity<ReferenceForSpecificView>().ToTable("ReferenceForSpecificView");
            modelBuilder.Entity<AuditTrial>().ToTable("AuditTrial");
            modelBuilder.Entity<C_RefNoCount>().ToTable("C_RefNoCount");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<EventType>().ToTable("EventType");
            modelBuilder.Entity<Exception>().ToTable("Exception");
            modelBuilder.Entity<C_RefNoCount>().ToTable("RefNoCount");
            modelBuilder.Entity<GeneratedReference>().ToTable("GeneratedReference");
            modelBuilder.Entity<LangTransalation>().ToTable("LangTransalation");
            modelBuilder.Entity<Language>().ToTable("Language");
            modelBuilder.Entity<RefUnAssignedPool>().ToTable("RefUnAssignedPool");
            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<UserDepartment>().ToTable("UserDepartment");
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<RefCounterByDep>().ToTable("RefCounterByDep");
            modelBuilder.Entity<RefCounterByUser>().ToTable("RefCounterByUser");
            base.OnModelCreating(modelBuilder);

        }

        public virtual IDbSet<C_RefNoCount> C_RefNoCount { get; set; }
        public virtual IDbSet<AuditTrial> AuditTrial { get; set; }
        public virtual IDbSet<CorrespondentTypeName> CorrespondentTypeName { get; set; }
        public virtual IDbSet<Department> Department { get; set; }
        public virtual IDbSet<EventType> EventType { get; set; }
        public virtual IDbSet<Exception> Exception { get; set; }
        public virtual IDbSet<GeneratedReference> GeneratedReference { get; set; }
        public virtual IDbSet<LangTransalation> LangTransalation { get; set; }
        public virtual IDbSet<Language> Language { get; set; }
        public virtual IDbSet<RefUnAssignedPool> RefUnAssignedPool { get; set; }
        public virtual IDbSet<Roles> Roles { get; set; }
        public virtual IDbSet<UserDepartment> UserDepartment { get; set; }
        public virtual IDbSet<Users> Users { get; set; }
        public virtual IDbSet<FullAuditView> FullAuditView { get; set; }
        public virtual IDbSet<LogginHistoryView> LogginHistoryView { get; set; }
        public virtual IDbSet<ReferenceByUserOrDeprt> ReferenceByUserOrDeprt { get; set; }
        public virtual IDbSet<ReferenceForSpecificView> ReferenceForSpecificView { get; set; }
        public virtual IDbSet<RefCounterByDep> RefCounterByDeps { get; set; }
        public virtual IDbSet<RefCounterByUser> RefCounterByUsers { get; set; }

    }
}