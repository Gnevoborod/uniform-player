using uniform_player.Domain.Interfaces.General;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.General
{
    public class ConditionChecker
    {
        public static bool TestValue(Predicate predicate, string componentValue, string conditionValue)
        {
            switch (predicate)
            {
                case Predicate.Equal:
                    {
                        if (componentValue == conditionValue)
                            return true;
                        return false;
                    }
                case Predicate.NotEqual:
                    {
                        if (componentValue != conditionValue)
                            return true;
                        return false;
                    }
                case Predicate.MoreThan:
                    {
                        int componentToCompare, conditionToCompare;
                        if (ConvertToNumber(out componentToCompare, out conditionToCompare, componentValue, conditionValue))
                        {
                            if(componentToCompare > conditionToCompare)
                                return true;
                        }
                        return false;
                    }
                case Predicate.LessThan:
                    {
                        int componentToCompare, conditionToCompare;
                        if (ConvertToNumber(out componentToCompare, out conditionToCompare, componentValue, conditionValue))
                        {
                            if (componentToCompare < conditionToCompare)
                                return true;
                        }
                        return false;
                    }
                case Predicate.Clicked:
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        private static bool ConvertToNumber(out int componentValueNumber, out int conditionValueNumber, string componentValue, string conditionValue)
        {
            if (Int32.TryParse(conditionValue, out conditionValueNumber))
            {
                if (Int32.TryParse(conditionValue, out componentValueNumber))
                {
                    return true;
                }
            }
            componentValueNumber = 0;
            return false;

        }
    }
}