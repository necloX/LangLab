using UnityEditor;
using UnityEngine;

namespace LangLab.Sample
{
    public class StatementTypeCheking : SimpleTypeChecking
    {
        public LLAstNode declOrAssignNode;

        public override SampleType EvaluateType(Environment<SampleType> environment)
        {
            return declOrAssignNode.GetComponent<SimpleTypeChecking>().EvaluateType(environment);
        }
    }
}