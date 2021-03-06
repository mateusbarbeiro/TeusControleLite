using TeusControleLite.Domain.Models.CommonModels;

namespace TeusControleLite.Domain.Models
{
    /// <summary>
    /// Classe de produtos
    /// </summary>
    public partial class Products : BaseEntity
    {
        /// <summary>
        /// Construtor classe modelo da entidade produtos
        /// </summary>
        public Products()
        {
        }

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
}
