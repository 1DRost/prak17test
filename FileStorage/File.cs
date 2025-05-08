using System;

namespace UnitTestEx
{
    public class File
    {
        private string extension;
        private string filename;
        private string content;
        private double size;

        public File(string filename, string content) 
        {
            this.filename = filename;
            this.content = content ?? ""; //инициализация content пустой строкой
            this.size = content.Length / 2.0; //вычисление размера файла
            this.extension = filename.Split('.').Length > 1 ? filename.Split('.')[filename.Split('.').Length - 1] : ""; //извлечение расширения файла
        }

        public double GetSize() //возвращает размер файла
        {
            return size;
        }

        public string GetFilename() //возвращает имя файла
        {
            return filename;
        }
    }
}
