using Pokedex.Core.Application.DTOS.Ids;

namespace Pokedex.Core.Application.Interfaces.Services
{
    public interface IIdsService
    {
        Task<UploadFileResponse> UploadFile(UploadFileRequest request);
        Task<DeleteFileResponse> DeleteFile(DeleteFileRequest request);
    }
}
