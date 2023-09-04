using System.Collections.Generic;

namespace VideoReader.Domain.Implementation;

public enum PluginOutputType {
    File,
    Playlist
}

public record ServiceResult<T> (
    PluginOutputType OutputType,
    IEnumerable<T> Result
);