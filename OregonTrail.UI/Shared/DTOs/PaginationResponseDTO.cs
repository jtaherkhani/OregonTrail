using System;
using System.Collections.Generic;
using System.Text;

namespace OregonTrail.UI.Shared.DTOs
{
    public class PaginationResponseDTO<T>
    {
        public T Content { get; set;}

        public int TotalRecordCount { get; set; }
    }
}
