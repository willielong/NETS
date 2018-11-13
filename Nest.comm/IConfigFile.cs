


/****************************************************************************
* 类名：IConfigFile
* 描述：对配置文件进行处理---接口
* 创建人：Author
* 创建时间：2018.05.04 
* 修改人;Author
* 修改时间：2018.05.04
* 修改描述：
* **************************************************************************
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.comm
{
    public interface IConfigFile
    {
        void WriteConfFile(object entity, string path, bool nessasary = false, bool encrypt = true);
        void WriteConfFile(string data, string path, bool nessasary = false, bool encrypt = true);

        T ReadConfFile<T>(string path, bool decrypt = true) where T : class, new();
        string ReadConfFile(string path, bool decrypt = true);
    }
}
