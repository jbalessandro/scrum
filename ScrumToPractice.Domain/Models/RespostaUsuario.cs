using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumToPractice.Domain.Models
{
    public class RespostaUsuario
    {
        public int IdResposta { get; set; }
        public string Descricao { get; set; }
        public bool Selecionada { get; set; }
    }
}
