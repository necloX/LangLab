using System.Collections.Generic;

namespace LangLab
{
    /// <summary>
    /// A data structure that can hold the textual bindings in a context and can be used for scoping.
    /// </summary>
    public class Environment<Type>
    {
        Dictionary<string, Type> bindings;
        Environment<Type> enclosing;
        public Environment()
        {
            bindings = new Dictionary<string, Type>();
            enclosing = null;
        }
        public Environment(Environment<Type> enclosing)
        {
            bindings = new Dictionary<string, Type>();
            this.enclosing = enclosing;
        }
        public void Add(string variableName, Type value)
        {
            bindings[variableName] = value;
        }
        public void Change(string variableName, Type value)
        {
            if (bindings.ContainsKey(variableName))
            {
                bindings[variableName] = value;
            }
            else
            {
                if (enclosing != null)
                {
                    enclosing.Change(variableName, value);
                }
                else
                {
                    throw new System.Exception(variableName + " does not exists in this context");
                }
            }
        }
        public bool Contains(string variableName)
        {
            if (bindings.ContainsKey(variableName))
            {
                return true;
            }
            else
            {
                if (enclosing != null)
                {
                    return enclosing.Contains(variableName);
                }
                else
                {
                    return false;
                }
            }
        }
        public Type Get(string variableName)
        {
            if (bindings.ContainsKey(variableName))
            {
                return bindings[variableName];
            }
            else
            {
                if (enclosing != null)
                {
                    return enclosing.Get(variableName);
                }
                else
                {
                    throw new System.Exception(variableName + " does not exists in this context");
                }
            }
        }
    }
}


