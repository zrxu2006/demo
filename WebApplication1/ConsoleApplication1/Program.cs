using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskDemo.Start(args);
            ////使用反射读取Attribute
            //System.Reflection.MemberInfo info = typeof(Student); //通过反射得到Student类的信息
            //Hobby hobbyAttr = (Hobby)Attribute.GetCustomAttribute(info, typeof(Hobby));
            //var attr = TypeDescriptor.GetAttributes(typeof(Student))[0] as Hobby;
            
            //if (hobbyAttr != null)
            //{
            //    Console.WriteLine("类名：{0}", info.Name);
            //    Console.WriteLine("兴趣类型：{0}", hobbyAttr.Type);
            //    Console.WriteLine("兴趣指数：{0}", hobbyAttr.Level);
            //}

            //Console.ReadKey();
        }
    }

    [Hobby("Sports", Level = 5)]
    class Student
    {
        [Hobby("Football")]
        public string profession;
        public string Profession
        {
            get { return profession; }
            set { profession = value; }
        }
    }
    //建议取名：HobbyAttribute
    class Hobby : Attribute // 必须以System.Attribute 类为基类
    {
        // 参数值为null的string 危险，所以必需在构造函数中赋值
        public Hobby(string _type) // 定位参数
        {
            this.type = _type;
        }
        //兴趣类型
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        //兴趣指数
        private int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
    }

}
