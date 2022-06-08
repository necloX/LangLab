using System;
using UnityEngine;

namespace LangLab
{
    [CreateAssetMenu()]
    public class SingleTextAsset : SourceTextAsset
    {
        public string text;


        public override string GetSource()
        {
            return text;
        }

        public override void GoThrough()
        {
            throw new NotImplementedException();
        }
    }
}
