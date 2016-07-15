using System;

namespace Kopigi.Portable.Object
{
    public class Switch
    {
        public Switch(System.Object o)
        {
            Object = o;
        }

        public System.Object Object { get; private set; }
    }


    /// <summary>
    /// Extension pour gérer le switch sur Type d'objet
    /// </summary>
    public static class SwitchExtensions
    {
        public static Switch Case<T>(this Switch s, Action<T> a)
              where T : class
        {
            return Case(s, o => true, a, false);
        }

        public static Switch Case<T>(this Switch s, Action<T> a,
             bool fallThrough) where T : class
        {
            return Case(s, o => true, a, fallThrough);
        }

        public static Switch Case<T>(this Switch s,
            Func<T, bool> c, Action<T> a) where T : class
        {
            return Case(s, c, a, false);
        }

        public static Switch Case<T>(this Switch s,
            Func<T, bool> c, Action<T> a, bool fallThrough) where T : class
        {
            if (s == null)
            {
                return null;
            }

            T t = s.Object as T;
            if (t != null)
            {
                if (c(t))
                {
                    a(t);
                    return fallThrough ? s : null;
                }
            }

            return s;
        }

        public static Switch CaseValueType<T>(this Switch s, Action<T> a) where T : struct
        {
            return CaseValueType(s, o => true, a, false);
        }

        public static Switch CaseValueType<T>(this Switch s,
            Func<T, bool> c, Action<T> a, bool fallThrough) where T : struct
        {
            if (s == null)
            {
                return null;
            }

            T t = (T)System.Convert.ChangeType(s.Object, typeof(T));
            if (c(t))
            {
                a(t);
                return fallThrough ? s : null;
            }

            return s;
        }
    }
}
