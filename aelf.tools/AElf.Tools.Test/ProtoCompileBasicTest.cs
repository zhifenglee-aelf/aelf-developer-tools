#region Copyright notice and license

#endregion

// UWYU: Object.GetType() extension.
using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Framework;
using Moq;
using NUnit.Framework;

namespace AElf.Tools.Test
{
    public class ProtoCompileBasicTest
    {
        // Mock task class that stops right before invoking protoc.
        public class ProtoCompileTestable : ProtoCompile
        {
            public string LastPathToTool { get; private set; }
            public string[] LastResponseFile { get; private set; }
            public List<string> StdErrMessages { get; } = new List<string>();

            protected override int ExecuteTool(string pathToTool,
                                               string response,
                                               string commandLine)
            {
                // We should never be using command line commands.
                Assert.That(commandLine, Is.Null | Is.Empty);

                // Must receive a path to tool
                Assert.That(pathToTool, Is.Not.Null & Is.Not.Empty);
                Assert.That(response, Is.Not.Null & Does.EndWith("\n"));

                LastPathToTool = pathToTool;
                LastResponseFile = response.Remove(response.Length - 1).Split('\n');

                foreach (string message in StdErrMessages)
                {
                    LogEventsFromTextOutput(message, MessageImportance.High);
                }

                // Do not run the tool, but pretend it ran successfully.
                return StdErrMessages.Any() ? -1 : 0;
            }
        };

        protected Mock<IBuildEngine> _mockEngine;
        protected ProtoCompileTestable _task;

        [SetUp]
        public void SetUp()
        {
            _mockEngine = new Mock<IBuildEngine>();
            _task = new ProtoCompileTestable {
                BuildEngine = _mockEngine.Object
            };
        }

        [TestCase("Protobuf")]
        [TestCase("Generator")]
        [TestCase("OutputDir")]
        [Description("We trust MSBuild to initialize these properties.")]
        public void RequiredAttributePresentOnProperty(string prop)
        {
            var pinfo = _task.GetType()?.GetProperty(prop);
            Assert.NotNull(pinfo);
            Assert.That(pinfo, Has.Attribute<RequiredAttribute>());
        }
    };
}
