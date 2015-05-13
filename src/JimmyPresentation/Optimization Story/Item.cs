using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyPresentation.Optimization_Story
{
    public class Item
    {
        public long A;
        public long B;
        public long C;
    }

    public enum ItemCompareResult
    {
        NotMatched,

        TooHighAWrongBC,
        CorrectAWrongBC,

        TooHighABC,
        TooHighACorrectBC,
        CorrectATooHighBC,

        Matched
    }

}
