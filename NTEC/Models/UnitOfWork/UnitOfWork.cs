using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NTEC.Models.Entities;
using NTEC.Models.Repositories;

namespace NTEC.Models.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private NTECDbContext _context;
        public UnitOfWork()
        {
            _context = new NTECDbContext();
        }

        private IGenericRepository<AuditTrial> _auditTrials;
        public Repositories.IGenericRepository<Entities.AuditTrial> AutitTrails
        {
            get
            {
                if (_auditTrials == null)
                {
                    return new EfGenericRepository<AuditTrial>(_context);
                }
                return _auditTrials;
            }
        }
        private IGenericRepository<C_RefNoCount> _refNoCount;
        public Repositories.IGenericRepository<Entities.C_RefNoCount> RefNoCounts
        {
            get
            {
                if (_refNoCount == null)
                {
                    return new EfGenericRepository<C_RefNoCount>(_context);
                }
                return _refNoCount;
            }
        }
        private IGenericRepository<CorrespondentTypeName> _correspondentTypeNames;
        public Repositories.IGenericRepository<Entities.CorrespondentTypeName> CorrespondentTypeNames
        {
            get
            {
                if (_correspondentTypeNames == null)
                {
                    return new EfGenericRepository<CorrespondentTypeName>(_context);
                }
                return _correspondentTypeNames;
            }
        }
        private IGenericRepository<Department> _departments;
        public Repositories.IGenericRepository<Entities.Department> Departments
        {
            get
            {
                if (_departments == null)
                {
                    return new EfGenericRepository<Department>(_context);
                }
                return _departments;
            }
        }
        private IGenericRepository<EventType> _eventTypes;
        public Repositories.IGenericRepository<Entities.EventType> EventTypes
        {
            get
            {
                if (_eventTypes == null)
                {
                    return new EfGenericRepository<EventType>(_context);
                }
                return _eventTypes;
            }
        }
        private IGenericRepository<Entities.Exception> _exceptions;
        public Repositories.IGenericRepository<Entities.Exception> Exceptions
        {
            get
            {
                if (_exceptions == null)
                {
                    return new EfGenericRepository<Entities.Exception>(_context);
                }
                return _exceptions;
            }
        }
        private IGenericRepository<FullAuditView> _fullAuditViews;
        public Repositories.IGenericRepository<Entities.FullAuditView> FullAuditViews
        {
            get
            {
                if (_fullAuditViews == null)
                {
                    return new EfGenericRepository<FullAuditView>(_context);
                }
                return _fullAuditViews;
            }
        }
        private IGenericRepository<GeneratedReference> _generatedReferences;
        public Repositories.IGenericRepository<Entities.GeneratedReference> GeneratedReferences
        {
            get
            {
                if (_generatedReferences == null)
                {
                    return new EfGenericRepository<GeneratedReference>(_context);
                }
                return _generatedReferences;
            }
        }
        private IGenericRepository<LangTransalation> _langTransalations;
        public Repositories.IGenericRepository<Entities.LangTransalation> LangTransalations
        {
            get
            {
                if (_langTransalations == null)
                {
                    return new EfGenericRepository<LangTransalation>(_context);
                }
                return _langTransalations;
            }
        }
        private IGenericRepository<LogginHistoryView> _logginHistoryViewss;
        public Repositories.IGenericRepository<Entities.LogginHistoryView> LogginHistoryViews
        {
            get
            {
                if (_logginHistoryViewss == null)
                {
                    return new EfGenericRepository<LogginHistoryView>(_context);
                }
                return _logginHistoryViewss;
            }
        }
        private IGenericRepository<ReferenceByUserOrDeprt> _referenceByUserOrDeprts;
        public Repositories.IGenericRepository<Entities.ReferenceByUserOrDeprt> ReferenceByUserOrDeprts
        {
            get
            {
                if (_referenceByUserOrDeprts == null)
                {
                    return new EfGenericRepository<ReferenceByUserOrDeprt>(_context);
                }
                return _referenceByUserOrDeprts;
            }
        }
        private IGenericRepository<ReferenceForSpecificView> _referenceForSpecificViews;
        public Repositories.IGenericRepository<Entities.ReferenceForSpecificView> ReferenceForSpecificViews
        {
            get
            {
                if (_referenceForSpecificViews == null)
                {
                    return new EfGenericRepository<ReferenceForSpecificView>(_context);
                }
                return _referenceForSpecificViews;
            }
        }
        private IGenericRepository<RefUnAssignedPool> _refUnAssignedPools;
        public Repositories.IGenericRepository<Entities.RefUnAssignedPool> RefUnAssignedPools
        {
            get
            {
                if (_refUnAssignedPools == null)
                {
                    return new EfGenericRepository<RefUnAssignedPool>(_context);
                }
                return _refUnAssignedPools;
            }
        }
        private IGenericRepository<Roles> _roless;
        public Repositories.IGenericRepository<Entities.Roles> Roless
        {
            get
            {
                if (_roless == null)
                {
                    return new EfGenericRepository<Roles>(_context);
                }
                return _roless;
            }
        }
        private IGenericRepository<UserDepartment> _UserDepartments;
        public Repositories.IGenericRepository<Entities.UserDepartment> UserDepartments
        {
            get
            {
                if (_UserDepartments == null)
                {
                    return new EfGenericRepository<UserDepartment>(_context);
                }
                return _UserDepartments;
            }
        }
        private IGenericRepository<Users> _useres;
        public Repositories.IGenericRepository<Entities.Users> Useres
        {
            get
            {
                if (_useres == null)
                {
                    return new EfGenericRepository<Users>(_context);
                }
                return _useres;
            }
        }
        private IGenericRepository<RefCounterByDep> _refCounterByDeps;

        public IGenericRepository<RefCounterByDep> RefCounterByDeps
        {
            get
            {
                if (_refCounterByDeps == null)
                {
                    return new EfGenericRepository<RefCounterByDep>(_context);
                }
                return _refCounterByDeps;
            }
        }
        private IGenericRepository<RefCounterByUser> _refCounterByUsers;
        public Repositories.IGenericRepository<Entities.RefCounterByUser> RefCounterByUsers
        {
            get
            {
                if (_refCounterByUsers == null)
                {
                    return new EfGenericRepository<RefCounterByUser>(_context);
                }
                return _refCounterByUsers;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}