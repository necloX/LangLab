using UnityEditor;
using UnityEngine;

namespace LangLab.Sample
{
    public class StatementTypeCheking : SimpleTypeChecking
    {
        [Match]
        public LLAstNode declOrAssignNode;

        public override SampleType EvaluateType(Environment<SampleType> environment)
        {
            return declOrAssignNode.GetComponent<SimpleTypeChecking>().EvaluateType(environment);
        }
    }
}