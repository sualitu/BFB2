using UnityEngine;

namespace Assets.Utilities {
    using System.Linq;

    public static class Helper
    {
        public static GameObject FindInChildWithTag(this GameObject parent, string tag)
        {
            return (from Transform tr in parent.transform where tr.tag == tag select tr.gameObject).FirstOrDefault();
        }
    }
}