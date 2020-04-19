using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kopigi.Utils.Extensions;
using NFluent;
using Xunit;

namespace Kopigi.Utils.Tests.Extensions
{
    public class ListExtensionShould
    {
        [Fact]
        public void return_1_2_3_for_list_which_contains_1_2_3()
        {
            Check.That(new List<int>() {1, 2, 3}.ToSeparateByChar('$')).IsEqualTo("1$2$3");
        }

        [Fact]
        public void return_string_empty_if_list_is_empty()
        {
            Check.That(new List<int>().ToSeparateByChar(' ')).IsEqualTo("");
        }
    }
}
