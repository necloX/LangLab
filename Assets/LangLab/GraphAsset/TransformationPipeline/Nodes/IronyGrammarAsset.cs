using System;
using System.Collections.Generic;
using UnityEngine;
using Irony.Parsing;
using LangLab;
namespace LangLab
{
    [CreateAssetMenu()]
    public class IronyGrammarAsset : CompilationNodeAsset
    {
        public GrammarGraphAsset grammarGraphAsset;
        [CreateOutputPort]
        public Grammar grammar;
        public override void GoThrough()
        {
            base.GoThrough();
            grammar = new LLGrammar(grammarGraphAsset);
        }
        public class LLGrammar : Grammar
        {
            private Dictionary<GrammarNodeAsset, BnfExpression> dictionnary;
            public LLGrammar(GrammarGraphAsset grammarGraphAsset) : base(false)
            {
                if(dictionnary == null) dictionnary = new Dictionary<GrammarNodeAsset,BnfExpression>();
                dictionnary.Clear();
                NonTerminal entry = new LLIronyNonTerminal(grammarGraphAsset.entryNode);
                entry.Rule = UnwrapTerm(grammarGraphAsset.entryNode);

                Root = entry;
            }
            private BnfExpression UnwrapTerm(GrammarNodeAsset grammarNodeAsset)
            {
                if (dictionnary.ContainsKey(grammarNodeAsset))
                {
                    return dictionnary[grammarNodeAsset];
                }
                string name = grammarNodeAsset.name;
                if (grammarNodeAsset is EntryNodeAsset entryNode)
                {
                    dictionnary[grammarNodeAsset] = CreateOrRule(entryNode.children, name);
                    
                } 
                if (grammarNodeAsset is KeyTermTerminalNode keyTermAsset)
                {
                    //dictionnary[grammarNodeAsset] = new LLIronyPseudoTerminal(keyTermAsset);
                    dictionnary[grammarNodeAsset] = ToTerm(keyTermAsset.keyTermText);
                    //Debug.Log(ToTerm(keyTermAsset.keyTermText).Text + " and "+(new LLIronyKeyTerm(keyTermAsset)).Text);
                    if (keyTermAsset.isOperator) RegisterOperators(keyTermAsset.operatorLayer, keyTermAsset.keyTermText);
                }
                if (grammarNodeAsset is IdentifierTerminalNodeAsset)
                {
                    dictionnary[grammarNodeAsset] = new IdentifierTerminal(name);
                }
                if (grammarNodeAsset is LitteralTerminalNode litteralTerminalNode)
                {
                    dictionnary[grammarNodeAsset] = new NumberLiteral(name);
                }
                if (grammarNodeAsset is NonTerminalNode chainNode)
                {
                    NonTerminal chain = new LLIronyNonTerminal(chainNode);
                    if (chainNode.children == null) chain.Rule = Empty;
                    if (chainNode.children.Count == 0) chain.Rule = Empty;
                    if (chainNode.children.Count == 1)
                    {
                        chain.Rule = CreateOrRule(chainNode.children[0],name);
                    }
                    else
                    {
                        chain.Rule = CreateOrRule(chainNode.children[0],name+ " Index " + 0.ToString());
                        for (int i = 1; i < chainNode.children.Count; i++)
                        {
                            chain.Rule += CreateOrRule(chainNode.children[i],name+ " Index " + i.ToString());
                       
                        }
                    }
                    if (chainNode.zeroAllowed && chainNode.moreThanOneAllowed)
                        chain.Rule = MakeStarRule(chain, chain);
                    else if (chainNode.zeroAllowed && !chainNode.moreThanOneAllowed)
                        chain.Rule = chain.Q();
                    else if (!chainNode.zeroAllowed && chainNode.moreThanOneAllowed)
                        chain.Rule = MakePlusRule(chain, chain);
                    dictionnary[grammarNodeAsset] = chain;
                }
                return dictionnary[grammarNodeAsset];
                throw new System.Exception("This grammar node asset has not been implemented for use with Irony.");
            }
            private NonTerminal CreateOrRule(List<GrammarNodeAsset> nodeAssets, string name)
            {
                NonTerminal output = new NonTerminal(name);
                if (nodeAssets == null) throw new System.Exception("Empty or rule");
                if (nodeAssets.Count == 0) throw new System.Exception("Empty or rule");
                output.Rule = UnwrapTerm(nodeAssets[0]);
                for(int i = 1; i < nodeAssets.Count; i++)
                {
                    output.Rule |= UnwrapTerm(nodeAssets[i]);
                }
                return output;
            }
        }
    }
    public class LLIronyNonTerminal : NonTerminal
    {
        public GrammarNodeAsset grammarNodeAsset;
        public LLIronyNonTerminal(GrammarNodeAsset grammarNodeAsset) : base(grammarNodeAsset.name)
        {
            this.grammarNodeAsset = grammarNodeAsset;
        }
    }
    public class LLIronyPseudoTerminal : NonTerminal
    {
        public GrammarNodeAsset grammarNodeAsset;
        public Terminal terminal;
        public LLIronyPseudoTerminal(KeyTermTerminalNode grammarNodeAsset) : base(grammarNodeAsset.name)
        {
            this.grammarNodeAsset = grammarNodeAsset;
            terminal = new KeyTerm(grammarNodeAsset.keyTermText, grammarNodeAsset.name);
            this.Rule = terminal;
        }
    }
}
