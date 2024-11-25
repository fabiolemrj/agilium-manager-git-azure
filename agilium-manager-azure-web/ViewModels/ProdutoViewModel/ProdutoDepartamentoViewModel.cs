﻿using agilium.webapp.manager.mvc.Enums;
using agilium.webapp.manager.mvc.ViewModels.Empresa;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace agilium.webapp.manager.mvc.ViewModels.ProdutoViewModel
{
    public class ProdutoDepartamentoViewModel
    {
        public long Id { get; set; }
        [Display(Name = "Empresa")]
        public long? idEmpresa { get; set; }
        [Display(Name = "Codigo")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(6, ErrorMessage = "Quantidade maxima de caracteres para o campo {0} deve ser de até {1}")]
        public string Codigo { get; set; }
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(50, ErrorMessage = "Quantidade maxima de caracteres para o campo {0} deve ser de até {1}")]
        public string Nome { get; set; }
        [Display(Name = "Situação")]
        public EAtivo? situacao { get; set; }
        public List<EmpresaViewModel> Empresas { get; set; } = new List<EmpresaViewModel> { };
    }
}