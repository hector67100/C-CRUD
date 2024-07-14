using System;
using System.ComponentModel.DataAnnotations;

namespace Testcrud.Models
{
    public class lista
    {
        [Key]
        public int idLista { get; set; }
        public string tarea { get; set; }
        public bool listo { get; set; }
    }
}