using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kopigi.Utils.Class;

namespace Kopigi.Utils.Helpers
{
    public static class Watch
    {
        private static readonly List<InstanceWatch> _instances = new List<InstanceWatch>();

        /// <summary>
        /// Permet de vider les instances de surveillance
        /// </summary>
        public static void Clear()
        {
            _instances.Clear();
        }

        /// <summary>
        /// Permet de vider les instances avec le label demandé
        /// </summary>
        /// <param name="label"></param>
        public static void Clear(string label)
        {
            _instances.RemoveAll(i => i.Label == label);
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
            _instances.Add(new InstanceWatch(guid, watch, label));
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
            var instancesFound = _instances.Where(i => i.Key == key);
            if (instancesFound.Any())
            {
                var watch = instancesFound.First();
                watch.Watch.Stop();
                watch.Elapsed = watch.Watch.Elapsed;
                if (logTime)
                {
                    Debug.WriteLine($"Execution time of {watch.Label} : {watch.Elapsed.TotalSeconds} s");
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
        public static double SumWatch(string label, TimeType timeType, bool logTime = true)
        {
            var instancesFound = _instances.Where(i => i.Label == label);
            if (instancesFound.Any())
            {
                var sum = ConvertTimeSumToTimeType(instancesFound.Sum(i => i.Elapsed.TotalMilliseconds), timeType);
                if (logTime)
                {
                    Debug.WriteLine($"Execution time of {label} : {sum} in {StringEnum.GetStringValue(timeType)}");
                }
                return sum;
            }
            throw new NoWatchFindException(label);
        }

        private static double ConvertTimeSumToTimeType(double timeSum, TimeType timeType)
        {
            switch (timeType)
            {
                case TimeType.Minutes:
                    return TimeSpan.FromMilliseconds(timeSum).TotalMinutes;
                case TimeType.Seconds:
                    return TimeSpan.FromMilliseconds(timeSum).TotalSeconds;
                case TimeType.Milliseconds:
                    return timeSum;
                default:
                    return timeSum;
            }
        }
    }
}
