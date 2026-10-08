using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exFila
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<int> filaNum = new Queue<int>();
            filaNum.Enqueue(6);
            filaNum.Enqueue(12);
            filaNum.Enqueue(9);
            filaNum.Enqueue(5);
            mostraFila(filaNum);

            Console.WriteLine("Primeiro da fila: {0}", filaNum.Peek());
            Console.WriteLine("Primeiro da fila: {0}", filaNum.First());
            Console.WriteLine("Último da fila: {0}", filaNum.Last());
            Console.WriteLine("2º elemento da fila: {0}", filaNum.ElementAt(1));
            Console.WriteLine("Desenfileirou: {0}", filaNum.Dequeue());
            mostraFila(filaNum);

            Console.WriteLine("Primeiro da fila: {0}", filaNum.Peek());
            Console.WriteLine("Primeiro da fila: {0}", filaNum.First());
            Console.WriteLine("Último da fila: {0}", filaNum.Last());
            Console.WriteLine("2º elemento da fila: {0}", filaNum.ElementAt(1));
            Console.WriteLine("Desenfileirou: {0}", filaNum.Dequeue());
            mostraFila(filaNum);


        }

        static void mostraFila(Queue<int> fn)
        {
            Console.WriteLine("==============================");
            Console.WriteLine("Qtde de elementos na fila: {0}", fn.Count);
            Console.WriteLine("==============================");
            foreach (int i in fn)
            {
                Console.WriteLine("Elemento: {0}", i);
            }
            Console.WriteLine("==============================");
        }
    }
}
