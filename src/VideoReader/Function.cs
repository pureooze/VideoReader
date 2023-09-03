using System.Collections.Generic;
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
            IVideoPluginProvider videoPluginProvider = new VideoPluginProvider();

            if( uri == null ) {
                return new APIGatewayProxyResponse {
                    Body = JsonSerializer.Serialize( "" ),
                    StatusCode = 400,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }
            
            IEnumerable<IVideoPlugin> plugins = videoPluginProvider.GetPlugins();
            foreach( IVideoPlugin plugin in plugins ) {
                if( plugin.CanHandleUri(sourceUri: uri) ) {
                    IEnumerable<ResponseEntry> results = await plugin.GetManifests(  sourceUri: uri );
                    
                    return new APIGatewayProxyResponse {
                        Body = JsonSerializer.Serialize( results ),
                        StatusCode = 200,
                        Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                    };
                }
            }
            
            return new APIGatewayProxyResponse {
                Body = JsonSerializer.Serialize( "" ),
                StatusCode = 500,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }
}
