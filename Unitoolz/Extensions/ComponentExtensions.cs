using System.Linq;
using System.Collections.Generic;

using UnityEngine;

namespace Unitoolz.Extensions
{

    public static class ComponentExtensions 
    {
        /// <summary>
        /// Returns a single child object corresponding to the given name
        /// </summary>
        public static T GetNamedComponentInChildren<T>(this Component self, string name, bool includeInactive = false) where T : Component
        {
            return self.GetComponentsInChildren<T>(includeInactive).First(c => c.name == name);
        }

        /// <summary>
        /// Returns multiple child objects corresponding to the given name
        /// </summary>
        public static IEnumerable<T> GetNamedComponentsInChildren<T>(this Component self, string name, bool includeInactive = false) where T : Component
        {
            return self.GetComponentsInChildren<T>(includeInactive).Where(c => c.name == name);
        }
    }

}
