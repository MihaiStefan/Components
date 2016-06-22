using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ObjSerialization
{
    public class ObjFile
    {
        private string _fileName;
        //private FileStream stream;


        public string MyProperty
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public ObjFile(string fileName)
        {
            _fileName = fileName;
            try
            {
                var file = File.Open(_fileName, FileMode.Create);
                file.Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error create file");
            }
        }

        ~ObjFile()
        {
            //saving
            //closing file
        }

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public void SaveObject<T>(T serializableObject)
        {
            if (serializableObject == null) { return; }

            try
            {
                FileStream outFile = File.Open(_fileName, FileMode.Append);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(outFile, serializableObject);
                outFile.Close();
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }


        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T LoadOnce<T>()
        {
            if (string.IsNullOrEmpty(_fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                Stream stream = File.Open(_fileName, FileMode.Open);

                BinaryFormatter formatter = new BinaryFormatter();

                objectOut = (T)formatter.Deserialize(stream);
                stream.Close();

            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }

        public T LoadMore<T>()
        {
            if (string.IsNullOrEmpty(_fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                Stream stream = File.Open(_fileName, FileMode.Open);

                BinaryFormatter formatter = new BinaryFormatter();

                objectOut = (T)formatter.Deserialize(stream);
                stream.Close();

            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }

    }
}
