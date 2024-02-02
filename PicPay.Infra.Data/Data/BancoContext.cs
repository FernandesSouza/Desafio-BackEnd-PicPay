using Microsoft.EntityFrameworkCore;
using PicPay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Infra.Data.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
            
        }

        public DbSet<LojistaModel> lojistaModels { get; set; }
        public DbSet<TransferenciaModel> transferenciaModels { get; set; }
        public DbSet<UsuarioModel> usuarioModels { get; set; }


    }
}
