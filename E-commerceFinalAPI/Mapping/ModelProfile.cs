using AutoMapper;
using E_commerceFinalAPI.DTO.Category;
using E_commerceFinalAPI.DTO.Product;
using E_commerceFinalAPI.Models;

namespace E_commerceFinalAPI.Mapping
{
    public class ModelProfile:Profile
    {
        public ModelProfile()
        {
            CreateMap<Category, GetCategoryDTO>();
            CreateMap<CreateCategoryDTO, Category>();

            CreateMap<Product, GetProductDTO>();
            CreateMap<CreateProductDTO, Product>();
        }
    }
}
