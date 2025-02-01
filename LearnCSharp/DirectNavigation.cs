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
            ["11217"] = LearnStatements.LearnUnsafeStatement,
            ["11218"] = LearnStatements.LearnFixedStatement
        };

        public static void DirectNavigate(string code)
        {
            if (!string.IsNullOrWhiteSpace(code) && funcDic.TryGetValue(code, out Action? action)) 
                action!.Invoke();
        }
    }
}
