namespace ZadanieRekrutacyjneWebApi.DownloadFileService
{
    public interface IDownloadFileService
    {
        public Task DownloadAndSaveFile(string url, string filename);
    }
}
