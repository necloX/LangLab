using Irony.Parsing;
using UnityEngine;


namespace LangLab
{
    [CreateAssetMenu(menuName = "Grammar Node/Keyterm")]
    public class KeyTermTerminalNode : TerminalNode
    {
        public string keyTermText;
        public bool isOperator;
        public int operatorLayer;
        public bool isPunctuation;
    }
}
