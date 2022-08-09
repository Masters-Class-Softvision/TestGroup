using GarconLibrary.DBFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace GarconLibrary.Repository
{
    class DbContext
    {
        static readonly object Padlock = new object();
        public GarconEntities Context
        {
            get
            {
                lock (Padlock)
                {
                    if (_context == null)
                        _context = new GarconEntities();

                }
                return _context;


            }

        }

        private GarconEntities _context;

    }
}
