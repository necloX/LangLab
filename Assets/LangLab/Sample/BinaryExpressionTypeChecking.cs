using UnityEditor;
using UnityEngine;

namespace LangLab.Sample
{
    public class BinaryExpressionTypeChecking : SimpleTypeChecking
    {
        public LLAstNode leftOperand;
        public LLAstNode rightOperand;
        public override SampleType EvaluateType(Environment<SampleType> environment)
        {
            SampleType rightType = rightOperand.GetComponent<SimpleTypeChecking>().EvaluateType(environment);
            SampleType leftType = leftOperand.GetComponent<SimpleTypeChecking>().EvaluateType(environment);
            bool sameType = rightType == leftType;
            if (sameType)
            {
                return leftType;
            }
            else
            {
                if(rightType == SampleType.Undefined || leftType == SampleType.Undefined)
                    return SampleType.Undefined;
                else return SampleType.String;
            }
        }
    }
}