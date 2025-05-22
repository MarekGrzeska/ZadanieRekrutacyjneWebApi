namespace ZadanieRekrutacyjneWebApi.CsvFileReader
{
    public interface ICsvFileReader
    {
        List<T> ReadCsvFileToList<T>(string filename, string delimiter, bool hasHeader = true);
        List<string> GetBadDataRowExceptionList();
    }
}
