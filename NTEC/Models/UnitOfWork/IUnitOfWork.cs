using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTEC.Models.Entities;
using NTEC.Models.Repositories;

namespace NTEC.Models.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<AuditTrial> AutitTrails { get; }
        IGenericRepository<C_RefNoCount> RefNoCounts { get; }
        IGenericRepository<CorrespondentTypeName> CorrespondentTypeNames { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<EventType> EventTypes { get; }

        IGenericRepository<Entities.Exception> Exceptions { get; }
        IGenericRepository<FullAuditView> FullAuditViews { get; }
        IGenericRepository<GeneratedReference> GeneratedReferences { get; }
        IGenericRepository<LangTransalation> LangTransalations { get; }
        IGenericRepository<LogginHistoryView> LogginHistoryViews { get; }
        IGenericRepository<ReferenceByUserOrDeprt> ReferenceByUserOrDeprts { get; }
        IGenericRepository<ReferenceForSpecificView> ReferenceForSpecificViews { get; }
        IGenericRepository<RefUnAssignedPool> RefUnAssignedPools { get; }
        IGenericRepository<Roles> Roless { get; }
        IGenericRepository<UserDepartment> UserDepartments { get; }
        IGenericRepository<Users> Useres{ get; }
        IGenericRepository<RefCounterByDep> RefCounterByDeps { get; }
        IGenericRepository<RefCounterByUser> RefCounterByUsers { get; }
        void Save(); //Commit

    }
}
