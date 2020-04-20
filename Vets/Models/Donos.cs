using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vets.Models
{
    public class Donos
    {
        public Donos()
        {
            Animais = new HashSet<Animais>();
        }

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
        [StringLength(40, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O {0} é de preeenchimento obrigatório")] //o parâmetro {0} representa o 'nome do atributo'
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Deve escrever exatamente {1} algarismos no {0}.")]
        [RegularExpression("[12567][0-9]{8}", ErrorMessage = "")]
        public string NIF { get; set; }

        // lista de Animais de um determinado dono
        public ICollection<Animais> Animais { get; set; }
    }
}
