using AutoMapper;
using Company.PL.ViewModels;
using Company.DAL.Models;

namespace Company.PL.Mapping
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }

    }
}
