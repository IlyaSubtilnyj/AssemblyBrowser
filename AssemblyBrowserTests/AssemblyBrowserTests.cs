using AssemblyBrowserLib;

namespace AssemblyBrowserTests
{
    [TestClass]
    public class AssemblyBrowserTests
    {
        AssemblyCrawler crawler = new AssemblyCrawler();

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {}

        [TestMethod]
        [ExpectedException(typeof(BadImageFormatException))]
        public void Crawler_WhenNoAssemblyGiven()
        {

            crawler.Process("D:\\workspace\\Visual_Studio_workspace\\studing_workspace\\AssemblyBrowserLab\\Process\\Mapper.cs");
        }

        [TestMethod]
        public void Crawler_WhenCorrectAssemblyGiven_ModelShouldBeNotNull()
        {

            crawler.Process("D:\\workspace\\Visual_Studio_workspace\\studing_workspace\\AssemblyBrowserLab\\bin\\Debug\\net7.0-windows\\AssemblyBrowserLib.dll");
            var model = crawler.Model();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Crawler_WhenAssemblyBrowserLibDllGiven_OneOfUpperNamespacesShouldBeAssemblyBrowserLib()
        {

            crawler.Process("D:\\workspace\\Visual_Studio_workspace\\studing_workspace\\AssemblyBrowserLab\\bin\\Debug\\net7.0-windows\\AssemblyBrowserLib.dll");
            var model = crawler.Model();
            Assert.IsTrue(model!.root.namespaces.Any(obj => obj.name == "AssemblyBrowserLib"));
        }

        [TestMethod]
        public void Crawler_WhenAssemblyBrowserLibDllGiven_ShouldBeThreeNamespaces()
        {

            crawler.Process("D:\\workspace\\Visual_Studio_workspace\\studing_workspace\\AssemblyBrowserLab\\bin\\Debug\\net7.0-windows\\AssemblyBrowserLib.dll");
            var model           = crawler.Model();
            var namespaceCount  = model!.root.namespaces.Count();
            Assert.AreEqual(namespaceCount, 3);
        }
    }
}