using System;
using System.Collections.Generic;

namespace Contract
{
   public interface IProtocol:IDisposable
   {
       /// <summary>
       /// 获取通讯命令
       /// </summary>
       /// <returns>需要发送的命令</returns>
       byte[] GetCBytesommand();

       /// <summary>
       /// 更新数据
       /// </summary>
       /// <param name="value">新数据</param>
       void Update(byte[] value);

        /// <summary>
        /// 获取指定测点ID
        /// </summary>
        /// <param name="id">测点唯一身份标识符</param>
        /// <returns>测点值</returns>
        /// <exception cref="KeyNotFoundException">指定Id未找到</exception>
        float GetData(int id);
   }
}
