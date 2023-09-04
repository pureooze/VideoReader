using Integrations.Domain;

namespace Integrations; 

public interface IIntegration {
    Task<IEnumerable<Manifests>> GetManifests(
        string videoId
    );
}