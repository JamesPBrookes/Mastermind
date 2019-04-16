using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastermind
{
    //Game object. Holds the game parameters (Secret Code, Maximum Guesses & No. of Pegs)
    //Maintains a list of all possible guesses.
    //Uses this list to choose the next guess.
    //Reduces the list after each guess based on the results returned for the guess
    class Game
    {
        public int iPegs, iMaxGuesses;//Stores user choices for no. of pegs and maximum guesses
        public bool bSolved;//True when the game is solved (all black pegs in the result
        public List<Guess> possibleCodes;//List of all possible guesses
        public Guess currentGuess;//Stores the current Guess object

        const int iColours = 6;//Fixed number of colours
        char[] colours = {'R', 'B', 'G', 'Y', 'O', 'P'};//Array of possible colours

        public char[] secretCode;//The secret code generated at the start of the game
        public int iTurnsTaken;//No. of guesses taken (turns)


        public Game(int pegs, int maxGuesses)//Constructor
        {
            iPegs = pegs;//Store No. of pegs chosen
            iMaxGuesses = maxGuesses;//Store max. guesses chosen
            bSolved = false;//set game solved to false
            int maxPossibleCodes = Convert.ToInt32(Math.Pow(iColours,iPegs));//Calculate the maximum no. of possible guesses for the no. of pegs and colours
            possibleCodes = new List<Guess>(maxPossibleCodes);//Create a list object to hold all possible guesses

            for (int i = 0; i < maxPossibleCodes; i++)//Populate the list with Guess objects
            {
                possibleCodes.Add(new Guess(iPegs));
            }

            InitialiseAllPossibleCodes(0);//Initialise the possible guesses with all possible codes
        }

        public void GenerateSecretCode(int pegs)//Generate the secret code
        {
            this.secretCode = new char[pegs];//Create an new array of char with length = no. of pegs for the secret code
            Random rnd = new Random();//Create a new Randomisation object

            for (int i = 0; i < pegs; i++)//For each peg position
            {
                this.secretCode[i] = colours[rnd.Next(0, iColours)];//Choose a random colour and add it to the secret code
            }
        }

        void InitialiseAllPossibleCodes(int pegNo)//Initialise the possible guesses list with all possible codes
        {
            if (pegNo >= iPegs)//stop when no. of pegs chosen by the user is reached
                return;

            int iPower = iPegs - pegNo - 1;//Decrement the power to be used in the calculation below for each iteration
            int iterations = Convert.ToInt32(Math.Pow(iColours, iPower));//Calculate the no. of iterations to perform based on the peg position we are up to
            int iCount = 0;//Initialise the counter

            while (iCount < Math.Pow(iColours, iPegs))//For all possible guesses
            {
                for (int i = 0; i < iColours; i++)//For each colour
                {
                    for (int j = 0; j < iterations; j++)//For each iteration
                    {
                        possibleCodes[iCount].Code[pegNo] = colours[i];//Set the colour of the peg on the current row (iteration)
                        iCount++;//Increment the counter
                    }
                }
            }

            InitialiseAllPossibleCodes(pegNo + 1);//Repeat the process for the next peg position
        }

        public void MakeNextGuess()//Generate a guess from the list of remaining possible guesses
        {
            if (possibleCodes.Count == 0)//If no possibilities exist, quit
                return;

            Random rnd = new Random();//Create a new Randomisation object
            int r = rnd.Next(0, possibleCodes.Count);//Choose a random no. between 0 and the last available possibile code index

            this.currentGuess = possibleCodes[r];//Set the current Guess to the selected possible guess (index generated above)

            iTurnsTaken++;//Increment the no. of guesses taken

        }

        //If the game is not complete, reduce the possible guesses based on the results for the current Guess
        public void ReducePossibilities(int correctPositionAndColour, int correctColourOnly)
        {
            if (correctPositionAndColour == iPegs)//All results pegs are black for current Guess
            {
                bSolved = true;//Game solved
            }
            else
            {
                for (int i = 0; i < possibleCodes.Count; i++)//For each available possible code
                {
                    int[] results = currentGuess.Match(possibleCodes[i].Code);//Compare the possible code with the current Guess code and get the result

                    if (!(results[0] == correctPositionAndColour && results[1] == correctColourOnly))//If the results for the possible code are not the same as for the current guess
                    {
                        possibleCodes.RemoveAt(i);//Remove the possible code from the list of possible codes as it cannot be valid
                        i--;//reduce the index counter to take into account the removed list item
                    }
                }
            }
        }
    }
}
