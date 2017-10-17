using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST_407_AI___Assignment_3
{
    class Synapse
    {
        public Neuron Axon { get; set; }        // output
        public Neuron Dentrite { get; set; }    // input
        public double Weight { get; set; }      // weight
        public double dW { get; set; }          // for training only

    }
}
