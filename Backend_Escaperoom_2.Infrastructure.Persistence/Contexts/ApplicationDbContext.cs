using Backend_Escaperoom_2.Application.Interfaces;
using Backend_Escaperoom_2.Application.Interfaces.Services;
using Backend_Escaperoom_2.Domain.Common;
using Backend_Escaperoom_2.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Role, string, IdentityUserClaim<string>, UsuarioRole,
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime,
            IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this._dateTime = dateTime;
            this._authenticatedUser = authenticatedUser;
        }

        public DbSet<Usuario> UsuariosDbSet { get; set; }
        public DbSet<Role> RolesDbSet { get; set; }
        public DbSet<UsuarioRole> UsuarioRolesDbSet { get; set; }
        public DbSet<EscapeRoom> EscapeRoomsDbSet { get; set; }
        public DbSet<Estacion> EstacionesDbSet { get; set; }
        public DbSet<Reto> RetosDbSet { get; set; }
        public DbSet<RespuestaRetos> RespuestaRetosDbSet { get; set; }
        public DbSet<Participante> ParticipantesDbSet { get; set; }
        public DbSet<TipoParticipante> TiposParticipantesDbSet { get; set; }
        public DbSet<ParticipantesRespuestas> ParticipantesRespuestasDbSet { get; set; }
        public DbSet<EncuestasParticipantes> EncuestasParticipantesDbSet { get; set; }
        public DbSet<Encuestas> EncuestasDbSet { get; set; }
        public DbSet<PreguntaEncuestas> PreguntaEncuestasDbSet { get; set; }
        public DbSet<RespuestaEncuestas> RespuestasEncuestasDbSet { get; set; }
        public DbSet<Team> TeamsDbSet { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = this._dateTime.Now;
                        entry.Entity.CreatedBy = entry.Entity.CreatedBy ?? this._authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = this._dateTime.Now;
                        entry.Entity.LastModifiedBy = entry.Entity.LastModifiedBy ?? this._authenticatedUser.UserId;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(u =>
            {
                u.HasIndex(x => x.UserName).IsUnique();
                u.HasIndex(x => x.Email).IsUnique();
                u.HasMany(d => d.UsuariosRoles).WithOne(x => x.Usuario).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Role>(r =>
            {
                r.HasMany(d => d.UsuariosRoles).WithOne(x => x.Role).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<UsuarioRole>(r =>
            {
                r.HasKey(x => new { x.UserId, x.RoleId });
                r.HasIndex("UserId", "RoleId").IsUnique();
                r.HasOne(d => d.Usuario).WithMany(x => x.UsuariosRoles).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.NoAction);
                r.HasOne(d => d.Role).WithMany(x => x.UsuariosRoles).HasForeignKey(f => f.RoleId).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<EscapeRoom>(er =>
            {
                er.HasMany(c => c.Participantes).WithOne(x => x.EscapeRoom).OnDelete(DeleteBehavior.Cascade);
                er.HasMany(c => c.Estaciones).WithOne(x => x.EscapeRoom).OnDelete(DeleteBehavior.Cascade);
                er.HasMany(c => c.TipoParticipantes).WithOne(x => x.EscapeRoom).OnDelete(DeleteBehavior.Cascade);
                er.HasMany(c => c.Equipos).WithOne(x => x.EscapeRoom).OnDelete(DeleteBehavior.Cascade);
                er.HasMany(c => c.Encuestas).WithOne(x => x.EscapeRoom).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Estacion>(e =>
            {
                e.HasOne(c => c.EscapeRoom).WithMany(x => x.Estaciones).OnDelete(DeleteBehavior.NoAction);
                e.HasMany(c => c.Retos).WithOne(x => x.Estacion).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Reto>(r =>
            {
                r.HasOne(c => c.Estacion).WithMany(x => x.Retos).OnDelete(DeleteBehavior.NoAction);
                r.HasOne(c => c.TipoParticipante).WithMany(x => x.Retos).OnDelete(DeleteBehavior.NoAction);
                r.HasOne(c => c.RetoPadre).WithOne().OnDelete(DeleteBehavior.NoAction);
                r.HasMany(c => c.Respuestas).WithOne(x => x.Reto).OnDelete(DeleteBehavior.Cascade);
                r.HasMany(c => c.ParticipantesRespuestas).WithOne(x => x.Reto).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RespuestaRetos>(re =>
            {
                re.HasOne(c => c.Reto).WithMany(x => x.Respuestas).OnDelete(DeleteBehavior.NoAction);
                re.HasOne(c => c.NextEstacion).WithOne().OnDelete(DeleteBehavior.NoAction);
                re.HasMany(c => c.ParticipantesRespuestas).WithOne(x => x.Respuesta).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ParticipantesRespuestas>(rp =>
            {
                rp.HasKey(x => new { x.ParticipanteId, x.RetoId });
                rp.HasIndex("ParticipanteId", "RetoId", "RespuestaId").IsUnique();
                rp.HasOne(c => c.Reto).WithMany(x => x.ParticipantesRespuestas).OnDelete(DeleteBehavior.NoAction);
                rp.HasOne(c => c.Respuesta).WithMany(x => x.ParticipantesRespuestas).OnDelete(DeleteBehavior.NoAction);
                rp.HasOne(c => c.Participante).WithMany(x => x.ParticipantesRespuestas).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<TipoParticipante>(tp =>
            {
                tp.HasOne(c => c.EscapeRoom).WithMany(x => x.TipoParticipantes).OnDelete(DeleteBehavior.NoAction);
                tp.HasMany(c => c.Participantes).WithOne(x => x.TipoParticipante).OnDelete(DeleteBehavior.NoAction);
                tp.HasMany(c => c.Retos).WithOne(x => x.TipoParticipante).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Participante>(p =>
            {
                p.HasOne(c => c.EscapeRoom).WithMany(x => x.Participantes).OnDelete(DeleteBehavior.NoAction);
                p.HasOne(c => c.TipoParticipante).WithMany(x => x.Participantes).OnDelete(DeleteBehavior.NoAction);
                p.HasOne(c => c.MyTeam).WithMany(x => x.Participantes).OnDelete(DeleteBehavior.NoAction);
                p.HasMany(c => c.ParticipantesRespuestas).WithOne(x => x.Participante).OnDelete(DeleteBehavior.Cascade);
                p.HasMany(c => c.EncuestasParticipantes).WithOne(x => x.Participante).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Encuestas>(e =>
            {
                e.HasOne(c => c.EscapeRoom).WithMany(x => x.Encuestas).OnDelete(DeleteBehavior.NoAction);
                e.HasMany(c => c.PreguntasEncuestas).WithOne(x => x.Encuesta).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PreguntaEncuestas>(pe =>
            {
                pe.HasOne(c => c.Encuesta).WithMany(x => x.PreguntasEncuestas).OnDelete(DeleteBehavior.NoAction);
                pe.HasMany(c => c.RespuestasEncuestas).WithOne(x => x.PreguntaEncuestas).OnDelete(DeleteBehavior.Cascade);
                pe.HasMany(c => c.EncuestasParticipantes).WithOne(x => x.PreguntaEncuesta).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RespuestaEncuestas>(re =>
            {
                re.HasOne(c => c.PreguntaEncuestas).WithMany(x => x.RespuestasEncuestas).OnDelete(DeleteBehavior.NoAction);
                re.HasMany(c => c.EncuestasParticipantes).WithOne(x => x.RespuestasEncuesta).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EncuestasParticipantes>(ep =>
            {
                ep.HasKey(x => new { x.ParticipanteId, x.PreguntaEncuestasId });
                ep.HasIndex("ParticipanteId", "PreguntaEncuestasId").IsUnique();
                ep.HasOne(c => c.PreguntaEncuesta).WithMany(x => x.EncuestasParticipantes).OnDelete(DeleteBehavior.NoAction);
                ep.HasOne(c => c.RespuestasEncuesta).WithMany(x => x.EncuestasParticipantes).OnDelete(DeleteBehavior.NoAction);
                ep.HasOne(c => c.Participante).WithMany(x => x.EncuestasParticipantes).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Team>(t =>
            {
                t.HasOne(c => c.EscapeRoom).WithMany(x => x.Equipos).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(c => c.Participantes).WithOne(x => x.MyTeam).OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
