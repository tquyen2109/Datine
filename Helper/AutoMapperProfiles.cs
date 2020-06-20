using AutoMapper;
using DatingApplication.Dtos;
using DatingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApplication.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForList>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                src.DateOfBirth.CalculateAge()));
            CreateMap<User, UserForDetail>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                src.DateOfBirth.CalculateAge())); ;
            CreateMap<Photo, PhotoForDetail>();
            CreateMap<UserForUpdate, User>();
            CreateMap<Photo, PhotoForReturn>();
            CreateMap<PhotoForCreation, Photo>();
            CreateMap<UserForRegister, User>();
            CreateMap<MessageForCreation, Message>();
            CreateMap<Message, MessageToReturn>()
                    .ForMember(m => m.SenderPhotoUrl, opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                    .ForMember(m => m.RecipientPhotoUrl, opt => opt.MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
        }
    }
}
