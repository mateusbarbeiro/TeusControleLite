﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeusControleLite.Infrastructure.Queries
{
    public class PaginatedList<T>
    {
        /// <summary>
        /// Informações
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Índice da pagina
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// Total de páginas
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Total de itens
        /// </summary>
        public int TotalItems { get; private set; }

        public PaginatedList(
            List<T> items, 
            int count, 
            int pageIndex, 
            int pageSize
        )
        {
            PageIndex = pageIndex;
            TotalItems = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            Data = items;
        }

        /// <summary>
        /// Tem página anterior?
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        /// <summary>
        /// Tem próxima página?
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        /// <summary>
        /// Cria busca assíncrona
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PaginatedList<T>> CreateAsync(
            IList<T> source, 
            int pageIndex,
            int pageSize
        )
        {
            var count = source.Count;
            var items = source.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedList<T>(
                items, 
                count, 
                pageIndex, 
                pageSize
            );
        }
    }
}
