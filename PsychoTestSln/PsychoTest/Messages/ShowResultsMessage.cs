using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PsychoTest.ViewModels;

namespace PsychoTest.Messages
{
    public class ShowResultsMessage
    {
        public TestViewModel TestViewModel { get; set; }
    }
}
