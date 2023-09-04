using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using VideoReader.Domain;
using VideoReader.Domain.Implementation;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace VideoReader
{
    public class Function {
        [LambdaFunction]
        public async Task<APIGatewayProxyResponse> FunctionHandler(
            APIGatewayProxyRequest apigProxyEvent, 
            ILambdaContext context
        ) {
            apigProxyEvent.QueryStringParameters.TryGetValue( "uri", out string uri );

            if( uri == null ) {
                return new APIGatewayProxyResponse {
                    Body = JsonSerializer.Serialize( "" ),
                    StatusCode = 400,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }

            IService service = new Service();
            IEnumerable<ResponseEntry> results = await service.GetVideoSourcesForUriAsync( uri: uri );
            
            return new APIGatewayProxyResponse {
                Body = JsonSerializer.Serialize( results ),
                StatusCode = 200,
                Headers = new Dictionary<string, string> {
                    { "Content-Type", "application/json" }, 
                    { "Access-Control-Allow-Origin", "*" }
                }
            };
        }
    }
}
