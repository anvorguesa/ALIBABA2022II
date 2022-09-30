using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aplicacionraiz2022postgress.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<aplicacionraiz2022postgress.Models.Contacto> DataContactos { get; set; }

    public DbSet<aplicacionraiz2022postgress.Models.Producto> DataProductos { get; set; }

    public DbSet<aplicacionraiz2022postgress.Models.Proforma> DataProforma { get; set; }

    public DbSet<aplicacionraiz2022postgress.Models.Pago> DataPago { get; set; }

    public DbSet<aplicacionraiz2022postgress.Models.Pedido> DataPedido { get; set; }

    public DbSet<aplicacionraiz2022postgress.Models.DetallePedido> DataDetallePedido { get; set; }
}
