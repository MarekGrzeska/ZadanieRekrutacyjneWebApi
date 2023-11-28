namespace ZadanieRekrutacyjneWebApi.DownloadFileService
{
    public class DownloadFileService : IDownloadFileService
    { 
        public async Task DownloadAndSaveFile(string url, string filename)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var content = response.Content)
                        {
                            var filesByte = await content.ReadAsByteArrayAsync();
                            await File.WriteAllBytesAsync(filename, filesByte);
                        }
                    }
                }
            }
        }
    }
}
