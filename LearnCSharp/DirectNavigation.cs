using LearnCSharp.Basic;

namespace LearnCSharp
{
    internal static class DirectNavigation
    {
        private readonly static Dictionary<string, Action> funcDic = new Dictionary<string, Action>()
        {
            ["10101"] = HelloWorld.SayHello,
            ["10201"] = LearnKeyWords.LearnAccessibilityLevelKeywords,
            ["10210"] = LearnKeyWords.LearnLiteralKeywords,
            ["10301"] = LearnLiteralsAndOthers.StartLearnLiteralsAndOthers,
            ["11117"] = LearnStatements.LearnUnsafeStatement,
            ["11118"] = LearnStatements.LearnFixedStatement,
            ["11001"] = LearnOperatorAndExpression.LearnArithmeticOperator,
            ["11002"] = LearnOperatorAndExpression.LearnBooleanLogicalOperator,
            ["11003"] = LearnOperatorAndExpression.LearnBitwiseAndShiftOperator,
            ["11004"] = LearnOperatorAndExpression.LearnEqualityAndComparisonOperator,
            ["11005"] = LearnOperatorAndExpression.LearnMemberAccessOperator,
            ["11006"] = LearnOperatorAndExpression.LearnNullOperator,
            ["11007"] = LearnOperatorAndExpression.LearnCollectionExpressionOperator,
            ["11008"] = LearnOperatorAndExpression.LearnTypeTestingAndCastOperator,
            ["11009"] = LearnOperatorAndExpression.LearnAssignmentOperator,
            ["11010"] = LearnOperatorAndExpression.LearnConditionalOperator,
            ["11011"] = LearnOperatorAndExpression.LearnAnonymousFuncAndLambdaOperator,
            ["11012"] = LearnOperatorAndExpression.LearnPointerOperator,
            ["11013"] = LearnOperatorAndExpression.LearnOtherPrimaryOperator,
            ["11014"] = LearnOperatorAndExpression.LearnOtherUnaryOperator,
            ["11015"] = LearnOperatorAndExpression.LearnSwitchAndWithOperator,
            ["11016"] = LearnOperatorAndExpression.LearnOperatorOverload
        };

        public static void DirectNavigate(string code)
        {
            if (!string.IsNullOrWhiteSpace(code) && funcDic.TryGetValue(code, out Action? action)) 
                action!.Invoke();
        }
    }
}
