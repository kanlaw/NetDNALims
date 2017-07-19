using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonLibraries
{
   public class ToolSerialize
    {

        /// <summary>
        /// 将对象序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="对象路径"></param>
        /// <returns></returns>
        public FileStream Serialize<T>(T value, string filename)
        {
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bs = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.FileStream stream = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                bs.Serialize(stream, value);

                stream.Close();
                return stream;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="path"></param>
        public CommonLibraries.Class1 InverseSequence(string path)
        {
            try
            {
                FileStream stream2 = new FileStream(path, FileMode.Open, FileAccess.Read);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf2 = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                CommonLibraries.Class1 cl = (CommonLibraries.Class1)bf2.Deserialize(stream2);

                return cl;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

    }
}
