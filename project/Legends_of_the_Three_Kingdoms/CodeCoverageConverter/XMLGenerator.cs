using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

using Microsoft.VisualStudio.Coverage.Analysis;

namespace CodeCoverageConverter
{
    public class XmlGenerator
    {
        private string inputCoverageFile;
        private string outXmlFile;
        private int linesCovered;
        private int linesNotCovered;
        private int linesPartiallyCovered;
        private int blocksCovered;
        private int blocksNotCovered;

        public XmlGenerator()
        {
            linesCovered = 0;
            linesNotCovered = 0;
            linesPartiallyCovered = 0;
            blocksCovered = 0;
            blocksNotCovered = 0;
        }

        private void computeInputOutputFiles()
        {
            string inputDir = "../../../TestResults/";
            DirectoryInfo mostRecentCoverageDir = new DirectoryInfo(inputDir).GetDirectories()
                       .OrderByDescending(d => d.LastWriteTimeUtc).First();

            FileInfo mostRecentCoverageFile = mostRecentCoverageDir.GetFiles()
                .Where(f => f.Name.EndsWith(".coverage"))
                .OrderByDescending(f => f.LastWriteTimeUtc)
                .First();

            inputCoverageFile = mostRecentCoverageFile.FullName;

            outXmlFile = "../../../TestResults/coverage.xml";
        }

        private void writeXml()
        {
            using (CoverageInfo info = CoverageInfo.CreateFromFile(inputCoverageFile))
            {
                CoverageDS data = info.BuildDataSet();
                data.WriteXml(outXmlFile);
                Console.WriteLine("Coverage XML written to: " + Path.GetFullPath(outXmlFile));
            }
        }

        private void printStatistics()
        {
            // Check the coverage XML by printing coverage info to console output
            using (XmlReader reader = XmlReader.Create(outXmlFile))
            {
                while(true)
                {
                    reader.ReadToFollowing("Module");

                    if (reader.EOF)
                        break;

                    reader.ReadToDescendant("ModuleName");
                    string moduleName = reader.ReadElementContentAsString();

                    if(moduleName.EndsWith("test.dll") || moduleName.EndsWith("test.exe"))
                    {
                        Console.WriteLine("Igonoring [" + moduleName + "] for printing statistics as it's name indicate it is a test suite for another project ...");
                        continue;
                    }

                    reader.ReadToFollowing("LinesCovered");
                    linesCovered += reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("LinesNotCovered");
                    linesNotCovered += reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("LinesPartiallyCovered");
                    linesPartiallyCovered += reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("BlocksCovered");
                    blocksCovered += reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("BlocksNotCovered");
                    blocksNotCovered += reader.ReadElementContentAsInt();
                }
            }

            Console.WriteLine("=============== Code Coverage Statistics ==================");
            Console.WriteLine("Lines Covered: " + linesCovered);
            Console.WriteLine("Lines Not Covered: " + linesNotCovered);
            Console.WriteLine("Lines Partially Covered: " + linesPartiallyCovered);
            Console.WriteLine("Blocks Covered: " + blocksCovered);
            Console.WriteLine("Blocks Not Covered: " + blocksNotCovered);
            Console.WriteLine("Lines Coverage: " + (linesCovered * 100/(float)(linesCovered + linesNotCovered + linesPartiallyCovered)).ToString("F2") + "%");
            Console.WriteLine("Block Coverage: " + (blocksCovered * 100/ (float)(blocksCovered + blocksNotCovered)).ToString("F2") + "%");
            Console.WriteLine("============= End Code Coverage Statistics ================");
        }

        public void generate()
        {
            computeInputOutputFiles();
            writeXml();
            printStatistics();
        }

        static void Main(string[] args)
        {
            XmlGenerator generator = new XmlGenerator();
            generator.generate();
        }
    }
}
