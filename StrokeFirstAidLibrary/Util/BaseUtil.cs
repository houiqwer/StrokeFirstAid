using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StrokeFirstAidLibrary.Util
{
    public class BaseUtil
    {
        public static T XmlDeepCopy<T>(T t)
        {
            //创建Xml序列化对象
            XmlSerializer xml = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())//创建内存流
            {
                //将对象序列化到内存中
                xml.Serialize(ms, t);
                ms.Position = default;//将内存流的位置设为0
                return (T)xml.Deserialize(ms);//继续反序列化
            }
        }


        public static List<EnumEntity> EnumToList<T>()
        {
            List<EnumEntity> enumEntityList = new List<EnumEntity>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                EnumEntity enumEntity = new EnumEntity();                     
                enumEntity.Value = Convert.ToInt32(e);
                enumEntity.Name = e.ToString();
                enumEntityList.Add(enumEntity);
            }
            return enumEntityList;
        }




        public class EnumEntity
        {           
            public string Name { set; get; }
            public int Value { set; get; }
        }
    }
}
