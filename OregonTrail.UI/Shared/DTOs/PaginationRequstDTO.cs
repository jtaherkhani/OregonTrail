using System;
using System.Collections.Generic;
using System.Text;

namespace OregonTrail.UI.Shared.DTOs
{
    /// <summary>
    /// Data transfer object to handle the pagination definition between the server and client.
    /// </summary>
    public class PaginationRequstDTO
    {
        private const int DefaultRecordsPerPage = 10; // todo: move into a constant project

        /// <summary>
        /// The current page containing the paginated data.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// The records allowed on the page.
        /// </summary>
        public int RecordsPerPage { get; set; } = DefaultRecordsPerPage;
    }
}
