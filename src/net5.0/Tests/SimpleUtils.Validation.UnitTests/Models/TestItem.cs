using System.Collections.Generic;

namespace SimpleUtils.Validation.UnitTests.Models
{
    public sealed class TestItem
    {
        public TestProperty Property { get; set; }

        public IEnumerable<TestProperty> PropertyCollection { get; set; }

        public TestEnum EnumProperty { get; set; }

        public string StringProperty { get; set; }
    }
}