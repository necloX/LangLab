using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Irony.Parsing;
using Irony.Ast;
namespace LangLab
{
    [CreateAssetMenu()]
    public class IronyParserAsset : CompilationNodeAsset
    {
        public IronyGrammarAsset grammarAsset; //-------------------Input
        public SourceTextAsset sourceTextAsset;//--------------Input
        public ParseTree parseTree;
        Parser parser;
        public override void GoThrough()
        {
            var language = new LanguageData(grammarAsset.grammar);
            parser = new Parser(language);
            if (parser == null) throw new System.Exception("Failed to produce parser");
            parseTree = parser.Parse(sourceTextAsset.GetSource());
            if(parseTree.HasErrors())
            {
                parseTree.ParserMessages.ForEach(message => Debug.Log(message.Message+" at "+message.Location.ToString()));
            }
            else
            {
                //Debug.Log(((LLIronyNonTerminal)parseTree.Root.Term).grammarNodeAsset);
                //Util.FormatParseTree( parseTree);
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

