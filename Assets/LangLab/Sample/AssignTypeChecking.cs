using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LangLab.Sample
{
    public class AssignTypeChecking :SimpleTypeChecking
    {
        KeyTermTerminalNode integerKeyTerm;
        KeyTermTerminalNode stringKeyTerm;
        [Match]
        public LLAstNode typeKeyTerm;
        [Match]
        public LLAstNode varIdentifier;
        [Match]
        public LLAstNode expression;
        public override SampleType EvaluateType(Environment<SampleType> environment)
        {
            expression.GetComponent<ExpressionTypeChecking>();
            SampleType expressionType = expression.GetComponent<ExpressionTypeChecking>().EvaluateType(environment);
            bool sameType = typeKeyTerm.grammarNodeAsset == integerKeyTerm && expressionType == SampleType.Integer
                || typeKeyTerm.grammarNodeAsset == stringKeyTerm && expressionType == SampleType.String;
            if (sameType)
            {
                environment.Add(varIdentifier.text, expressionType);
                return expressionType;
            }
            else if (expressionType == SampleType.Undefined)
            {
                return expressionType;
            }
            environment.Add(varIdentifier.text, SampleType.String);
            return SampleType.String;
        }
    }
}