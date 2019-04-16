using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastermind
{
    //Guess object. This contains the code that has been guessed.
    //The Match method compares this guess to the secret code and
    //returns the number of black and white pegs generated.
    class Guess
    {
        public char[] Code;//The colour combination chosen for the guess

        public Guess(int pegs)//Constructor
        {
            this.Code = new char[pegs];//Create a char array the size of the number of pegs chosen
        }

        public int[] Match(char[] InputCode)//Match this guess to the code passed in and return results (number of black & white pegs)
            {
                int[] results = new int[2];//Create an integer array to hold number of black and white pegs
                results[0] = 0;
                results[1] = 0;

                bool[] bGuess = new bool[this.Code.Length];//Array of booleans to hold true or false match for each colour in this guess code
                bool[] bSecret = new bool[InputCode.Length];//Array of booleans to hold true or false match for each colour in the secret code

            for (int i = 0; i < this.Code.Length; i++)//For each colour in this guess
                {
                    for (int j = 0; j < InputCode.Length; j++)//For each colour in the secret code
                    {
                        if (this.Code[i] == InputCode[j] && i == j)//If the secret code and guess peg colours match and they are in the same position
                        {
                            if (bGuess[i] == false && bSecret[j] == false)//If a result has not already been recorded for this peg
                            {
                                bGuess[i] = true;//record that this peg in the guess code has been given a result
                                bSecret[j] = true;//record that this peg in the secret code has been given a result
                                results[0]++;//Increment the black peg count
                            }
                        }
                    }
                }

                for (int i = 0; i < this.Code.Length; i++)//For each colour in this guess
            {
                    for (int j = 0; j < InputCode.Length; j++)//For each colour in the secret code
                {
                        if (this.Code[i] == InputCode[j] && i != j)//If the secret code and guess peg colours match but they are not in the same position
                    {
                            if (bGuess[i] == false && bSecret[j] == false)//If a result has not already been recorded for this peg
                            {
                                bGuess[i] = true;//record that this peg in the guess code has been given a result
                                bSecret[j] = true;//record that this peg in the secret code has been given a result
                                results[1]++;//Increment the white peg count
                            }
                        }
                    }
                }
                return results;//Return the number of black and white pegs for this guess
            }

    }
}
