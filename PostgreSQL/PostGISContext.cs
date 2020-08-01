using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Configuration;
namespace PostGISWeb.PostgreSQL
{
    public class PostGISContext : DbContext
    {
        private IConfiguration Configuration;
        public DbSet<Gis> GisObjects { get; set; }
        public DbSet<GisMap> GisMap { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Configuration.GetConnectionString("PostgreSQLConnection");
            optionsBuilder.UseNpgsql(connectionString,
                        o => o.UseNetTopologySuite());
        }
    public PostGISContext(IConfiguration _configuration)
    {
        Configuration = _configuration;
    }
    }


    [Table("gis")]
    public class Gis
    {
        [Key]
        public int gid { get; set; }
        public string jpt_nazwa_ { get; set; }

        public Geometry geom { get; set; }

    }

    public class GisMap
    {
        [Key]
        public int gid { get; set; }
        public string geoJSON { get; set; }
    }
}