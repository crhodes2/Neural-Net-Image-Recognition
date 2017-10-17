using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST_407_AI___Assignment_3
{
    class Net
    {
        public Net()
        {
            inputNeurons = new List<Neuron>();
            outputNeurons = new List<Neuron>();
            synapses = new List<Synapse>();
        }

        private List<Neuron> inputNeurons;
        private List<Neuron> outputNeurons;
        private List<Synapse> synapses;

        public void AddInput(Neuron n) { inputNeurons.Add(n);}
        public void AddOutput(Neuron n) { outputNeurons.Add(n); }

        public void Connect(Neuron from, Neuron to, double weight)
        {
            Synapse s = new Synapse();
            s.Axon = from;
            s.Dentrite = to;
            s.Weight = weight;
            synapses.Add(s);
        }

        public Synapse GetSynapse(Neuron from, Neuron to)
        {
            foreach (Synapse s in synapses)
            {
                if (s.Axon == from && s.Dentrite == to)
                    return s;
            }
            return null;
        }

        public void Evaluate()
        {
            foreach (Neuron outNeuron in outputNeurons)
            {
                double value = 0.0;
                foreach (Neuron inNeuron in inputNeurons)
                {
                    Synapse s = GetSynapse(inNeuron, outNeuron);
                    value += s.Weight * inNeuron.Value;
                }
                outNeuron.Value = value;
            }
        }

        public void Train(TrainingData[] data)
        {
            // set weights random
            Random r = new Random(0);
            foreach (Synapse s in synapses)
            {
                s.Weight = r.NextDouble() * 2 - 1.0;  // value between -1.0 and 1.0
            }

            // minimize the error
            double learningRate = 0.01;
            double precision = 0.01;
            double lastError;
            double currentError = double.MaxValue;
            do
            {
                lastError = currentError;
                currentError = 0.0;
                foreach (Synapse s in synapses)
                    s.dW = 0.0;

                // for each training point...
                foreach (TrainingData d in data)
                {
                    // for each output neuron...
                    for (int j = 0; j < outputNeurons.Count; j++)
                    {
                        // calculate Yj from inputNeurons and weights
                        outputNeurons[j].Value = 0.0;
                        for (int i = 0; i < inputNeurons.Count; i++)
                        {
                            Synapse s = GetSynapse(inputNeurons[i], outputNeurons[j]);
                            outputNeurons[j].Value += s.Weight * d.X[i];
                        }

                        // determine error contribution from this output node and training point
                        currentError += Math.Pow(d.T[j] - outputNeurons[j].Value, 2.0);

                        // determine weight gradient for each synapse
                        for (int i = 0; i < inputNeurons.Count; i++)
                        {
                            Synapse s = GetSynapse(inputNeurons[i], outputNeurons[j]);
                            s.dW += (d.T[j] - outputNeurons[j].Value) * d.X[i];
                        }
                    }
                }

                // update error for number of training points
                currentError /= data.Length;

                // adjust weights
                foreach (Synapse s in synapses)
                    s.Weight += learningRate * s.dW;
            }
            while (Math.Abs(currentError - lastError) > precision);
        }

    }
}
