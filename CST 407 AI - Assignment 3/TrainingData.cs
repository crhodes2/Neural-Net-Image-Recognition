using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST_407_AI___Assignment_3
{
    public class TrainingData
    {
        public TrainingData()
        {
            X = new List<double>();
            T = new List<double>();
        }

        public TrainingData(double[] input, double[] expected)
        {
            X = new List<double>(input);
            T = new List<double>(expected);
        }

        public List<double> X { get; set; }     // input values
        public List<double> T { get; set; }     // expected output values
    }
}
