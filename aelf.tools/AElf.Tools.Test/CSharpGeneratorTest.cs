#region Copyright notice and license

#endregion

using NUnit.Framework;

namespace AElf.Tools.Test
{
    public class CSharpGeneratorTest : GeneratorTest
    {
        GeneratorServices _generator;

        [SetUp]
        public new void SetUp()
        {
            _generator = GeneratorServices.GetForLanguage("CSharp", _log);
        }

        [TestCase("foo.proto", "Foo.cs", "Foo.cs")]
        [TestCase("sub/foo.proto", "Foo.cs", "Foo.cs")]
        [TestCase("one_two.proto", "OneTwo.cs", "OneTwo.cs")]
        [TestCase("ONE_TWO.proto", "ONETWO.cs", "ONETWO.cs")]
        [TestCase("one.two.proto", "OneTwo.cs", "OneTwo.cs")]
        [TestCase("one123two.proto", "One123Two.cs", "One123Two.cs")]
        [TestCase("__one_two!.proto", "OneTwo.cs", "OneTwo.cs")]
        [TestCase("one(two).proto", "OneTwo.cs", "OneTwo.cs")]
        [TestCase("one_(two).proto", "OneTwo.cs", "OneTwo.cs")]
        [TestCase("one two.proto", "OneTwo.cs", "OneTwo.cs")]
        [TestCase("one_ two.proto", "OneTwo.cs", "OneTwo.cs")]
        [TestCase("one .proto", "One.cs", "One.cs")]
        public void NameMangling(string proto, string expectCs, string expectContractCs)
        {
            var poss = _generator.GetPossibleOutputs(Utils.MakeItem(proto, "services", "both"));
            Assert.AreEqual(2, poss.Length);
            Assert.Contains(expectCs, poss);
            Assert.Contains(expectContractCs, poss);
        }

        [Test]
        public void NoContractOneOutput()
        {
            var poss = _generator.GetPossibleOutputs(Utils.MakeItem("foo.proto"));
            Assert.AreEqual(2, poss.Length);
        }

        [TestCase("none")]
        [TestCase("")]
        public void ContractNoneOneOutput(string contract)
        {
            var item = Utils.MakeItem("foo.proto", "services", contract);
            var poss = _generator.GetPossibleOutputs(item);
            Assert.AreEqual(2, poss.Length);
        }

        [Test]
        public void OutputDirMetadataRecognized()
        {
            var item = Utils.MakeItem("foo.proto", "OutputDir", "out");
            var poss = _generator.GetPossibleOutputs(item);
            Assert.AreEqual(2, poss.Length);
            Assert.That(poss[0], Is.EqualTo("out/Foo.cs") | Is.EqualTo("out\\Foo.cs"));
        }

        [Test]
        public void OutputDirPatched()
        {
            var item = Utils.MakeItem("sub/foo.proto", "OutputDir", "out");
            var output = _generator.PatchOutputDirectory(item);
            var poss = _generator.GetPossibleOutputs(output);
            Assert.AreEqual(2, poss.Length);
            Assert.That(poss[0], Is.EqualTo("out/sub/Foo.cs") | Is.EqualTo("out\\sub\\Foo.cs"));
        }
    };
}
