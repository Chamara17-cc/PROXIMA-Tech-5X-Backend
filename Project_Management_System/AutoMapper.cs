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
        }
    }
}