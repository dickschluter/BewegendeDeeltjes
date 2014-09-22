using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BewegendeDeeltjes
{
    class MinPriorityQueue
    {
        Botsing[] heap; // onderliggende array, index 0 is wisselvariabele
        int capaciteit; // max aantal items in array
        public int N; // heapgrootte /////////////////////////////////////////////// uiteindelijk private

        public bool BevatItem
        {
            get { return N > 0; }
        }

        public float Minimum
        {
            get { return N > 0 ? heap[1].Tijd : 0; }
        }

        public MinPriorityQueue(int capaciteit)
        {
            this.capaciteit = capaciteit;
            heap = new Botsing[capaciteit + 1];
        }

        public void Invoegen(Botsing item)
        {
            if (N == capaciteit) // als vol dan verdubbel array
            {
                capaciteit *= 2;
                Botsing[] grotereArray = new Botsing[capaciteit + 1];
                for (int i = 1; i <= N; i++)
                    grotereArray[i] = heap[i];
                heap = grotereArray;
            }
            
            N++;
            int index = N, ouder = index / 2;
            while (index > 1 && heap[ouder] > item)
            {
                heap[index] = heap[ouder];
                index = ouder;
                ouder = index / 2;
            }
            heap[index] = item;
        }

        public Botsing WisMinimum()
        {
            if (N == 0)
                return null;

            Botsing minimum = heap[1];
            heap[0] = heap[N];
            N--;

            int index = 1, linker = 2, rechter = 3;
            while ((linker <= N && heap[linker] < heap[0]) || (rechter <= N && heap[rechter] < heap[0]))
            {
                if (rechter > N || heap[linker] < heap[rechter]) // linker kind promoveren
                {
                    heap[index] = heap[linker];
                    index = linker;
                }
                else // rechter kind promoveren
                {
                    heap[index] = heap[rechter];
                    index = rechter;
                }
                linker = index * 2;
                rechter = linker + 1;
            }
            heap[index] = heap[0];

            return minimum;
        }

        public void Legen()
        {
            for (int i = 0; i < heap.Length; i++)
                heap[i] = null;
            N = 0;
        }
    }
}
