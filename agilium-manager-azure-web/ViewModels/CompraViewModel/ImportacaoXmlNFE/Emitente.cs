﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace agilium.webapp.manager.mvc.ViewModels.CompraViewModel.ImportacaoXmlNFE
{
    public class Emitente
    {
        [Display(Name = "CPF/CNPJ")]
        public string CNPJ { get; set; }
        [Display(Name = "Razão Social")]
        public string xNome { get; set; }
        [Display(Name = "Nome Fantasia")]
        public string xFant { get; set; }
        [XmlElement("enderEmit")]
        [Display(Name = "Logradouro")]
        public Endereco Endereco { get; set; }=new Endereco();
        [Display(Name = "Inscrição Estadual")]
        public string IE { get; set; }
        public string IEST { get; set; }
        public int CRT { get; set; }
    
    }
}
