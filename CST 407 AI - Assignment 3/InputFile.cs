using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST_407_AI___Assignment_3
{
    public class InputFile
    {
        private string exp_value;
        private string[,] inputVal;

        public InputFile()
        {
            exp_value = null;
            inputVal = new string[8,8];
        }

        public static InputFile Read(string fileName)
        {
            InputFile result = new InputFile();

            // OPEN FILE
            StreamReader reader = File.OpenText(fileName);

            // Read Expected Result
            result.exp_value = reader.ReadLine();

            // Read Actual Result
            for (int i = 0; i < 8; i++)
            {
                string row = reader.ReadLine();
                string[] values = row.Split(',');
                for (int j = 0; j < 8; j++)
                {
                    result.inputVal[i, j] = values[j];
                }
            }

            reader.Close();

            return result;
        }

        public TrainingData GetTrainingData()
        {
            double[] input = new double[64];
            double[] output = new double[2];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    input[i * 8 + j] = System.Convert.ToDouble(inputVal[i, j]);
                }
            }


            if (exp_value == "X")
            {
                output[0] = 1.0;
                output[1] = 0.0;
            }
            else if (exp_value == "0")
            {
                // if it's not x then it's 0
                output[0] = 0.0;
                output[1] = 1.0;
            }

            // otherwise we don't know the expected output


            return new TrainingData(input, output);
        }
    }


}
