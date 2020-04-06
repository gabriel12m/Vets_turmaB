using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vets.Models
{
    public class Donos
    {
        public Donos()
        {
            ListaAnimais = new HashSet<Animais>();
        }

        public int ID { get; set; }

        public string Nome { get; set; }

        public string NIF { get; set; }


        //lista de Animais a que o Dono está associado
        public ICollection<Animais> ListaAnimais { get; set; }
    }
}
