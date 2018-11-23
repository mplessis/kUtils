using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kopigi.Utils.Extensions;
using NFluent;
using NUnit.Framework;

namespace Kopigi.Utils.Tests.Extensions
{
    public class ListExtensionShould
    {
        [Test]
        public void Return_1_2_3_for_list_which_contains_1_2_3()
        {
            Check.That(new List<int>() {1, 2, 3}.ToSeparateByChar('$')).IsEqualTo("1$2$3");
        }

        [Test]
        public void Return_string_empty_if_list_is_empty()
        {
            Check.That(new List<int>().ToSeparateByChar(' ')).IsEqualTo("");
        }
    }
}
