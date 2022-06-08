using UnityEditor;
using UnityEngine;

namespace LangLab.Sample
{
    public class IdentifierTypeChecking : SimpleTypeChecking
    {
        public override SampleType EvaluateType(Environment<SampleType> environment)
        {
            if(environment.Contains(node.text)) return environment.Get(node.text);
            else
            {
                Debug.Log("Use of undeclared variable named " + node.text);
                return SampleType.Undefined;
            }
        }
    }
}