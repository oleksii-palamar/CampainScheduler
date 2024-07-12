using CampainScheduler.Domain.Enums;
using CampainScheduler.Domain.Models;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace CampainScheduler.Utils.CsvParsers
{
    public class CustomerCsvParser : CsvParser<Customer>
    {
        protected override CsvMapping<Customer> Mapping =>
            new CustomerCsvMapping();
    }

    public class CustomerCsvMapping : CsvMapping<Customer>
    {
        public CustomerCsvMapping()
            : base()
        {
            MapProperty(0, x => x.Id);
            MapProperty(1, x => x.Age);
            MapProperty(2, x => x.Gender, new GenderTypeConverter());
            MapProperty(3, x => x.City);
            MapProperty(4, x => x.Deposit);
            MapProperty(5, x => x.IsNewCusomer, new BoolTypeConverter());
        }
    }

    public class GenderTypeConverter : ITypeConverter<CustomerGender>
    {
        public Type TargetType => typeof(CustomerGender);

        public bool TryConvert(string value, out CustomerGender result) 
            => CustomerGender.TryParse(value, out result);
    }

    public class BoolTypeConverter : ITypeConverter<bool>
    {
        public Type TargetType => typeof(bool);

        public bool TryConvert(string value, out bool result)
        {
            if(value == "0")
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return true;
        }
    }
}
