using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IPort:IDisposable
    {
        /// <summary>
        /// 打开端口
        /// </summary>
        /// <param name="o">参数</param>
        /// <returns>是否成功打开</returns>
        bool Start(object o);

        /// <summary>
        /// 停止端口工作
        /// </summary>
        /// <returns>是否成功关闭</returns>
        bool Stop();

        /// <summary>
        /// 发送数据
        /// </summary>
        void Send(byte[] array);

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns></returns>
        byte[] Recive();
    }
}