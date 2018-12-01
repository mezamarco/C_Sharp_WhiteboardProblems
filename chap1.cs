using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpWhiteboardProblems
{
    class chap1
    {
        static void Main(string[] args)
        {

            Console.Write("How large will the list be: ");
            int size = int.Parse(Console.ReadLine());

            //We need a random object
            Random rand = new Random();
            SortedSet<int> mySet = new SortedSet<int>();

            List<int> myList = new List<int>();

            for (int i = 0; i < size; i++) {
                //Range of random element: [1,20]
                int num = rand.Next(1, 21);
                myList.Add(num);
                mySet.Add(num);
            }

            printList(myList);
            
            Console.WriteLine("\n\nTesting Binary Search");
            for (int i = 1; i < 21; i++)
                Console.WriteLine("Searching for: " + i + " -> " + binarySearch(mySet, i));
            Console.WriteLine("\n\n");
            string strA = "Hello";
            string strB = "Marco";
            string strC = "Thank You";
            Console.WriteLine("All Unique Characters:");
            Console.WriteLine(strA + ": " + checkUnique(strA));
            Console.WriteLine(strB + ": " + checkUnique(strB));
            Console.WriteLine(strC + ": " + checkUnique(strC));
            Console.WriteLine("\n\n");
            string strD = "dnjask";
            string strE = "dnjaks";
            string strF = "dnjqwk";
            string strG = "dnfqwk";
            string strH = "dfqwk";

            Console.WriteLine("Permutation: " + checkPermutation(strD,strE));
            Console.WriteLine("Permutation: " + checkPermutation(strF, strE));
            Console.WriteLine("\n\n");
            Console.WriteLine("One Away: " + oneAway(strD, strE));
            Console.WriteLine("One Away: " + oneAway(strF, strG));
            Console.WriteLine("One Away: " + oneAway(strH, strG));
            Console.WriteLine("\n\n");
            string strI = "meza marco . com";
            Console.WriteLine(urlify(strI));
            Console.WriteLine("\n\n");
            string strJ = "Car";
            getPermutation(strJ);
            Console.WriteLine("\n\n");
            string strK = "aaaaabbbbbeeeewwz";
            string strL = "abcdefff";
            Console.WriteLine("Compressing:" + strK  + " -> " + stringCompression(strK));
            Console.WriteLine("Compressing:" + strL + " -> " + stringCompression(strL));

            Console.WriteLine("\n\n");
            Console.WriteLine("Reverse string:" + strK + " -> " + stringReverse(strK));
            Console.WriteLine("Reverse string:" + strL + " -> " + stringReverse(strL));
            Console.WriteLine("\n\n");

            string sentence = "Happy Holidays Marco!";
            string sentenceTwo = "Feliz Navidad        Nemo  ds";
            Console.WriteLine(sentence + "->" + reverseWordSentence(sentence));
            Console.WriteLine(sentenceTwo + "->" + reverseWordSentence(sentenceTwo));


            Console.WriteLine("\n" +
                "");
            int[,] matrix = new int[5,5];

            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    matrix[i, j] = i + j;
                }
            }
            printMatrix(matrix);

            //Rotate the given matrix
            Console.WriteLine("\n\n\nWe are now rotating the matrix 90deg(Clockwise):");
            rotateMatrix(matrix, 5);
            printMatrix(matrix);
            Console.WriteLine("\n\n");

            Console.WriteLine("Zero out the row and column were we find a zero:");
            zeroMatrix(matrix);
            printMatrix(matrix);
            Console.WriteLine("\n\n");

            Console.ReadLine();

        }//End of main function







	//Can we find the given elmement in the given array?
        //Run time: 0(log n)
        static bool binarySearch(SortedSet<int> mySet, int num) {
            if (mySet.Count == 0)
                return false;

            int left = 0;
            int right = mySet.Count - 1;

            while (left <= right) {

                int mid = (left + right) / 2;

                if (mySet.ElementAt(mid) == num)
                    return true;
                else if (mySet.ElementAt(mid) < num)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return false;
        }


        //Does the given string have all unique characters?
        static bool checkUnique(string word){
            if (word.Length == 0)
                return true;

            Dictionary<char, int> myMap = new Dictionary<char, int>();

            for (int i = 0; i < word.Length; i++) {
                if (myMap.ContainsKey(word[i]))
                    return false;
                else
                    myMap[word[i]] = i; 
            }
            return true;
        }

        //Is one string a permutation of the other string?
        static bool checkPermutation(string a, string b) {

            if (a.Length != b.Length)
                return false;

            Dictionary<char, int> myMap = new Dictionary<char, int>();

            //Get the frequency of all the character of string a
            foreach (char ch in a) {
                if (!myMap.ContainsKey(ch)) {
                    myMap[ch] = 1;
                }
                else {
                    myMap[ch] += 1;
                }
            }

            //Traverse string b
            for (int i = 0; i < b.Length; i++) {
                if (myMap.ContainsKey(b[i]))
                {
                    if (b[i] == 0)
                        return false;
                    else
                        myMap[b[i]] -= 1;
                }
                else {
                    return false;
                }
            }

            return true;
        }



        //If the string has a space character, replace it with '%20'
        static string urlify(string word)
        {
            if (word.Length == 0)
                return "";

            string ans = "";

            foreach (char ch in word)
            {
                if (ch == ' ')
                    ans = ans + "%20";
                else
                    ans = ans + ch;
            }
            return ans;
        }



        //Is string a one step away from string b. 
        //A step is done by: {replacing a character, inserting a character, or removing a character}
        static bool oneAway(string a, string b)
        {

            //The valid difference in length is: {-1,0,1}
            int delta = a.Length - b.Length;

            if (delta < -1 || 1 < delta)
                return false;

            //Deal with the case where they both have the same length
            if (delta == 0)
            {
                //Allow only zero or one unmatching elements
                bool flag = false;

                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i])
                    {
                        if (flag == true)
                            return false;
                        else
                            flag = true;
                    }
                }
                return true;
            }
            else
            {
                //We need to organize to only treat this case as insertion of an element
                string smaller = delta > 0 ? b : a;
                string larger = delta > 0 ? a : b;

                int i = 0;
                int j = 0;

                while (i < smaller.Length)
                {

                    if (smaller[i] != larger[j])
                    {
                        if (i != j)
                            return false;
                        else
                            ++j;
                    }
                    else
                    {
                        ++i;
                        ++j;
                    }

                }

                return true;
            }
        }



        //Print all the permutations of a string, we will need a use a helper function.
        static void getPermutation(string word)
        {
            getPermutation(word, "");
        }
        //Helper function
        static void getPermutation(string word, string prefix)
        {

            if (word.Length == 0)
                Console.WriteLine(prefix);
            else
            {
                for (int i = 0; i < word.Length; i++)
                {
                    string rem = word.Substring(0, i) + word.Substring(i + 1);
                    getPermutation(rem, prefix + word[i]);
                }
            }
        }



        //Compress to this format:  "aabcccccaaa" -> "a2b1c5a3"
        static string stringCompression(string word)
        {
            if (word.Length == 0)
                return "";

            string ans = "";

            for (int i = 0; i < word.Length; i++)
            {

                char ch = word[i];
                int count = 1;

                if (i != word.Length - 1)
                {
                    while (word[i + 1] == ch)
                    {
                        ++count;
                        ++i;

                        if (i == word.Length - 1)
                            break;
                    }
                }

                //int to string
                string strCount = count.ToString();
                ans = ans + ch + strCount;
            }
            return ans;

        }


        //Reverse a string(Strings in C# are immutable objects(They are readonly))
        static string stringReverse(string a)
        {

            if (a.Length == 0)
                return "";

            //We need to initialze a stringbuilder object
            //StringBuilder objects are not immutable
            StringBuilder sb = new StringBuilder(a);

            int i = 0;
            int j = a.Length - 1;

            while (i < j)
            {
                char temp = sb[i];
                sb[i] = sb[j];
                sb[j] = temp;

                ++i;
                --j;
            }

            //Convert our give StringBuilder object into a string
            string ans = sb.ToString();
            return ans;
        }


        //Reverse the words and of a given sentence.
        //The space bar tells us that we have a word.
        static string reverseWordSentence(string sentence) {
            if (sentence.Length == 0)
                return sentence;

            string ans = "";
            //We need to traverse the sentece character by character
            //Two cases: ends with a space character or ends any other character.
            string word = "";

            for (int i = 0; i < sentence.Length; i++) {

                if (i == sentence.Length - 1 && sentence[i] != ' ') {
                    word = word + sentence[i];
                    string rev = stringReverse(word);

                    ans = ans + rev;
                    return ans;
                }

                if (sentence[i] == ' ') {
                    string rev = stringReverse(word);
                    rev = rev + " ";
                    ans = ans + rev;
                    word = "";
                }
                else {
                    word = word + sentence[i];
                }
            }

           
            return ans;
        }


        //Print a matrix
        static void printMatrix(int[,] twoDimArr)
        {
            for (int i = 0; i < twoDimArr.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < twoDimArr.GetLength(1); j++)
                {
                    Console.Write(twoDimArr[i, j] + "  ");
                }
            }
        }

        //Rotate a matrix by 90 degrees to the right.
        static void rotateMatrix(int[,] myMatrix, int n)
        {
            //We will rotate the matrix value by value from the outside squares into the inside.
            //How many inner squares we will rotate. Just divide by two. 
            for (int layer = 0; layer < n / 2; layer++)
            {
                //Determine the bounds
                int first = layer;
                int last = n - 1 - layer;

                //We are ready to switch all the values, 
                //iterate thorugh all the elements of the given layer
                for (int i = first; i < last; i++)
                {
                    int offset = i - first;

                    //Save the top, so now overwrite the top
                    int top = myMatrix[first, i];
                    //left to top 
                    myMatrix[first, i] = myMatrix[last - offset, first];
                    //bottom to left 
                    myMatrix[last - offset, first] = myMatrix[last, last - offset];
                    //right to bottom
                    myMatrix[last, last - offset] = myMatrix[i, last];
                    //top to right 
                    myMatrix[i, last] = top;

                }
            }
        }

        //If you find a zero, make the given row and column all zeros
        static void zeroMatrix(int[,] matrix)
        {
            bool[] row = new bool[matrix.GetLength(0)];
            bool[] column = new bool[matrix.GetLength(1)];

            //Traverse the entire matrix
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        row[i] = true;
                        column[j] = true;
                    }
                }
            }

            for (int m = 0; m < row.Length; m++)
            {
                if (row[m] == true)
                {
                    for (int n = 0; n < column.Length; n++)
                    {
                        matrix[m, n] = 0;
                    }
                }
            }


            for (int m = 0; m < column.Length; m++)
            {
                if (column[m] == true)
                {
                    for (int n = 0; n < row.Length; n++)
                    {
                        matrix[n, m] = 0;
                    }
                }
            }
        }

        static void printList(List<int> myList) {
            Console.WriteLine("Printing List:");
            foreach (int num in myList) {
                Console.Write( num + " ");
            }
        }

    }//End of class
}

