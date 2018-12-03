using System;
using System.Collections.Generic;

namespace CSharpWhiteboardProblems
{
    class chap10
    {
        static void Main(string[] args)
        {
            Console.Write("How large will the List be: ");
            int size = int.Parse(Console.ReadLine());

            Random rand = new Random();
            List<int> original = new List<int>();
            List<int> originalTwo = new List<int>();
            List<int> originalThree = new List<int>();

            for (int i = 0; i < size; i++) { 
                int num = rand.Next(1, 51);
                original.Add(num);
                originalTwo.Add(num);
                originalThree.Add(num);

            }

            Console.WriteLine("\n\nOriginal List:");
            displayList(original);
            Console.WriteLine("\nAfter Selection Sort:");
            //Apply selection sort to our list
            selectionSort(original);
            displayList(original);

            Console.WriteLine("\n\nOriginal List:");
            displayList(originalTwo);
            Console.WriteLine("\nAfter Bubble Sort:");
            //Apply bubble sort to our list
            bubbleSort(originalTwo);
            displayList(originalTwo);

            Console.WriteLine("\n\nOriginal List:");
            displayList(originalThree);
            Console.WriteLine("\nAfter Quick Sort:");
            //Apply quick sort to our list
            quickSort(originalThree, 0, originalThree.Count - 1);
            displayList(originalThree);

            Queue<int> a = new Queue<int>();
            Queue<int> b = new Queue<int>();
            Queue<int> c = new Queue<int>();

            a.Enqueue(3);
            a.Enqueue(54);
            a.Enqueue(77);
            a.Enqueue(544);
            a.Enqueue(999);
            a.Enqueue(55555);

            b.Enqueue(3);
            b.Enqueue(23);
            b.Enqueue(25);
            b.Enqueue(233);
            b.Enqueue(777);

            Console.WriteLine("\n\nWe are now going to work with two sorted queues:");
            displayQueue(a);
            Console.WriteLine();
            displayQueue(b);
            Console.WriteLine("\nWe apply a recursive algorithm and we get: ");
            recursiveMerge(a, b, c);
            displayQueue(c);          

            Console.WriteLine("\n\n");
        }

        static void displayList(List<int> myList){
            foreach(int num in myList)
                Console.Write(num + "  ");
        }

        static void displayQueue(Queue<int> myQueue) {
            Queue<int> temp = new Queue<int>();

            while (myQueue.Count != 0)
            {
                int number = myQueue.Dequeue();
                temp.Enqueue(number);
                Console.Write(number + "  ");
            }

            while (temp.Count != 0)
            {
                int number = temp.Dequeue();
                myQueue.Enqueue(number);
            }
        }


        //We are passing by reference, given that we are dealing with an object parameter
        static void selectionSort(List<int> myList) {

            if (myList.Count == 0)
                return;

            for (int i = 0; i < myList.Count -1 ; i++) {
                int min = myList[i];
                int minIndex = i;

                for (int j = i + 1; j < myList.Count; j++) {
                    if (min > myList[j]) {
                        min = myList[j];
                        minIndex = j;
                    }
                }

                //We need to do the swap
                myList[minIndex] = myList[i];
                myList[i] = min;
            }
        }


        static void bubbleSort(List<int> myList) {
            if (myList.Count == 0)
                return;

            bool swapFlag;

            for (int i = 0; i < myList.Count - 1; i++) {

                swapFlag = false;

                for (int j = 0; j < myList.Count - 1 - i; j++) {

                    if (myList[j] > myList[j + 1]) {
                        int temp = myList[j];
                        myList[j] = myList[j + 1];
                        myList[j + 1] = temp;
                        swapFlag = true;
                    }

                }
                if (swapFlag == false)
                    return;
            }
        }

        static void quickSort(List<int> myList, int left, int right) {

            if (left < right) {
                int pi = partition(myList, left, right);
                quickSort(myList, left, pi - 1);
                quickSort(myList, pi + 1, right);
            }
        }

        static int partition(List<int> myList, int left, int right) {
            int pivot = myList[right];
            int i = left - 1;

            for (int j = left; j < right; j++) {
                if (myList[j] <= pivot) {
                    ++i;
                    int tempo = myList[i];
                    myList[i] = myList[j];
                    myList[j] = tempo;
                }
            }
            int temp = myList[i + 1];
            myList[i + 1] = myList[right];
            myList[right] = temp;
            return i + 1;
        }

        //Recursively merge two queues
        //Note that the two original queues are both sorted.
        static void recursiveMerge(Queue<int> a, Queue<int> b, Queue<int> c) {
            if (a.Count == 0)
            {
                while (b.Count != 0)
                {
                    int num = b.Dequeue();
                    c.Enqueue(num);
                }
            }
            else if (b.Count == 0)
            {
                while (a.Count != 0)
                {
                    int num = a.Dequeue();
                    c.Enqueue(num);
                }
            }
            else {
                if (a.Peek() < b.Peek())
                {
                    int val = a.Dequeue();
                    c.Enqueue(val);
                    recursiveMerge(a, b, c);
                }
                else {
                    int val = b.Dequeue();
                    c.Enqueue(val);
                    recursiveMerge(a, b, c);
                }
            }
        }




    }//End of class
}

