using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.Usuarios.CRUD;
using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.AuthenticationParticipante;
using Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.Participante;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Participante;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Roles;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Teams;
using Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.CRUD;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Extensions;
using Backend_Escaperoom_2.Application.Features.Usuarios.Queries;
using Backend_Escaperoom_2.Application.Features.WebApi.EscapeRoom.Queries;
using Backend_Escaperoom_2.Application.Features.WebApi.TipoParticipantes.Queries;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Models;
using Backend_Escaperoom_2.Domain.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Linq;

namespace Backend_Escaperoom_2.Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile(IDataProtectionProvider protectionProvider)
        {
            //var protectorEscape = protectionProvider.CreateProtector(nameof(IEscapeRoomsRepositoryAsync));
            //var protectorReto = protectionProvider.CreateProtector(nameof(IRetosRepositoryAsync));
            //var protectorParticipante = protectionProvider.CreateProtector(nameof(IParticipantesRepositoryAsync));


            var roles = Enum.GetValues(typeof(TiposUsuarios)).Cast<TiposUsuarios>()
                .Select(x => new EnumItems
                {
                    Id = (int)x,
                    Nombre = x.ToString(),
                    Descripcion = x.GetEnumDescription()
                }).ToList();

            //FLUENTVALIDATION
            CreateMap<ValidationFailure, ValidationFailureResponse>();

            //USUARIO
            CreateMap<Usuario, UsuarioResponse>().ReverseMap();
            CreateMap<CreateUsuarioRequest, Usuario>()
                .ForMember(x => x.Email, o => o.MapFrom(x => x.Email.ToLower()))
                .ReverseMap();
            CreateMap<UpdateUsuarioResquest, Usuario>().ReverseMap();
            CreateMap<GetAllUsuariosRequest, GetAllUsuariosParameter>().ReverseMap();

            //ROLES
            CreateMap<Role, RolesResponse>()
                .ForMember(x => x.Role, o => o.MapFrom(x => x.Name.ToLower()))
                .ReverseMap();

            //ESCAPE ROOM
            CreateMap<EscapeRoom, EscapeRoomResponse>()
                .ForMember(x => x.UrlQREscapeId, o => o.MapFrom(x => HashHelper.Encrypt(x.Id.ToString())))
                .ReverseMap();
            CreateMap<GetAllEscapeRoomsPaginationRequest, GetAllEscapeRoomParameter>().ReverseMap();
            CreateMap<CreateEscapeRoomResquest, EscapeRoom>();
            CreateMap<UpdateEscapeRoomResquest, EscapeRoom>();

            //TIPOS PARTICIPANTES
            CreateMap<TipoParticipante, TipoParticipanteResponse>().ReverseMap();
            CreateMap<GetAllTiposParticipantesPaginationRequest, GetAllTipoParticipanteParameter>().ReverseMap();
            CreateMap<CreateTipoParticipanteResquest, TipoParticipante>();
            CreateMap<UpdateTipoParticipanteResquest, TipoParticipante>();

            //PARTICIPANTES
            CreateMap<Participante, ParticipanteResponse>().ReverseMap();
            CreateMap<Participante, EscapeParticipanteResponse>()
                .ForMember(x => x.Id, o => o.MapFrom(x => HashHelper.Encrypt(x.Id.ToString())))
                .ReverseMap();
            CreateMap<Participante, AuthenticationParticipanteResponse>()
                .ForMember(x => x.Id, o => o.MapFrom(x => HashHelper.Encrypt(x.Id.ToString())))
                .ForMember(x => x.EscapeRoomId, o => o.MapFrom(x => HashHelper.Encrypt(x.EscapeRoomId.ToString())))
                .ReverseMap();
            CreateMap<CreateParticipanteResquest, Participante>();
            CreateMap<UpdateParticipanteResquest, Participante>();

            //TIPOS PARTICIPANTES
            CreateMap<Team, TeamResponse>().ReverseMap();
            CreateMap<GetAllTeamsPaginationRequest, GetAllTeamParameter>().ReverseMap();
            CreateMap<CreateTeamResquest, Team>();
            CreateMap<UpdateTeamResquest, Team>();

            /*//RETOS
            CreateMap<Reto, RetoResponse>()
                .ForMember(x => x.UrlQRRetoId, o => o.MapFrom(x => HashHelper.Encrypt(x.Id.ToString())))
                .ForMember(x => x.UrlQREscapeId, o => o.MapFrom(x => HashHelper.Encrypt(x.EscapeRoomId.ToString())))
                .ReverseMap();
            CreateMap<Reto, RetoParticipanteResponse>()
                .ForMember(x => x.Id, o => o.MapFrom(x => HashHelper.Encrypt(x.Id.ToString())))
                .ForMember(x => x.EscapeRoomId, o => o.MapFrom(x => HashHelper.Encrypt(x.EscapeRoomId.ToString())))
                .ReverseMap();
            CreateMap<Reto, RetoResponseReq>()
                .ForMember(x => x.RetoId, o => o.MapFrom(x => HashHelper.Encrypt(x.Id.ToString())))
                .ForMember(x => x.EscapeRoomId, o => o.MapFrom(x => HashHelper.Encrypt(x.EscapeRoomId.ToString())))
                .ReverseMap();
            CreateMap<GetAllRetosPaginationRequest, GetAllRetosParameter>().ReverseMap();
            CreateMap<CreateRetosResquest, Reto>();
            CreateMap<UpdateRetosResquest, Reto>();

            //RESPUESTAS
            CreateMap<Respuesta, RespuestaResponse>().ReverseMap();
            CreateMap<CreateRespuestaResquest, Respuesta>();
            CreateMap<UpdateRespuestaResquest, Respuesta>();

            //RESPUESTAS-PARTICIPANTES
            CreateMap<RespuestaParticipante, RespuestaParticipanteResponse>().ReverseMap();
            CreateMap<RespuestaParticipante, ParticipanteRespuestaResponse>().ReverseMap();

            //ENCUESTAS
            CreateMap<Encuesta, EncuestaResponse>().ReverseMap();


            CreateMap<ParticipanteEncuestaRequest, Encuesta>();*/
        }
    }
}
