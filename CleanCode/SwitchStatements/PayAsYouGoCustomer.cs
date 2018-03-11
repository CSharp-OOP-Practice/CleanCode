using System;

namespace CleanCode.SwitchStatements
{
    public class PayAsYouGoCustomer : Customer
    {
        public override MonthlyStatement GenerateStatement(MonthlyUsage usage)
        {
            var statement = new MonthlyStatement();

            statement.CallCost = 0.12f * usage.CallMinutes;
            statement.SmsCost = 0.12f * usage.SmsCount;
            statement.TotalCost = statement.CallCost + statement.SmsCost;

            return statement;
        }
    }
}
