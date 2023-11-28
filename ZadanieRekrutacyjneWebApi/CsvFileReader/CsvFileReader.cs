using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace ZadanieRekrutacyjneWebApi.CsvFileReader
{
    public class CsvFileReader : ICsvFileReader
    {
        private List<string> badDataRows = new List<string>();

        public List<string> GetBadDataRowExceptionList()
        {
            return badDataRows;
        }

        public List<T> ReadCsvFileToList<T>(string filename, string delimiter, bool hasHeader = true)
        {
            using (var reader = new StreamReader(filename)) 
            {
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = delimiter,
                    HasHeaderRecord = hasHeader,
                }))
                {
                    var records = new List<T>();
                    while (csv.Read())
                    {
                        try
                        {
                            var record = csv.GetRecord<T>();
                            records.Add(record);
                        }
                        catch (Exception e)
                        { 
                            badDataRows.Add($"In file: {filename} {e.Message}");
                        }
                    }
                    return records;
                }
            }
        }
    }
}