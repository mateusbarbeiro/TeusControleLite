namespace TeusControleLite.Domain.Dtos
{
    /// <summary>
    /// Dto de inserção de produto
    /// </summary>
    public class CreateProductsModel
    {
        /// <summary>
        /// Código de barras
        /// Global Trade Item Number
        /// </summary>
        public string Gtin { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Preço
        /// </summary>
        public decimal Price { get; set; }


        /// <summary>
        /// Marca
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Código da classificação global de produtos 
        /// Global Product Classification
        /// </summary>
        public string GpcCode { get; set; }

        /// <summary>
        /// Descrição do GPC
        /// </summary>
        public string GpcDescription { get; set; }

        /// <summary>
        /// Código da Nomenclatura Comum do Mercosul
        /// </summary>
        public string NcmCode { get; set; }

        /// <summary>
        /// Descrição do NCM
        /// </summary>
        public string NcmDescription { get; set; }

        /// <summary>
        /// Descrição completa do NCM
        /// </summary>
        public string NcmFullDescription { get; set; }

        /// <summary>
        /// Url - Imagem do produto
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Em estoque
        /// </summary>
        public int InStock { get; set; }
    }

    /// <summary>
    /// Dto para atualizar um produto
    /// </summary>
    public class UpdateProductsModel : CreateProductsModel
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Está ativo?
        /// </summary>
        public bool Active { get; set; }
    }

    /// <summary>
    /// Dto com informações do produto
    /// </summary>
    public class ProductsModel : UpdateProductsModel
    {

    }
}
