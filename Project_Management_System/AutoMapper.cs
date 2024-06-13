using AutoMapper;
using Project_Management_System.DTOs;
using Project_Management_System.Models;


namespace Project_Management_System
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //Create Automappers here EX: CreateMap<AddbudgetDTO, Budget>();MAp AddBudget to Budget 
            CreateMap<DeveloperRate, GetRateDto>();
            CreateMap<AddBudgetDto, Budget>();
            CreateMap<Budget, GetBudgetDto>();
            CreateMap<AddTransacDto, Transaction>();
            CreateMap<Transaction, GetTransacDto>();
            CreateMap<Project, GetProjectDto>();
            CreateMap<Project, GetDeveloperProjectDTO>();
            CreateMap<Project, ProjectDescriptionDTO>();
            CreateMap<Models.Task, GetTaskDTO>();
            CreateMap<Models.Task, TaskDescriptionDTO>();


            CreateMap<UpdateTaskDTO, Models.Task>();
            CreateMap<UpdateProjectTimeDTO, Project>();
            CreateMap<Project, GetTeamDTO>();
            CreateMap<CreateTaskTimeDTO, TaskTime>();

            CreateMap<AddPaymentDto, Payment>();
            CreateMap<Payment, AddPaymentDto>();
           
            CreateMap<UpdateTaskStatusDTO, Models.Task>();





        }
    }
}