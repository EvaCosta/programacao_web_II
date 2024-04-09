﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projeto1_IF.Models;

[Table("tbSuplemento")]
public partial class TbSuplemento
{
    [Key]
    public int IdSuplemento { get; set; }

    public int IdTipoQuantidade { get; set; }

    public int Tipo { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; }

    public double DoseMinima { get; set; }

    public double DoseMaxima { get; set; }

    public double Carboidrato { get; set; }

    public double VitaminaA { get; set; }

    public double VitaminaB { get; set; }
}