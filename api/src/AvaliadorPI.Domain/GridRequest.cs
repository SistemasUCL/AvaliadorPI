namespace AvaliadorPI.Domain
{
    public enum Direction { Ascending, Descendent }

    /// <summary>
    /// Objeto de pesquisa e paginação de dados
    /// </summary>
    public class GridRequest
    {
        /// <summary>
        /// Valor a ser pesquisado 
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Número da página a ser retornada
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Tamanho de registro por página
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Campo utilizado para ordenação
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Direção da ordenação
        /// </summary>
        public Direction? Direction { get; set; }

        public bool IsDirty
        {
            get
            {
                return (Page > 0 && PageSize > 0) || Direction != null ||
                    !string.IsNullOrWhiteSpace(Search) || !string.IsNullOrWhiteSpace(OrderBy);
            }
        }
    }
}
