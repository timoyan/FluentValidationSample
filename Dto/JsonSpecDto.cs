using System.Collections.Generic;

namespace ValidationSample.Dto
{

    /// <summary>
    /// Follow with jsonapi top level spec
    /// </summary>
    /// <see cref="https://jsonapi.org/format/#document-top-level"/>
    public class JsonSpecDto
    {
        /// <summary>
        /// the document’s “primary data”
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// an array of error objects
        /// </summary>
        /// <see cref="https://jsonapi.org/format/#errors"/>
        public List<JsonSpecErrorDto> Error { get; set; }
        /// <summary>
        /// a meta object that contains non-standard meta-information.
        /// </summary>
        /// <see cref="https://jsonapi.org/format/#document-meta"/>
        public object Meta { get; set; }

        public JsonSpecDto()
        {
            Data = new object();
            Error = new List<JsonSpecErrorDto>();
            Meta = new object();
        }
    }

    /// <summary>
    /// Error objects provide additional information about problems encountered while performing an operation. 
    /// Error objects MUST be returned as an array keyed by errors in the top level of a JSON:API document.
    /// </summary>
    /// <see cref="https://jsonapi.org/format/#error-objects"/>
    public class JsonSpecErrorDto
    {
        /// <summary>
        /// a short, human-readable summary of the problem that SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// a human-readable explanation specific to this occurrence of the problem. Like title, this field’s value can be localized.
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// a meta object containing non-standard meta-information about the error
        /// </summary>
        public object Meta { get; set; }

        public JsonSpecErrorDto()
        {
            Title = "";
            Detail = "";
            Meta = new object();
        }
    }
}
