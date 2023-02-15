using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Enums;
/**********************************************
If new drive results are added, leave EndOfGame
at the end of the list. Changing the last value
will result in breaking changes. There are 'for'
loops that use (int)EndOfGame to determine the
length of the DriveResults list for iteration.
Always add in the middle of the list.
**********************************************/
public enum DriveResult
{
    Touchdown,
    FieldGoal,
    Punt,
    Interception,
    Fumble,
    TurnoverOnDowns,
    Safety,
    EndOfHalf,
    EndOf4th,
    EndOfGame
}
