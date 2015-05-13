using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyPresentation.Optimization_Story
{

    public class Subset
    {
        public FirstAttempt Lookup;

        public ItemCompareResult[] Results;
        public List<Item> Items = new List<Item>();

        public void Preprocess()
        {
            Results = new ItemCompareResult[Items.Count];
        }

        public void UpdateResults(Item item2)
        {
            var apath2 = Lookup.APaths[item2.A];
        }

    }

    internal class FirstAttempt
    {
        public Dictionary<long, long[]> APaths;
        public Dictionary<long, long[]> BPaths;
        public Dictionary<long, long[]> CPaths;

        public void CompareSets(List<Item> set1, List<Item> set2)
        {
            var subsets = new Dictionary<long, Subset>();
            foreach (var item1 in set1)
            {
                var aroot = APaths[item1.A].First();
                Subset subset;
                if (!subsets.TryGetValue(aroot, out subset))
                    subsets.Add(aroot, subset = new Subset {Lookup = this});
                subset.Items.Add(item1);
            }

            foreach (var item1 in subsets.Values)
                item1.Preprocess();

            foreach (var item2 in set2)
            {
                Subset subset;
                if (subsets.TryGetValue(APaths[item2.A].First(), out subset))
                    subset.UpdateResults(item2);
            }

        }

    }

}
