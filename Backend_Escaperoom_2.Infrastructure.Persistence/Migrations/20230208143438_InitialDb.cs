using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendEscaperoom2.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoLogueo = table.Column<int>(type: "int", nullable: false),
                    EstadoUsuario = table.Column<bool>(type: "bit", nullable: false),
                    ChangedPassword = table.Column<bool>(type: "bit", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastSignin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escape_Room",
                columns: table => new
                {
                    idescaperoom = table.Column<int>(name: "id_escape_room", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreescaperoom = table.Column<string>(name: "nombre_escape_room", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    fechainiciojuego = table.Column<DateTime>(name: "fecha_inicio_juego", type: "datetime2", nullable: false),
                    fechafinjuego = table.Column<DateTime>(name: "fecha_fin_juego", type: "datetime2", nullable: false),
                    organizador = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    celularorganizador = table.Column<string>(name: "celular_organizador", type: "nvarchar(15)", maxLength: 15, nullable: false),
                    tiempolimitegeneral = table.Column<TimeSpan>(name: "tiempo_limite_general", type: "time", nullable: false),
                    tiempolimiteparticipantes = table.Column<TimeSpan>(name: "tiempo_limite_participantes", type: "time", nullable: false),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escape_Room", x => x.idescaperoom);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encuestas",
                columns: table => new
                {
                    idencuesta = table.Column<int>(name: "id_encuesta", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreencuesta = table.Column<string>(name: "nombre_encuesta", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    idescaperoom = table.Column<int>(name: "id_escape_room", type: "int", nullable: false),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuestas", x => x.idencuesta);
                    table.ForeignKey(
                        name: "FK_Encuestas_Escape_Room_id_escape_room",
                        column: x => x.idescaperoom,
                        principalTable: "Escape_Room",
                        principalColumn: "id_escape_room");
                });

            migrationBuilder.CreateTable(
                name: "Estacion",
                columns: table => new
                {
                    idestacion = table.Column<int>(name: "id_estacion", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreestacion = table.Column<string>(name: "nombre_estacion", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    contextoestacion = table.Column<string>(name: "contexto_estacion", type: "nvarchar(max)", nullable: true),
                    pathmultimedia = table.Column<string>(name: "path_multimedia", type: "nvarchar(max)", nullable: true),
                    tipomultimedia = table.Column<int>(name: "tipo_multimedia", type: "int", nullable: false),
                    idescaperoom = table.Column<int>(name: "id_escape_room", type: "int", nullable: false),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estacion", x => x.idestacion);
                    table.ForeignKey(
                        name: "FK_Estacion_Escape_Room_id_escape_room",
                        column: x => x.idescaperoom,
                        principalTable: "Escape_Room",
                        principalColumn: "id_escape_room");
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    idteam = table.Column<int>(name: "id_team", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreteam = table.Column<string>(name: "nombre_team", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    capacidad = table.Column<int>(type: "int", nullable: false),
                    timescoregrupal = table.Column<TimeSpan>(name: "time_score_grupal", type: "time", nullable: true),
                    idescaperoom = table.Column<int>(name: "id_escape_room", type: "int", nullable: false),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.idteam);
                    table.ForeignKey(
                        name: "FK_Team_Escape_Room_id_escape_room",
                        column: x => x.idescaperoom,
                        principalTable: "Escape_Room",
                        principalColumn: "id_escape_room");
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Participante",
                columns: table => new
                {
                    idtipoparticipante = table.Column<int>(name: "id_tipo_participante", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombretipo = table.Column<string>(name: "nombre_tipo", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    idescaperoom = table.Column<int>(name: "id_escape_room", type: "int", nullable: false),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Participante", x => x.idtipoparticipante);
                    table.ForeignKey(
                        name: "FK_Tipo_Participante_Escape_Room_id_escape_room",
                        column: x => x.idescaperoom,
                        principalTable: "Escape_Room",
                        principalColumn: "id_escape_room");
                });

            migrationBuilder.CreateTable(
                name: "Preguntas_Encuestas",
                columns: table => new
                {
                    idpreguntaencuesta = table.Column<int>(name: "id_pregunta_encuesta", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    preguntaencuesta = table.Column<string>(name: "pregunta_encuesta", type: "nvarchar(150)", maxLength: 150, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tipopregunta = table.Column<int>(name: "tipo_pregunta", type: "int", nullable: false),
                    numeroorden = table.Column<int>(name: "numero_orden", type: "int", nullable: false),
                    pathmultimedia = table.Column<string>(name: "path_multimedia", type: "nvarchar(max)", nullable: true),
                    tipomultimedia = table.Column<int>(name: "tipo_multimedia", type: "int", nullable: false),
                    idencuesta = table.Column<int>(name: "id_encuesta", type: "int", nullable: false),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas_Encuestas", x => x.idpreguntaencuesta);
                    table.ForeignKey(
                        name: "FK_Preguntas_Encuestas_Encuestas_id_encuesta",
                        column: x => x.idencuesta,
                        principalTable: "Encuestas",
                        principalColumn: "id_encuesta");
                });

            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    idparticipante = table.Column<int>(name: "id_participante", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoidentificacion = table.Column<int>(name: "tipo_identificacion", type: "int", nullable: false),
                    identificacion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telefono = table.Column<long>(type: "bigint", nullable: true),
                    estado = table.Column<int>(type: "int", nullable: false),
                    timescore = table.Column<TimeSpan>(name: "time_score", type: "time", nullable: true),
                    idescaperoom = table.Column<int>(name: "id_escape_room", type: "int", nullable: false),
                    idtipoparticipante = table.Column<int>(name: "id_tipo_participante", type: "int", nullable: false),
                    idteam = table.Column<int>(name: "id_team", type: "int", nullable: true),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.idparticipante);
                    table.ForeignKey(
                        name: "FK_Participantes_Escape_Room_id_escape_room",
                        column: x => x.idescaperoom,
                        principalTable: "Escape_Room",
                        principalColumn: "id_escape_room");
                    table.ForeignKey(
                        name: "FK_Participantes_Team_id_team",
                        column: x => x.idteam,
                        principalTable: "Team",
                        principalColumn: "id_team");
                    table.ForeignKey(
                        name: "FK_Participantes_Tipo_Participante_id_tipo_participante",
                        column: x => x.idtipoparticipante,
                        principalTable: "Tipo_Participante",
                        principalColumn: "id_tipo_participante");
                });

            migrationBuilder.CreateTable(
                name: "Retos",
                columns: table => new
                {
                    idreto = table.Column<int>(name: "id_reto", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombrereto = table.Column<string>(name: "nombre_reto", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    contextoreto = table.Column<string>(name: "contexto_reto", type: "nvarchar(max)", nullable: true),
                    preguntareto = table.Column<string>(name: "pregunta_reto", type: "nvarchar(250)", maxLength: 250, nullable: false),
                    tipopregunta = table.Column<int>(name: "tipo_pregunta", type: "int", nullable: false),
                    numeroreto = table.Column<int>(name: "numero_reto", type: "int", nullable: false),
                    obligatorio = table.Column<bool>(type: "bit", nullable: false),
                    tiporeto = table.Column<int>(name: "tipo_reto", type: "int", nullable: false),
                    bonificacion = table.Column<TimeSpan>(type: "time", nullable: true),
                    numerooportunidades = table.Column<int>(name: "numero_oportunidades", type: "int", nullable: false),
                    qrcolor = table.Column<string>(name: "qr_color", type: "nvarchar(10)", maxLength: 10, nullable: true),
                    qrbgcolor = table.Column<string>(name: "qr_bg_color", type: "nvarchar(10)", maxLength: 10, nullable: true),
                    pathmultimedia = table.Column<string>(name: "path_multimedia", type: "nvarchar(max)", nullable: true),
                    tipomultimedia = table.Column<int>(name: "tipo_multimedia", type: "int", nullable: false),
                    idestacion = table.Column<int>(name: "id_estacion", type: "int", nullable: false),
                    idtipoparticipante = table.Column<int>(name: "id_tipo_participante", type: "int", nullable: false),
                    idretopadre = table.Column<int>(name: "id_reto_padre", type: "int", nullable: true),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retos", x => x.idreto);
                    table.ForeignKey(
                        name: "FK_Retos_Estacion_id_estacion",
                        column: x => x.idestacion,
                        principalTable: "Estacion",
                        principalColumn: "id_estacion");
                    table.ForeignKey(
                        name: "FK_Retos_Retos_id_reto_padre",
                        column: x => x.idretopadre,
                        principalTable: "Retos",
                        principalColumn: "id_reto");
                    table.ForeignKey(
                        name: "FK_Retos_Tipo_Participante_id_tipo_participante",
                        column: x => x.idtipoparticipante,
                        principalTable: "Tipo_Participante",
                        principalColumn: "id_tipo_participante");
                });

            migrationBuilder.CreateTable(
                name: "Respuestas_Encuestas",
                columns: table => new
                {
                    idrespuestaencuesta = table.Column<int>(name: "id_respuesta_encuesta", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    respuesta = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    idpreguntaencuesta = table.Column<int>(name: "id_pregunta_encuesta", type: "int", nullable: false),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas_Encuestas", x => x.idrespuestaencuesta);
                    table.ForeignKey(
                        name: "FK_Respuestas_Encuestas_Preguntas_Encuestas_id_pregunta_encuesta",
                        column: x => x.idpreguntaencuesta,
                        principalTable: "Preguntas_Encuestas",
                        principalColumn: "id_pregunta_encuesta");
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    idrespuesta = table.Column<int>(name: "id_respuesta", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    respuestareto = table.Column<string>(name: "respuesta_reto", type: "nvarchar(200)", maxLength: 200, nullable: false),
                    correcta = table.Column<bool>(type: "bit", nullable: false),
                    posicionllave = table.Column<int>(name: "posicion_llave", type: "int", nullable: true),
                    llave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    palabraretoretorno = table.Column<string>(name: "palabra_reto_retorno", type: "nvarchar(200)", maxLength: 200, nullable: true),
                    idreto = table.Column<int>(name: "id_reto", type: "int", nullable: false),
                    idnextestacion = table.Column<int>(name: "id_next_estacion", type: "int", nullable: true),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => x.idrespuesta);
                    table.ForeignKey(
                        name: "FK_Respuestas_Estacion_id_next_estacion",
                        column: x => x.idnextestacion,
                        principalTable: "Estacion",
                        principalColumn: "id_estacion");
                    table.ForeignKey(
                        name: "FK_Respuestas_Retos_id_reto",
                        column: x => x.idreto,
                        principalTable: "Retos",
                        principalColumn: "id_reto");
                });

            migrationBuilder.CreateTable(
                name: "Encuestas_Participantes",
                columns: table => new
                {
                    idparticipante = table.Column<int>(name: "id_participante", type: "int", nullable: false),
                    idpreguntaencuesta = table.Column<int>(name: "id_pregunta_encuesta", type: "int", nullable: false),
                    respuestaabierta = table.Column<string>(name: "respuesta_abierta", type: "nvarchar(max)", nullable: true),
                    fecharespuesta = table.Column<DateTime>(name: "fecha_respuesta", type: "datetime2", nullable: false),
                    idrespuestaencuesta = table.Column<int>(name: "id_respuesta_encuesta", type: "int", nullable: true),
                    RespuestasEncuestaId = table.Column<int>(type: "int", nullable: true),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuestas_Participantes", x => new { x.idparticipante, x.idpreguntaencuesta });
                    table.ForeignKey(
                        name: "FK_Encuestas_Participantes_Participantes_id_participante",
                        column: x => x.idparticipante,
                        principalTable: "Participantes",
                        principalColumn: "id_participante");
                    table.ForeignKey(
                        name: "FK_Encuestas_Participantes_Preguntas_Encuestas_id_pregunta_encuesta",
                        column: x => x.idpreguntaencuesta,
                        principalTable: "Preguntas_Encuestas",
                        principalColumn: "id_pregunta_encuesta");
                    table.ForeignKey(
                        name: "FK_Encuestas_Participantes_Respuestas_Encuestas_RespuestasEncuestaId",
                        column: x => x.RespuestasEncuestaId,
                        principalTable: "Respuestas_Encuestas",
                        principalColumn: "id_respuesta_encuesta");
                });

            migrationBuilder.CreateTable(
                name: "Participantes_Respuestas",
                columns: table => new
                {
                    idparticipante = table.Column<int>(name: "id_participante", type: "int", nullable: false),
                    idreto = table.Column<int>(name: "id_reto", type: "int", nullable: false),
                    tiemporespuesta = table.Column<TimeSpan>(name: "tiempo_respuesta", type: "time", nullable: false),
                    fecharespuesta = table.Column<DateTime>(name: "fecha_respuesta", type: "datetime2", nullable: false),
                    idrespuesta = table.Column<int>(name: "id_respuesta", type: "int", nullable: false),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastmodifiedby = table.Column<string>(name: "last_modified_by", type: "nvarchar(max)", nullable: true),
                    lastmodified = table.Column<DateTime>(name: "last_modified", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes_Respuestas", x => new { x.idparticipante, x.idreto });
                    table.ForeignKey(
                        name: "FK_Participantes_Respuestas_Participantes_id_participante",
                        column: x => x.idparticipante,
                        principalTable: "Participantes",
                        principalColumn: "id_participante",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participantes_Respuestas_Respuestas_id_respuesta",
                        column: x => x.idrespuesta,
                        principalTable: "Respuestas",
                        principalColumn: "id_respuesta");
                    table.ForeignKey(
                        name: "FK_Participantes_Respuestas_Retos_id_reto",
                        column: x => x.idreto,
                        principalTable: "Retos",
                        principalColumn: "id_reto");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId_RoleId",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_id_escape_room",
                table: "Encuestas",
                column: "id_escape_room");

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_Participantes_id_participante_id_pregunta_encuesta",
                table: "Encuestas_Participantes",
                columns: new[] { "id_participante", "id_pregunta_encuesta" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_Participantes_id_pregunta_encuesta",
                table: "Encuestas_Participantes",
                column: "id_pregunta_encuesta");

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_Participantes_RespuestasEncuestaId",
                table: "Encuestas_Participantes",
                column: "RespuestasEncuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_Estacion_id_escape_room",
                table: "Estacion",
                column: "id_escape_room");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_id_escape_room",
                table: "Participantes",
                column: "id_escape_room");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_id_team",
                table: "Participantes",
                column: "id_team");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_id_tipo_participante",
                table: "Participantes",
                column: "id_tipo_participante");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_Respuestas_id_participante_id_reto_id_respuesta",
                table: "Participantes_Respuestas",
                columns: new[] { "id_participante", "id_reto", "id_respuesta" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_Respuestas_id_respuesta",
                table: "Participantes_Respuestas",
                column: "id_respuesta");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_Respuestas_id_reto",
                table: "Participantes_Respuestas",
                column: "id_reto");

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_Encuestas_id_encuesta",
                table: "Preguntas_Encuestas",
                column: "id_encuesta");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_id_next_estacion",
                table: "Respuestas",
                column: "id_next_estacion",
                unique: true,
                filter: "[id_next_estacion] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_id_reto",
                table: "Respuestas",
                column: "id_reto");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_Encuestas_id_pregunta_encuesta",
                table: "Respuestas_Encuestas",
                column: "id_pregunta_encuesta");

            migrationBuilder.CreateIndex(
                name: "IX_Retos_id_estacion",
                table: "Retos",
                column: "id_estacion");

            migrationBuilder.CreateIndex(
                name: "IX_Retos_id_reto_padre",
                table: "Retos",
                column: "id_reto_padre",
                unique: true,
                filter: "[id_reto_padre] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Retos_id_tipo_participante",
                table: "Retos",
                column: "id_tipo_participante");

            migrationBuilder.CreateIndex(
                name: "IX_Team_id_escape_room",
                table: "Team",
                column: "id_escape_room");

            migrationBuilder.CreateIndex(
                name: "IX_Tipo_Participante_id_escape_room",
                table: "Tipo_Participante",
                column: "id_escape_room");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Encuestas_Participantes");

            migrationBuilder.DropTable(
                name: "Participantes_Respuestas");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Respuestas_Encuestas");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "Preguntas_Encuestas");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Retos");

            migrationBuilder.DropTable(
                name: "Encuestas");

            migrationBuilder.DropTable(
                name: "Estacion");

            migrationBuilder.DropTable(
                name: "Tipo_Participante");

            migrationBuilder.DropTable(
                name: "Escape_Room");
        }
    }
}
