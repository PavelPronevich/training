
namespace DBLayer
{
    public class Report : IDbEntity
    {
        public int Id { get; set; }
        public string FileReport { get; set; }
        public int ManagerID { get; set; }
        public virtual Manager Manager { get; set; }
 
    }
}
