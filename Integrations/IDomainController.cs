using Integrations.Domain;

namespace Integrations; 

public interface IDomainController {
    bool GetPlayerResponseAsync(
        VideoId url
    );
}