using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithNETUdemy.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
