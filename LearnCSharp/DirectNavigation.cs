using LearnCSharp.Basic;
using LearnCSharp.Professional;

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
            ["10401"] = LearnDataType.LearnObject,
            ["10402"] = LearnDataType.LearnDynamic,
            ["10501"] = LearnSimpleType.LearnIntegerType,
            ["10502"] = LearnSimpleType.LearnFloatingPointType,
            ["10503"] = LearnSimpleType.LearnCharacterType,
            ["10504"] = LearnSimpleType.LearnBooleanType,
            ["10505"] = LearnSimpleType.LearnStringType,
            ["10506"] = LearnSimpleType.LearnObjectType,
            ["10507"] = LearnSimpleType.LearnDynamicType,
            ["10601"] = LearnTuple.StartLearnTuple,
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
            ["11016"] = LearnOperatorAndExpression.LearnOperatorOverload,
            ["12601"] = LearnPattern.LearnConstantPattern,
            ["12602"] = LearnPattern.LearnRelationalPattern,
            ["12603"] = LearnPattern.LearnLogicalPattern,
            ["12604"] = LearnPattern.LearnDeclarationPattern,
            ["12605"] = LearnPattern.LearnTypePattern,
            ["12606"] = LearnPattern.LearnPropertyPattern,
            ["12607"] = LearnPattern.LearnPositionalPattern,
            ["12608"] = LearnPattern.LearnVarPattern,
            ["12609"] = LearnPattern.LearnDiscardPattern,
            ["12610"] = LearnPattern.LearnParenthesizedPattern,
            ["12611"] = LearnPattern.LearnListPattern,
            ["12612"] = LearnPattern.LearnSlicePattern,
            ["20101"] = LearnDelegate.LearnSystemDelegate,
            ["20102"] = LearnDelegate.LearnCustomerDelegate,
            ["20103"] = LearnDelegate.LearnMultiDelegate,
            ["20301"] = LearnEvent.StartLearnEvent
        };

        public static void DirectNavigate(string code)
        {
            if (!string.IsNullOrWhiteSpace(code) && funcDic.TryGetValue(code, out Action? action)) 
                action!.Invoke();
        }
    }
}
