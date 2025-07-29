using AutoMapper;

namespace TaskManager;
using TaskManager.Models;
using TaskManager.Dtos;


public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<TaskItem, TaskDto>().ReverseMap();
    }
}