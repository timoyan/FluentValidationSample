using ValidationSample.Attribute;

namespace ValidationSample.Dto
{
    public class GetListRequestDto
    {
        public Criteria criteria { get; set; }

        public GetListRequestDto()
        {
            criteria = new Criteria();
        }
    }

    public class Criteria
    {
        public string keyword { get; set; }
        [ErrorProperty(propertyName: "dateRange")]
        public string[] dateRange { get; set; }
        public string[] status { get; set; }

        public Criteria()
        {
            keyword = "";
            dateRange = new string[] { };
            status = new string[] { };
        }
    }

    
}
