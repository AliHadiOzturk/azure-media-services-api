using Microsoft.Azure.Management.Media.Models;

namespace VideoAPI.app.models
{
    public enum StreamProtocol
    {
        EMPTY = 0,
        HLS = 1,
        DASH = 2,
        SmoothStreaming = 3,

    }
}