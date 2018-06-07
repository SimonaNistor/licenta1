﻿using Microsoft.Extensions.Logging;
using NeuralNetworks.Library;
using NeuralNetworks.Library.Components.Activation;
using NeuralNetworks.Library.Data;
using NeuralNetworks.Library.Extensions;
using NeuralNetworks.Library.Logging;
using NeuralNetworks.Library.Training;
using NeuralNetworks.Library.Training.BackPropagation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Extensions
{
    public class Network
    {
        public static NeuralNetwork Functie()
        {
            ConfigureLogging();

            var neuralNetwork = NeuralNetwork.For(NeuralNetworkContext.MaximumPrecision)
                .WithInputLayer(neuronCount: 2, activationType: ActivationType.Sigmoid)
                .WithHiddenLayer(neuronCount: 5, activationType: ActivationType.TanH)
                .WithOutputLayer(neuronCount: 1, activationType: ActivationType.Sigmoid)
                .Build();

            //TrainingController
            //    .For(BackPropagation.WithConfiguration(
            //            neuralNetwork,
            //            ParallelOptionsExtensions.UnrestrictedMultiThreadedOptions,
            //            learningRate: 0.6,
            //            momentum: 0.9)
            //        ).TrainForEpochsOrErrorThresholdMet(GetXorTrainingData(), maximumEpochs: 5000, errorThreshold: 0.001)
            //    .GetAwaiter()
            //    .GetResult();

            //string s = MakeExamplePredictions(neuralNetwork);
            return neuralNetwork;
        }

        public static string Functie1(int f1, int f2)
        {
            ConfigureLogging();

            var neuralNetwork = NeuralNetwork.For(NeuralNetworkContext.MaximumPrecision)
                .WithInputLayer(neuronCount: 2, activationType: ActivationType.Sigmoid)
                .WithHiddenLayer(neuronCount: 5, activationType: ActivationType.TanH)
                .WithOutputLayer(neuronCount: 1, activationType: ActivationType.Sigmoid)
                .Build();

            //TrainingController
            //    .For(BackPropagation.WithConfiguration(
            //            neuralNetwork,
            //            ParallelOptionsExtensions.UnrestrictedMultiThreadedOptions,
            //            learningRate: 0.6,
            //            momentum: 0.9)
            //        ).TrainForEpochsOrErrorThresholdMet(GetXorTrainingData(), maximumEpochs: 5000, errorThreshold: 0.001)
            //    .GetAwaiter()
            //    .GetResult();

            string s = MakeExamplePredictions(neuralNetwork,f1,f2).ToString();
            return s;
        }

        private static void ConfigureLogging()
        {
            var logger = new LoggerFactory();

            logger
                .AddConsole(LogLevel.Information)
                .InitialiseLoggingForNeuralNetworksLibrary();
        }

        public static double MakeExamplePredictions(NeuralNetwork neuralNetwork, int valueKeywords, int valueLanguage)
        {
            double vK = valueKeywords/10;
            double vL = valueLanguage/100;
            StringBuilder s = new StringBuilder();
            s.Append($"{neuralNetwork.PredictionFor(new[] { vK, vL }, ParallelOptionsExtensions.SingleThreadedOptions)[0]}");
            //System.Console.WriteLine(
            //    $"PREDICTION (0, 1): {neuralNetwork.PredictionFor(new[] { 0.0, 1.0 }, ParallelOptionsExtensions.SingleThreadedOptions)[0]}, EXPECTED: 1");

            //if (Debugger.IsAttached) System.Console.ReadLine();

            //double result = { neuralNetwork.PredictionFor(new[] { vK, vL }, ParallelOptionsExtensions.SingleThreadedOptions)[0]}
            double result = Convert.ToDouble(s.ToString());
            return result;
        }

        private static List<TrainingDataSet> GetXorTrainingData()
        {
            var inputs = new[]
            {
                new[] {0.0, 0.0}, new[] {0.0, 1.0}, new[] {1.0, 0.0}, new[] {1.0, 1.0}
            };

            var outputs = new[]
            {
                new[] {0.0}, new[] {1.0}, new[] {1.0}, new[] {0.0}
            };

            return inputs.Select((input, i) => TrainingDataSet.For(input, outputs[i])).ToList();
        }
    }
}
