using NUnit.Framework;
using System;
using System.Reflection;
using UnitTestEx;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject
{
    [TestFixture]
    public class FileTest
    {

        public const string SIZE_EXCEPTION = "Wrong size";
        public const string NAME_EXCEPTION = "Wrong name";
        public const string SPACE_STRING = " ";
        public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string CONTENT_STRING = "Some text";
        public double lenght;

        /* ПРОВАЙДЕР */
        static object[] FilesData =
        {
            new object[] {new File(FILE_PATH_STRING, CONTENT_STRING), FILE_PATH_STRING, CONTENT_STRING},
            new object[] { new File(SPACE_STRING, SPACE_STRING), SPACE_STRING, SPACE_STRING}
        };

        /* Тестируем получение размера */
        [Test, TestCaseSource(nameof(FilesData))]
        public void GetSizeTest(File newFile, String name, String content)
        {
            lenght = content.Length / 2.0d; // Используем такой же расчет как в классе
            Assert.That(newFile.GetSize(), Is.EqualTo(lenght).Within(0.001), SIZE_EXCEPTION);
        }

        /* Тестируем получение имени */
        [Test, TestCaseSource(nameof(FilesData))]
        public void GetFilenameTest(File newFile, String name, String content)
        {
            Assert.That(newFile.GetFilename(), Is.EqualTo(name), NAME_EXCEPTION);
        }
    }
}