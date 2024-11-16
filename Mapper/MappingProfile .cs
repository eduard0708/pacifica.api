using AutoMapper;
using Pacifica.API.Dtos.Branch;
using Pacifica.API.Dtos.BranchProduct;
using Pacifica.API.Dtos.Category;
using Pacifica.API.Dtos.Product;
using Pacifica.API.Dtos.Supplier;
using Pacifica.API.Dtos.TransactionReference;
using Pacifica.API.Dtos.Admin;
using Pacifica.API.Dtos.StockInOut;

namespace Pacifica.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<EmployeeProfile, EmployeeProfileDto>().ReverseMap();
            CreateMap<LoginDto, Employee>().ReverseMap();
            CreateMap<RegisterDto, Employee>().ReverseMap();

            CreateMap<TransactionReference, TransactionReferenceDto>().ReverseMap();
            CreateMap<TransactionReference, CreateTransactionReferenceDto>().ReverseMap();
            CreateMap<TransactionReference, UpdateTransactionReferenceDto>().ReverseMap();

            CreateMap<TransactionType, TransactionTypeDto>().ReverseMap();
            CreateMap<TransactionType, CreateTransactionTypeDto>().ReverseMap();
            CreateMap<TransactionType, UpdateTransactionTypeDto>().ReverseMap();

            CreateMap<Status, StatusDto>().ReverseMap();
            CreateMap<Status, UpdateStatusDto>().ReverseMap();
            CreateMap<Status, CreateTransactionTypeDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();

            CreateMap<Branch, BranchDto>().ReverseMap();
            CreateMap<Branch, CreateBranchDto>().ReverseMap();
            CreateMap<Branch, UpdateBranchDto>().ReverseMap();
            CreateMap<Branch, BranchProduct_BranchDto>().ReverseMap();


            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();


            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Supplier, CreateSupplierDto>().ReverseMap();
            CreateMap<Supplier, UpdateSupplierDto>().ReverseMap();

            CreateMap<BranchProduct, BranchProductDto>().ReverseMap();
            CreateMap<BranchProduct, AddBranchProductDto>().ReverseMap();
            CreateMap<BranchProduct, UpdateBranchProductDto>().ReverseMap();
            CreateMap<BranchProduct, UpdateBranchProductDto>().ReverseMap();

            CreateMap<StockInOut, CreateStockInOutDto>().ReverseMap();

            CreateMap<StockInOut, GetStockInOutDto>();
            CreateMap<CreateStockInOutDto, StockInOut>();
            CreateMap<GetByReferenceNumberStockInOutDto, StockInOut>().ReverseMap();


        }
    }
}