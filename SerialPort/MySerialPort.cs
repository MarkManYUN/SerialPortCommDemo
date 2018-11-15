using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Contract;

namespace SerialPort
{
  public  class MySerialPort:IPort
    {
        public void Dispose()
        {
            
        }

        System.IO.Ports.SerialPort Sp=new System.IO.Ports.SerialPort();
        public bool Start(object o)
        {  /*
             * 反射属性
             */

            Type type = o.GetType();
            PropertyInfo pinfo = type.GetProperty("PortName");
            if (null != pinfo && pinfo.PropertyType == typeof(string))
            {
               string PortName =pinfo.GetValue(o, null).ToString();
            }

            pinfo = type.GetProperty("StopBits");
            if (null != pinfo && pinfo.PropertyType == typeof(StopBits))
            {
                StopBits bits = (StopBits)Enum.Parse(typeof(StopBits),pinfo.GetValue(o, null).ToString());
            }

           
            return true;
        }

        public bool Stop()
        {
            return true;

        }

        public void Send(byte[] array)
        {
           
//            if(Sp.IsOpen)
//                Sp.Write(array,0,array.Length);
        }

        public byte[] Recive()
        {
           return new byte[10];
        }
    }
}
