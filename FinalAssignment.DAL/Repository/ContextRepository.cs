using FinalAssignment.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalAssignment.DAL.Repository
{
    public abstract class ContextRepository : IDisposable
    {

        private static UserDbContext _joinContext;


        public ContextRepository()
        {
        }

        internal static UserDbContext JoinContext
        {
            get
            {
                if (_joinContext == null)
                {
                    _joinContext = new UserDbContext();
                }
                return _joinContext;
            }
        }

        public void Dispose()
        {
            if (_joinContext !=null)
            {
                _joinContext.Dispose();
                _joinContext = null;
            }
        }
    }
}
    