using Sytem;
using System.Collections.Generic;
using System.Text;


namespace CSharpWhiteboardProblems
{
    class chap5
    {
       

        static void Main(string[] args)
        {
            int zeroInt = 0;
            int num = 25;
            int numTwo = 33;
            int maxInt = (int)Math.Pow(2, 31) - 1;

            Console.Write("0 -> ");
            printInBits(zeroInt);
            Console.Write("\n25 -> ");
            printInBits(num);
            Console.Write("\n33 -> ");
            printInBits(numTwo);
            Console.Write("\n" + ((int)Math.Pow(2,31)-1) + " -> ");
            printInBits(maxInt);

            Console.WriteLine("We are now going to place 1 in random positions:\n");
            Console.Write("0 -> ");
            printInBits(zeroInt);
            Console.Write("Placing 1 in position 5 -> "); 
            printInBits(setBit(zeroInt, 5, 1));
            Console.WriteLine(" -> " + setBit(zeroInt, 5, 1) );


            Console.WriteLine("We are now going to place 1 in random positions:\n");
            Console.Write("33 -> ");
            printInBits(numTwo);
            Console.Write("Placing 1 in position 7 -> ");
            printInBits(setBit(numTwo, 7, 1));
            Console.WriteLine(" -> " + setBit(numTwo, 7, 1));

            Console.WriteLine( bitEqual(num, 4, numTwo, 5));
            Console.WriteLine(bitEqual(num, 4, numTwo, 2) );


            int number = 99;
            int numberTwo = 26;

            Console.WriteLine("\n\nBinary sum: 99 + 26 = " + binarySum(number, numberTwo) );

            Console.WriteLine("\n\n");
            int valM = 5434325;
            int valN = 35432;

            insertion(valM, valN, 19, 0);
        }



        static bool getBit(int num, int i)
        {
            //We will get the bit at position
            //AND the number with (1<<i), that will gives us the bit
            num = num & (1 << i);
            //If the modified value is zero, then we have a zero, otherwise we have a one.
            if (num == 0)
                return false;
            else
                return true;
        }


        //Define the function that will set a bit for us
        static int setBit(int num, int pos, int bit)
        {
            //We need to use or, in order to modify the specific bit position

            //Move the bit into a the correct position
            int temp = (bit << pos);
            //We need to mask the original number by AND with ~(1<<i)
            int mask = ~(1 << pos);
            num = num & mask;

            //Now use OR in order to modify this specific bit position
            num = num | temp;

            //We have updated the original number, so we just need to return it
            return num;
        }


        static void printInBits(int number)
        {
            //Lets create a string for our bit representation
            string myBitRepresentation = "";

            for (int i = (sizeof(int) * 8) - 1; i >= 0; i--)
            {
                if (getBit(number, i))
                    myBitRepresentation = myBitRepresentation + "1";
                else
                    myBitRepresentation = myBitRepresentation + "0";
            }

            Console.WriteLine(myBitRepresentation + "\n");
        }


        static int clearBit(int num, int i)
        {
            //We just need to mask the specific bit position
            int mask = ~(1 << i);
            num = num & mask;
            return num;
        }


        static bool bitEqual(int numOne, int i, int numTwo, int j)
        {
            //Determine if we have equal bits in a given position
            //What is numOne, position i?
            int temp1 = numOne & (1 << i);
            int bitOne;

            if (temp1 == 0)
                bitOne = 0;
            else
                bitOne = 1;

            //What is in numTwo at j?
            int tempTwo = numTwo & (1 << j);
            int bitTwo;

            if (tempTwo == 0)
                bitTwo = 0;
            else
                bitTwo = 1;


            //Check bitOne and bitTwo
            if (bitOne == bitTwo)
                return true;
            else
                return false;
        }



        
        static int insertion(int M, int N, int j, int i)
        {
            //Lets print out the contents of M and N
            //Every integer is 4 bytes, therefore print all the 32 bits of the integers
            string mbits = "";
            string nbits = "";

            int nSize = 0;

            for (int k = 31; k >= 0; k--)
            {
                //Get every single digit and then make it into a character
                int bitM = getBit(M, k) == true ? 1 : 0;
                int bitN = getBit(N, k) == true ? 1 : 0;

                //Convert the integer into a characte and then add it to the string of bits
                if (bitM == 0)          
                    mbits = mbits + "0";           
                else
                    mbits = mbits + "1";
                

                if (bitN == 0)
                    nbits = nbits + "0";
                else { 
                    //For the case that we see a one, then we can determine that nSize is larger ,
                    //So we need to update the nSize.
                    if (nSize < k)    
                        nSize = k;
                    nbits = nbits + "1";
                }
            }

            Console.WriteLine("M: " +  mbits + "\nN: "+ nbits +"\n");
            //So how will we insert the Nbit into the Mbits
            Console.WriteLine( "The size of N is : " + nSize + "\n");
            //We need to insert nSize elements into the M.

            int temp = M;
            while (j >= i)
            {
                //Starting at position j, we need to insert all the way to position i
                //On M, we need to get to index j
                //On N, we need to get to index nSize
                int val = getBit(N, nSize) == true ? 1:0;
                Console.WriteLine("We got:" + val);
                temp = setBit(temp, j, val);
                --nSize;
                if (nSize < 0)
                    break;
                --j;
            }
            printInBits(temp);
            return 1;
        }


    

        static void integerToString(int val)
        {
            for (int i = 31; i >= 0; i--)
            {
                //Get under the correct position
                int temp = (val >> i);
                //Clear all the element except for the first digit
                temp = temp & 1;
                Console.Write(temp);
            }
        }

        


        //To get the sum, we will use recursion
        static int binarySum(int a, int b)
        {
            if (b == 0)
                return a;
           
            int partialSum = a ^ b;
            int carry = (a & b) << 1;

            return binarySum(partialSum, carry);
        }
        

    }//End of class
}




