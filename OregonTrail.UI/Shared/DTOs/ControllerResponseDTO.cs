using System;
using System.Collections.Generic;
using System.Text;

namespace OregonTrail.UI.Shared.DTOs
{
    /// <summary>
    /// Data transfer object created during the server controller response.
    /// </summary>
    public class ControllerResponseDTO<T>
    {
        public string ErrorMessage { get; set; }
        public bool HasError { get { return !string.IsNullOrEmpty(ErrorMessage); } }
        public int TotalRecordCount { get; set; } // non-mandatory, added to handle server side pagination.
        public T Content { get; set; }
    }
}
