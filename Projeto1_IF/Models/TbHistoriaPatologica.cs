﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projeto1_IF.Models;

[Table("tbHistoriaPatologica")]
[Index("IdPaciente", Name = "IX_tbHistoriaPatologica_IdPaciente")]
[Index("IdPatologia", Name = "IX_tbHistoriaPatologica_IdPatologia")]
public partial class TbHistoriaPatologica
{
    [Key]
    public int IdHistoriaPatologica { get; set; }

    public int IdPaciente { get; set; }

    public int? IdPatologia { get; set; }

    [Column("FlgHAS")]
    public bool? FlgHas { get; set; }

    [Column("FlgAVC")]
    public bool? FlgAvc { get; set; }

    public bool? FlgDoencasPulmonares { get; set; }

    public bool? FlgDoencasCardiacas { get; set; }

    public bool? FlgDoencaRenal { get; set; }

    public bool? FlgDoencaHepatica { get; set; }

    public bool? FlgNeoplasia { get; set; }

    public bool? FlgHipercolesterolemia { get; set; }

    public bool? FlgHipertrigliciridemia { get; set; }

    public bool? FlgHiperuricemia { get; set; }

    public bool? FlgAnemias { get; set; }

    public bool? FlgCirurgias { get; set; }

    public bool? FlgDoencasAutoImunes { get; set; }

    public bool? FlgDiabetes { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Obs { get; set; }

    [ForeignKey("IdPaciente")]
    [InverseProperty("TbHistoriaPatologica")]
    public virtual TbPaciente IdPacienteNavigation { get; set; }

    [ForeignKey("IdPatologia")]
    [InverseProperty("TbHistoriaPatologica")]
    public virtual TbPatologia IdPatologiaNavigation { get; set; }
}