using System.Collections;
using UnityEngine;

namespace LangLab.Sample
{
    public class EntryTypeChecking : LLBehaviour
    {
        LLAstNode statementTypeCheking;
        public override void Execute()
        {
            statementTypeCheking.GetComponent<SimpleTypeChecking>().EvaluateType(new Environment<SampleType>());
        }
    }
}