using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Builder
{

    public class SqlUpdate
    {
        public string TableName { get; set; }
        public List<(string, string)> ValuesToBeSet = new List<(string, string)>();
        public List<(string, string)> Conditions = new List<(string, string)>();
        private StringBuilder stringBuilder = new StringBuilder();

        private string ToStringImp()
        {
            stringBuilder.AppendLine($"UPDATE {TableName}");

            if (ValuesToBeSet.Count > 0)
            {
                // TODO: add numeric value
                var ValuesToBeSetStrig = ValuesToBeSet
                    .Select(x => $"{x.Item1} = '{x.Item2}'")
                    .Aggregate((i, j) => $"{i}, {j}");
                stringBuilder.AppendLine($"SET " + ValuesToBeSetStrig);
            }

            if (Conditions.Count > 0)
            {
                var ConditionsString = Conditions
                    .Select(x => $"{x.Item1} = '{x.Item2}'")
                    .Aggregate((i, j) => $"{i} AND {j}");
                stringBuilder.AppendLine($"WHERE {ConditionsString}");
            }
            return stringBuilder.ToString();
        }
        public override string ToString()
        {
            return ToStringImp();
        }
    }

    public class SqlUpdateBuilder
    {
        private readonly SqlUpdate _sqlUpdate = new SqlUpdate();
        public SqlUpdateBuilder(string tableName)
        {
            _sqlUpdate.TableName = tableName;
        }

        // fluent interfce builder
        public SqlUpdateBuilder AddValueToBeSet(string columName, string newValue)
        {
            _sqlUpdate.ValuesToBeSet.Add((columName, newValue));
            return this;
        }

        public SqlUpdateBuilder AddCondition(string columnName, string conditionValue)
        {
            _sqlUpdate.Conditions.Add((columnName, conditionValue));
            return this;
        }

        public override string ToString()
        {
            return _sqlUpdate.ToString();
        }
    }
}
