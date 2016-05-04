namespace CarbonCore.JSharpBridge
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using CarbonCore.JSharpBridge.Collections;
    using CarbonCore.JSharpBridge.Core;

    public static class BridgeList
    {
        public static int Count(this IList list)
        {
            return list.Count;
        }

        public static object Get(this IList list, int index)
        {
            return list[index];
        }

        public static object Get(this IDictionary<object, object> dictionary, object key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return null;
        }

        public static bool IsEmpty(this IList list)
        {
            return list.Count <= 0;
        }

        public static bool IsEmpty(this ISet<object> set)
        {
            return set.Count <= 0;
        }

        public static bool IsEmpty(this Dictionary<object, object> dictionary)
        {
            return dictionary.Count <= 0;
        }

        public static int Size(this IList list)
        {
            return list.Count;
        }

        public static int Size(this ISet<object> set)
        {
            return set.Count;
        }

        public static void Set(this IList list, int index, object entry)
        {
            if (list.Count <= index)
            {
                list.Add(entry);
            }
            else
            {
                list.Insert(index, entry);
            }
        }

        public static void Put(this Dictionary<object, object> dictionary, object key, object value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static void AddAll(this Iterable list, IList other)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static Iterator Iterator(this IList list)
        {
            return new ListIterator(list, list.GetEnumerator());
        }

        public static Iterator Iterator(this ISet<object> set)
        {
            return new SetIterator<object>(set, set.GetEnumerator());
        }

        public static Iterator Iterator(this ICollection<object> set)
        {
            return new Iterator(set.GetEnumerator());
        }

        public static Iterator Iterator(this Dictionary<object, object>.KeyCollection collection)
        {
            return new Iterator(collection.GetEnumerator());
        }

        public static Iterator Iterator(this Dictionary<object, object>.ValueCollection collection)
        {
            return new Iterator(collection.GetEnumerator());
        }

        public static void Sort(JavaCollection javaCollection, JavaComparator comparer = null)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }
        
        public static void Fill<T>(T[] array, int count)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static JavaCollection AsList(object[] entries)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<JavaCollection>();
        }

        public static void Shuffle(JavaCollection other)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static JavaCollection EmptyList()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<JavaCollection>();
        }

        public static JavaCollection SynchronizedList(JavaCollection javaCollection)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<JavaCollection>();
        }

        public static JavaHashSet EmptySet()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<JavaHashSet>();
        }

        public static JavaHashSet NewHashSet()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<JavaHashSet>();
        }

        public static JavaTreeSet NewTreeSet(JavaCollection values)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<JavaTreeSet>();
        }

        public static object[] ToArray(object[] split, Type type)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<object[]>();
        }

        public static JavaHashSet NewLinkedHashSet()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<JavaHashSet>();
        }

        public static object Transform(JavaCollection par1List, Function transformFunction)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<object>();
        }

        public static void Reverse(JavaCollection var12)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static JavaCollection UnmodifiableList(JavaCollection listOfLanServers)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<JavaCollection>();
        }

        public static void Sort(object[] javaCollection)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static void AddAll<T>(JavaHashSet var1, IList<T> getAvailableDisplayModes)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static void Fill<T>(T[] par1ArrayOfBiomeGenBase, int count, int i, T biomeToUse)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static int GetHashCode<T>(T[] data)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<int>();
        }
    }
}
