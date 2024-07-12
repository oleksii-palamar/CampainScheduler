using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace CampainScheduler.Utils.CsvParsers
{
    public abstract class CsvParser<T> : Interfaces.ICsvParser<T>
        where T : class, new()
    {
        private readonly CsvParserOptions _options;
        private readonly TinyCsvParser.CsvParser<T> _parser;

        protected abstract CsvMapping<T> Mapping { get; }

        public CsvParser()
        {
            _options = new CsvParserOptions(true, ',');
            _parser = new TinyCsvParser.CsvParser<T>(_options, Mapping);
        }

        public List<T> Parse(string fileName) =>
           _parser
                .ReadFromFile(@$"{fileName}", Encoding.Default)
                .Select(x => x.Result)
                .ToList();

    }
}
