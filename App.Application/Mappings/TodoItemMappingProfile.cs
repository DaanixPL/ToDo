using ToDo.Domain.Entities;
using AutoMapper;
using ToDo.Application.Commands.TodoItems.UpdateTodoItem;
using ToDo.Application.Commands.TodoItems.AddTodoItem;

namespace ToDo.Application.Mappings
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            CreateMap<AddTodoItemCommand, TodoItem>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<UpdateTodoItemCommand, TodoItem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<TodoItem, DTOs.TodoItemDto>().ReverseMap();
        }
    }
}
