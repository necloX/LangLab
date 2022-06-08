using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LangLab;
using Irony.Parsing;

namespace LangLab
{
    [CreateAssetMenu(menuName = "Grammar Node/Quantifier Node")]
    public class QuantifierNode : GrammarNodeAsset
    {
        [HideInInspector] public List<GrammarNodeAsset> parents;
        public List<GrammarNodeAsset> childrens;
        public bool zeroAllowed;
        public bool moreThanOneAllowed;
    }
}

