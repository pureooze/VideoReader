using Amazon.Lambda.Annotations;
using Microsoft.Extensions.DependencyInjection;
using VideoReader.Domain;
using VideoReader.Domain.Implementation;

namespace VideoReader; 

[LambdaStartup]
internal class Startup {
    public void ConfigureServices(
        IServiceCollection services
    ) {
        services.AddScoped<IVideoPluginProvider, VideoPluginProvider>();
    }
}