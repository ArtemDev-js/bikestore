using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
        private readonly IGenericRepository<ProductType> _productTypesRepo;
				private readonly IMapper _mapper;

		public ProductsController(
            IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> productBrandsRepo,
            IGenericRepository<ProductType> productTypesRepo,
						IMapper mapper
        )
        {
            _productTypesRepo = productTypesRepo;
						_mapper = mapper;
						_productsRepo = productsRepo;
            _productBrandsRepo = productBrandsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
						var spec = new ProductsWithTypesAndBrandsSpecification(); //calls parameterless constructor in ProductsWithTypesAndBrandsSpecification

            var products = await _productsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]  //endpoint
        [ProducesResponseType(StatusCodes.Status200OK)] //response format
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)] //if response is 404NotFound return a typeof ApiResponse by passig our StatusCodes.Status404NotFound info into it
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) 
        {
						var spec = new ProductsWithTypesAndBrandsSpecification(id); //calls constructor with the parameter in ProductsWithTypesAndBrandsSpecification

            var product =  await _productsRepo.GetEntityWithSpec(spec); //goes to GenericRepo and execute GetEntityWithSpec(spec)

            if(product == null) return NotFound(new ApiResponse(404)); //if product not found...

						return _mapper.Map<Product,ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _productBrandsRepo.ListAllAsync();

            return Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productTypesRepo.ListAllAsync();

            return Ok(productTypes);
        }
    }
}
