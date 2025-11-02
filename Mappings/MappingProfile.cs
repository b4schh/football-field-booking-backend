using AutoMapper;
using FootballField.API.Entities;
using FootballField.API.Dtos.User;
using FootballField.API.Dtos.Complex;
using FootballField.API.Dtos.Field;
using FootballField.API.Dtos.Booking;
using FootballField.API.Dtos.TimeSlot;
using FootballField.API.Dtos.Review;
using FootballField.API.Dtos.Notification;

namespace FootballField.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ========== USER MAPPINGS ==========
            CreateMap<User, UserDto>();
            CreateMap<User, UserProfileDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // ========== COMPLEX MAPPINGS ==========
            CreateMap<Entities.Complex, ComplexDto>();
            CreateMap<Entities.Complex, ComplexWithFieldsDto>();
            CreateMap<Entities.Complex, ComplexFullDetailsDto>()
                .ForMember(dest => dest.Fields, opt => opt.Ignore()); // Ignore vì map thủ công trong Service
            CreateMap<CreateComplexDto, Entities.Complex>();
            CreateMap<CreateComplexByOwnerDto, Entities.Complex>();
            CreateMap<CreateComplexByAdminDto, Entities.Complex>();
            CreateMap<UpdateComplexDto, Entities.Complex>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // ========== FIELD MAPPINGS ==========
            CreateMap<Entities.Field, FieldDto>();
            CreateMap<Entities.Field, FieldWithTimeSlotsDto>();
            CreateMap<CreateFieldDto, Entities.Field>();
            CreateMap<UpdateFieldDto, Entities.Field>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ComplexId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // ========== BOOKING MAPPINGS ==========
            CreateMap<Entities.Booking, BookingDto>();
            CreateMap<CreateBookingDto, Entities.Booking>();
            CreateMap<UpdateBookingDto, Entities.Booking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FieldId, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // ========== TIMESLOT MAPPINGS ==========
            CreateMap<Entities.TimeSlot, TimeSlotDto>();
            CreateMap<CreateTimeSlotDto, Entities.TimeSlot>();
            CreateMap<UpdateTimeSlotDto, Entities.TimeSlot>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FieldId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // ========== REVIEW MAPPINGS ==========
            CreateMap<Entities.Review, ReviewDto>();
            CreateMap<CreateReviewDto, Entities.Review>();
            CreateMap<UpdateReviewDto, Entities.Review>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FieldId, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.BookingId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // ========== NOTIFICATION MAPPINGS ==========
            CreateMap<Entities.Notification, NotificationDto>();
            CreateMap<CreateNotificationDto, Entities.Notification>();
        }
    }
}
