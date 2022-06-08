using UnityEditor;
using UnityEngine;

namespace LangLab.Sample
{
    public class ExpressionTypeChecking : SimpleTypeChecking
    {
        public override SampleType EvaluateType(Environment<SampleType> environment)
            => node.GetChild(0).GetComponent<SimpleTypeChecking>().EvaluateType(environment);
    }
}