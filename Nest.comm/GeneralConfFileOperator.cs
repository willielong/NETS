

/****************************************************************************
* 类名：GeneralConfFileOperator
* 描述：对配置文件进行处理--对接口实现
* 创建人：Author
* 创建时间：2018.05.04 
* 修改人;Author
* 修改时间：2018.05.04
* 修改描述：
* **************************************************************************
*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Workflow.comm
{
    public class GeneralConfFileOperator:IConfigFile
    {
        public string ReadConfFile(string path, bool decrypt = true)
        {
            if (string.IsNullOrEmpty(path))
                return "";

            string result = "";

            if (File.Exists(path))
            {
                if (decrypt)
                    result = FileEncrypt.Decrypt(path);
                else
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        StreamReader sr = new StreamReader(fs);
                        result = sr.ReadToEnd();

                        sr.Close();
                    }
                }
            }

            return result;
        }

        public T ReadConfFile<T>(string path, bool decrypt = true) where T : class, new()
        {
            if (string.IsNullOrEmpty(path))
                return default(T);

            T result = default(T);
            string xmlsource = "";

            if (File.Exists(path))
            {
                if (decrypt)
                    xmlsource = FileEncrypt.Decrypt(path);
                else
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        StreamReader sr = new StreamReader(fs);
                        xmlsource = sr.ReadToEnd();

                        sr.Close();
                    }
                }
                result = XmlHelper.XmlDeserialize<T>(xmlsource, Encoding.UTF8);
            }

            return result;
        }

        public void WriteConfFile(string data, string path, bool nessasary = false, bool encrypt = true)
        {
            if (string.IsNullOrEmpty(path))
                return;

            if (encrypt)
                FileEncrypt.Encrypt(path, data);
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(data);

                    sw.Close();
                }
            }
        }

        public void WriteConfFile(object entity, string path, bool nessasary = false, bool encrypt = true)
        {
            if (string.IsNullOrEmpty(path))
                return;

            string xmlsource = XmlHelper.XmlSerialize(entity, Encoding.UTF8, true);

            if (encrypt)
                FileEncrypt.Encrypt(path, xmlsource);
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(xmlsource);

                    sw.Close();
                }
            }
        }
    }
}
