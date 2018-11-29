using ValidationSample.Attribute;

namespace ValidationSample.Dto
{
    public class CreatePeoplePayloadDto
    {
        public string Name { get; set; }

        // [ErrorProperty(propertyName: "JoinTime")]
        public string JoinDate { get; set; }
        // [ErrorProperty(propertyName: "JoinTime")]
        public string JoinHour { get; set; }
        // [ErrorProperty(propertyName: "JoinTime")]
        public string JoinMin { get; set; }

        public CreatePeoplePayloadDto()
        {
            Name = "";
            JoinDate = "";
            JoinHour = "";
            JoinMin = "";
        }
    }
}
