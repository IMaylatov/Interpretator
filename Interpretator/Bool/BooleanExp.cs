using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpretator.Bool
{
    public interface BooleanExp
    {
        bool Evaluate(Context c);

        BooleanExp Replace(String name, BooleanExp exp);

        BooleanExp Copy();
    }
}
