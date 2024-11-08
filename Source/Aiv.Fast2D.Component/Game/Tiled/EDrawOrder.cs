using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Component.Tiled
{
    enum EDrawOrder
    {
        UnknownOrder,
        TopDown,
        IndexOrder
    }

    static class DrawOrderMethods
    {
        private static Dictionary<string, EDrawOrder> drawDict = new Dictionary<string, EDrawOrder> {
                {"unknown", EDrawOrder.UnknownOrder},
                {"topdown", EDrawOrder.TopDown},
                {"index", EDrawOrder.IndexOrder}
            };

        public static bool Decode( ref EDrawOrder _eDrawOrder, string _sValue )
        {
            if (_sValue != null)
            {
                return drawDict.TryGetValue(_sValue, out _eDrawOrder);
            }
            return false;
        }
    }
}
