using System.Collections.Generic;

namespace VideoReader.Domain.Implementation;

public enum PluginType {
    YouTube,
    Twitch
}

public record ServiceResult<T> (
    PluginType Type,
    IEnumerable<T> Result
);