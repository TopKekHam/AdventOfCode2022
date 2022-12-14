
class Parser
{

    //return false is end of file.
    public static bool RemoveWhiteSpace(string str, ref int i)
    {
        while (str.Length > i)
        {
            if (char.IsWhiteSpace(str[i]))
            {
                i++;
            }
            else
            {
                break;
            }
        }

        return str.Length != i;
    }

    // return false if end of line.
    public static bool NextLine(string str, ref int i)
    {
        while (str.Length > i)
        {
            if (str[i] == '\n')
            {
                i++;

                return str.Length > i;
            }

            i++;
        }

        return false;
    }

    // returns false if counld't parse int
    public static bool NextInt(string str, ref int i, out int number)
    {
        bool minus = false;

        if (str[i] == '-')
        {
            i++;
            minus = true;
        }

        int start = i;

        while (str.Length > i)
        {
            if (str[i] >= '0' && str[i] <= '9')
            {
                i++;
            }
            else
            {
                break;
            }
        }

        if (start != i)
        {
            number = 0;
            int mult = 1;

            for (int ci = i - 1; ci >= start; ci--)
            {
                int digit = str[ci] - '0';
                number += mult * digit;
                mult *= 10;
            }

            if (minus)
            {
                number *= -1;
            }

            return true;
        }

        number = -1;
        return false;
    }
    
}

