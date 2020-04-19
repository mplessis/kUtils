using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kopigi.Utils.Extensions;
using NFluent;
using Xunit;

namespace Kopigi.Utils.Tests.Extensions
{
    public class ObservableCollectionExtensionsShould
    {
        [Fact]
        public void return_three_object_when_a_list_with_three_elements_are_added_to_observable_collection()
        {
            var list = new List<string>()
            {
                "first",
                "second",
                "third"
            };

            var observable = new ObservableCollection<string>();
            observable.AddRange(list);
            Check.That(observable.Count).IsEqualTo(3);
        }
        
        [Fact]
        public void check_that_first_element_in_observable_collection_is_the_same_in_list()
        {
            var list = new List<string>()
            {
                "first",
                "second",
                "third"
            };

            var observable = new ObservableCollection<string>();
            observable.AddRange(list);
            Check.That(observable.First()).IsEqualTo(list.First());
        }
    }
}
