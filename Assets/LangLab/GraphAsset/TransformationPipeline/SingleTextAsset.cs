using System;
using System.Collections.Generic;
using UnityEngine;

namespace LangLab
{
    public class SingleTextAsset : SourceTextAsset
    {
        [CreateOutputPort]
        public string text;
    }
}
