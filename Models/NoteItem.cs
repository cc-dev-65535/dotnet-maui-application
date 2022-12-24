using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scratch.Models
{
    [Table("NoteItems")]
    public class NoteItem
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
