using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class Cliente
    {
        [Key]
        [DisplayName("#")]
        public long ClienteId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string NomeCliente { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "O campo Tipo Cliente é obrigatório.")]
        public string TipoCliente { get; set; }

        [DisplayName("Contato")]
        [Required(ErrorMessage = "O campo Nome Contato é obrigatório.")]
        public string NomeContato { get; set; }

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "O campo Telefone Contato é obrigatório.")]
        [MaxLength(15, ErrorMessage = "O campo Telefone Contato pode ter no máximo 15 caracteres.")]
        public string TelefoneContato { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        public string Cidade { get; set; }

        [DisplayName("Bairro")]
        [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
        public string Bairro { get; set; }

        [DisplayName("Logradouro")]
        [Required(ErrorMessage = "O campo Logradouro é obrigatório.")]
        public string Logradouro { get; set; }

        [DisplayName("Cadastrado")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string DataCadastro { get; set; }

        [DisplayName("Atualizado")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string DataAtualizacao { get; set; }
    }
}