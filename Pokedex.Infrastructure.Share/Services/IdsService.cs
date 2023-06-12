using Microsoft.Extensions.Options;
using Pokedex.Core.Application.DTOS.Ids;
using Pokedex.Core.Application.Interfaces.Services;
using System.Text.Json;

namespace Pokedex.Infrastructure.Share.Services
{
    public class IdsService : IIdsService
    {
        private readonly string _servicio;

        public IdsService(string servicios)
        {
            _servicio = servicios;
        }

        public async Task<UploadFileResponse> UploadFile(UploadFileRequest request)
        {
            UploadFileResponse response = new UploadFileResponse();
            response.Info = new InfoIDSResponse();
            response.Info.HasError = false;

            try
            {
                JsonSerializerOptions opt = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                string url = _servicio + "Document/uploadFile";

                if (string.IsNullOrWhiteSpace(url))
                {
                    response.Info.HasError = true;
                    response.Info.Message = "La url de servicio IDS no sea especificado, favor contactar con servicio tecnico.";
                    return response;
                }

                using (var htpClient = new HttpClient())
                {
                    var formData = new MultipartFormDataContent();
                    formData.Add(new StreamContent(request.file.OpenReadStream()), "file", request.file.FileName);
                    formData.Add(new StringContent(request.Id), "Id");
                    formData.Add(new StringContent(request.editMode.ToString()), "editMode");
                    formData.Add(new StringContent(request.path), "path");

                    var result = await htpClient.PostAsync(url, formData);

                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        response = JsonSerializer.Deserialize<UploadFileResponse>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    }
                    else
                    {
                        response.Info.HasError = true;
                        response.Info.Message = "El servicio de correo no se encuentra disponible, favor contactar con servicio tecnico.";
                        response.Info.Technicalfailure = await result.Content.ReadAsStringAsync();
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Info.HasError = true;
                response.Info.Message = "Ha ocurrido una falla tecnica.";
                response.Info.Technicalfailure = ex.Message;
                return response;
            }
        }

        public async Task<DeleteFileResponse> DeleteFile(DeleteFileRequest request)
        {
            DeleteFileResponse response = new DeleteFileResponse();
            response.Info = new InfoIDSResponse();
            response.Info.HasError = false;

            try
            {
                JsonSerializerOptions opt = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                string url = _servicio + "Document/deleteFile";

                if (string.IsNullOrWhiteSpace(url))
                {
                    response.Info.HasError = true;
                    response.Info.Message = "La url de servicio IDS no sea especificado, favor contactar con servicio tecnico.";
                    return response;
                }

                using (var htpClient = new HttpClient())
                {
                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(request.Id), "Id");
                    formData.Add(new StringContent(request.Owner.ToString()), "Owner");
                    formData.Add(new StringContent(request.Route), "Route");

                    var result = await htpClient.PostAsync(url, formData);

                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        response = JsonSerializer.Deserialize<DeleteFileResponse>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    }
                    else
                    {
                        response.Info.HasError = true;
                        response.Info.Message = "El servicio de correo no se encuentra disponible, favor contactar con servicio tecnico.";
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Info.HasError = true;
                response.Info.Message = "Ha ocurrido una falla tecnica.";
                response.Info.Technicalfailure = ex.Message;
                return response;
            }
        }
    }
}