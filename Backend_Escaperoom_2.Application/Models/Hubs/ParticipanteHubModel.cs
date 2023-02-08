using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Backend_Escaperoom_2.Application.Models.Hubs
{
    public class ParticipanteHubModel
    {
        public int ParticipanteId { get; set; }

        public string ParticipanteIdEncrypt => HashHelper.Encrypt(ParticipanteId.ToString());

        public TimeSpan TimeScore { get; set; }

        //public string TimeScoreV => this.TimeScore.ToString();
    }
}
