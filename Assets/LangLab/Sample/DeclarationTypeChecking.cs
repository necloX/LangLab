using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LangLab.Sample
{
    public class DeclarationTypeChecking : SimpleTypeChecking
    {
        public KeyTermTerminalNode integerKeyTerm;
        public KeyTermTerminalNode stringKeyTerm;
        [Match]
        public LLAstNode varIdentifier;
        [Match]
        public LLAstNode typeKeyTerm;
        public override SampleType EvaluateType(Environment<SampleType> environment)
        {
            SampleType type;
            if (typeKeyTerm.grammarNodeAsset == integerKeyTerm)
                type = SampleType.Integer;
            else
                type = SampleType.String;
            environment.Add(varIdentifier.text, type);
            return type;
        }
    }
}