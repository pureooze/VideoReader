using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VideoReader.Domain.Implementation;

namespace VideoReader; 

public interface IService {
    Task<ServiceResult<ResponseEntry>> GetVideoSourcesForUriAsync(
        string uri
    );
}