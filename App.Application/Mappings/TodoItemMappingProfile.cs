using App.Application.Commands.TodoItems.AddTodoItem;
using ToDo.Domain.Entities;
using AutoMapper;

namespace App.Application.Mappings
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            CreateMap<AddTodoItemCommand, TodoItem>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<TodoItem, DTOs.TodoItemDto>().ReverseMap();
        }
    }
}
