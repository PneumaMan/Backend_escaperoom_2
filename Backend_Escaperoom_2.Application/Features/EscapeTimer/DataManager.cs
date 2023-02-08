using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Models.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.EscapeTimer
{
    public class DataManager
    {
        public List<EscapeHubModel> TimesEscape { get; set; }

        /*Contructor*/
        public DataManager()
        {
            this.TimesEscape = new List<EscapeHubModel>();
        }

        /* Metodos */
        public void AddEscape(EscapeHubModel escape)
        {
            this.TimesEscape.Add(escape);
        }

        public int GetIndexEscape(int escapeId)
        {
            return this.TimesEscape.FindIndex(x => x.EscapeRoomId == escapeId);
        }

        public void RevomeEscape(int escapeId)
        {
            var index = this.GetIndexEscape(escapeId);
            this.TimesEscape.RemoveAt(index);
        }

        /*public void AddParticipante(int escapeId, ParticipanteHubModel participante)
        {
            var index = this.TimesEscape.FindIndex(x => x.EscapeRoomId == escapeId);
            this.TimesEscape[index].Participantes.Add(participante);
        }

        public void RevomeParticipante(int participanteId)
        {
            var index = this.Participantes.FindIndex(x => x.ParticipanteId == participanteId);
            this.Participantes.RemoveAt(index);
        }*/

        public List<EscapeHubModel> ActualizarTiempos()
        {
            
            for (int i = 0; i < this.TimesEscape.Count; i++)
            {
                Thread.Sleep(500);
                if (this.TimesEscape[i].TimeGeneral > new TimeSpan(0,0,0))
                {
                    //var time = TimeSpan.FromSeconds(-1);
                    //var time = Convert.ToDateTime(this.TimesEscape[i].TimeGeneral);
                    //time.AddSeconds(-1);
                    //this.TimesEscape[i].TimeGeneral = TimeSpan.Parse(time.ToString());

                    this.TimesEscape[i].TimeGeneral = this.TimesEscape[i].TimeGeneral.Add(new TimeSpan(0,0,-1));

                    for (int j = 0; j < this.TimesEscape[i].Participantes.Count; j++)
                    {
                        if (this.TimesEscape[i].Participantes[j].TimeScore < this.TimesEscape[i].TiempoLimiteParticipantes)
                        {
                            //var timeP = TimeSpan.FromSeconds(1);
                            //var timeP = Convert.ToDateTime(this.TimesEscape[i].TimeGeneral);
                            //time.AddSeconds(1);
                            //this.TimesEscape[i].Participantes[j].TimeScore = TimeSpan.Parse(timeP.ToString());
                            this.TimesEscape[i].Participantes[j].TimeScore = this.TimesEscape[i].Participantes[j].TimeScore.Add(new TimeSpan(0, 0, 1));
                        }
                    }
                }

            }

            return this.TimesEscape;
        }
    }
}
