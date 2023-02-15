using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Records
{
    public record TeamRecord(
        int Id,
        string City,
        string Name,
        string Abbreviation,
        int Conference_Index,
        int Division_Index);
}
