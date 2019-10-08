using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    public class ObjectMapper<T1, T2>
    {
        private readonly Dictionary<T1, T2> pairs = new Dictionary<T1, T2>();
        private readonly Dictionary<T2, T1> pairsInverted = new Dictionary<T2, T1>();

        public void AddPair(T1 item1, T2 item2)
        {
            if (!pairs.ContainsKey(item1))
                pairs.Add(item1, item2);

            if (!pairsInverted.ContainsKey(item2))
                pairsInverted.Add(item2, item1);
        }

        public T1 GetItem1ByItem2(T2 item2)
        {
            return pairsInverted[item2];
        }

        public T2 GetItem2ByItem1(T1 item1)
        {
            return pairs[item1];
        }
    }
}
