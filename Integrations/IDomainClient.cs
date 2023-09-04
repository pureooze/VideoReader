using Integrations.Domain;

namespace Integrations; 

public interface IDomainClient {
    Task<IEnumerable<Manifests>> GetManifests(
        VideoId videoId
    );
}