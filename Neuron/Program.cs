using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuron
{
    
    class Program
    {
        public class Neuron
        {
            private decimal weight = 0.5m;
            public decimal LastError { get; private set; }
            public decimal Smoothing { get; set; } = 0.0001m;
            public decimal ProcessInputData(decimal input)
            {
                return input * weight;
            }
            public decimal RestoreInputData(decimal output)
            {
                return output / weight;
            }
            public void Train(decimal input, decimal expectedResult)
            {
                var actualResult = input * weight;
                LastError = expectedResult - actualResult;
                var correction = (LastError / actualResult) * Smoothing;
                weight += correction;
            }
        }
        static void Main(string[] args)
        {
            decimal usd = 1;
            decimal rub = 74.09m;

            Neuron neuron = new Neuron();
            int i = 0;
            do
            {
                i++;
                neuron.Train(usd, rub);
                if (i% 10000 == 0)
                {
                    Console.WriteLine($"Iteration: {i}\tError:\t{neuron.LastError}");
                }
            } while (neuron.LastError > neuron.Smoothing || neuron.LastError < -neuron.Smoothing);

            Console.WriteLine("Training is over");

            Console.WriteLine($"{Decimal.Round(neuron.ProcessInputData(100),4)} rub in {100} usd");

            Console.WriteLine($"{Decimal.Round(neuron.ProcessInputData(541),4)} rub in {541} usd");

            Console.WriteLine($"{Decimal.Round(neuron.RestoreInputData(10), 4)} usd in {10} rub");
        }
    }
}
