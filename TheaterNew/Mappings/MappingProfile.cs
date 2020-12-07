using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Theater.Domain.Core.Entities;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Models.Performance;
using Theater.Domain.Core.Models.Poster;
using Theater.Domain.Core.Models.Actor;
using Theater.Domain.Core.Models.Role;
using Theater.Domain.Core.Models.ActorRole;
using Theater.Domain.Core.Models.User.Account;

namespace Theater.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            #region ActorRole

            CreateMap<ActorRole, ActorRoleDTO>();
            CreateMap<ActorRoleDTO, ActorRole>();

            CreateMap<CreateActorRoleModel, ActorRoleDTO>();
            CreateMap<UpdateActorRoleModel, ActorRoleDTO>();

            #endregion

            #region Actor

            CreateMap<Actor, ActorDTO>();
            CreateMap<ActorDTO, Actor>();

            CreateMap<CreateActorModel, ActorDTO>();
            CreateMap<UpdateActorModel, ActorDTO>();

            #endregion

            #region Performance

            CreateMap<Performance, PerformanceDTO>();
            CreateMap<PerformanceDTO, Performance>();

            CreateMap<CreatePerformanceModel, PerformanceDTO>();
            CreateMap<UpdatePerformanceModel, PerformanceDTO>();

            #endregion

            #region Poster

            CreateMap<Poster, PosterDTO>();
            CreateMap<PosterDTO, Poster>();

            CreateMap<CreatePosterModel, PosterDTO>();
            CreateMap<UpdatePosterModel, PosterDTO>();

            #endregion

            #region Role

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

            CreateMap<CreateRoleModel, RoleDTO>();
            CreateMap<UpdateRoleModel, RoleDTO>();

            #endregion

            #region User

            CreateMap<RegisterUserModel, UserDTO>();
            CreateMap<LoginUserModel, UserDTO>();

            #endregion

            #region IdentityUser

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            #endregion
        }
    }
}
