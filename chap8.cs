using System;

namespace CSharpWhiteboardProblems
{
    class chap8.cs
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Slow Fib(30) : " + fibSlow(30));
            Console.WriteLine("Fast Fib(30) : " + fib(30));

            Console.WriteLine("\nSlow stepCount(30) : " + slowStepCount(30) );
            Console.WriteLine("Fast stepCount(30) : " + stepCount(30));


            int[] arr = { -2,-1, 0,1,2,3,6,19 };
            Console.WriteLine("\n\nSlow magicIndex: " + slowMagicIndex(arr));
            Console.WriteLine("Fast magicIndex : " +  magicIndex(arr));

            int[] coinsArr = { 25, 10, 5, 1 };
            Console.WriteLine("\n\nMake change with the minimum amount of coins(22): " + minCoinsMakeChange(coinsArr,22) );
            Console.WriteLine("Make change with the minimum amount of coins(34): " + minCoinsMakeChange(coinsArr, 34));
            Console.WriteLine("Make change with the minimum amount of coins(99): " + minCoinsMakeChange(coinsArr, 99));

            Console.WriteLine("\n\nCount all the ways to make change(2): " + countAllMakeChange(2));
            Console.WriteLine("\n\nCount all the ways to make change(5): " + countAllMakeChange(5));
            Console.WriteLine("Count all the ways to make change(78): " + countAllMakeChange(78));
            Console.WriteLine("Count all the ways to make change(99): " + countAllMakeChange(99));
            Console.WriteLine("Count all the ways to make change(100): " + countAllMakeChange(100));

            Console.WriteLine("\n");

        }


        static int fibSlow(int n)
        {
            if (n <= 0)
                return 0;
            else if (n == 1)
                return 1;
            else
                return fibSlow(n - 1) + fibSlow(n - 2);
        }

        //Lets use dynamic programming for the fibonacci sequeunce
        static int fib(int n) {

            if (n <= 0)
                return 0;

            int[] arr = new int[n + 1];
            arr[0] = 0;
            arr[1] = 1;

            for (int i = 2; i <= n; i++)
                arr[i] = arr[i - 1] + arr[i - 2];

            return arr[n];
        }



        //THe child can hop 1 step, 2 steps, or 3 steps.
        //Write a function that can count the number of ways that the child can go up the stairs.
        static int slowStepCount(int n) {
            if (n < 0)
                return 0;
            else if (n == 0)
                return 1;
            else
                return stepCount(n - 1) + stepCount(n - 2) + stepCount(n - 3);               
        }

        //Lets use dynamic programming for the count steps function
        static int stepCount(int n) {

            if (n < 0)
                return 0;

            int[] arr = new int[n + 1];

            //We need to make every single element be -1.
            for (int i = 0; i <= n; i++)
                arr[i] = -1;

            return stepCount(n, arr);
        }

        static int stepCount(int n, int[] arr) {
            if (n < 0)
                return 0;
            else if (n == 0)
                return 1;
            else if (arr[n] > -1)
                return arr[n];
            else {
                arr[n] = stepCount(n - 1, arr) + stepCount(n - 2, arr) + stepCount(n - 3,arr);
                return arr[n];
            }
        }


        //Find the magic index, arr[i] = i
        //There is only one and the array is sorted
        static int slowMagicIndex(int[] arr) {
            //Simply traverse the array and find the answer
            for (int i = 0; i < arr.Length; i++) {
                if (arr[i] == i)
                    return i;
            }
            //Error
            return -1;
        }


        //Find the magic index, arr[i] = i
        //There is only one and the array is sorted
        static int magicIndex(int[] arr)
        {
            //Instead of simply traversing the arr,
            //we will do something similar to binary search.
            return magicIndex(arr, 0, arr.Length - 1);
        }
        static int magicIndex(int[] arr, int left, int right) {

            if (left > right)
                return -1;

            int mid = (left + right) / 2;

            if (mid == arr[mid])
                return mid;
            else if (arr[mid] < mid)
                return magicIndex(arr, mid + 1, right);
            else
                return magicIndex(arr, left, mid - 1);
        }


        //Make change with the least amount of coins
        static int minCoinsMakeChange(int[] coinsVect, int n) {

            if (coinsVect.Length == 0 || n < 1)
                return -1;

            int[] answerVect = new int[n + 1];
            answerVect[0] = 0;

            for (int i = 1; i <= n; i++) {
                int minCoins = Int32.MaxValue;

                foreach (int coin in coinsVect) {
                    if (i - coin >= 0) {
                        int currentMinCoins = answerVect[i - coin] + 1;
                        if (currentMinCoins < minCoins)
                            minCoins = currentMinCoins;
                    }
                }
                answerVect[i] = minCoins;
            }
            return answerVect[n];
        }

        //Count all the possible ways to make change
        static int countAllMakeChange(int n) {
            int[] denomsArr = { 25, 10, 5, 1 };
            //To solve this problem we need a 2D array
            int[,] arr = new int[n + 1, denomsArr.Length];
            return countAllMakeChange(n, denomsArr, 0, arr);
        }

        static int countAllMakeChange(int amount, int[] denoms, int index, int[,] arr) {
            if (arr[amount, index] > 0)
                return arr[amount, index];

            if (index >= denoms.Length-1)
                return 1;

            int demonsAmount = denoms[index];
            int ways = 0;

            for (int i = 0; i * demonsAmount <= amount; i++) {
                int amountRemaining = amount - i * demonsAmount;
                ways = ways + countAllMakeChange(amountRemaining, denoms, index + 1, arr);
            }

            arr[amount, index] = ways;
            return ways;
        }


    }//End of class
}


