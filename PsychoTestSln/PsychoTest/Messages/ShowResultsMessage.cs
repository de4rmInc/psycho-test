using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PsychoTest.Models;

namespace PsychoTest.Messages
{
    public class ShowResultsMessage
    {
        public ShowResultsMessage(TestAnswers testAnswers)
        {
            TestAnswers = testAnswers;
        }

        public TestAnswers TestAnswers { get; set; }
    }
}
