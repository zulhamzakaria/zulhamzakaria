using System.Text.Json.Serialization;

namespace BlazorAPIClient.DTO
{
    public partial class GraphQLData
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonPropertyName("launches")]
        public LaunchDTO[] Launches { get; set; }
    }
}