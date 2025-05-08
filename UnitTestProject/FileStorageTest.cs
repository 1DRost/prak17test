using NUnit.Framework;
using System;
using System.Reflection;
using UnitTestEx;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject
{
    /// <summary>
    /// Summary description for FileStorageTest
    /// </summary>
    [TestFixture]
    public class FileStorageTest
    {
        public const string MAX_SIZE_EXCEPTION = "DIFFERENT MAX SIZE";
        public const string NULL_FILE_EXCEPTION = "NULL FILE";
        public const string NO_EXPECTED_EXCEPTION_EXCEPTION = "There is no expected exception";

        public const string SPACE_STRING = " ";
        public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string CONTENT_STRING = "Some text";
        public const string REPEATED_STRING = "AA";
        public const string WRONG_SIZE_CONTENT_STRING = "TEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtext";
        public const string TIC_TOC_TOE_STRING = "tictoctoe.game";

        public const int NEW_SIZE = 100;

        public FileStorage storage = new FileStorage(NEW_SIZE);

        /* ПРОВАЙДЕРЫ */

        static object[] NewFilesData =
        {
            new object[] { new File(REPEATED_STRING, CONTENT_STRING) },
            new object[] { new File(SPACE_STRING, WRONG_SIZE_CONTENT_STRING) },
            new object[] { new File(FILE_PATH_STRING, CONTENT_STRING) }
        };

        static object[] FilesForDeleteData =
        {
            new object[] { new File(REPEATED_STRING, CONTENT_STRING), REPEATED_STRING },
            new object[] { null, TIC_TOC_TOE_STRING }
        };

        static object[] NewExceptionFileData = {
            new object[] { new File(REPEATED_STRING, CONTENT_STRING) }
        };

        /* Тестирование записи файла */
        [Test, TestCaseSource(nameof(NewFilesData))]
        public void WriteTest(File file)
        {
            Assert.That(storage.Write(file), Is.True);
        }

        /* Тестирование записи дублирующегося файла */
        [Test, TestCaseSource(nameof(NewExceptionFileData))]
        public void WriteExceptionTest(File file)
        {
            storage.DeleteAllFiles();

            Assert.That(storage.Write(file), Is.True, "Первая запись файла должна быть успешной");
            var ex = Assert.Throws<FileNameAlreadyExistsException>(() => storage.Write(file));
            Assert.That(ex.Message, Is.EqualTo("File with this name already exists"));

            storage.DeleteAllFiles();
        }

        /* Тестирование проверки существования файла */
        [Test, TestCaseSource(nameof(NewFilesData))]
        public void IsExistsTest(File file)
        {
            storage.DeleteAllFiles();
            string name = file.GetFilename();

            Assert.That(storage.IsExists(name), Is.False, "Файл не должен существовать до записи");

            Assert.That(storage.Write(file), Is.True, "Файл должен быть успешно записан");
            Assert.That(storage.IsExists(name), Is.True, "Файл должен существовать после записи");
        }

        /* Тестирование удаления файла */
        [Test, TestCaseSource(nameof(FilesForDeleteData))]
        public void DeleteTest(File file, string fileName)
        {
            if (file != null)
            {
                storage.Write(file);
                Assert.That(storage.Delete(fileName), Is.True);
            }
            else
            {
                Assert.That(() => storage.Delete(fileName), Throws.Nothing);
            }
        }

        /* Тестирование получения файлов */
        [Test]
        public void GetFilesTest()
        {
            foreach (File el in storage.GetFiles())
            {
                Assert.That(el, Is.Not.Null);
            }
        }

        /* Тестирование получения файла */
        [Test, TestCaseSource(nameof(NewFilesData))]
        public void GetFileTest(File expectedFile)
        {
            storage.DeleteAllFiles();
            Assert.That(storage.Write(expectedFile), Is.True, "Файл должен быть успешно записан");

            File actualFile = storage.GetFile(expectedFile.GetFilename());

            Assert.That(actualFile, Is.Not.Null, "Файл должен быть найден");
            Assert.That(actualFile.GetFilename(), Is.EqualTo(expectedFile.GetFilename()));
            Assert.That(actualFile.GetSize(), Is.EqualTo(expectedFile.GetSize()));
        }
    }
}