using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PsychoTest.Extensions;
using PsychoTest.ViewModels;

namespace PsychoTest.Models
{
    public class TestProcessor
    {
        public List<TestResultItem> CalcProQualities(TestAnswers testAnswers)
        {
            return CalcQualities(testAnswers, QuestionType.ProQualities);
        }

        public List<TestResultItem> CalcPersonalQualities(TestAnswers testAnswers)
        {
            return CalcQualities(testAnswers, QuestionType.PersonalQualities);
        }

        public List<TestResultItem> CalcQualities(TestAnswers testAnswers, QuestionType questionType)
        {
            var result = new List<TestResultItem>(testAnswers.Count);
            var resultMatrix = new int[testAnswers.Count, testAnswers.Count];
            var questionMatrices = new Dictionary<Guid, List<BitArray>>();
            var participants = testAnswers.Participants.ToList();

            foreach (var participant in participants)
            {
                result.Add(new TestResultItem(participant));
            }

            for (int pIdx = 0; pIdx < participants.Count; pIdx++)
            {
                var participant = participants[pIdx];
                var proAnswers =
                    testAnswers.GetAnswersForParticipant(participant)
                        .Where(answer => answer.Question.Type == questionType);
                foreach (var proAnswer in proAnswers)
                {
                    questionMatrices
                        .GetOrAdd(proAnswer.Question.Id, new List<BitArray>())
                        .GetOrSet(pIdx, new BitArray(testAnswers.Count))
                        .Set(participants.IndexOf(proAnswer.Answer), true);
                        
                    resultMatrix[pIdx, participants.IndexOf(proAnswer.Answer)] += 1;
                }
            }

            for (int colIdx = 0; colIdx < resultMatrix.GetLength(1); colIdx++)
            {
                int sum = 0, mutualSum = 0;
                var resultItem = result[colIdx];

                for (int rowIdx = 0; rowIdx < resultMatrix.GetLength(0); rowIdx++)
                {
                    sum += resultMatrix[rowIdx, colIdx];
                }
                foreach (var matrix in questionMatrices)
                {
                    for (int rowIdx = 0; rowIdx < matrix.Value.Count; rowIdx++)
                    {
                        if (matrix.Value[rowIdx][colIdx] && matrix.Value[colIdx][rowIdx])
                        {
                            mutualSum += 1;
                        }
                    }
                }
                var totalSum = sum + mutualSum;

                resultItem.MutualSum = mutualSum;
                resultItem.TotalSum = totalSum;
            }

            return result;
        }
    }
}
