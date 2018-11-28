using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpWhiteboardProblems
{
    class Chap0
    {
        static void Main(string[] args)
        {
            List<int> myList = new List<int>();


            //Let add random numbers to the list
            Console.Write("How many elements do you want in the List: ");
            int size;
            size = int.Parse(Console.ReadLine());

            //Create a random object
            Random rand = new Random();
            for (int i = 0; i < size; i++) {
                int num = rand.Next(1, 21);
                myList.Add(num);
            }

            Console.WriteLine("\nThis is the list that was generated: ");
            foreach (int num in myList)
                Console.Write(num + "  ");



            Console.WriteLine("\n\nWhat element should we remove from the list: " );
            int val = int.Parse(Console.ReadLine());

            removeElement(myList, val);

            Console.WriteLine("\nThe current list is now: ");
            foreach(int num in myList)
                Console.Write(num + "  ");



            Console.WriteLine("\n\nWe will reverse the list:");
            reverseList(myList);
            foreach (int num in myList)
                Console.Write(num + "  ");


            Console.Write("\n\nTesting the target sum function\nWhat is the target sum: ");
            int target = int.Parse(Console.ReadLine());
            targetSum(myList, target);

            Console.WriteLine("\n\nTesting the valid parentheses function: ");
            Console.WriteLine("\"[{()}{()}]\" : " + validParentheses("[{()}{()}]") );
            Console.WriteLine("\"[{()()}]\" : " + validParentheses("[{()()}]"));
            Console.WriteLine("\"[{(}{)}]\" : " + validParentheses("[{(}{)}]"));
            Console.WriteLine("\"()()\" : " + validParentheses("()()") );
            Console.WriteLine("\"\" : " + validParentheses(""));
            Console.WriteLine("\"(]\" : " + validParentheses("(]"));


            Console.WriteLine("\n\nTesting the recursive sum function(n + n-1 + ... + 2 + 1)");

            Console.WriteLine("10 : " +  recursiveSum(10));
            Console.WriteLine("8 : " + recursiveSum(8));
            Console.WriteLine("5 : " + recursiveSum(5));
            Console.WriteLine("4 : " + recursiveSum(4));
            Console.WriteLine("0 : " + recursiveSum(0));
            Console.WriteLine("-3 : " + recursiveSum(-3));





            Console.ReadLine();

        }



        //Remove all the instances of a specific element from a list
        static void removeElement(List<int> myList, int num) {
            // Check if the list is empty
            if (myList.Count == 0) 
                return;

            int k = 0;

            for (int i = 0; i < myList.Count; i++) {
                if (myList[i] == num) {

                    myList.RemoveAt(i);
                    --i;
                }
            }
            return;
        }

        //Find the index of two elements that will give us the target sum
        //Solve in O(n) runtime
        static bool targetSum(List<int> myList, int target) {
            if (myList.Count == 0) {
                Console.WriteLine("No Solution was found");
                return false;
            }

            Dictionary<int,int> myMap = new Dictionary<int,int>(); 

            for (int i = 0; i < myList.Count; i++) {
                int complement = target - myList[i];


                if (myMap.ContainsKey(complement)) {
                    //We found the solution
                    int m = myMap[complement];
                    int n = i;
                    Console.WriteLine("Found solution at: i:" +m+ "  j:" + n );
                    return true;
                }

                myMap[myList[i]] = i;

            }
            Console.WriteLine("No Solution was found");

            return false;
        }

        //Do we have valid opening and closing parentheses,
        //Ex: valid:  {}{}()[][], valid : {[()]}, invalid: ({[}])
        static bool validParentheses(string word) {
            if (word.Length == 0)
                return false;

            //We need a stack to solve this problem
            Stack<int> myStack = new Stack<int>();

            foreach (char ch in word) {

                if (ch == '{' || ch == '(' || ch == '[')
                    myStack.Push(ch);
                else if (myStack.Count == 0)
                    return false;
                else if (ch == '}' && myStack.Peek() == '{' ||
                    ch == ')' && myStack.Peek() == '(' ||
                    ch == ']' && myStack.Peek() == '[')
                    myStack.Pop();
                else
                    return false;
            }
            //We have a valid string.
            return true;

        }


        //Recursive sum: n + n-1 + n-2 + .... + 2 + 1
        //General Formula:  n(n+1) / 2
        static int recursiveSum(int n) {
            if (n <= 0)
                return 0;
            else
                return n + recursiveSum(n - 1);
        }

        //Reverse the elements of a list
        static void reverseList(List<int> myList) {
            if (myList.Count == 0)
                return;

            int i = 0;
            int j = myList.Count - 1;

            while (i < j) {
                int temp = myList[i];
                myList[i] = myList[j];
                myList[j] = temp;

                ++i;
                --j;
            }
            return;
        }


        //Is the given number a prime number?
        static bool isPrime(int num) {
            if (num < 2)
                return false;

            for (int i = 2; i <= Math.Sqrt(num); i++) {
                if (num % i == 0)
                    return false;
            }
            return true;
        }


        // a^(b) Power Algorithm
        static int power(int a, int b) {
            if (b == 0)
                return 1;
            else
                return a * power(a, b - 1);
        }

        // a % b Mod Algorithm 
        static int mod(int a, int b) {
            if (b <= 0)
                return -1;

            int div = a / b;
            int rem = a - (div * b);
            return rem;
        }

        // a / b Division Algorithm
        static int division(int a, int b) {
            if (b <= 0)
                return -1;

            int sum = b;
            int counter = 0;
            while (sum <= a) {
                sum = sum + b;
                ++counter;
            }
            return counter;
        }


        //GIven a number, get the sum of every single digit
        static int sumDigits(int num) {

            int sum = 0;
            int digit;
            while (num > 0) {
                digit = num % 10;
                sum = sum + digit;

                num = num / 10;
            }
            return sum;
        }

        //Get the number of common elements between the two given lists
        static int numCommon(List<int> a, List<int> b) {
            if (a.Count == 0 || b.Count == 0)
                return 0;

            Dictionary<int, int> myMap = new Dictionary<int, int>();

            //Get the frequency of every number from List a
            for (int i = 0; i < a.Count; i++) {
                myMap[a[i]] += 1;
            }

            //Traverse List b
            int counter = 0;
            for (int i = 0; i < b.Count; i++) {
                if (myMap.ContainsKey(b[i]) && b[i] > 0) {
                    ++counter;
                    myMap[b[i]] -= 1;
                }
            }
            return counter;
        }


    }
}

