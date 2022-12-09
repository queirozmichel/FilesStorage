using AutoMapper;
using FilesStorage.WebAPI.Models;
using File = FilesStorage.WebAPI.Models.File;

namespace FilesStorage.WebAPI.DTOs.Mappings
{
  public class MappingProfile : Profile
  {
	public MappingProfile()
	{
	  CreateMap<Client, ClientDTO>().ReverseMap();
	  CreateMap<Address, AddressDTO>().ReverseMap();
	  CreateMap<File, FileDTO>().ReverseMap();
	}
  }
}
