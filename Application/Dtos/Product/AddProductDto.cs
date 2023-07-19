namespace Application.Dtos.Product
{
    public class AddProductDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Quantity { get; set; } = default!;
    }
}