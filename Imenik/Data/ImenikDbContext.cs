using Imenik.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imenik.Data
{
    public class ImenikDbContext :DbContext
    {
        public ImenikDbContext(DbContextOptions options): base(options) 
        { }
        
        public DbSet<Tablica> Tablicas { get; set; }
    }
}
