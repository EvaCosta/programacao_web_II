﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projeto1_IF.Models;

[Table("tbReceitaAlimentarPadrao_X_Alimento")]
[Index("IdAlimento", Name = "IX_tbReceitaAlimentarPadrao_X_Alimento_IdAlimento")]
[Index("IdReceitaAlimentarPadrao", Name = "IX_tbReceitaAlimentarPadrao_X_Alimento_IdReceitaAlimentarPadrao")]
public partial class TbReceitaAlimentarPadraoXAlimento
{
    [Key]
    [Column("IdReceitaAlimentarPadrao_X_Alimento_X_QuantidadeAlimento")]
    public int IdReceitaAlimentarPadraoXAlimentoXQuantidadeAlimento { get; set; }

    public int IdReceitaAlimentarPadrao { get; set; }

    public int IdAlimento { get; set; }

    public int IdQuantidadeAlimento { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Periodicidade { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string QuantoTempo { get; set; }

    [ForeignKey("IdAlimento")]
    [InverseProperty("TbReceitaAlimentarPadraoXAlimento")]
    public virtual TbAlimento IdAlimentoNavigation { get; set; }

    [ForeignKey("IdReceitaAlimentarPadrao")]
    [InverseProperty("TbReceitaAlimentarPadraoXAlimento")]
    public virtual TbReceitaAlimentarPadrao IdReceitaAlimentarPadraoNavigation { get; set; }
}