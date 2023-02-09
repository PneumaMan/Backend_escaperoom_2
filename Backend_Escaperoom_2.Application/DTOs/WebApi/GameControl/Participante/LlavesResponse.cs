using Backend_escaperoom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend_escaperoom.Application.DTOs.WebApi.RespuestaParticipante;
using Backend_escaperoom.Application.DTOs.WebApi.EscapeRoom;
using Backend_escaperoom.Application.DTOs.WebApi.Encuesta;
using Backend_escaperoom.Application.Enums;
using Backend_escaperoom.Application.Extensions;

namespace Backend_escaperoom.Application.DTOs.WebApi.GameControl.Participante
{
    public class LlavesResponse
    {
        public string Llave { get; set; }

        public bool Correcta { get; set; }

    }

}
