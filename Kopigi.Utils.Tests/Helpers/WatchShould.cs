using Kopigi.Utils.Class;
using Kopigi.Utils.Helpers;
using NFluent;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Xunit;

namespace Kopigi.Utils.Tests.Helpers
{
    public class WatchShould
    {
        public WatchShould()
        {
            Watch.Clear();
        }

        [Fact]
        public void return_not_null_on_new_start_watch()
        {
            Check.That(Watch.StartWatch("watch_test")).IsNotEqualTo(null);
        }

        [Fact]
        public void return_guid_on_new_start_watch()
        {
            Check.That(Watch.StartWatch("watch_test")).IsInstanceOf<Guid>();
        }

        [Fact]
        public void return_one_instancewatch_with_time_elapsed_on_stop_watch()
        {
            var guidInstance = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instance = Watch.StopWatch(guidInstance);
            Check.That(instance).IsNotEqualTo(null);
            Check.That(instance.Elapsed.Milliseconds >= 100).IsTrue();
        }

        [Fact]
        public void return_null_if_no_watch_found_on_stop_watch()
        {
            Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            var instance = Watch.StopWatch(Guid.NewGuid());
            Check.That(instance).IsEqualTo(null);
        }

        [Fact]
        public void return_null_if_instances_watch_are_clear_before_stop_watch()
        {
            var guidInstance = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.Clear("watch_test");
            var instance = Watch.StopWatch(guidInstance);
            Check.That(instance).IsEqualTo(null);
        }

        [Fact]
        public void return_time_elapsed_greater_than_200_milliseconds_for_2_watch_with_same_label()
        {
            var guidInstanceOne = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.StopWatch(guidInstanceOne);

            var guidInstanceTwo = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.StopWatch(guidInstanceTwo);

            Check.That(Watch.SumWatch("watch_test", TimeType.Milliseconds) >= 200).IsTrue();
        }

        [Fact]
        public void return_time_elapsed_greater_than_0_dot_2_seconds_for_2_watch_with_same_label()
        {
            var guidInstanceOne = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.StopWatch(guidInstanceOne);

            var guidInstanceTwo = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.StopWatch(guidInstanceTwo);

            Check.That(Watch.SumWatch("watch_test", TimeType.Seconds) >= 0.19).IsTrue();
        }

        [Fact]
        public void return_time_elapsed_greater_than_0_dot_003_minutes_for_2_watch_with_same_label()
        {
            var guidInstanceOne = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.StopWatch(guidInstanceOne);

            var guidInstanceTwo = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.StopWatch(guidInstanceTwo);

            Check.That(Watch.SumWatch("watch_test", TimeType.Minutes) >= 0.0029).IsTrue();
        }

        [Fact]
        public void throw_ArgumentException_for_TimeType_unknow()
        {
            var guidInstanceOne = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.StopWatch(guidInstanceOne);

            var guidInstanceTwo = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.StopWatch(guidInstanceTwo);

            Check.ThatCode(() =>
                Watch.SumWatch("watch_test", (TimeType) Int32.MaxValue)
            ).Throws<ArgumentException>();
        }

        [Fact]
        public void throw_NoWatchFindException_if_no_watch_instances_found_for_label()
        {
            var guidInstanceOne = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.StopWatch(guidInstanceOne);

            var guidInstanceTwo = Watch.StartWatch("watch_test");
            Thread.Sleep(100);
            Watch.StopWatch(guidInstanceTwo);

            Check.ThatCode(() =>
                    Watch.SumWatch("watch_test_not_found", TimeType.Milliseconds))
                .Throws<NoWatchFindException>();
        }

        [Fact]
        public void return_type_NoWatchFindException_is_serializable()
        {
            var assembly = typeof(NoWatchFindException).Assembly;
            var unserializableTypes = (from t in assembly.GetTypes() where t.IsSerializable select t).ToArray();
            Check.That(unserializableTypes.Any()).IsTrue();
        }

        [Fact]
        public void return_NoWatchFindException_serialization()
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
