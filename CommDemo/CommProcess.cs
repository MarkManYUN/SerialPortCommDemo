using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Contract;
using MudbusPortocol;
using SerialPort;

namespace CommDemo
{
    public class CommProcess
    {
        private Task MainTask;

        public bool Start(object o)
        {
            if (null != PortInstance)
                return PortInstance.Start(o);
            MainTask = Task.Factory.StartNew(TaskOn);
            return false;
        }

        /// <summary>
        /// 异步任务
        /// </summary>
        protected virtual void TaskOn()
        {
            try
            {
                while (true)
                {
                    Task.Delay(1000);
                    OneCommunication();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        /// <summary>
        /// 一次通讯过程
        /// </summary>
        protected virtual void OneCommunication()
        {
            try
            {
                if (PortInstance != null && ProtocolIntance != null)
                {
                    #region 发送

                    byte[] array = ProtocolIntance.GetCBytesommand();
                    if (array != null)
                        PortInstance.Send(array);

                    #endregion  end 发送 

                    #region 接收

                    //等1s超时
                    DateTime dtLastSendOn = DateTime.Now;
                    while (DateTime.Now.Subtract(dtLastSendOn).TotalMilliseconds < 1000)
                    {
                        byte[] arrayRec;
                        if ((arrayRec = PortInstance.Recive()) != null)
                        {
                            byte[] arrayBack;
                            if (RegDeata(arrayRec, out arrayBack))
                            {
                                ProtocolIntance.Update(arrayBack);
                            }

                            return;
                        }
                    }

                    #endregion end 接收
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="array">原始数据</param>
        /// <param name="array2">解析数据</param>
        /// <returns></returns>
        protected virtual bool RegDeata(byte[] array, out byte[] array2)
        {
            array2 = array;
            return true;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            MainTask.Dispose();
            PortInstance.Dispose();
            PortInstance = null;
            ProtocolIntance.Dispose();
            ProtocolIntance = null;
        }

        /// <summary>
        /// 
        /// </summary>
        protected IPort _portInstance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IProtocol _protocolIntance { get; set; }


        /// <summary>
        /// 
        /// </summary>
        protected virtual IPort PortInstance
        {
            get
            {
                if (null == _portInstance)
                {
                    _portInstance = new MySerialPort();
                }

                return _portInstance;
            }
            set { _portInstance = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual IProtocol ProtocolIntance
        {
            get
            {
                if (null == _protocolIntance)
                {
                    _protocolIntance = new Modbus();
                }

                return _protocolIntance;
            }
            set { _protocolIntance = value; }
        }

        /// <summary>
        /// 获取指定测点数据
        /// </summary>
        /// <param name="id">测点唯一标识符</param>
        /// <returns>返回测点值</returns>
        public float GetData(int id)
        {
            try
            {
                if (null != ProtocolIntance)
                    return ProtocolIntance.GetData(id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            return 0;
        }
    }
}