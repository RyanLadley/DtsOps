using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using dtso.data.Entities;
using dtso.data.Context;
using System.Linq;

namespace dtso.data.Repositories
{
    public class BugRepository : IBugRepository
    {
        private MainContext _context;

        public BugRepository(MainContext context)
        {
            _context = context;
        }

        public int Add(Bug bug)
        {
            _context.Bugs.Add(bug);
            _context.SaveChanges();

            return bug.BugId;
        }

        public List<Bug> GetAll()
        {
            return _context.Bugs.ToList();
        }
    }
}
