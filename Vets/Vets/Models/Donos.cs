﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vets.Models {
   public class Donos {

      public Donos() {
         Animais = new HashSet<Animais>();
      }

      [Key]
      public int ID { get; set; }

      [Required(ErrorMessage ="O Nome é de preenchimento obrigatório")]
      [StringLength(40, ErrorMessage ="O {0} não pode ter mais de {1} carateres.")]
      [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,3}",
                          ErrorMessage ="Deve escrever entre 2 e 4 nomes, começados por uma Maiúscula, seguidos de minúsculas.")]
      public string Nome { get; set; }

      [Required(ErrorMessage ="O {0} é de preenchimento obrigatório")] // o parâmetro {0} representa o 'nome do atributo'
      [StringLength (9, MinimumLength =9, ErrorMessage ="Deve escrever exatamente {1} algarismos no {0}.")]
      [RegularExpression("[12567][0-9]{8}", ErrorMessage ="Deve escrever um nº, com 9 algarismos, começando por 1, 2, 5, 6 ou 7.")]
      public string NIF { get; set; }

      // lista de Animais de um determinado dono
      public ICollection<Animais> Animais { get; set; }
   }
}
