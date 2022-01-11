namespace Core.Entities
{
    public class Product: BasaEntity
    {
				public string Name { get; set; }
				public string Description { get; set; }
				public decimal Price { get; set; }
				public string PictureUrl { get; set; }
				public ProductType ProductType { get; set; }  //migration will create a relationship of Product table to ProductType table with a foreign key
				public int ProductTypeId { get; set; }
				public ProductBrand ProductBrand { get; set; } //migration will create a relationship of Product table to ProductBrand table with a foreign key
				public int ProductBrandId { get; set; }
    }

}