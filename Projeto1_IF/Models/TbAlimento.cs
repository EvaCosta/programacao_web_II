﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projeto1_IF.Models;

[Table("tbAlimento")]
public partial class TbAlimento
{
    [Key]
    public int IdAlimento { get; set; }

    public int IdTipoQuantidade { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; }

    public double Carboidrato { get; set; }

    public double VitaminaA { get; set; }

    public double VitaminaB { get; set; }

    [InverseProperty("IdAlimentoNavigation")]
    public virtual ICollection<TbReceitaAlimentarPadraoXAlimento> TbReceitaAlimentarPadraoXAlimento { get; set; } = new List<TbReceitaAlimentarPadraoXAlimento>();
}