using UnityEditor;
using UnityEngine;

namespace LangLab.Sample
{
    public abstract class SimpleTypeChecking : LLBehaviour
    {
        public abstract SampleType EvaluateType(Environment<SampleType> environment);
    }
}