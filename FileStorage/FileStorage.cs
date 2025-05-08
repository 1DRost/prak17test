using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestEx
{
    public class FileStorage
    {
        private List<File> files = new List<File>();
        private double availableSize;
        private readonly double maxSize;

        public FileStorage(int size = 100)
        {
            maxSize = size;
            availableSize = size;
        }

        public bool Write(File file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            if (file.GetSize() > availableSize) return false; //проверка на доступный размер
            if (IsExists(file.GetFilename())) throw new FileNameAlreadyExistsException(); //проверка на существование файла
            files.Add(file);
            availableSize -= file.GetSize(); //обновление доступного размера
            return true;
        }

        public bool IsExists(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return false; //проверка на пустое имя файла
            return files.Exists(f => f != null && f.GetFilename() != null && f.GetFilename().Equals(fileName, StringComparison.OrdinalIgnoreCase)); //поиск файла, игнорируя регистр
        }

        public bool Delete(string fileName)
        {
            var file = GetFile(fileName); //получение файла по имени
            if (file == null) return false; //проверка на существование файла
            availableSize += file.GetSize(); //освобождение места
            return files.Remove(file);
        }

        public List<File> GetFiles() => new List<File>(files);

        public File GetFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return null; //проверка на пустое имя файла
            return files.FirstOrDefault(f => f != null && f.GetFilename() != null && f.GetFilename().Equals(fileName, StringComparison.OrdinalIgnoreCase)); //поиск файла, игнорируя регистр
        }

        public bool DeleteAllFiles()
        {
            availableSize = maxSize; //восстанавливает доступный размер до максимального
            files.Clear(); //очищает список файлов
            return true;
        }
    }
}
