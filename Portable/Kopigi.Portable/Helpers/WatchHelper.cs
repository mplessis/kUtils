using Kopigi.Portable.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopigi.Portable.Helpers
{
    public static class WatchHelper
    {
        private static readonly List<InstanceWatch> Instances = new List<InstanceWatch>();

        /// <summary>
        /// Permet de vider les instances de surveillance
        /// </summary>
        public static void Clear()
        {
            Instances.Clear();
        }

        /// <summary>
        /// Permet de vider les instances avec le label demandé
        /// </summary>
        /// <param name="label"></param>
        public static void Clear(string label)
        {
            Instances.RemoveAll(i => i.Label == label);
        }

        /// <summary>
        /// Permet de demarrer une nouvelle surveillance de temps
        /// </summary>
        /// <param name="label">Label identifiant humainenement la surveillance</param>
        /// <returns>Clé de l'instance de surveillance</returns>
        public static Guid StartWatch(string label)
        {
            var guid = Guid.NewGuid();
            var watch = new Stopwatch();
            watch.Start();
            Instances.Add(new InstanceWatch(guid, watch, label));
            return guid;
        }

        /// <summary>
        /// Permet de retourner le temps d'execution
        /// </summary>
        /// <param name="key">Clé de l'instance de surveillance</param>
        /// <param name="logTime"></param>
        /// <returns>Instance de surveillance</returns>
        public static InstanceWatch StopWatch(Guid key, bool logTime = true)
        {
            var watchs = Instances.Where(i => i.Key == key);
            if (watchs.Any())
            {
                var watch = watchs.First();
                watch.Watch.Stop();
                watch.Elapsed = watch.Watch.Elapsed;
                if (logTime)
                {
                    Debug.WriteLine($"Temps d'execution de {watch.Label} : {watch.Elapsed.TotalSeconds} s");
                }
                return watch;
            }
            return null;
        }

        /// <summary>
        /// Permet de faire la somme des instances comportant le label demandé
        /// </summary>
        /// <param name="label"></param>
        /// <param name="logTime"></param>
        /// <returns></returns>
        public static double SumWatch(string label, bool logTime = true)
        {
            var watchs = Instances.Where(i => i.Label == label);
            if (watchs.Any())
            {
                var sum = watchs.Sum(i => i.Elapsed.TotalSeconds);
                if (logTime)
                {
                    Debug.WriteLine($"Temps d'execution total de {label} : {sum} s");
                }
                return sum;
            }
            throw new NoWatchFindException(label);
        }
    }

    public class InstanceWatch
    {
        public Guid Key { get; set; }
        public String Label { get; set; }
        public Stopwatch Watch { get; set; }
        public TimeSpan Elapsed { get; set; }

        public InstanceWatch(Guid key, Stopwatch watch, string label)
        {
            Key = key;
            Watch = watch;
            Label = label;
        }
    }
}
