using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastermind
{
    //Board object. This takes the number of pegs (columns) 
    //and maximum number of guesses (rows) to create a grid.
    //It maintains a list of all guesses made so far along 
    //with a list of their results (black & white pegs).
    //It then draws the grid with all guesses and results.
    class Board
    {
        int iCols, iRows;//Stores the grid dimensions
        List<char[]> codes;//Stores a list of all guesses made
        List<char[]> results;//Stores a list of the results (black & white) for the guesses made
        
        public Board(int cols, int rows)//Constructor
        {
            iCols = cols;//set columns size
            iRows = rows;//set rows size
            codes = new List<char[]>(rows);//Create the list object to contain the guesses
            results = new List<char[]>(rows);//Create the list object to contain the results

            InitialiseBoard();//Initialise the grid values to '*'
        }

        public void DrawBoard(Guess g, int[] res, int turn)//Display the grid
        {
            codes[turn-1]=(g.Code);//Store the current guess in the codes list with index = current guess number (turn)

            char[] temp = new char[iCols];//Create a new character array to hold the results colours for the current guess
            int i;//Counter for number of pegs processed

            for (i=0; i<res[0]; i++)//Create the number of black pegs from the results
            {
                temp[i] = 'B'; 
            }

            if (i < iCols)//If we did not create all black pegs above
            {
                for (int j=0; j<res[1]; j++)//Create the number of white pegs from the results
                {
                    temp[i] = 'W';
                    i++;
                }
            }

            if (i < iCols)//If not all of the results pegs were filled
            {
                for(; i < iCols; i++)//For the remaining pegs add a star (empty)
                {
                    temp[i] = '*';
                }
            }

            //Copy the temporary array of results pegs to the 
            //results list with index = current guess number (turn)
            results[turn - 1] = temp;

            //Display all guesses and results
            for (int r = 0; r < iRows; r++)
            {
                for (int c = 0; c < iCols; c++)
                {
                    Console.Write("{0} ",codes[r][c]);//Display each character of the code for each guess stored
                }

                Console.Write("|");

                for (int c = 0; c < iCols; c++)
                {
                    Console.Write("{0} ", results[r][c]);//Display each character of the result for each result stored
                }

                Console.WriteLine();
            }
        }

        void InitialiseBoard()//Set all pegs to '*'
        {
            char[] stars = new char[iCols];//Create a char array to hold the '*' characters
            
            for (int c = 0; c < iCols; c++)//Store a '*' character for each peg
            {
                stars[c] = '*';
            }

            for (int r = 0; r < iRows; r++)//For each row, fill all guesses and results with stars
            {
                codes.Add(stars);
                results.Add(stars);
            }
        }

    }
}
