using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kopigi.Utils.Class;
using Kopigi.Utils.Helpers;
using NFluent;
using NSubstitute.Routing.Handlers;
using NUnit.Framework;

namespace Kopigi.Utils.Tests.Helpers
{
    public class WatchShould
    {
        [SetUp]
        public void Setup()
        {
            Watch.Clear();
        }

        [Test]
        public void Return_not_null_on_new_start_watch()
        {
            Check.That(Watch.StartWatch("watch_test")).IsNotEqualTo(null);
        }

        [Test]
        public void Return_guid_on_new_start_watch()
        {
            Check.That(Watch.StartWatch("watch_test")).IsInstanceOf<Guid>();
        }

        [Test]
        public void Return_one_instancewatch_with_time_elapsed_on_stop_watch()
        {
            var guidInstance = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instance = Watch.StopWatch(guidInstance);
            Check.That(instance).IsNotEqualTo(null);
            Check.That(instance.Elapsed.Milliseconds >= 100).IsTrue();
        }

        [Test]
        public void Return_null_if_no_watch_found_on_stop_watch()
        {
            var guidInstance = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instance = Watch.StopWatch(Guid.NewGuid());
            Check.That(instance).IsEqualTo(null);
        }

        [Test]
        public void Return_null_if_instances_watch_are_clear_before_stop_watch()
        {
            var guidInstance = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.Clear("watch_test");
            var instance = Watch.StopWatch(guidInstance);
            Check.That(instance).IsEqualTo(null);
        }

        [Test]
        public void Return_time_elapsed_greater_than_200_milliseconds_for_2_watch_with_same_label()
        {
            var guidInstanceOne = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instanceOne = Watch.StopWatch(guidInstanceOne);

            var guidInstanceTwo = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instanceTwo = Watch.StopWatch(guidInstanceTwo);

            Check.That(Watch.SumWatch("watch_test", TimeType.Milliseconds) >= 200).IsTrue();
        }

        [Test]
        public void Return_time_elapsed_greater_than_0_dot_2_seconds_for_2_watch_with_same_label()
        {
            var guidInstanceOne = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instanceOne = Watch.StopWatch(guidInstanceOne);

            var guidInstanceTwo = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instanceTwo = Watch.StopWatch(guidInstanceTwo);

            Check.That(Watch.SumWatch("watch_test", TimeType.Seconds) >= 0.19).IsTrue();
        }

        [Test]
        public void Return_time_elapsed_greater_than_0_dot_003_minutes_for_2_watch_with_same_label()
        {
            var guidInstanceOne = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instanceOne = Watch.StopWatch(guidInstanceOne);

            var guidInstanceTwo = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instanceTwo = Watch.StopWatch(guidInstanceTwo);

            Check.That(Watch.SumWatch("watch_test", TimeType.Minutes) >= 0.0029).IsTrue();
        }


        [Test]
        public void Throw_NoWatchFindException_if_no_watch_instances_found_for_label()
        {
            var guidInstanceOne = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instanceOne = Watch.StopWatch(guidInstanceOne);

            var guidInstanceTwo = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instanceTwo = Watch.StopWatch(guidInstanceTwo);

            Check.ThatCode(() =>
                    Watch.SumWatch("watch_test_not_found", TimeType.Milliseconds))
                .Throws<NoWatchFindException>();
        }

        [Test]
        public void Return_type_NoWatchFindException_is_serializable()
        {
            var assembly = typeof(NoWatchFindException).Assembly;
            var unserializableTypes = (from t in assembly.GetTypes() where t.IsSerializable select t).ToArray();
            Check.That(unserializableTypes.Any()).IsTrue();
        }

        [Test]
        public void Return_NoWatchFindException_serialization()
        {
            var e = new NoWatchFindException("Test NoWatchFindException");
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, e);
                s.Position = 0;
                e = (NoWatchFindException)formatter.Deserialize(s);
            }
            Check.That(e.Message).IsEqualTo("No watch found for label Test NoWatchFindException");
        }
    }
}
