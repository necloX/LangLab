using UnityEngine;
using System;
namespace LangLab
{
    /// <summary>
    /// The base class for implementing behaviors on abstract syntax tree nodes.
    /// </summary>
    [Serializable]
    public abstract class LLBehaviour
    {
        public LLAstNode node;
        public virtual void Execute() { }
    }
}
