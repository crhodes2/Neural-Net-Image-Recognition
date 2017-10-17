using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CST_407_AI___Assignment_3
{
    class Program
    {
        static void Main(string[] args)
        {

            // create the net itself
            Net theNet = new Net();

            //================================ CREATE THE NEURONS ================================//

            // the two output neurons
            Neuron outX = new Neuron();
            Neuron out0 = new Neuron();
            theNet.AddOutput(outX);
            theNet.AddOutput(out0);

            // the 64 input neurons
            Neuron[,] inputNeurons = new Neuron[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Neuron input = new Neuron();
                    inputNeurons[i, j] = input;
                    theNet.AddInput(input);
                    theNet.Connect(input, outX, 0.0);
                    theNet.Connect(input, out0, 0.0);

                }
            }

            //================================ TRAIN THE NETS ================================//

            List<TrainingData> trainingdata = GetInputFiles("files\\mytrainingdata\\");


            //================================ TEST THE NETS ================================//

            theNet.Train(trainingdata.ToArray());

            /* using the 8 test files provided by instructor */
            List<TrainingData> testData = GetInputFiles("files\\mytestdata\\");
            foreach (TrainingData testPoint in testData)
            {
                // set net's input neurons to test input
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        //load point to corresponding input neuron
                        double value = testPoint.X[i * 8 + j];
                        inputNeurons[i, j].Value = value;
                        Console.Write(value.ToString() + " ");
                    }
                    Console.WriteLine();
                }

                //eval based on test input
                theNet.Evaluate();

                // print output

                Console.WriteLine("Out X = " + outX.Value.ToString());
                Console.WriteLine("Out O = " + out0.Value.ToString());
                Console.WriteLine();
            }

            //================================ CREATE THE OUTPUT FILES ================================//
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter("files\\myfiledata\\Test.txt");


                //Write a line of text

                Console.WriteLine("Creating text file...");

                foreach (TrainingData testPoint in testData)
            {
                // set net's input neurons to test input
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        //load point to corresponding input neuron
                        double value = testPoint.X[i * 8 + j];
                        inputNeurons[i, j].Value = value;
                    }
                }

                // print output

                sw.WriteLine("Out X = " + outX.Value.ToString());
                sw.WriteLine("Out O = " + out0.Value.ToString());
                sw.WriteLine();
            }

                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Text File Created");
            }

        }

        private static List<TrainingData> GetInputFiles(string directory)
        {
            // train the net
            List<TrainingData> data = new List<TrainingData>();

            /* using your very own training data that you create */
            // read in all the input training files
            foreach (string fname in Directory.EnumerateFiles(directory))
            {
                InputFile file = InputFile.Read(fname);
                data.Add(file.GetTrainingData());
            }

            return data;

        }

    }
}
