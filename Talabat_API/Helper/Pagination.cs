﻿namespace Talabat_API.Helper
{
    public class Pagination<T>
    {
        public Pagination(int pageSize, int pageIndex,IReadOnlyList<T> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
