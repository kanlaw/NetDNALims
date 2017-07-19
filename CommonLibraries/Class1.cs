using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CommonLibraries
{
    [Serializable] //这个[Serializable]的作用是：指示一个类可以序列化。无法继承此类。  
    public class Class1
    {
        public string Name { get; set; }

        

        public int Age { get; set; }

        public char Gender { get; set; }

        public Class1() { }

        public Class1(string name, int age, char gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }
    }
}
