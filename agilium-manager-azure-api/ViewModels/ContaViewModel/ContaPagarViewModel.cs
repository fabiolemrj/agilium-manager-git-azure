﻿using agilium.api.business.Enums;
using agilium.api.business.Models;
using System;

namespace agilium.api.manager.ViewModels.ContaViewModel
{
    public class ContaPagarViewModel
    {
        public long Id { get; set; }
        public Int64? IDCONTAPAI { get; set; }
        public Int64? IDCATEG_FINANC { get; set; }
        public Int64? IDUSUARIO { get; set; }
        public Int64? IDFORNEC { get; set; }
        public Int64? IDCONTA { get; set; }
        public Int64? IDEMPRESA { get; set; }
        public Int64? IDLANC { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public double? ValorConta { get; set; } = 0;
        public double? ValorDesconto { get; set; } = 0;
        public double? ValorAcrescimo { get; set; } = 0;
        public int? ParcelaInicial { get; set; } = 1;
        public ETipoConta? TipoConta { get; set; }
        public int? Situacao { get; set; }
        public string OBS { get; set; }
        public string NumeroNotaFiscal { get; set; }
        public DateTime? DataNotaFiscal { get; set; }
        public DateTime? DatCadastro { get; set; }
    }

    public class ContaPagarViewModelIndex: ContaPagarViewModel
    {
        public string CategoriaFinanceira { get; set; }
        public string Usuario { get; set; }
        public string Fornecedor { get; set; }
        public string Conta { get; set; }
    }
}