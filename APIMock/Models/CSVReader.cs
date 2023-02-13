namespace APIMock.Models
{
    public abstract class CSVReader<T>
    {
        protected string _csvName { get; set; }
        public IEnumerable<T> GetValues()
        {
            return File.ReadAllLines($"Data\\{_csvName}.CSV")
                                               .Skip(1)
                                               .Select(v => FromCsv(v))
                                               .ToList();
        }

        protected abstract T FromCsv(string csvLine);
    }
}
