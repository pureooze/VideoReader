using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace VideoReader; 

public interface IService {
    Task<IEnumerable<ResponseEntry>> GetVideoSourcesForUriAsync(
        string uri
    );
}