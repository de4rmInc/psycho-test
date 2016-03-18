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
                Question = "1. Кем был Цветик в сказке Н. Носова про Незнайку?",
                Type = QuestionType.ProQualities
            },
            new TestQuestion()
            {
                Question = "2. Какой официальный язык в Египте, Марокко, Тунисе?",
                Type = QuestionType.PersonalQuailities
            },
            new TestQuestion()
            {
                Question = "3. Какой из этих российских придворных чинов был ниже остальных?",
                Type = QuestionType.ProQualities
            },
            new TestQuestion()
            {
                Question = "4. Какой из этих соборов превосходит по высоте пирамиду Хеопса?",
                Type = QuestionType.PersonalQuailities
            },
            new TestQuestion()
            {
                Question = "5. Как называется революционный роман Максима Горького?",
                Type = QuestionType.ProQualities
            },
            new TestQuestion()
            {
                Question = "6. Какая из этих звезд - альфа созвездия Близнецов?",
                Type = QuestionType.PersonalQuailities
            },
        };
    }
}
