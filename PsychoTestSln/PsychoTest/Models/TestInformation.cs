using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoTest.Models
{
    public static class TestInformation
    {
        public static List<TestQuestion> Questions = new List<TestQuestion>()
        {
            new TestQuestion()
            {
                Id = Guid.Parse("1E9D554D-45AC-4590-AC7E-2C7950C6A0E7"),
                Question = "1. Кем был Цветик в сказке Н. Носова про Незнайку?",
                Type = QuestionType.ProQualities
            },
            new TestQuestion()
            {
                Id = Guid.Parse("F7A4748F-0856-4AF3-88E7-B3A6043CFEC3"),
                Question = "2. Какой официальный язык в Египте, Марокко, Тунисе?",
                Type = QuestionType.PersonalQualities
            },
            //new TestQuestion()
            //{
            //    Id = Guid.Parse("5732ED55-B661-4064-A6D9-8B8A26D1E296"),
            //    Question = "3. Какой из этих российских придворных чинов был ниже остальных?",
            //    Type = QuestionType.ProQualities
            //},
            //new TestQuestion()
            //{
            //    Id = Guid.Parse("E5C47703-2CFC-4DDD-9AC4-3C8232E84402"),
            //    Question = "4. Какой из этих соборов превосходит по высоте пирамиду Хеопса?",
            //    Type = QuestionType.PersonalQualities
            //},
            //new TestQuestion()
            //{
            //    Id = Guid.Parse("1CA57E56-9CDC-407E-A189-3FD73DD9695E"),
            //    Question = "5. Как называется революционный роман Максима Горького?",
            //    Type = QuestionType.ProQualities
            //},
            //new TestQuestion()
            //{
            //    Id = Guid.Parse("AB8B66CF-F77E-4638-8368-C1F5E6BE0E75"),
            //    Question = "6. Какая из этих звезд - альфа созвездия Близнецов?",
            //    Type = QuestionType.PersonalQualities
            //},
        };
    }
}
