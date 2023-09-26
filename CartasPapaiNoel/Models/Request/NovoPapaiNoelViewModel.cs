using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;



namespace CartasPapaiNoel.Models.Request
{
    public class NovoPapaiNoelViewModel
    {
        [Required(ErrorMessage = "O Nome é obrigatório!")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 255 caracteres")]
        public string Nome { get; set; }



        [Required(ErrorMessage = "o Endereco Completo é obrigatório!")]
        public string EnderecoCompleto { get; set; }


        [Required(ErrorMessage = "A Idade é obrigatória!")]
        [Range(1, 15, ErrorMessage = "A crianca deve ter de 1 a 15 anos")]
        public int Idade { get; set; }
        [Required(ErrorMessage = "O texto é obrigatório!")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "O texto deve ter de 1 a 500 caracteres")]
        public string TextoCarta { get; set; }






    }
}
