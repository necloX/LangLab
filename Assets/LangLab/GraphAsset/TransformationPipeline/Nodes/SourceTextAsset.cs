using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangLab
{
    public abstract class SourceTextAsset : CompilationNodeAsset
    {
        public abstract string GetSource();
    }
}
