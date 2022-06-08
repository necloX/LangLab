using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Irony.Parsing;


namespace LangLab
{
    [CreateAssetMenu()]
    public class IronyParserAsset : CompilationNodeAsset
    {
        [CreateInputPort]
        public Grammar grammar;
        [CreateInputPort]
        public string sourceText;
        ParseTree parseTree;
        [CreateOutputPort]
        public LLAst ast;
        Parser parser;
        public override void GoThrough()
        {
            base.GoThrough();
            var language = new LanguageData(grammar);
            parser = new Parser(language);
            if (parser == null) throw new System.Exception("Failed to produce parser");
            parseTree = parser.Parse(sourceText);
            if(parseTree.HasErrors())
            {
                parseTree.ParserMessages.ForEach(message => Debug.Log(message.Message+" at "+message.Location.ToString()));
            }
            else
            {
                Debug.Log(DispAst(parseTree.Root, 0));
            }
        }
        static string DispAst(ParseTreeNode node, int level)
        {
            string s = "";
            if (node.Term is LLIronyNonTerminal nonTerminal && !nonTerminal.grammarNodeAsset.hideInAst)
            {
                s = RegisterNode(s,level);
                level++;
                s += nonTerminal.grammarNodeAsset.ToString();
            }
            if (node.Term is IdentifierTerminal identifier) 
            {
                s = RegisterNode(s, level);
                level++;
                s += identifier.Name;
            }
            if (node.Term is LLIronyPseudoTerminal keyTerm && !keyTerm.grammarNodeAsset.hideInAst)
            {
                s = RegisterNode(s, level);
                level++;
                s += ((KeyTerm)keyTerm.terminal).Text;
            }
            if (node.Term is NumberLiteral number) 
            {
                s = RegisterNode(s, level);
                level++;
                s += node.Token.Value;
            }
            foreach (ParseTreeNode child in node.ChildNodes) s += DispAst(child, level);
            return s;
        }
        static string RegisterNode(string s,int level)
        {
            s = System.Environment.NewLine;
            for (int i = 0; i < level; i++) s += "      ";
            s += "|_";
            return s;
        }
    }
}

