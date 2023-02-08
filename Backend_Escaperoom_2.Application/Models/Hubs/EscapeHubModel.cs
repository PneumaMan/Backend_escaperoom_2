using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Models.Hubs
{
    public class EscapeHubModel
    {
        public int EscapeRoomId { get; set; }

        public string EscapeRoomIdEncrypt => HashHelper.Encrypt(EscapeRoomId.ToString());

        public TimeSpan TimeGeneral { get; set; }

        public TimeSpan TiempoLimiteParticipantes { get; set; }

        //public string TimeGeneralV => this.TimeGeneral.ToString();

        public List<ParticipanteHubModel> Participantes { get; set; }

        /*Contsructor*/
        public EscapeHubModel()
        {
            this.Participantes = new List<ParticipanteHubModel>();
        }

        /*Metodos*/
        public void AddParticipante(ParticipanteHubModel participante)
        {

            this.Participantes.Add(participante);
        }

        public int GetIndexParticipante(int participanteId)
        {
            return this.Participantes.FindIndex(x => x.ParticipanteId == participanteId);
        }

        public void RevomeParticipante(int participanteId)
        {;
            var index = this.GetIndexParticipante(participanteId);
            this.Participantes.RemoveAt(index);
        }

        public TimeSpan GetTimeScoreParticipante(int participanteId)
        {
            var index = this.GetIndexParticipante(participanteId);
            return this.Participantes[index].TimeScore;
        }

        public void AddBonificacionParticipante(int participanteId, TimeSpan bonificacion)
        {
            var index = this.GetIndexParticipante(participanteId);
            this.Participantes[index].TimeScore = this.Participantes[index].TimeScore.Add(-bonificacion);
        }

    }
}
