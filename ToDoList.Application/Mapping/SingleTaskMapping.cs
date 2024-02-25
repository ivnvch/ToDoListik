﻿using AutoMapper;
using ToDoList.Application.Dto.SingleTask;
using ToDoList.Domain.Entity;

namespace ToDoList.Application.Mapping
{
    public class SingleTaskMapping : Profile
    {
        public SingleTaskMapping()
        {
            CreateMap<SingleTask, CreateSingleTaskDto>().ReverseMap();
            CreateMap<SingleTask, UpdateSingleTaskDto>().ReverseMap();
        }
    }
}
